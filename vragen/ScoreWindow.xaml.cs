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
        private string bestandsNaam;

        public ScoreWindow(List<Vraag> vragenLijst, string bestandsNaam)
        {
            InitializeComponent();
            this.bestandsNaam = bestandsNaam;
            foreach (Vraag vraag in vragenLijst)
            {
                scoreListBox.Items.Add(vraag.IsJuist + "\t" + vraag.Antwoord+ "\t" + vraag.Ingevuld);
            }
            
            // sla de score van de student op
            int score = vragenLijst.Count;
            //StreamWriter outputStream = File.CreateText("NaamVoornaam"+bestandsNaam+"Score.txt");
            StreamWriter outputStream = File.CreateText("NaamVoornaam.txt");
            outputStream.WriteLine("Voornaam Naam");
            foreach (Vraag vraag in vragenLijst)
            {
                outputStream.WriteLine(vraag.IsJuist + "\t" + vraag.Antwoord + "\t" + vraag.Ingevuld);
                if (!vraag.IsJuist)
                {
                    score--;
                }
            }
            outputStream.WriteLine("Score: "+score+"/"+vragenLijst.Count+"\t Percentage: "+((double)score/vragenLijst.Count)*100+"%");
            outputStream.Close();
            
        }
    }
}
