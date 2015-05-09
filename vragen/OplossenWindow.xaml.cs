using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for OplossenWindow.xaml
    /// </summary>
    public partial class OplossenWindow : Window
    {
        private List<Vraag> vragenLijst;
        private int counter;
        private string bestandsNaam;
        private MainVragenWindow menuWindow;
        private string programmaDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Challenger");
        private string vragenlijstenDirPath;
        private Leerling gebruiker;
        public OplossenWindow(string bestandsNaam, Leerling gebruiker, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.bestandsNaam = bestandsNaam;
            vragenlijstenDirPath = programmaDirPath + "\\Vragenlijsten";
            this.menuWindow = menuWindow;
            this.gebruiker = gebruiker;
        }

        private void vorigeButton_Click(object sender, RoutedEventArgs e)
        {
            SlaVraagOp();
            if ((counter - 1) >= 0)
            {
                counter--; // ga naar de vorige vraag
                LaadVraag();
            }
        }



        private void volgendeButton_Click(object sender, RoutedEventArgs e)
        {
            SlaVraagOp();
            if (counter + 1 < vragenLijst.Count)
            {
                counter++;
                LaadVraag();
            }
            else
            {
                MessageBox.Show("Laatste vraag, druk op Klaar om af te sluiten uw score te bekijken");
            }
        }

        private void Klaar()
        {
            Window w = new ScoreWindow(menuWindow, gebruiker, vragenLijst, bestandsNaam);
            w.Show();
            this.Close();
        }

        private void klaarButton_Click(object sender, RoutedEventArgs e)
        {
            SlaVraagOp();
            Klaar(); // misschien onnodig om methode voor te gebruiken
        }

        private void LaadVraag()
        {
            opgaveTextBlock.Text = vragenLijst[counter].Opgave;
            invulListBox.Items.Clear();
            switch (vragenLijst[counter].TypeVraag)
            {
                case (VraagType.basis):
                    invulTextBox.Visibility = Visibility.Visible;
                    invulListBox.Visibility = Visibility.Hidden;
                    invulTextBox.Text = vragenLijst[counter].Ingevuld;
                    break;
                case (VraagType.meerkeuze):
                    invulTextBox.Visibility = Visibility.Hidden;
                    invulListBox.Visibility = Visibility.Visible;

                    List<string> antwoordenLijst = new List<string>();
                    foreach (string antwoord in ((MeerkeuzeVraag)vragenLijst[counter]).AntwoordenLijst) // copy by value
                    {
                        antwoordenLijst.Add(antwoord);
                    }
                    Random randomIndex = new Random();
                    int r;
                    RadioButton radioKnop;
                    while (antwoordenLijst.Count > 0)// zet de antwoorden in willekeurige volgorde in de ListBox
                    {
                        r = randomIndex.Next(0, antwoordenLijst.Count);
                        radioKnop = new RadioButton();
                        radioKnop.Content = antwoordenLijst[r];
                        invulListBox.Items.Add(radioKnop);
                        antwoordenLijst.RemoveAt(r);
                    }

                    // duid het eventueel ingevulde antwoord aan
                    if (vragenLijst[counter].IsIngevuld)
                    {
                        foreach (RadioButton item in invulListBox.Items)
                        {
                            if ((String)item.Content == vragenLijst[counter].Ingevuld)  //als de radiobutton dezelfde inhoud heeft als het ingevulde antwoord, duid de radiobutton aan
                            {
                                item.IsChecked = true;
                            }
                        }
                    }
                    break;
                case (VraagType.wiskunde):
                    invulTextBox.Visibility = Visibility.Hidden;
                    invulListBox.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void SlaVraagOp()
        {
            switch (vragenLijst[counter].TypeVraag)
            {
                case (VraagType.basis):
                case (VraagType.wiskunde):
                    vragenLijst[counter].Ingevuld = invulTextBox.Text;
                    break;
                case (VraagType.meerkeuze):
                    foreach (RadioButton radioKnop in invulListBox.Items)
                    {
                        if ((bool)radioKnop.IsChecked)
                        {
                            vragenLijst[counter].Ingevuld = (string)radioKnop.Content;
                        }
                    }
                    break;
            }
        }

        private void Window_Closed(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VragenLezer lezer=null;
            try
            {
                lezer = new VragenLezer(bestandsNaam);
                lezer.Initialise();
                vragenLijst=lezer.VragenLijst;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Invoerbestand bestaat niet");
                this.Close();
            }
            catch (OnbekendVraagTypeException exception)
            {
                MessageBox.Show(exception.Message + "/n Bestand is mogelijk corrupt, programma zal nu afsluiten");
                this.Close();
            }
            catch (VraagIsNullException exception)
            {
                MessageBox.Show(exception.Message + "/n Bestand is mogelijk corrupt, programma zal nu afsluiten");
                this.Close();
            }
            catch (BestandTeGrootException exception)
            {
                MessageBox.Show(exception.Message);
                this.Close();
            }    
            finally
            {
                if (lezer != null)
                {
                    lezer.Close();
                }
                if (vragenLijst.Count < 1)
                {
                    this.Close();
                    throw new LeegBestandException("Bestand " + lezer.BestandsNaam + " bevat geen geldige vragen.");
                }
            }



            // zet de vragen in willekeurige volgorde
            List<Vraag> hulpLijst = new List<Vraag>();
            Random randomIndex = new Random();
            int r;
            while (vragenLijst.Count > 0)
            {
                r = randomIndex.Next(0, vragenLijst.Count);
                hulpLijst.Add(vragenLijst[r]);
                vragenLijst.RemoveAt(r);
            }
            vragenLijst = hulpLijst;

            LaadVraag();

        }
    }
}
