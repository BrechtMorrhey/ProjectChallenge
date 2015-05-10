using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectChallenge
{
    //  interface die de klassen
    //  die deze interface implementeren
    //  verplicht om de volgende
    //  Methodes aan te maken
    //
    //  Author: Brecht Morrhey
    interface iBeweegbaar
    {
        //  Methoden
        void Move();
        void UpdateElement();
        void DisplayOn(Canvas drawingCanvas);
    }
}
