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
    /// Interaction logic for AanpassenWindow.xaml
    /// </summary>
    public partial class AanpassenWindow : Window
    {
        private List<Vraag> vragenLijst;
        private string bestandsNaam;
        private int counter;
        
              
        public AanpassenWindow(string bestandsNaam, bool nieuweLijst)
        {
            InitializeComponent();

            // BELANGRIJK
            typeVraagComboBox.SelectedIndex = 0;
            // als ge dit in XAML probeert te doen krijgt ge een NullReferenceException in uw InitializeComponent 
            //door een WPF bug die uw events checked voor de bijbehorende controls zijn aangemaakt
            // http://stackoverflow.com/questions/2518231/wpf-getting-control-null-reference-during-initializecomponent
                                   
            vragenLijst = new List<Vraag>();
            this.bestandsNaam = bestandsNaam;
            if (!nieuweLijst)   // als het geen nieuwe lijst is, laadt de oude lijst in
            {
                string line;
                BasisVraag basisVraag;

                counter = 0;

                try
                {
                    StreamReader inputStream = File.OpenText(bestandsNaam);
                    line = inputStream.ReadLine();
                    bool fouteInvoer = false;
                    while (line != null && fouteInvoer == false)
                    {
                        switch (line.Split(',')[0])
                        {
                            case "basis":
                                basisVraag = new BasisVraag(line.Split(',')[1], line.Split(',')[2]);
                                vragenLijst.Add(basisVraag);
                                line = inputStream.ReadLine();
                                break;
                            case "meerkeuze":
                                //code voor meerkeuze
                                break;
                            case "wiskunde":
                                //code voor wiskunde
                                break;
                            default: MessageBox.Show("Fout invoerbestand");
                                fouteInvoer = true;
                                this.Close();
                                break;
                        }
                    }


                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Bestand niet gevonden.");
                    this.Close();
                }
                catch (ArgumentException)
                {
                    // deze exception treedt op als de user het OpenFileDialog sluit
                    // AanpassenWindow moet dan niet opengaan
                    this.Close();
                }

            }
            

            LaadVraag();
            
            
        }

        private void volgendeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegVraagToe();
            counter++;
            LaadVraag();
        }

        private void vorigeButton_Click(object sender, RoutedEventArgs e)
        {
            if ((counter - 1) >= 0)
            {
                VoegVraagToe();
                counter--;
                LaadVraag();
            }
        }

        private void verwijderButton_Click(object sender, RoutedEventArgs e)
        {
            vragenLijst.RemoveAt(counter);
            LaadVraag();
        }

        private void opslaanButton_Click(object sender, RoutedEventArgs e)
        {
            VoegVraagToe();
            StreamWriter outputStream = File.CreateText(bestandsNaam);
            foreach (Vraag vraag in vragenLijst)
            {
                outputStream.WriteLine(vraag.ToString());
            }
            outputStream.Close();
        }

        private void VoegVraagToe()
        {
            switch (typeVraagComboBox.SelectedIndex)
            {
                case 0: BasisVraag vraag = new BasisVraag(opgaveTextBox.Text, antwoordTextBox.Text);
                    if (counter >= vragenLijst.Count)
                    {
                        vragenLijst.Add(vraag);
                    }
                    else
                    {
                        vragenLijst[counter] = vraag;
                    }
                    break;
                case 1: // code voor meerkeuze vraag
                    break;
                case 2: // code voor wiskunde vrag
                    break;
            }
        }



        private void LaadVraag()
        {
            if (counter >= vragenLijst.Count)
            {
                opgaveTextBox.Text = "";
                antwoordTextBox.Text = "";
            }
            else
            {
                opgaveTextBox.Text = vragenLijst[counter].Opgave;
                antwoordTextBox.Text = vragenLijst[counter].Antwoord;
            }
        }

        private void typeVraagComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           switch (typeVraagComboBox.SelectedIndex)
                {
                    case 0:
                        meerkeuzeLabel.Visibility = Visibility.Hidden;
                        meerkeuzeListBox.Visibility = Visibility.Hidden;
                        break;
                    case 1:
                        meerkeuzeLabel.Visibility = Visibility.Visible;
                        meerkeuzeListBox.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        meerkeuzeLabel.Visibility = Visibility.Hidden;
                        meerkeuzeListBox.Visibility = Visibility.Hidden;
                        break;
                }
            
        }

        
    }
}
