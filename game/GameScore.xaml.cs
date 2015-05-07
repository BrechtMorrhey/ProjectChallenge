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
    public partial class GameScore : Window
    {
        private int scoreRood, scoreBlauw;
        private MainVragenWindow menuWindow;
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

        private void Window_Closed(object sender, EventArgs e)
        {
            menuWindow.Show();
        }

        private void naarMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
