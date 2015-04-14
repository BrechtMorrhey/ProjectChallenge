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
using System.IO;

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlleGebruikers alleGebruikers;
        public MainWindow()
        {
            InitializeComponent();

            alleGebruikers = new AlleGebruikers();
            
        }

        private void registratieButton_Click(object sender, RoutedEventArgs e)
        {
            RegistratieWindow registratie = new RegistratieWindow(alleGebruikers);
            Hide();
            registratie.Show();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow(alleGebruikers);
            Hide();
            login.Show();
        }
    }
}
