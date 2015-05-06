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
        private RoodObject roodObject;
        private BlauwObject blauwObject;

        private List<RoodObject> bolletjes = new List<RoodObject>();
        private List<BlauwObject> vierkantjes = new List<BlauwObject>();
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
            roodObject = new RoodObject(gameCanvas);
            bolletjes.Add(roodObject);
            roodObject.DisplayOn(gameCanvas);
        }

        private void blauwButton_Click(object sender, RoutedEventArgs e)
        {
            blauwObject = new BlauwObject(gameCanvas);
            vierkantjes.Add(blauwObject);
            blauwObject.DisplayOn(gameCanvas);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            //if (roodObject != null)
            //{
            //    roodObject.Move();
            //}
            foreach (RoodObject bolletje in bolletjes)
            {
                bolletje.Move(); 
            }


            foreach (BlauwObject vierkantje in vierkantjes)
            {
                vierkantje.Move();
            }
        }
      }
    }

