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
   
    // Author: Timo Biesmans
    public partial class Game : Window
    {
        private List<Sprite> sprites = new List<Sprite>();
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
            sprites.Add(roodObject);
            roodObject.DisplayOn(gameCanvas);
        }

        private void blauwButton_Click(object sender, RoutedEventArgs e)
        {
            BlauwObject blauwObject;
            blauwObject = new BlauwObject(gameCanvas);
            sprites.Add(blauwObject);
            blauwObject.DisplayOn(gameCanvas);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {

            foreach (Sprite sprite in sprites)
            {
                sprite.Move();
            }
        }
    }
}
