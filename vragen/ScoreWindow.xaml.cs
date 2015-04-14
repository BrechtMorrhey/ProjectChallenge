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
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        

        public ScoreWindow(List<Vraag> vragenLijst)
        {
            InitializeComponent();
            foreach (Vraag vraag in vragenLijst)
            {
                scoreListBox.Items.Add(vraag.IsJuist + "\t" + vraag.Antwoord+ "\t" + vraag.Ingevuld);
            }
            
            
        }
    }
}
