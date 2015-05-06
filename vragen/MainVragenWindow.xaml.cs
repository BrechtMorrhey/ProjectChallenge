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

        public MainVragenWindow()
        {
            InitializeComponent();
        }

        public MainVragenWindow(Leerling leerling)
        {
            InitializeComponent();
        }
        public MainVragenWindow(Leerkracht leerkracht, AlleGebruikers allegebruikers)
        {
            InitializeComponent();
            this.alleGebruikers = allegebruikers;
        }

        private void opstellenButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                Window w = new AanpassenWindow(bestandsNaam, true);
                w.Show();
            }
        }

        private void oplossenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam != "")
            {
                Window w = new OplossenWindow(bestandsNaam);
                w.Show();
            }
        }
             
        private void aanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            bestandsNaam = dialog.FileName;
            if (bestandsNaam != null && bestandsNaam!="")
            {
                Window w = new AanpassenWindow(bestandsNaam, false);
                w.Show();
            }
        }

        private void scoreButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OverzichtScoresWindow();
            w.Show();
        }

        private void NieuweKlas_Click()
        {
            Window nieuweKlasWindow = new login.maakKlasWindow(alleGebruikers);
            nieuweKlasWindow.Show();
            this.Hide();
        }

    }
}
