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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainVragenWindow : Window
    {
        private string bestandsNaam;
        private AlleGebruikers alleGebruikers;
        private MainWindow mainWindow;
        private string programmaDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Challenger");
        private string vragenlijstenDirPath;
        private Leerling gebruiker;
        public MainVragenWindow()
        {
            InitializeComponent();
            vragenlijstenDirPath = programmaDirPath + "\\Vragenlijsten";
            if (!(System.IO.Directory.Exists(vragenlijstenDirPath)))
            {
                System.IO.Directory.CreateDirectory(vragenlijstenDirPath);
            }
        }

        public MainVragenWindow(Leerling leerling, MainWindow mainWindow)
            :this()
        {
            this.mainWindow = mainWindow;
            bekijkScoreButton.Visibility = Visibility.Visible;
            oplossenButton.Visibility = Visibility.Visible;
            gebruiker = leerling;
        }
        public MainVragenWindow(Leerkracht leerkracht, AlleGebruikers allegebruikers, MainWindow mainWindow)
            :this()
        {
            this.alleGebruikers = allegebruikers;
            this.mainWindow = mainWindow;
            aanpassenButton.Visibility = Visibility.Visible;
            nieuweKlasButton.Visibility = Visibility.Visible;
            opstellenButton.Visibility = Visibility.Visible;
            scoreButton.Visibility = Visibility.Visible;
        }

        private void opstellenButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            // initial directory 
            dialog.InitialDirectory = vragenlijstenDirPath;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                Window w = new AanpassenWindow(bestandsNaam, true, this);
                w.Show();
                this.Hide();
            }
        }

        private void oplossenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            // initial directory 
            dialog.InitialDirectory = vragenlijstenDirPath ;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                
                OplossenWindow w = new OplossenWindow(bestandsNaam, gebruiker, this);

                // als windowNotClosed op true staat
                // dan het venster tonen
                if (w.WindowNotClosed)
                {
                    w.Show();
                    
                }
                //  checken of window wel getoond word
                //  (dit kan door middel van exceptions 
                //  geclosed, dus niet getoond worden)
                //  voor we het menu hiden

                if (w.IsActive)
                {
                    this.Hide();
                }        
            }
        }
             
        private void aanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            // initial directory 
            dialog.InitialDirectory = vragenlijstenDirPath;
            // only .txt files
            dialog.Filter = "Text files (*.txt)|*.txt;";
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam!="")
            {
                Window w = new AanpassenWindow(bestandsNaam, false, this);
                w.Show();
                this.Hide();
            }
        }

        private void scoreButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OverzichtScoresWindow(this);
            w.Show();
            this.Hide();
        }

        //private void GameButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Game game = new Game(this);
        //    game.Show();
        //    this.Hide();
        //}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void LogUitButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        private void NieuweKlasButton_Click(object sender, RoutedEventArgs e)
        {
            Window nieuweKlasWindow = new login.maakKlasWindow(alleGebruikers, this);
            nieuweKlasWindow.Show();
            this.Hide();
        }

        public AlleGebruikers Gebruikers
        {
            get
            {
                return alleGebruikers;
            }
        }

        private void bekijkScoreButton_Click(object sender, RoutedEventArgs e)
        {
            ScoreLeerlingWindow window = new ScoreLeerlingWindow(gebruiker, this);
            window.klasButton.Visibility = Visibility.Hidden;
            window.Show();
            this.Hide();
        }
    }
}
