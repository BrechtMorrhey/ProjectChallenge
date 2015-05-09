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
using Microsoft.Win32;

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
        Brush juistBrush;
        FontWeight juistFontWeight;
        double textBoxWidth;
        double textBoxHeight;
        bool nieuweLijst;
        private wiskundigeVraag wiskundeVraagTemp; // gebruikt om de wiskundevraag tijdelijk in op te slaan
              
        public AanpassenWindow(string bestandsNaam, bool nieuweLijst)
        {
            InitializeComponent();
            this.bestandsNaam = bestandsNaam;
            this.nieuweLijst = nieuweLijst;
            this.wiskundeVraagTemp = null;
        }

        private void volgendeButton_Click(object sender, RoutedEventArgs e)
        {
            VoegVraagToe();
            if (counter + 1 < vragenLijst.Count + 1)    // Dit zorgt ervoor dat de gebruiker niet eindeloos voorbij het einde van de lijst kan gaan, maar wel een nieuwe vraag kan toevoegen
            {
                counter++;
            }
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
            // voeg enkel een vraag toe als er ook data in de vakjes zit
            if (opgaveTextBox.Text != "")
                VoegVraagToe();

            StreamWriter outputStream = File.CreateText(bestandsNaam);
            foreach (Vraag vraag in vragenLijst)
            {
                outputStream.WriteLine(vraag.ToString());
            }
            outputStream.Close();
            MessageBox.Show("Vraag opgeslagen in " +bestandsNaam);
        }

        private void opslaanAlsButton_Click(object sender, RoutedEventArgs e)
        {
            // voeg enkel een vraag toe als er ook data in de vakjes zit
            if (opgaveTextBox.Text != "")
                VoegVraagToe();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                StreamWriter outputStream = File.CreateText(bestandsNaam);
                foreach (Vraag vraag in vragenLijst)
                {
                    outputStream.WriteLine(vraag.ToString());
                }
                outputStream.Close();
                MessageBox.Show("Vraag opgeslagen in " + bestandsNaam);
            }
            //niet nodig, treedt alleen op als op Cancel gedrukt wordt
            //else
            //{
            //    MessageBox.Show("Geen bestandsnaam opgegeven");
            //}
        }

        private void VoegVraagToe()
        {
            if (!(opgaveTextBox.Text == "" && (antwoordTextBox.Text == "" || ((TextBox)meerkeuzeListBox.Items[0]).Text == "")))    // kijk of de gebruiker alle velden heeft ingevuld
            {
                Vraag vraag = null;
                switch (typeVraagComboBox.SelectedIndex)
                {
                    case 0: vraag = new BasisVraag(opgaveTextBox.Text, antwoordTextBox.Text);
                        break;
                    case 1: List<string> antwoordenLijst = new List<string>();
                        foreach (TextBox antwoordBox in meerkeuzeListBox.Items)
                        {
                            antwoordenLijst.Add(antwoordBox.Text);
                        }
                        vraag = new MeerkeuzeVraag(opgaveTextBox.Text, antwoordenLijst);
                        break;
                    case 2:
                        if (wiskundeVraagTemp == null) // wiskunde vraag werd handmatig ingegeven
                        {
                            GenerateMathQuestion();
                        }
                        vraag = wiskundeVraagTemp;
                        wiskundeVraagTemp = null;
                        
                        break;
                }
                if (vraag != null)
                {
                    if (counter >= vragenLijst.Count)   // kijk of er een vraag moet worden toegevoegd of moet worden aangepast
                    {
                        vragenLijst.Add(vraag);
                    }
                    else
                    {
                        vragenLijst[counter] = vraag;
                    }
                }
                else
                {
                    MessageBox.Show("Vraag is null, programma zal nu afsluiten");
                    this.Close();
                }
            }            
        }



        private void LaadVraag()
        {
            if (counter >= vragenLijst.Count)
            {
                opgaveTextBox.Text = "";
                antwoordTextBox.Text = "";
                // reset de meerkeuze ListBox
                meerkeuzeListBox.Items.Clear();
                TextBox lijstItem;
                for (int i=0; i < 2; i++)
                {
                    // dit stukje code komt meerdere keren terug, kan gerefactored worden
                    lijstItem = new TextBox();
                    lijstItem.Height = textBoxHeight;
                    lijstItem.Width = textBoxWidth;
                    meerkeuzeListBox.Items.Add(lijstItem);
                }
                ((TextBox)meerkeuzeListBox.Items[0]).Foreground = juistBrush;
                ((TextBox)meerkeuzeListBox.Items[0]).FontWeight = juistFontWeight; 
            }
            else
            {
                opgaveTextBox.Text = vragenLijst[counter].Opgave;
                    switch (vragenLijst[counter].TypeVraag)
                    {
                        case VraagType.basis: antwoordTextBox.Text = vragenLijst[counter].Antwoord;
                            typeVraagComboBox.SelectedIndex = 0;
                            break;
                        case VraagType.meerkeuze:
                            //meerkeuzeListBox.ItemsSource = ((MeerkeuzeVraag)vragenLijst[counter]).AntwoordenLijst;
                            //het gebruik van ItemsSource levert verschillende problemen op, dus gebruiken we in de plaats een simpele lus                     
                            meerkeuzeListBox.Items.Clear();
                            TextBox lijstItem;
                            foreach (string antwoord in ((MeerkeuzeVraag)vragenLijst[counter]).AntwoordenLijst)
                            // we kunnen niet zomaar aan de antwoordenlijst aangezien dit geen property van Vraag is dus moeten we eerst naar MeerkeuzeVraag casten
                            // dit is niet zo een mooie oplossing, alternatieven??
                            {
                                lijstItem = new TextBox();
                                lijstItem.Height = textBoxHeight;
                                lijstItem.Width = textBoxWidth;
                                lijstItem.Text = antwoord;
                                meerkeuzeListBox.Items.Add(lijstItem);
                            }
                            while (meerkeuzeListBox.Items.Count < 2)  //als de antwoorden niet volledig zijn ingevuld, vul dan aan met lege
                            {
                                lijstItem = new TextBox();
                                lijstItem.Height = textBoxHeight;
                                lijstItem.Width = textBoxWidth;
                                meerkeuzeListBox.Items.Add(lijstItem);
                            }
                            ((TextBox)meerkeuzeListBox.Items[0]).FontWeight = juistFontWeight;
                            ((TextBox)meerkeuzeListBox.Items[0]).Foreground = juistBrush;
                            typeVraagComboBox.SelectedIndex = 1;
                            break;
                        case VraagType.wiskunde: 
                            antwoordTextBox.Text = vragenLijst[counter].Antwoord;
                            typeVraagComboBox.SelectedIndex = 2;
                            break;
                    }
            }
        }

        private void typeVraagComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           switch (typeVraagComboBox.SelectedIndex) // toon de relevante velden op basis van het geselecteerde type vraag
                   // dit kan misschien gerefactored worden om overzichtelijker te zijn, maar wrs best mee wachten tot alle vraagtypes geimplementeerd zijn
                {
                    case 0: //basis
                        antwoordLabel.Visibility = Visibility.Visible;
                        antwoordTextBox.Visibility = Visibility.Visible;
                        meerkeuzeLabel.Visibility = Visibility.Hidden;
                        meerkeuzeListBox.Visibility = Visibility.Hidden;
                        plusButton.Visibility = Visibility.Hidden;
                        minButton.Visibility = Visibility.Hidden;
                        plusButton.IsEnabled = false;
                        minButton.IsEnabled = false;
                        antwoordTextBox.IsEnabled = true;
                        meerkeuzeListBox.IsEnabled = false;
                        GenereerOpgaveButton.Visibility = Visibility.Hidden;
                        getal1TextBox.Visibility = Visibility.Hidden;
                        getal2TextBox.Visibility = Visibility.Hidden;
                        bewerkingTextBox.Visibility = Visibility.Hidden;
                        break;
                    case 1: //meerkeuze
                        antwoordLabel.Visibility = Visibility.Hidden;
                        antwoordTextBox.Visibility = Visibility.Hidden;
                        meerkeuzeLabel.Visibility = Visibility.Visible;
                        meerkeuzeListBox.Visibility = Visibility.Visible;
                        plusButton.Visibility = Visibility.Visible;
                        minButton.Visibility = Visibility.Visible;
                        plusButton.IsEnabled = true;
                        minButton.IsEnabled = true;
                        antwoordTextBox.IsEnabled = false;
                        meerkeuzeListBox.IsEnabled = true;
                        GenereerOpgaveButton.Visibility = Visibility.Hidden;
                        getal1TextBox.Visibility = Visibility.Hidden;
                        getal2TextBox.Visibility = Visibility.Hidden;
                        bewerkingTextBox.Visibility = Visibility.Hidden;
                        break;
                    case 2: //wiskunde
                        antwoordLabel.Visibility = Visibility.Visible;
                        antwoordTextBox.Visibility = Visibility.Visible;
                        meerkeuzeLabel.Visibility = Visibility.Hidden;
                        meerkeuzeListBox.Visibility = Visibility.Hidden;
                        plusButton.Visibility = Visibility.Hidden;
                        minButton.Visibility = Visibility.Hidden;
                        plusButton.IsEnabled = false;
                        minButton.IsEnabled = false;
                        antwoordTextBox.IsEnabled = true;
                        meerkeuzeListBox.IsEnabled = false;
                        GenereerOpgaveButton.Visibility = Visibility.Visible;
                        getal1TextBox.Visibility = Visibility.Visible;
                        getal2TextBox.Visibility = Visibility.Visible;
                        bewerkingTextBox.Visibility = Visibility.Visible;
                        break;
                }
            
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            if (meerkeuzeListBox.Items.Count < 6)
            {
                TextBox lijstItem = new TextBox();
                lijstItem.Width = 350;
                lijstItem.Height = 25;
                meerkeuzeListBox.Items.Add(lijstItem);
            }else
            {
                MessageBox.Show("Maximum aantal antwoorden bereikt");
            }
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            if (meerkeuzeListBox.Items.Count > 2)
            {
                meerkeuzeListBox.Items.RemoveAt(meerkeuzeListBox.Items.Count - 1); // verwijder het laatste item in de lijst
            }
            else
            {
                MessageBox.Show("Minimum aantal antwoorden bereikt");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // BELANGRIJK
            typeVraagComboBox.SelectedIndex = 0;
            // als ge dit in XAML probeert te doen krijgt ge een NullReferenceException in uw InitializeComponent 
            //door een WPF bug die uw events checked voor de bijbehorende controls zijn aangemaakt
            // http://stackoverflow.com/questions/2518231/wpf-getting-control-null-reference-during-initializecomponent

            vragenLijst = new List<Vraag>();

            if (!nieuweLijst)   // als het geen nieuwe lijst is, laadt de oude lijst in
            {
                int i;
                string line, lijst;
                Vraag vraag = null;
                List<string> antwoordenLijst;
                StreamReader inputStream = null;
                counter = 0;

                int j = 0;
                try
                {
                    inputStream = File.OpenText(bestandsNaam);
                    line = inputStream.ReadLine();
                    while (line != null && j < 10000)
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
                                    MessageBox.Show("Lege antwoordenlijst voor vraag " + line.Split(',')[1] + "/n vraag wordt overgeslagen");
                                    // de vraag wordt niet ingelezen en het programma probeert verder te gaan
                                }
                                //code voor meerkeuze
                                break;
                            case "wiskunde":
                                double getal1 = Convert.ToDouble(line.Split(',')[1]);
                                double getal2 = Convert.ToDouble(line.Split(',')[2]);
                                string bewerking = line.Split(',')[3];
                                vraag = new wiskundigeVraag(getal1, getal2, bewerking);
                                wiskundeVraagTemp = (wiskundigeVraag)vraag; // <---------------------------------
                                break;
                            default: 
                                throw new OnbekendVraagTypeException("Onbekend Type Vraag voor vraag " + line.Split(',')[1]);

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
                    MessageBox.Show("Bestand niet gevonden.");
                    this.Close();
                }
                catch (ArgumentException)
                {
                    // deze exception treedt op als de user het OpenFileDialog sluit
                    // AanpassenWindow moet dan niet opengaan
                    this.Close();
                }
                catch (OnbekendVraagTypeException exception)
                {
                    MessageBox.Show(exception.Message + "/n Bestand is mogelijk corrupt, programma sluit nu af.");
                    this.Close();
                }
                finally
                {
                    if (inputStream != null)
                    {
                        inputStream.Close();
                    }
                    if (j >= 10000)
                    {
                        MessageBox.Show("Bestand te groot, programma sluit nu af");
                        this.Close();
                    }
                }
            }

            // kopieer de opmaak en grootte van de TextBoxes in de listbox voor later
            juistBrush = ((TextBox)meerkeuzeListBox.Items[0]).Foreground;
            juistFontWeight = ((TextBox)meerkeuzeListBox.Items[0]).FontWeight;
            textBoxWidth = ((TextBox)meerkeuzeListBox.Items[0]).Width;
            textBoxHeight = ((TextBox)meerkeuzeListBox.Items[0]).Height;


            LaadVraag();   
        }

        private void GenereerOpgaveButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateMathQuestion();
        }

        private void GenerateMathQuestion()
        {
            int n;
            if (int.TryParse(getal1TextBox.Text, out n) && int.TryParse(getal2TextBox.Text, out n))
            {
                wiskundeVraagTemp = new wiskundigeVraag(int.Parse(getal1TextBox.Text), int.Parse(getal2TextBox.Text), bewerkingTextBox.Text);
                opgaveTextBox.Text = wiskundeVraagTemp.Opgave;
                antwoordTextBox.Text = wiskundeVraagTemp.Antwoord;
            }
            else if (opgaveTextBox.Text != "") // niet nodig om het anwoord te checken, want dit wordt toch automatisch gegenereerd
            {
                 ///////////////////////////////////////hier zit de fout ivm handmatige wiskunde vragen die verkeerd ingegeven worden
                    int m;
                    if (int.TryParse((opgaveTextBox.Text.Split(' ')[0]), out m) && int.TryParse((opgaveTextBox.Text.Split(' ')[2]), out m))
                    {
                        double getal1 = Convert.ToDouble(opgaveTextBox.Text.Split(' ')[0]);
                        double getal2 = Convert.ToDouble(opgaveTextBox.Text.Split(' ')[2]);
                        string bewerking = opgaveTextBox.Text.Split(' ')[1];
                        wiskundeVraagTemp = new wiskundigeVraag(getal1, getal2, bewerking);
                        opgaveTextBox.Text = wiskundeVraagTemp.Opgave;
                        antwoordTextBox.Text = wiskundeVraagTemp.Antwoord;
                    }
                    else
                    {
                        MessageBox.Show("opgave dient in dit formaat ingegeven te worden, 'getal1 + getal2'");
                    }

              
                
                       
            }
            else

            {
                wiskundeVraagTemp = new wiskundigeVraag();
                opgaveTextBox.Text = wiskundeVraagTemp.Opgave;
                antwoordTextBox.Text = wiskundeVraagTemp.Antwoord;
            }
        }

        private void bewerkingTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            bewerkingTextBox.Text = "";
        }

        private void getal1TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            getal1TextBox.Text = "";
        }

        private void getal2TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            getal2TextBox.Text = "";
        }




        

        
        
    }
}
