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

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for ScoreVraagWindow.xaml
    /// </summary>
    public partial class ScoreVraagWindow : Window
    {
        private string bestandNaam;

        public ScoreVraagWindow(string bestandNaam)
        {
            InitializeComponent();
            this.bestandNaam = bestandNaam;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader inputStream = null;
            int j = 0;
            try
            {
                inputStream = File.OpenText(bestandNaam);
                string filename = System.IO.Path.GetFileName(bestandNaam);
                string klas = filename.Split('_')[0];
                string line = inputStream.ReadLine();
                string voorNaam = line.Split(',')[0];
                string naam = line.Split(',')[1];

                while (line != null && j < 10000)
                {
                    scoresListBox.Items.Add(line);
                    line = inputStream.ReadLine();
                    j++;
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Bestand " + bestandNaam + " niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Argument Exception bij inlezen bestand " + bestandNaam, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
                if (j >= 10000)
                {
                    MessageBox.Show("Bestand te groot, programma sluit nu af");
                    this.Close();
                }
            }
        }
    }
}
