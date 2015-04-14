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
        public LoginWindow(AlleGebruikers alleGebruikers)
        {
            InitializeComponent();
            this.alleGebruikers = alleGebruikers;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool loginOk = Login();

            if (loginOk)
            {
                foreach(Leerling student in alleGebruikers.Leerlingen)
                {
                    gebruikersTextBox.AppendText(student.ToString());
                    gebruikersTextBox.AppendText(Environment.NewLine);
                }
                MessageBox.Show("Login OK");
                MainVragenWindow w = new MainVragenWindow();
                w.Show();
            }
            else
            {
                gebruikersTextBox.Clear();
                MessageBox.Show("Foute login");

                // ENKEL VOOR TESTDOELEINDEN
                MessageBox.Show("Login OK");
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
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
