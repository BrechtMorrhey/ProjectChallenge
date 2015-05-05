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
        
        
        public OplossenWindow(string bestandsNaam)
        {
            int i;
            string line, lijst;
            Vraag vraag=null;
            StreamReader inputStream = null;
            List<string> antwoordenLijst;

            this.bestandsNaam = bestandsNaam;
            counter = 0;
            vragenLijst = new List<Vraag>();
            InitializeComponent();
            int j = 0;
            try
            {
                inputStream = File.OpenText(bestandsNaam);
                line = inputStream.ReadLine();
                while (line != null && j<10000)
                {
                    switch (line.Split(',')[0])
                    {
                        case "basis":
                            vraag = new BasisVraag(line.Split(',')[1], line.Split(',')[2]);
                            
                            break;
                        case "meerkeuze":
                            antwoordenLijst = new List<string>();
                                lijst = line.Split(',')[2];
                                i = 0;
                                while ((lijst.Split('|')[i]).Trim() != "")   // maak de antwoordenlijst door elementen in te lezen zolang er geen lege waarde komt
                                {
                                    antwoordenLijst.Add(lijst.Split('|')[i]);
                                    i++;
                                }
                                if (antwoordenLijst != null)
                                {
                                    vraag = new MeerkeuzeVraag(line.Split(',')[1], antwoordenLijst);
                                }
                                else
                                {
                                    MessageBox.Show("Lege antwoordenlijst");
                                    // de vraag wordt niet ingelezen en het programma probeert verder te gaan
                                }
                                //code voor meerkeuze
                                break;
                           
                        case "wiskunde":
                            //code voor wiskunde
                            break;
                        default: throw new OnbekendVraagTypeException("Onbekend Type Vraag voor vraag " + line.Split(',')[1]);
                    }
                    if (vraag != null)
                    {
                        vragenLijst.Add(vraag);
                    }
                    else
                    {
                        MessageBox.Show("Vraag is null, programma zal nu afsluiten");
                        this.Close();
                    }
                    line = inputStream.ReadLine();
                    j++;
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Invoerbestand bestaat niet");
                this.Close();
            }
            catch (OnbekendVraagTypeException e)
            {
                MessageBox.Show(e.Message + "/n Bestand is mogelijk corrupt, programma zal nu afsluiten");
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
                if (j >= 10000)
                {
                    MessageBox.Show("Bestand is te groot, programma zal nu afsluiten");
                    this.Close();
                }
            }

            LaadVraag();
            //StreamReader inputStream = File.OpenText(bestandsNaam);
            //line = inputStream.ReadLine();
            //while (line != null)
            //{
            //    basisVraag=new BasisVraag(line.Split(',')[0],line.Split(',')[1]);
            //    vragenLijst.Add(basisVraag);
            //    line = inputStream.ReadLine();

            //}
            //opgaveTextBlock.Text = vragenLijst[counter].Opgave; //zet eerste vraag klaar
            
            
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
            if (counter+1 < vragenLijst.Count)
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
            Window w = new ScoreWindow(vragenLijst, bestandsNaam);
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
            switch (vragenLijst[counter].TypeVraag) { 
                case(VraagType.basis):
                    invulTextBox.Visibility = Visibility.Visible;
                    invulListBox.Visibility = Visibility.Hidden;
                    invulTextBox.Text = vragenLijst[counter].Ingevuld;
                    break;
                case(VraagType.meerkeuze):
                    invulTextBox.Visibility = Visibility.Hidden;
                    invulListBox.Visibility = Visibility.Visible;

                    List<string> antwoordenLijst=new List<string>();
                    foreach (string antwoord in ((MeerkeuzeVraag)vragenLijst[counter]).AntwoordenLijst) // copy by value
                    {
                        antwoordenLijst.Add(antwoord);
                    }
                    Random randomIndex = new Random();
                    int r;
                    RadioButton radioKnop;
                    while(antwoordenLijst.Count>0)// zet de antwoorden in willekeurige volgorde in de ListBox
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
    }
}
