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
//Author: Brecht Morrhey 

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
                //Author: Stijn Stas
                Leerling gebruiker = null;
                bool gebruikerGevonden = false;
                foreach (Leerling leerling in menuWindow.Gebruikers.Leerlingen)
                {
                    if (userIdTextBox.Text == leerling.ID)
                    {
                        gebruiker = leerling;
                        gebruikerGevonden = true;
                        Window w = new ScoreLeerlingWindow(gebruiker, menuWindow);
                        w.Show();
                        this.Hide();
                    }
                }
                if (!gebruikerGevonden)
                {
                    MessageBox.Show("UserId niet gevonden, probeer opnieuw", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }   
            else
            {
                MessageBox.Show("Gelieve eerst een bestaand userId in te geven", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void alleButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new ScoreAlleWindow(this, menuWindow);
            w.Show();
            this.Hide();
        }

        private void scoreKlasButton_Click(object sender, RoutedEventArgs e)
        {
            if (scoreKlasTextBox.Text != "" && scoreKlasTextBox.Text != null)
            {
                //Author: Stijn Stas
                bool klasGevonden = false;
                foreach (string klas in menuWindow.Gebruikers.Klassen)
                {
                    if (klas == scoreKlasTextBox.Text)
                    {
                        klasGevonden = true;
                        Window w = new ScoreKlasWindow(scoreKlasTextBox.Text, menuWindow);
                        w.Show();
                        this.Hide();
                    }
                    if (!klasGevonden)
                    {
                        MessageBox.Show("KlasId niet gevonden, probeer opnieuw", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst een bestaand klasId in te geven", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            menuWindow.Show();
            this.Close();
        }
    }
}
