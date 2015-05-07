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
    /// Interaction logic for ScoreLeerlingWindow.xaml
    /// </summary>
    public partial class ScoreLeerlingWindow : Window
    {
        private string userId;
        private Dictionary<Button, string> bestandsNaamDictionary;
        private MainVragenWindow menuWindow;
        private Window vorigWindow;

        public ScoreLeerlingWindow(Leerling leerling, Window vorigWindow, MainVragenWindow menuWindow)
        { 
            InitializeComponent();
            this.menuWindow = menuWindow;
            this.vorigWindow = vorigWindow;
            this.userId = leerling.ID;
            klasEnLeerlingLabel.Content = leerling.Voornaam + " " + leerling.Naam + "\n" + leerling.Klas;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                filename = System.IO.Path.GetFileName(file);
                if (userId == filename.Split('_')[1])
                {
                    userFiles.Add(file);
                }
            }

            string score;
            string vraag;
            Button b;
            StreamReader inputStream = null;
            bestandsNaamDictionary = new Dictionary<Button, string>();
            foreach (string file in userFiles)
            {
                try
                {
                    inputStream = File.OpenText(file);
                    filename = System.IO.Path.GetFileName(file);
                    vraag = filename.Split('_')[3];
                    inputStream.ReadLine(); //sla de eerste lijn over
                    score = inputStream.ReadLine().Split(':')[2];
                    b = new Button();
                    b.Click += scoresListBoxItem_Click;
                    b.Content=(vraag + ":\t" + score);
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
                    MessageBox.Show("Argument Exception bij inlezen bestand " + file , "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
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
          
        }

        private void scoresListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            string bestandsNaam = bestandsNaamDictionary[(Button)sender];
            Window w = new ScoreVraagWindow(bestandsNaam, this, menuWindow);
            w.Show();
            this.Hide();
        }
        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            vorigWindow.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            NaarMenu();
        }

        private void NaarMenu()
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
