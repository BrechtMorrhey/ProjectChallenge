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
//Author: Brecht Morrhey

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        //variables
        private MainVragenWindow menuWindow;
        private string bestandsNaam;
        private int verdiendeMinuten;
        private List<Vraag> vragenLijst;
        private Leerling gebruiker;
        private string moeilijkHeidsGraad;
        private int tijdPerVraag = 0;
        
        //constructors
        public ScoreWindow(MainVragenWindow menuWindow, Leerling gebruiker, List<Vraag> vragenLijst, string bestandsNaam, int tijd)
        {
            InitializeComponent();
            this.bestandsNaam = bestandsNaam;
            this.vragenLijst = vragenLijst;
            this.gebruiker = gebruiker;
            this.menuWindow = menuWindow;

            //Author: Stijn Stas
            this.tijdPerVraag = tijd;
            
            if (tijdPerVraag == 10)
            {
                moeilijkHeidsGraad = "Moeilijk";
            }
            else if (tijdPerVraag == 20)
            {
                moeilijkHeidsGraad = "Gemiddeld";
            }
            else if (tijdPerVraag == 30)
            {
                moeilijkHeidsGraad = "Makkelijk";
            }
            else
            {
                moeilijkHeidsGraad = "Oefenen";
            }

        }
            
        //event handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            String userId, voorNaam, achterNaam, klas;
            userId = gebruiker.ID;
            voorNaam = gebruiker.Voornaam;
            achterNaam = gebruiker.Naam;
            klas = gebruiker.Klas;
            string juistOfFout, juistOfFout2;

            int score = vragenLijst.Count;  //bereken maximum score
            foreach (Vraag vraag in vragenLijst)
            {
                if (vraag.IsJuist)
                {
                    juistOfFout = "juist";
                }
                else
                {
                    juistOfFout = "Fout";
                    score--;
                }
                scoreListBox.Items.Add(vraag.Opgave+"\t"+ juistOfFout + "\t" + vraag.Antwoord + "\t" + vraag.Ingevuld);
            }
            scoreListBox.Items.Add("Score: " + score + "/" + vragenLijst.Count + "\t Percentage: " + ((double)score / vragenLijst.Count) * 100 + "%");
            naamTextBlock.Text = voorNaam + " " + achterNaam;
            klasTextBlock.Text = klas;

            // zet de score om in game tijd
            verdiendeMinuten = (int)Math.Round(((double)score / vragenLijst.Count) * 10 / (tijdPerVraag / 10));

            // sla de score van de student op
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Challenger\\challenge scores\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter outputStream = File.CreateText(path + klas + "_" + userId + "_Score_" + System.IO.Path.GetFileName(bestandsNaam).Split('.')[0] + moeilijkHeidsGraad+".txt"); //oppassen voor Directory Not Found Exception
            outputStream.WriteLine(voorNaam + ", " + achterNaam);
            // zet de score vanboven om makkelijker te kunnen inlezen
            outputStream.WriteLine("Score: " + score + "/" + vragenLijst.Count + "\t Percentage: " + ((double)score / vragenLijst.Count) * 100 + "%");
            
            // sla de specifieke antwoorden op
            foreach (Vraag vraag in vragenLijst)
            {
                if (vraag.IsJuist)
                {
                    juistOfFout2 = "juist";
                }
                else
                {
                    juistOfFout2 = "Fout";
                }
                outputStream.WriteLine(vraag.Opgave +", "+ juistOfFout2 + ", " + vraag.Antwoord + ", " + vraag.Ingevuld);
            }
            outputStream.Close();

        }

        private void gameButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Game(menuWindow, verdiendeMinuten);
            w.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            menuWindow.Show();
            this.Close();
        }


    }
}
