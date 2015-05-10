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
    /// Interaction logic for GameScore.xaml
    /// </summary>
    /// 

    //  Code achter het score scherm van de game
    //  hierin wordt de score berekent en op
    //  het scherm getoond
    //
    //  Author: Brecht Morrhey

    public partial class GameScore : Window
    {
        //  Eigenschappen
        private int scoreRood, scoreBlauw;
        private MainVragenWindow menuWindow;

        //  Constructor
        public GameScore(List<GameObject> gameObjecten, MainVragenWindow menuWindow)
        {
            InitializeComponent();
            scoreRood = 0;
            scoreBlauw = 0;
            foreach (GameObject gameObject in gameObjecten)
            {
                if (gameObject.Leven && gameObject is RoodObject)
                {
                    scoreRood++;
                }
                else if (gameObject.Leven && gameObject is BlauwObject)
                {
                    scoreBlauw++;
                }
            }
            this.menuWindow = menuWindow;

            scoreBlauwTextBlock.Text = Convert.ToString(scoreBlauw);
            scoreRoodTextBlock.Text = Convert.ToString(scoreRood);
        }

        //  methoden

        //  Code die uitgevoerd word als 
        //  het scherm word gesloten
        //  Hierin wordt het menu scherm geopent
        //
        //  Author: Stijn Stas
        private void Window_Closed(object sender, EventArgs e)
        {
            menuWindow.Show();
        }

        //  Code achter de menu knop
        //  Scherm word gesloten
        //  en in het closed event
        //  wordt het menu opgeroepen
        //
        //  Author: Stijn Stas
        private void naarMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
