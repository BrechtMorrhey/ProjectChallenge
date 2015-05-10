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
    //  De code achter de blauwe vierkantjes 
    //  die je in de game kunt maken
    //
    // Author: Timo Biesmans
    public class BlauwObject : GameObject
    {
        //  Eigenschappen
        private static SolidColorBrush objectKleur = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush kleur;
        private Rectangle blauwObject;

        //  Constructor
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

        //  Properties
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

        //  Methoden

        //  UpdateElement() word gebruikt
        //  om de nieuwe locatie, hoogte, breedte
        //  en kleur van het object mee te 
        //  geven
        //
        //  Author: Timo Biesmans

        public override void UpdateElement()
        {
            blauwObject.Margin = new Thickness(X, Y, 0, 0);
            blauwObject.Width = Width;
            blauwObject.Height = Height;
            blauwObject.Fill = kleur;
        }
    }
}

