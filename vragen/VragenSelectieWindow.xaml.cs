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
//Author: Stijn Stas

namespace ProjectChallenge.vragen
{
    /// <summary>
    /// Interaction logic for VragenSelectieWindow.xaml
    /// </summary>
    public partial class VragenSelectieWindow : Window
    {
        private Leerling gebruiker;
        private MainVragenWindow menuWindow;

        public VragenSelectieWindow(Leerling gebruiker, MainVragenWindow menuWindow)
        {
            InitializeComponent();

            this.gebruiker = gebruiker;
            this.menuWindow = menuWindow;
            oefenenRadioButton.IsChecked = true;
        }

        

        private void beginButton_Click(object sender, RoutedEventArgs e)
        {
            OplossenWindow oplossen = null;
            if(vakkenListBox.SelectedItem != null)
            {
                string bestand = ((ListBoxItem)vakkenListBox.SelectedItem).Content.ToString();
                if (oefenenRadioButton.IsChecked == true)
                {
                    oplossen = new OplossenWindow(bestand, gebruiker, menuWindow, this);
                }
                else if (makkelijkRadioButton.IsChecked == true)
                {
                    oplossen = new OplossenWindow(bestand, 30, gebruiker, menuWindow, this);
                }
                else if (gemiddeldRadioButton.IsChecked == true)
                {
                    oplossen = new OplossenWindow(bestand, 20, gebruiker, menuWindow, this);
                }
                else if (moeilijkRadioButton.IsChecked == true)
                {
                    oplossen = new OplossenWindow(bestand, 10, gebruiker, menuWindow, this);
                }
                else
                {
                    MessageBox.Show("Geen Moeilijkheidsgraad geselecteerd", "fout", MessageBoxButton.OK);
                }

                if (oplossen != null)
                {
                    // als windowNotClosed op true staat
                    // dan het venster tonen
                    if (oplossen.WindowNotClosed)
                    {
                        oplossen.Show();

                    }
                    //  checken of window wel getoond word
                    //  (dit kan door middel van exceptions 
                    //  geclosed, dus niet getoond worden)
                    //  voor we het menu hiden

                    if (oplossen.IsActive)
                    {
                        this.Close();
                    } 
                }

            }
            else
            {
                MessageBox.Show("Selecteer eerst een vak ", "fout", MessageBoxButton.OK);
            }
        }

        private void terugButton_Click(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Challenger\\Vragenlijsten";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);
            string filename, vak = "";
            ListBoxItem vakItem;

            foreach (string file in files)
            {
                
                filename = System.IO.Path.GetFileName(file);
                if (filename.Split('.')[1] == "txt")
                {
                    vak = filename.Split('.')[0];
                    vakItem = new ListBoxItem();
                    vakItem.Content = vak;
                    vakkenListBox.Items.Add(vakItem);
                }
            }
        }
      
        private void NaarMenu()
        {
            menuWindow.Show();
            this.Close();
        }
    }
}
