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
    /// 

    //  Verzorgt de code achter het 
    //  maak klas formulier
    //
    //  Author: Stijn Stas

    public partial class MaakKlasWindow : Window
    {

        //  Eigenschappen
        private MainVragenWindow menuWindow;
        private AlleGebruikers alleGebruikers;

        //  Constructor
        public MaakKlasWindow(AlleGebruikers alleGebruikers, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.alleGebruikers = alleGebruikers;
            this.menuWindow = menuWindow;
        }

        //  Methoden

        
        //  Verzorgt code achter de nieuwe klas button
        //  maakt gebruik van allegebruikers om klas
        //  aan te maken en op te slaan
        //
        //  Author: Stijn Stas
        private void nieuweKlasButton_Click(object sender, RoutedEventArgs e)
        {
            string klas = nieuweKlasTextBox.Text;
            alleGebruikers.SlaKlasOp(klas);
            MessageBox.Show("Klas aangemaak", "Klas "+klas+" aangemaakt", MessageBoxButton.OK);
        }

        //  Keer terug naar het menu.
        //
        //  Author: Stijn Stas

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
