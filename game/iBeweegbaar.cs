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
    interface iBeweegbaar
    {
        void Move();
        void UpdateElement();
        void DisplayOn(Canvas drawingCanvas);
    }
}
