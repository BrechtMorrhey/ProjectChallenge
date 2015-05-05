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
        private AlleGebruikers gebruikers;

        public MainVragenWindow(Leerkracht gebruiker, AlleGebruikers alleGebruikers)
        {
            InitializeComponent();

            gebruikers = alleGebruikers;
            nieuweKlasButton.Visibility = Visibility.Visible;
            opstellenButton.Visibility = Visibility.Visible;
            aanpassenButton.Visibility = Visibility.Visible;
        }

        public MainVragenWindow(Leerling gebruiker)
        {
            InitializeComponent();
            oplossenButton.Visibility = Visibility.Visible;
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

        private void nieuweKlasButton_Click(object sender, RoutedEventArgs e)
        {
            Window nieuweKlas = new login.maakKlasWindow(gebruikers, this);

        } 
    }
}
