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
    public partial class LoginWindow : Window
    {
        private AlleGebruikers alleGebruikers;
        private Persoon gebruiker;
        private MainWindow mainWindow;
        public LoginWindow(MainWindow mainWindow, AlleGebruikers alleGebruikers)
        {
            InitializeComponent();
            this.alleGebruikers = alleGebruikers;
            this.mainWindow = mainWindow;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool loginOk = Login();
            //MainVragenWindow w;
            if (loginOk)
            {
                MessageBox.Show("Login OK");
                if( gebruiker.GeefGebruikersType() == "leerling")
                {
                    Leerling leerling = (Leerling) gebruiker;
                    //w = new MainVragenWindow(leerling);
                }
                else 
                {
                    Leerkracht leerkracht = (Leerkracht)gebruiker;
                    //w = new MainVragenWindow(leerkracht, alleGebruikers);
                }

                //w.Show();
            }
            else
            {
                MessageBox.Show("Foute login");

                // ENKEL VOOR TESTDOELEINDEN

                MainVragenWindow w = new MainVragenWindow();
                w.Show();
                // VERWIJDER UIT UITEINDELIJK PROJECT
            }
        }

        private bool Login()
        {
            string gebruikerId = idTextBox.Text;
            string gebruikerPassword = passwordTextBox.Text;

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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }
    }
}
