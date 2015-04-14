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
    public class BlauwObject : Sprite
    {
        private Rectangle blauwObject;
        private int xStepSize, yStepSize;
        private Canvas canvas;
        private Random randomNumber = new Random();

        public BlauwObject(Canvas drawingCanvas)
        {
            blauwObject = new Rectangle();
            canvas = drawingCanvas;
            X = randomNumber.Next(0, 497);
            Y = randomNumber.Next(0, 248);
            Width = 10;
            Height = 10;
            xStepSize = 1;
            yStepSize = 1;

            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            blauwObject.Fill = new SolidColorBrush(Colors.Blue);
        }

        public override void DisplayOn(Canvas drawingCanvas)
        {
            canvas.Children.Add(blauwObject);
        }

        public void Move()
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
        {
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Fill = new SolidColorBrush(Colors.Blue);
        }
    }
}
