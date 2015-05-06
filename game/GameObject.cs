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
    public abstract class GameObject :iBeweegbaar
    {
        private static SolidColorBrush doodKleur = new SolidColorBrush(Colors.Black);
        private bool leven;
        private int x, y, width, height;
        private int xStepSize, yStepSize;
        private Canvas canvas;        

        public GameObject(Canvas drawingCanvas)
        {
            Random randomNumber = new Random();
            canvas = drawingCanvas;
            width = 10;
            height = 10;
            xStepSize = 1;
            yStepSize = 1;
            leven = true;            
            x = randomNumber.Next(1, (int)(canvas.Width-width));
            y = randomNumber.Next(1, (int)(canvas.Height-height));            
        }

        public abstract Shape objectShape { get; }
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
        abstract public SolidColorBrush Kleur
        {
            get;
            set;
        }
        abstract public SolidColorBrush ObjectKleur
        {
            get;
        }
        public bool Leven
        {
            get{return leven;}
            set {
                if (value)
                {
                    this.Kleur = this.ObjectKleur;
                }
                else
                {
                    this.Kleur = GameObject.doodKleur;
                }
                leven = value; }
        }
        //methods
        public void Move()
        {

            if ((X > (int)(canvas.Width - width)) || (X < 1))
            {
                xStepSize = -xStepSize;
            }
            if ((Y > (int)(canvas.Height - height)) || (Y < 1))
            {
                yStepSize = -yStepSize;
            }
            X += xStepSize;
            Y += yStepSize;
        }
        public void Move(ref List<GameObject> gameObjecten, out GameObject botser)
        {
            gameObjecten.Remove(this);  // zodat dit object niet met zichzelf wordt vergeleken
            this.Move();
            bool overlap;            
            this.Overlapping(gameObjecten, out overlap, out botser);
            if (overlap)//normale botsing
            {
                Game.VeranderKleuren(this, botser);
                xStepSize = -xStepSize;
                yStepSize = -yStepSize;
                this.Move();
                botser.xStepSize = -botser.xStepSize;
                botser.yStepSize = -botser.yStepSize;
                botser.Move();
            }
            int i = 0;
            while (this.Overlapping(gameObjecten) && i<100) //objecten geraken niet uit mekaars oppervlakte of drieweg botsing
            {
                this.Move();
                i++;
            }            
        }

        public bool Overlapping(List<GameObject> gameObjecten)
        {
            bool overlap = false;
            foreach (GameObject item in gameObjecten)
            {
                int linkerrandA = this.X;
                int rechterrandA = this.X + this.Width;
                int linkerrandB = item.X;
                int rechterrandB = item.X + item.Width;
                bool horizontaleOverlap = (rechterrandA >= linkerrandB && linkerrandA <= rechterrandB);

                int onderrandA = this.Y;
                int bovenrandA = this.Y + this.Height;
                int onderrandB = item.Y;
                int bovenrandB = item.Y + item.Height;
                bool vertikaleOverlap = (bovenrandA >= onderrandB && onderrandA <= bovenrandB);

                overlap = (horizontaleOverlap && vertikaleOverlap)|| overlap;   //verander overlap naar true, kan niet terug naar false veranderen                
            }
            return overlap;
        }
        public void Overlapping(List<GameObject> gameObjecten, out bool overlap, out GameObject botser)
        {
            overlap = false;
            botser = null;
            int i = 0;
            while (!overlap && i < gameObjecten.Count)
            {
                int linkerrandA = this.X;
                int rechterrandA = this.X + this.Width;
                int linkerrandB = gameObjecten[i].X;
                int rechterrandB = gameObjecten[i].X + gameObjecten[i].Width;
                bool horizontaleOverlap = (rechterrandA >= linkerrandB && linkerrandA <= rechterrandB);

                int onderrandA = this.Y;
                int bovenrandA = this.Y + this.Height;
                int onderrandB = gameObjecten[i].Y;
                int bovenrandB = gameObjecten[i].Y + gameObjecten[i].Height;
                bool vertikaleOverlap = (bovenrandA >= onderrandB && onderrandA <= bovenrandB);

                overlap = (horizontaleOverlap && vertikaleOverlap) || overlap;   //verander overlap naar true, kan niet terug naar false veranderen
                i++;
            }
            if (overlap)
            {
                botser = gameObjecten[i - 1];
            }
        }
        public void DisplayOn(Canvas drawingCanvas)
        {
            drawingCanvas.Children.Add(this.objectShape);
        }        
        public abstract void UpdateElement();
    }
}
