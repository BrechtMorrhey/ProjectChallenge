using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectChallenge
{
    public abstract class Sprite
    {
        int x, y, width, height;

        public int X
        {
            get { return x; }
            set { x = value; UpdateElement(); }
        }
        
        public int Y
        {
            get { return y; }
            set { y = value; UpdateElement(); }
        }

        public int Width
        {
            get { return width; }
            set { width = value; UpdateElement(); }
        }
        public int Height
        {
            get { return height; }
            set { height = value; UpdateElement(); }
        }

        public abstract void DisplayOn(Canvas drawingCanvas);
        protected abstract void UpdateElement();

    }
}
