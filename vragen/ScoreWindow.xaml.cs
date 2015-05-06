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
            String userId, voorNaam, achterNaam, klas;
            userId = "userId";
            voorNaam="voorNaam";
            achterNaam="achterNaam";
            klas="klas";
            
            InitializeComponent();
            this.bestandsNaam = bestandsNaam;
            int score = vragenLijst.Count;  //bereken maximum score
            foreach (Vraag vraag in vragenLijst)
            {
                scoreListBox.Items.Add(vraag.IsJuist + "\t" + vraag.Antwoord+ "\t" + vraag.Ingevuld);
                if (!vraag.IsJuist)
                {
                    score--;
                }
            }           
            scoreListBox.Items.Add("Score: " + score + "/" + vragenLijst.Count + "\t Percentage: " + ((double)score / vragenLijst.Count) * 100 + "%");
            naamTextBlock.Text=voorNaam+" "+achterNaam;
            klasTextBlock.Text=klas;


            // sla de score van de student op

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/challenge scores/";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter outputStream = File.CreateText(path + klas + "_" + userId + "_Score_" + System.IO.Path.GetFileName(bestandsNaam)); //oppassen voor Directory Not Found Exception
            //StreamWriter outputStream = File.CreateText("NaamVoornaam.txt");
            outputStream.WriteLine(voorNaam+", "+achterNaam);
            // zet de score vanboven om makkelijker te kunnen inlezen
            outputStream.WriteLine("Score: "+score+"/"+vragenLijst.Count+"\t Percentage: "+((double)score/vragenLijst.Count)*100+"%");
            // sla de specifieke antwoorden op
            foreach (Vraag vraag in vragenLijst)
            {
                outputStream.WriteLine(vraag.IsJuist + "," + vraag.Antwoord + "," + vraag.Ingevuld);
            }
            outputStream.Close();
            
        }
    }
}
