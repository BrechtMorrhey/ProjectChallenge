using System;
using System.Collections.Generic;
using System.IO;
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
//Author: Brecht Morrhey
namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for ScoreKlasWindow.xaml
    /// </summary>
    public partial class ScoreKlasWindow : Window
    {
        //variables
        string klas;
        private MainVragenWindow menuWindow;

        //constructors
        public ScoreKlasWindow(string klas, MainVragenWindow menuWindow)
        {
            this.klas = klas;
            InitializeComponent();
            //Author: Stijn Stas
            this.menuWindow = menuWindow;
            klasLabel.Content = klas + ":";
        }

        //event handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Challenger\\challenge scores";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);  //oppassen voor Directory Not Found Exception
            List<string> leerlingenLijst = new List<string>();
            string filename, userId;

            // maak een lijst van alle leerlingen

            foreach (string file in files)
            {
                try
                {
                    filename = System.IO.Path.GetFileName(file);
                    if (klas == filename.Split('_')[0])
                    {
                        userId = filename.Split('_')[1];
                        if (!leerlingenLijst.Contains(userId))
                        {
                            leerlingenLijst.Add(userId);
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Index Out of Range Exception in " + file + ". Bestand is mogelijk corrupt");
                    this.NaarMenu();
                }
            }


            Dictionary<string, double> leerlingScores = new Dictionary<string, double>();
            foreach (string item in leerlingenLijst)    //initialiseer Dictionary waar per leerling de gemiddeldescore wordt bijgehouden
            {
                leerlingScores.Add(item, 0);
            }

            double score;
            string vraag = "";
            string voorNaam, naam;
            ScoreLezer lezer = new ScoreLezer();
            foreach (string file in files)
            {
                try
                {
                    lezer.BestandsNaam = file;
                    lezer.Initialise();

                    if (klas == lezer.Klas)
                    {
                        userId = lezer.UserId;
                        vraag = lezer.Vraag;
                        voorNaam = lezer.VoorNaam;
                        naam = lezer.Naam;
                        score = lezer.Score;

                        //bereken nieuwe gemiddelde score
                        if (leerlingScores[userId] != 0)
                        {
                            score = (score + leerlingScores[userId]) / 2;
                        }
                        leerlingScores[userId] = score; //ken aan elk userId de juiste score toe
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Bestand " + file + " niet gevonden.");
                    this.NaarMenu();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Argument Exception bij inlezen bestand " + file);
                    this.NaarMenu();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Kan score in " + file + " bij vraag " + vraag + " niet omzetten");
                    this.NaarMenu();
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Score in " + file + " bij vraag " + vraag + " is te groot");
                    this.NaarMenu();
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("KeyNotFoundException, de bestanden zijn mogelijk aangepast tijdens het inladen, programma keert terug naar menu");
                    this.NaarMenu();
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Index Out of Range Exception in " + file + ". Bestand is mogelijk corrupt");
                    this.NaarMenu();
                }
                catch (BestandTeGrootException exception)
                {
                    MessageBox.Show(exception.Message);
                    this.NaarMenu();
                }
                finally
                {
                    lezer.Close();
                }
            }
            Button b;
            foreach (KeyValuePair<string, double> entry in leerlingScores)
            {
                b = new Button();
                b.Content = entry.Key + ":\t" + entry.Value + "%";
                b.Click += scoresListBoxItem_Click;
                scoresListBox.Items.Add(b);
            }
            //http://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary-in-c

            if (leerlingenLijst.Count == 0)
            {
                b = new Button();
                b.Content = "geen leerlingen";
                scoresListBox.Items.Add(b);
            }
        }
        private void scoresListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            string userId = ((string)((Button)(sender)).Content).Split(':')[0];
            Leerling gebruiker = null;
            //Author: Stijn Stas
            foreach (Leerling leerling in menuWindow.Gebruikers.Leerlingen)
            {
                if (userId == leerling.ID)
                {
                    gebruiker = leerling;
                    Window w = new ScoreLeerlingWindow(gebruiker, menuWindow);
                    w.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Foutief Bestand", "FOUT", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void klassenButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            ScoreAlleWindow klassen = new ScoreAlleWindow(new OverzichtScoresWindow(menuWindow), menuWindow);
            klassen.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            this.NaarMenu();
        }

        //methods
        private void NaarMenu()
        {
            //Author: Stijn Stas
            menuWindow.Show();
            this.Close();
        }
    }
}
