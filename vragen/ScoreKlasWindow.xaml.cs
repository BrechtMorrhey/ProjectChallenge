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

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for ScoreKlasWindow.xaml
    /// </summary>
    public partial class ScoreKlasWindow : Window
    {
        string klas;
        private MainVragenWindow menuWindow;
        private Window vorigWindow;

        public ScoreKlasWindow(string klas, Window vorigWindow, MainVragenWindow menuWindow)
        {
            this.klas=klas;            
            InitializeComponent();
            this.menuWindow = menuWindow;
            this.vorigWindow = vorigWindow;
        }

        private void scoresListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            string userId = ((string)((Button)(sender)).Content).Split(':')[0];
            Window w = new ScoreLeerlingWindow(userId, this, menuWindow);
            w.Show();
            this.Close();
        }

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

            Dictionary<string, double> leerlingScores = new Dictionary<string, double>();
            foreach (string item in leerlingenLijst)    //initialiseer Dictionary
            {
                leerlingScores.Add(item, 0);
            }

            double score;
            string vraag = "";
            string voorNaam, naam, line;
            StreamReader inputStream = null;
            foreach (string file in files)
            {
                try
                {
                    inputStream = File.OpenText(file);
                    filename = System.IO.Path.GetFileName(file);
                    if (klas == filename.Split('_')[0])
                    {
                        userId = filename.Split('_')[1];
                        vraag = filename.Split('_')[3];
                        line = inputStream.ReadLine();
                        voorNaam = line.Split(',')[0];
                        naam = line.Split(',')[1];
                        score = Convert.ToDouble(((inputStream.ReadLine().Split(':')[2]).Split('%')[0])); //verwijder procent teken en converteer naar double

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
                finally
                {
                    if (inputStream != null)
                    {
                        inputStream.Close();
                    }
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
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            vorigWindow.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NaarMenu();
        }

        private void NaarMenu()
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
