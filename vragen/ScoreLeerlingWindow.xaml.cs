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
    /// Interaction logic for ScoreLeerlingWindow.xaml
    /// </summary>
    public partial class ScoreLeerlingWindow : Window
    {
        //variables
        private string userId;
        private Leerling leerling;
        private Dictionary<Button, string> bestandsNaamDictionary;
        private MainVragenWindow menuWindow;

        //constructors
        public ScoreLeerlingWindow(Leerling leerling, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            //Author: Stijn Stas
            this.menuWindow = menuWindow;
            this.userId = leerling.ID;
            this.leerling = leerling;

        }

        //event handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            klasEnLeerlingLabel.Content = leerling.Voornaam + " " + leerling.Naam + "\n" + leerling.Klas;
            //

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Challenger\\challenge scores";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);  //oppassen voor Directory Not Found Exception
            List<string> userFiles = new List<string>();
            string filename;

            // zoek alle scores met die userId

            foreach (string file in files)
            {
                try
                {

                    filename = System.IO.Path.GetFileName(file);
                    if (userId == filename.Split('_')[1])
                    {
                        userFiles.Add(file);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Index Out of Range Exception in " + file + ". Bestand is mogelijk corrupt");
                    this.NaarMenu();
                }
            }



            double score;
            string vraag;
            Button b;
            ScoreLezer lezer = new ScoreLezer();
            bestandsNaamDictionary = new Dictionary<Button, string>(); //Maak een dictionary om voor elke score het bijbehorende filepath bij te houden
            foreach (string file in userFiles)
            {
                try
                {
                    lezer.BestandsNaam = file;
                    lezer.Initialise();
                    vraag = lezer.Vraag;
                    score = lezer.Score;
                    b = new Button();
                    b.Click += scoresListBoxItem_Click;
                    b.Content = (vraag + ":\t" + score+"%");
                    scoresListBox.Items.Add(b);
                    bestandsNaamDictionary.Add(b, file);

                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Bestand " + file + " niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NaarMenu();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Argument Exception bij inlezen bestand " + file, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (bestandsNaamDictionary.Count == 0)
            {
                b = new Button();
                b.Content = "geen resultaten";
                scoresListBox.Items.Add(b);
            }
        }

        private void scoresListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            string bestandsNaam = bestandsNaamDictionary[(Button)sender];
            Window w = new ScoreVraagWindow(bestandsNaam, this, menuWindow);
            w.Show();
            this.Hide();
        }

        private void klasButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            ScoreKlasWindow klas = new ScoreKlasWindow(leerling.Klas, menuWindow);
            klas.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            NaarMenu();
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
