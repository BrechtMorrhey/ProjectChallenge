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
            string line;
            BasisVraag basisVraag;

            this.bestandsNaam = bestandsNaam;
            counter = 0;
            vragenLijst = new List<Vraag>();
            InitializeComponent();

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
                MessageBox.Show("Invoerbestand bestaat niet");
                this.Close();
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
            vragenLijst[counter].Ingevuld = invulTextBox.Text;
            if ((counter - 1) >= 0)
            {
                counter--; // ga naar de vorige vraag
                LaadVraag();
            }
        }

        private void volgendeButton_Click(object sender, RoutedEventArgs e)
        {
            if (vragenLijst[counter].TypeVraag == VraagType.meerkeuze)
            {
                // code voor meerkeuze
            }
            else
            {
                vragenLijst[counter].Ingevuld = invulTextBox.Text;
            }
            counter++; // ga naar de volgende vraag

            if (counter < vragenLijst.Count)
            {
                LaadVraag();
            }
            else
            {
                MessageBox.Show("Laatste vraag, druk op Klaar om af te sluiten uw score te bekijken");
                counter--;
            }
        }

        private void Klaar()
        {
            Window w = new ScoreWindow(vragenLijst);
            w.Show();
            this.Close();
        }

        private void klaarButton_Click(object sender, RoutedEventArgs e)
        {
            Klaar(); // misschien onnodig om methode voor te gebruiken
        }

        private void LaadVraag()
        {
            switch (vragenLijst[counter].TypeVraag) { 
                case(VraagType.basis):
                    invulTextBox.Visibility = Visibility.Visible;
                    invulListBox.Visibility = Visibility.Hidden;
                    break;
                case(VraagType.meerkeuze):
                    invulTextBox.Visibility = Visibility.Visible;
                    invulListBox.Visibility = Visibility.Hidden;
                    break;
                case(VraagType.wiskunde):
                    invulTextBox.Visibility = Visibility.Hidden;
                    invulListBox.Visibility = Visibility.Visible;
                    break;
                default: MessageBox.Show("Verkeerd vraag type");
                    // this.Close();
                    break;
            }

            opgaveTextBlock.Text = vragenLijst[counter].Opgave;
            invulTextBox.Text = vragenLijst[counter].Ingevuld;
        
        }
       
    }
}
