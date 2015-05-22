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
    //  De code achter de rode bolletjes
    //  die je in de game kunt maken
    //
    // Author: Timo Biesmans

    public class RoodObject : GameObject
    {
        //  Eigenschappen
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Red);
        private SolidColorBrush kleur;
        private Ellipse roodObject;
        
        //  Constructor
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

        //  Properties
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

        //  Methoden

        //  UpdateElement() word gebruikt
        //  om de nieuwe locatie, hoogte, breedte
        //  en kleur van het object mee te 
        //  geven
        //
        //  Author: Timo Biesmans
        public override void UpdateElement()
        {
            roodObject.Margin = new Thickness(X, Y, 0, 0);
            roodObject.Width = Width;
            roodObject.Height = Height;
            roodObject.Fill = kleur;
        }
    }
}
