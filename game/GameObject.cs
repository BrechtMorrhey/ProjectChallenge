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

        
        public void DetecteerBotsing(List<GameObject> botsingObjecten)
        {
            botsingObjecten.Remove(this);  //doe het object zelf weg uit de lijst
            foreach (GameObject botsingObject in botsingObjecten)
            {
                // http://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
                //A is gameobject en B is botsingObject
                int linkerrandA = this.X;
                int rechterrandA = this.X + this.Width;
                int linkerrandB = botsingObject.X;
                int rechterrandB = botsingObject.X + botsingObject.Width;
                bool horizontaleOverlap = (rechterrandA >= linkerrandB && linkerrandA <= rechterrandB);

                int onderrandA = this.Y;
                int bovenrandA = this.Y + this.Height;
                int onderrandB = botsingObject.Y;
                int bovenrandB = botsingObject.Y + this.Height;
                bool vertikaleOverlap = (bovenrandA >= onderrandB && onderrandA <= bovenrandB);

                if (horizontaleOverlap && vertikaleOverlap)
                {
                    //botsing
                    //laat we de andere kant uitbewegen
                    this.xStepSize = -this.xStepSize;
                    this.yStepSize = -this.yStepSize;
                    botsingObject.xStepSize = -botsingObject.xStepSize;
                    botsingObject.yStepSize = -botsingObject.yStepSize;

                    //verander kleur
                    
                    //this.Leven = !(this.GetType() == botsingObject.GetType());
                    //botsingObject.Leven = !(this.GetType() == botsingObject.GetType());
                    
                    if (!(this.GetType() == botsingObject.GetType()) && this.Leven && botsingObject.Leven)
                    {
                        this.Leven = false;
                        botsingObject.Leven = false;
                    }
                    else if ((this.GetType() == botsingObject.GetType()) && (this.Leven || botsingObject.Leven))
                    {
                        this.Leven = true;
                        botsingObject.Leven = true;
                    }
                }
            }
        }


        public void DisplayOn(Canvas drawingCanvas)
        {
            drawingCanvas.Children.Add(this.objectShape);
        }        
        public abstract void UpdateElement();
        

    }
}
