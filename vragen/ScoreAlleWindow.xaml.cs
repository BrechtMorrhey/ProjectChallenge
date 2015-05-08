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
    /// Interaction logic for ScoreAlleWindow.xaml
    /// </summary>
    public partial class ScoreAlleWindow : Window
    {
        MainVragenWindow menuWindow;
        OverzichtScoresWindow vorigWindow;

        public ScoreAlleWindow(OverzichtScoresWindow vorigWindow, MainVragenWindow menuWindow )
        {
            InitializeComponent();
            this.menuWindow = menuWindow;
            this.vorigWindow = vorigWindow;
        }
        private void scoresListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            string klas = ((string)((Button)(sender)).Content).Split(':')[0];
            Window w = new ScoreKlasWindow(klas, menuWindow);
            w.Show();
            this.Hide();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string klas = "";
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Challenger\\challenge scores\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);  //oppassen voor Directory Not Found Exception
            List<string> klassenLijst = new List<string>();
            string filename;

            // maak een lijst van alle klassen
            foreach (string file in files)
            {
                filename = System.IO.Path.GetFileName(file);
                klas = filename.Split('_')[0];
                if (!klassenLijst.Contains(klas))
                {
                    klassenLijst.Add(klas);
                }
            }

            Dictionary<string, double> klasScores = new Dictionary<string, double>();
            foreach (string item in klassenLijst)
            {
                klasScores.Add(item, 0);
            }

            double score;
            string vraag = "";
            StreamReader inputStream = null;
            foreach (string file in files)
            {
                try
                {
                    inputStream = File.OpenText(file);
                    filename = System.IO.Path.GetFileName(file);
                    klas = filename.Split('_')[0];
                    vraag = filename.Split('_')[3];
                    inputStream.ReadLine(); //sla de eerste lijn over
                    score = Convert.ToDouble(((inputStream.ReadLine().Split(':')[2]).Split('%')[0])); //verwijder procent teken en converteer naar double

                    //bereken nieuwe gemiddelde score
                    if (klasScores[klas] != 0)
                    {
                        score = (score + klasScores[klas]) / 2;
                    }
                    klasScores[klas] = score;
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
                    MessageBox.Show("KeyNotFoundException, de bestanden zijn mogelijk aangepast tijdens het inladen, programma keert terug naar hoofdmenu");
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
            foreach (KeyValuePair<string, double> entry in klasScores)
            {
                b = new Button();
                b.Content = entry.Key + ":\t" + entry.Value + "%";
                b.Click += scoresListBoxItem_Click;
                scoresListBox.Items.Add(b);
            }
            //http://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary-in-c

            if (klasScores.Count == 0)
            {
                b = new Button();
                b.Content = "geen klassen met resultaten";
                scoresListBox.Items.Add(b);
            }
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NaarMenu();
        }

        private void overzichtButton_Click(object sender, RoutedEventArgs e)
        {
            vorigWindow.Show();
            this.Close();
        }

        private void NaarMenu()
        {
            menuWindow.Show();
            vorigWindow.Close();
            this.Close();
        }
    }
}
