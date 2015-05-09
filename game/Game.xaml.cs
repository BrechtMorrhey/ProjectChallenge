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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    // Author: Timo Biesmans, Brecht Morrhey
    public partial class Game : Window
    {
        private List<GameObject> gameObjecten = new List<GameObject>();
        private DispatcherTimer animationTimer, klok;
        private MainVragenWindow menuWindow;
        private int secondsLeft;

        public Game(MainVragenWindow menuWindow)
        {
            InitializeComponent();
            this.menuWindow = menuWindow;
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(4);
            animationTimer.Tick += animationTimer_Tick;
            animationTimer.IsEnabled = true;
        }
        public Game(MainVragenWindow menuWindow, int minuten):this(menuWindow)
        {

            klok = new DispatcherTimer();
            klok.Interval = TimeSpan.FromSeconds(1);
            klok.Tick += klok_Tick;
            klok.Start();            

            secondsLeft = minuten * 60;

            klokTextBox.Text = "tijd:\n" + secondsLeft + " sec";
        }

        void klok_Tick(object sender, EventArgs e)
        {
            secondsLeft -= 1;
            klokTextBox.Text = "tijd:\n" + secondsLeft + " sec";

            if (secondsLeft <= 0)
            {
                this.Close();
                MessageBox.Show("Tijd is om! Verdien meer tijd door beter te scoren");
                Window w = new GameScore(gameObjecten, menuWindow);
                w.Show();
            }
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

            while (botsingLijst.Count > 1)
                {
                    GameObject botser;
                    botsingLijst[0].Move(ref botsingLijst, out botser);
                    if (botser != null)
                    {
                        botsingLijst.Remove(botser); // vermijd onnodig telwerk
                    }
                    //botsingLijst.RemoveAt(0); // gebeurt in .Move()
                }
            if (botsingLijst.Count == 1)
            {
                botsingLijst[0].Move(); //beweeg het eventueel overblijvend object
            }

        }

        private void scoreButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new GameScore(gameObjecten, menuWindow);
            w.Show();
            this.Close();
        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            animationTimer.Stop();  // https://social.msdn.microsoft.com/Forums/en-US/992b4aa3-f066-4ccf-8c14-aec871eccdb6/how-to-properly-close-dispose-a-wpf-window?forum=wpf
            if (klok != null)
            {
                klok.Stop();
            }
        }
    }
}

