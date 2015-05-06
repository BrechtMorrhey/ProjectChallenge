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
    /// Interaction logic for ScoreLeerlingWindow.xaml
    /// </summary>
    public partial class ScoreLeerlingWindow : Window
    {
        string userId;

        public ScoreLeerlingWindow(string userId)
        {
            this.userId = userId;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/challenge scores";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);  //oppassen voor Directory Not Found Exception
            List<string> userFiles = new List<string>();
            string filename;

            // zoek alle scores met die userId
            foreach (string file in files)
            {
                filename = System.IO.Path.GetFileName(file);
                if (userId == filename.Split('_')[1])
                {
                    userFiles.Add(file);
                }
            }

            string score;
            string vraag;
            StreamReader inputStream = null;
            foreach (string file in userFiles)
            {
                try
                {
                    inputStream = File.OpenText(file);
                    filename = System.IO.Path.GetFileName(file);
                    vraag = filename.Split('_')[3];
                    inputStream.ReadLine(); //sla de eerste lijn over
                    score = inputStream.ReadLine().Split(':')[2];
                    scoresListBox.Items.Add(vraag + "\t" + score);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Bestand " + file + " niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Argument Exception bij inlezen bestand " + file , "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                finally
                {
                    if (inputStream != null)
                    {
                        inputStream.Close();
                    }
                }
            }
          
        }
    }
}
