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
        int scoreRood, scoreBlauw;
        public GameScore(List<GameObject> gameObjecten)
        {
            InitializeComponent();
            foreach (GameObject gameObject in gameObjecten)
            {
                if (gameObject.GetType() == typeOf(RoodObject))
                {

                }             
                    
                
            }
        }
    }
}
