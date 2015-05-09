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
using System.Windows.Threading;

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
        private vragen.VragenSelectieWindow vragenSelectie;
        private string programmaDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Challenger");
        private string vragenlijstenDirPath;
        private Leerling gebruiker;
        private bool windowClosed;
        private DispatcherTimer klok;
        private int tijd = 0;
        private int juisteTijd = 0;
        private int restVragen;

        public OplossenWindow(string bestandsNaam, Leerling gebruiker,MainVragenWindow menuWindow, vragen.VragenSelectieWindow vragenSelectie)
        {
            InitializeComponent();
            this.menuWindow = menuWindow;
            this.bestandsNaam = bestandsNaam + ".txt";
            vragenlijstenDirPath = programmaDirPath + "\\Vragenlijsten";
            this.vragenSelectie = vragenSelectie;
            this.gebruiker = gebruiker;
            windowClosed = true;
        }

        public OplossenWindow(string bestandsNaam,int tijd, Leerling gebruiker, MainVragenWindow menuWindow, vragen.VragenSelectieWindow vragenSelectie)
                :this(bestandsNaam, gebruiker, menuWindow, vragenSelectie)
        {
            this.juisteTijd = tijd; 
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
            volgendeVraag();
        }

        private void volgendeVraag()
        {
            SlaVraagOp();
            if (counter + 1 < vragenLijst.Count)
            {
                counter++;
                LaadVraag();
            }
            else
            {
                if (juisteTijd != 0)
                {
                    Klaar();
                }
                else
                {
                    MessageBox.Show("Laatste vraag, druk op Klaar om af te sluiten uw score te bekijken");
                }
            }
            restVragen--;
        }
        private void Klaar()
        {
            Window w = new ScoreWindow(menuWindow, gebruiker, vragenLijst, bestandsNaam, juisteTijd);
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
                case(VraagType.wiskunde):
                    invulTextBox.Visibility = Visibility.Visible;
                    invulListBox.Visibility = Visibility.Hidden;
                    invulTextBox.Text = vragenLijst[counter].Ingevuld;
                    break;
            }
            tijd = juisteTijd;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VragenLezer lezer = null;
            try
            {
                String volledigPath = System.IO.Path.Combine(vragenlijstenDirPath, bestandsNaam);
                lezer = new VragenLezer(volledigPath);
                lezer.Initialise();
                vragenLijst = lezer.VragenLijst;                
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Invoerbestand bestaat niet");
                this.Close();
                vragenSelectie.Show();
            }
            catch (OnbekendVraagTypeException exception)
            {
                MessageBox.Show(exception.Message + "/n Bestand is mogelijk corrupt, programma zal nu afsluiten");
                this.Close();
                vragenSelectie.Show();
            }
            catch (VraagIsNullException exception)
            {
                MessageBox.Show(exception.Message + "/n Bestand is mogelijk corrupt, programma zal nu afsluiten");
                this.Close();
                vragenSelectie.Show();
            }
            catch (BestandTeGrootException exception)
            {
                MessageBox.Show(exception.Message);
                this.Close();
                vragenSelectie.Show();
            }
//  opvangen leegbestand exception
            catch (LeegBestandException exception)
            {
                MessageBox.Show(exception.Message);
                windowClosed = false; // boolean op vals om te zeggen dat window gesloten is
                this.Close();
                vragenSelectie.Show();  // menuwindow tonen
            }
            finally
            {
                if (lezer != null)
                {
                    lezer.Close();
                }
                if (vragenLijst == null)    //  als vragenlijst null is window sluiten
                {
                    this.Close();
                    vragenSelectie.Show();
                    //throw new LeegBestandException("Bestand " + lezer.BestandsNaam + " bevat geen geldige vragen.");
                }
            }

            //  als window geclosed is word deze code toch nogsteeds uitgevoerd
            //  daarom eerst testen of er wel een vragenLijst is aangemaakt
            //  in geval van exceptions
            if (vragenLijst != null) 
            {
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

                restVragen = vragenLijst.Count() - 1;

                LaadVraag();
            }
            
            if(tijd != 0)
            {
                vorigeButton.Visibility = Visibility.Hidden;
                klok = new DispatcherTimer();
                klok.Interval = TimeSpan.FromSeconds(1);
                klok.Tick += klok_Tick;
                klok.Start();
            }

        }

        void klok_Tick(object sender, EventArgs e)
        {
            overigeVragenTextBlock.Text = "resterende vragen: " + restVragen;
            tijdLabel.Content = "Resterende tijd : " + tijd + " sec";
            if (tijd == 0)
            {
                volgendeVraag();
            }
            tijd--;
        }
        
        public bool WindowNotClosed
        {
            get
            {
                return windowClosed;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (klok != null)
            {
                klok.Stop();
            }
        }

        private void invulTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
