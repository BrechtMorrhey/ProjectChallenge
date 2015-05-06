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

namespace ProjectChallenge.login
{
    /// <summary>
    /// Interaction logic for maakKlas.xaml
    /// </summary>
    public partial class maakKlasWindow : Window
    {
        private MainVragenWindow menuWindow;
        private AlleGebruikers alleGebruikers;
        public maakKlasWindow(AlleGebruikers alleGebruikers, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.alleGebruikers = alleGebruikers;
            this.menuWindow = menuWindow;
        }

        private void nieuweKlasButton_Click(object sender, RoutedEventArgs e)
        {
            string klas = nieuweKlasTextBox.Text;
            alleGebruikers.SlaKlasOp(klas);
            MessageBox.Show("Klas aangemaakt !!!", "Klas aangemaakt", MessageBoxButton.OK);
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
