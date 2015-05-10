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

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    /// 

    //  Verzorgt de code achter het 
    //  login formulier
    //
    //  Author: Stijn Stas

    public partial class LoginWindow : Window
    {
        //  Eigenschappen
        private AlleGebruikers alleGebruikers;
        private Persoon gebruiker;
        private MainWindow mainWindow;

        //  Constructor
        public LoginWindow(MainWindow mainWindow, AlleGebruikers alleGebruikers)
        {
            InitializeComponent();
            this.alleGebruikers = alleGebruikers;
            this.mainWindow = mainWindow;
        }
        
        //  Methoden

        //  Code achter login button
        //  gebruiker word ingelogd
        //  via methode Login()
        //  en menu word opgeroepen
        //
        //  Author: Stijn Stas

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool loginOk = Login();
            MainVragenWindow menuWindow;
            if (loginOk)
            {
                if( gebruiker.GeefGebruikersType() == "leerling")
                {
                    Leerling leerling = (Leerling) gebruiker;
                    menuWindow = new MainVragenWindow(leerling, Main);
                }
                else 
                {
                    Leerkracht leerkracht = (Leerkracht)gebruiker;
                    menuWindow = new MainVragenWindow(leerkracht, alleGebruikers, Main);
                }

                menuWindow.Show();
                this.Close();
            }
            else
            { 
                MessageBox.Show("Foute login", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //  Login() controleert de ingegeven
        //  gegevens en vergelijkt deze met
        //  de gebruikersgegevens in de lijsten
        //  die zich in het allegebruikers object
        //  bevinden
        //
        //  Author: Stijn Stas

        private bool Login()
        {
            string gebruikerId = idTextBox.Text;
            string gebruikerPassword = passwordPasswordBox.Password;

            foreach (Leerling student in alleGebruikers.Leerlingen)
            {
                if (gebruikerId == student.ID)
                {
                    if (gebruikerPassword == student.Paswoord)
                    {
                        gebruiker = student;
                        return true; 
                    }
                }
            }
            foreach (Leerkracht leerkracht in alleGebruikers.Leerkrachten)
            {
                if (gebruikerId == leerkracht.ID)
                {
                    if (gebruikerPassword == leerkracht.Paswoord)
                    {
                        gebruiker = leerkracht;
                        return true;
                    }
                }
            }
            return false;
        }
        
        //  Keert terug naar het
        //  beginscherm
        //
        //  Author: Stijn Stas

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        //  Properties
        public MainWindow Main
        {
            get
            {
                return mainWindow;
            }
            set
            {
            }
        }
    }
}
