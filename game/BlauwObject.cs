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
    // Author: Timo Biesmans
    public class BlauwObject : GameObject
    {
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush kleur;
        private Rectangle blauwObject;


        public BlauwObject(Canvas drawingCanvas)
            : base(drawingCanvas)
        {
            blauwObject = new Rectangle();
            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            kleur = ObjectKleur;
            blauwObject.Fill = kleur;
        }
        public override Shape objectShape
        {
            get { return blauwObject; }
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
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Fill = kleur;
        }
    }
}

