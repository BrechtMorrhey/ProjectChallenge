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

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RegistratieWindow : Window
    {
        private string naam;
        private string voornaam;
        private string geboorteDatum;
        private string passwoord;
        private string klas;
        private AlleGebruikers allegebruikers;
        private MainWindow mainWindow;
        public RegistratieWindow(AlleGebruikers allegebruikers, MainWindow mainWindow)
        {
            InitializeComponent();
            this.allegebruikers = allegebruikers;
            klasComboBox.ItemsSource = allegebruikers.Klassen;
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            naam = naamTextBox.Text;
            voornaam = voornaamTextBox.Text;
            passwoord = passwoordPasswordBox.Password;
            geboorteDatum = datumDatePicker.Text;
            

            if (soortRegistratieComboBox.SelectedItem == leerlingItem)
            {
                Leerling student = new Leerling(naam, voornaam, geboorteDatum, passwoord, klas, allegebruikers);
                student.SlaOp();
                MessageBox.Show(string.Format("student:\t\t{0}\npasswoord:\t{1}\nOpgeslagen", student.ID, student.Paswoord));
            }
            else if (soortRegistratieComboBox.SelectedItem == leerkrachtItem)
            {
                Leerkracht leerkracht = new Leerkracht(naam, voornaam, geboorteDatum, passwoord, allegebruikers);
                leerkracht.SlaOp();
                MessageBox.Show(string.Format("leerkracht:\t{0}\npasswoord:\t{1}\nOpgeslagen", leerkracht.ID, leerkracht.Paswoord));
            }
        }

        private void soortRegistratieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (soortRegistratieComboBox.SelectedItem == leerlingItem)
            {
                klasLabel.Visibility = Visibility.Visible;
                klasComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                klasLabel.Visibility = Visibility.Hidden;
                klasComboBox.Visibility = Visibility.Hidden;
            }
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }
    }
}
