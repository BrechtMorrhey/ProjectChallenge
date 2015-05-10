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
            //http://stackoverflow.com/questions/1472498/wpf-global-exception-handler

            AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UEHandler);
            InitializeComponent();
            alleGebruikers = new AlleGebruikers();
            
        }

        private void registratieButton_Click(object sender, RoutedEventArgs e)
        {
            RegistratieWindow registratie = new RegistratieWindow(alleGebruikers, this);
            Hide();
            registratie.Show();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow(this, alleGebruikers);
            Hide();
            login.Show();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        static void UEHandler(object sender, UnhandledExceptionEventArgs args)
        {
            MessageBox.Show("An unhandled exception has occured: " + (sender.ToString())+"/n Program will shutdown");
            System.Environment.Exit(0);
        }
    }
}
