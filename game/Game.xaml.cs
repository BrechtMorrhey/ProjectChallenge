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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjectChallenge
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// versie 3
    /// </summary>
    public partial class Game : Window
    {
        private List<GameObject> gameObjecten = new List<GameObject>();
        private DispatcherTimer animationTimer;

        public Game()
        {
            InitializeComponent();
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(4);
            animationTimer.Tick += animationTimer_Tick;
            animationTimer.IsEnabled = true;
        }

        private void roodButton_Click(object sender, RoutedEventArgs e)
        {
            RoodObject roodObject;
            roodObject = new RoodObject(gameCanvas);
            gameObjecten.Add(roodObject);
            roodObject.DisplayOn(gameCanvas);
        }

        private void blauwButton_Click(object sender, RoutedEventArgs e)
        {
            BlauwObject blauwObject;
            blauwObject = new BlauwObject(gameCanvas);
            gameObjecten.Add(blauwObject);
            blauwObject.DisplayOn(gameCanvas);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            List<GameObject> botsingLijst = new List<GameObject>();
            foreach (GameObject gameObject in gameObjecten)
            {
                gameObject.Move();                
                botsingLijst.Add(gameObject);
            }
                        
            while(botsingLijst.Count>0)
            {
                botsingLijst[0].DetecteerBotsing(botsingLijst);
                //botsingLijst.Remove(botsingLijst[0]);    dit wordt in DetecteerBotsing gedaan            
            }
           
        }
      }
    }

