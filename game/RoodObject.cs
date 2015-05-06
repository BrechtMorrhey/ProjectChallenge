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
    public class RoodObject : GameObject
    {
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Red);
        private SolidColorBrush kleur;
        private Ellipse roodObject;
        
        
        public RoodObject(Canvas drawingCanvas)
            : base(drawingCanvas)
        {
            roodObject = new Ellipse();
            roodObject.Width = Width;
            roodObject.Height = Height;
            roodObject.Margin = new Thickness(X, Y, 0, 0);
            kleur = ObjectKleur;
            roodObject.Fill = kleur;
        }
        public override Shape objectShape
        {
            get { return roodObject; }
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

        public override void UpdateElement()
        {
            roodObject.Margin = new Thickness(X, Y, 0, 0);
            roodObject.Width = Width;
            roodObject.Height = Height;
            roodObject.Fill = kleur;
        }
    }
}
