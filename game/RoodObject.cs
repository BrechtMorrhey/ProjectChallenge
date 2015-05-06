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
<<<<<<< HEAD
    // Author: Timo Biesmans
    public class RoodObject : Sprite
=======
    public class RoodObject : GameObject
>>>>>>> origin/stijn
    {
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Red);
        private SolidColorBrush kleur;
        private Ellipse roodObject;
        private int xStepSize, yStepSize;
        private Canvas canvas;
        private Random randomNumber = new Random();

        public RoodObject(Canvas drawingCanvas)
        {
            roodObject = new Ellipse();
            canvas = drawingCanvas;
            X = randomNumber.Next(0, 497);
            Y = randomNumber.Next(0, 248);
            Width = 10;
            Height = 10;
            xStepSize = 1;
            yStepSize = 1;

            roodObject.Width = Width;
            roodObject.Height = Height;
            roodObject.Margin = new Thickness(X, Y, 0, 0);
            kleur = ObjectKleur;
            roodObject.Fill = kleur;
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

<<<<<<< HEAD
        public override void Move()
        {

            if ((X > 480) || (X < 0))
            {
                xStepSize = -xStepSize;
            }
            if ((Y > 240) || (Y < 0))
            {
                yStepSize = -yStepSize;
            }
            X += xStepSize;
            Y += yStepSize;
        }
   
        protected override void UpdateElement()
=======
        public override void DisplayOn(Canvas drawingCanvas)
        {
            canvas.Children.Add(roodObject);
        }
        
            
        public override void UpdateElement()
>>>>>>> origin/stijn
        {
            roodObject.Margin = new Thickness(X, Y, 0, 0);
            roodObject.Width = Width;
            roodObject.Height = Height;
            roodObject.Fill = kleur;
        }
    }
}
