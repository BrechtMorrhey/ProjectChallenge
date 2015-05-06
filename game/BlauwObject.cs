using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectChallenge
{
    public class BlauwObject : GameObject
    {
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush kleur;
        private Rectangle blauwObject;        
        private Canvas canvas;
        private Random randomNumber = new Random();

        public BlauwObject(Canvas drawingCanvas) :base()
        {
            blauwObject = new Rectangle();
            canvas = drawingCanvas;
            X = randomNumber.Next(0, 497);
            Y = randomNumber.Next(0, 248);
            Width = 10;
            Height = 10;            

            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            kleur = ObjectKleur;
            blauwObject.Fill = kleur;
        }

        public override SolidColorBrush Kleur
        {
            get { return kleur; }
            set { kleur = value; }
        }
        public override SolidColorBrush ObjectKleur
        {
            get { return objectKleur; }
        }

        public override void DisplayOn(Canvas drawingCanvas)
        {
            canvas.Children.Add(blauwObject);
        }

        public override void UpdateElement()
        {
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Fill = kleur;
        }
    }
}
