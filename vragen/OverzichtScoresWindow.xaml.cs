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
    /// Interaction logic for OverzichtScoresWindow.xaml
    /// </summary>
    public partial class OverzichtScoresWindow : Window
    {
        private MainVragenWindow menuWindow;
        public OverzichtScoresWindow(MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.menuWindow = menuWindow;
        }
        private void userIdButton_Click(object sender, RoutedEventArgs e)
        {
            if (userIdTextBox.Text != "" && userIdTextBox.Text != null)
            {
                Window w = new ScoreLeerlingWindow(userIdTextBox.Text);
                w.Show();
            }
            else
            {
                MessageBox.Show("Gelieve eerst een userId in te geven", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void klasButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void alleButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new ScoreAlleWindow();
            w.Show();
        }

        private void scoreKlasButton_Click(object sender, RoutedEventArgs e)
        {
            if (scoreKlasTextBox.Text != "" && scoreKlasTextBox.Text != null)
            {
                Window w = new ScoreKlasWindow(scoreKlasTextBox.Text);
                w.Show();
            }
            else
            {
                MessageBox.Show("Gelieve eerst een klasId in te geven", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
