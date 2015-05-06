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

        public static void VeranderKleuren(GameObject a, GameObject b)
        {
            if (!(a.GetType() == b.GetType()) && a.Leven && b.Leven)
            {
                a.Leven = false;
                b.Leven = false;
            }
            else if ((a.GetType() == b.GetType()) && (a.Leven || b.Leven))
            {
                a.Leven = true;
                b.Leven = true;
            }
        }

        private void roodButton_Click(object sender, RoutedEventArgs e)
        {
            RoodObject roodObject;
            roodObject = new RoodObject(gameCanvas);
            while (roodObject.Overlapping(gameObjecten))
            {
                roodObject = new RoodObject(gameCanvas);
            }
            gameObjecten.Add(roodObject);
            roodObject.DisplayOn(gameCanvas);
        }

        private void blauwButton_Click(object sender, RoutedEventArgs e)
        {
            BlauwObject blauwObject;
            blauwObject = new BlauwObject(gameCanvas);
            while (blauwObject.Overlapping(gameObjecten))
            {
                blauwObject = new BlauwObject(gameCanvas);
            }
            gameObjecten.Add(blauwObject);
            blauwObject.DisplayOn(gameCanvas);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            List<GameObject> botsingLijst = new List<GameObject>();
            foreach (GameObject gameObject in gameObjecten) //copy by value
            {
                botsingLijst.Add(gameObject);
            }

            if (botsingLijst.Count > 1)
            {
                while (botsingLijst.Count > 0)
                {
                    GameObject botser;
                    botsingLijst[0].Move(botsingLijst, out botser);
                    if (botser != null)
                    {
                        botsingLijst.Remove(botser); // vermijd onnodig telwerk
                    }
                    botsingLijst.RemoveAt(0);
                }
            }
           
        }

        private void scoreButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new GameScore(gameObjecten);
            w.Show();
        }
      }
    }

