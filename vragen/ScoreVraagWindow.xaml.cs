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
using System.IO;
//Author: Brecht Morrhey

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for ScoreVraagWindow.xaml
    /// </summary>
    public partial class ScoreVraagWindow : Window
    {
        private string bestandNaam;
        private MainVragenWindow menuWindow;
        private Window vorigWindow;

        public ScoreVraagWindow(string bestandNaam, Window vorigWindow, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.bestandNaam = bestandNaam;
            //Author: Stijn Stas
            this.vorigWindow = vorigWindow;
            this.menuWindow = menuWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ScoreLezer lezer = new ScoreLezer();
            try
            {
                lezer.BestandsNaam = bestandNaam;
                lezer.Initialise();
                string klas = lezer.Klas;
                string voorNaam = lezer.VoorNaam;
                string naam = lezer.Naam;
                string vraag = lezer.Vraag;

                klasEnLeerlingLabel.Content = voorNaam + " " + naam + "\n" + klas + "\n" + lezer.Vraag;
                List<string> resultaten = lezer.Resultaten;
                foreach (string resultaat in resultaten)
                {
                    scoresListBox.Items.Add(resultaat);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Bestand " + bestandNaam + " niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.NaarMenu();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Argument Exception bij inlezen bestand " + bestandNaam, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.NaarMenu();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Index Out of Range Exception in " + bestandNaam + ". Bestand is mogelijk corrupt");
                this.NaarMenu();
            }
            catch (BestandTeGrootException exception)
            {
                MessageBox.Show(exception.Message);
                this.NaarMenu();
            }
            finally
            {
                lezer.Close();
            }
        }

        private void leerlingButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            vorigWindow.Show();
            this.Close();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Author: Stijn Stas
            NaarMenu();
        }

        private void NaarMenu()
        {
            //Author: Stijn Stas
            menuWindow.Show();
            this.Close();
        }
    }
}
