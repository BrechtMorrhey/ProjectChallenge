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
//Author: Brecht Morrhey

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainVragenWindow : Window
    {
        //variables
        private string bestandsNaam;
        private AlleGebruikers alleGebruikers;
        private MainWindow mainWindow;
        private string programmaDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Challenger");
        private string vragenlijstenDirPath;
        private Leerling gebruiker;
        
        //constructors
        public MainVragenWindow()
        {
            InitializeComponent();
            //Author: Stijn Stas
            vragenlijstenDirPath = programmaDirPath + "\\Vragenlijsten";
            if (!(System.IO.Directory.Exists(vragenlijstenDirPath)))
            {
                System.IO.Directory.CreateDirectory(vragenlijstenDirPath);
            }
        }

        public MainVragenWindow(Leerling leerling, MainWindow mainWindow)
            :this()
        {
            //Author: Stijn Stas
            this.mainWindow = mainWindow;
            bekijkScoreButton.Visibility = Visibility.Visible;
            oplossenButton.Visibility = Visibility.Visible;
            gebruiker = leerling;
        }
        public MainVragenWindow(Leerkracht leerkracht, AlleGebruikers allegebruikers, MainWindow mainWindow)
            :this()
        {
            //Author: Stijn Stas
            this.alleGebruikers = allegebruikers;
            this.mainWindow = mainWindow;
            aanpassenButton.Visibility = Visibility.Visible;
            nieuweKlasButton.Visibility = Visibility.Visible;
            opstellenButton.Visibility = Visibility.Visible;
            scoreButton.Visibility = Visibility.Visible;
        }

        //event handlers
        private void opstellenButton_Click(object sender, RoutedEventArgs e)
        {
            // Author: Akki Stankidis
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
            //OpenFileDialog dialog = new OpenFileDialog();
            //// initial directory 
            //dialog.InitialDirectory = vragenlijstenDirPath;
            //// only .txt files
            //dialog.Filter = "Text files (*.txt)|*.txt;";
            //dialog.ShowDialog();
            //bestandsNaam = dialog.FileName;
            //if (bestandsNaam != null && bestandsNaam != "")
            //{
            //    Window w = new OplossenWindow(bestandsNaam, gebruiker, this);
            //    w.Show();
            //    this.Hide();
            //}

            //Author: Stijn Stas
            vragen.VragenSelectieWindow vragenSelectieWindow = new vragen.VragenSelectieWindow(gebruiker, this);
            vragenSelectieWindow.Show();
            this.Hide();
        }
             
        private void aanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            // Author: Akki Stankidis

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
            Window nieuweKlasWindow = new login.MaakKlasWindow(alleGebruikers, this);
            nieuweKlasWindow.Show();
            this.Hide();
        }
        private void bekijkScoreButton_Click(object sender, RoutedEventArgs e)
        {
            ScoreLeerlingWindow window = new ScoreLeerlingWindow(gebruiker, this);
            window.klasButton.Visibility = Visibility.Hidden;
            window.Show();
            this.Hide();
        }

        //properties
        //Author: Stijn Stas
        public AlleGebruikers Gebruikers
        {
            get
            {
                return alleGebruikers;
            }
        }

    }
}
