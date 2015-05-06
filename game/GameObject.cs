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
                int bovenrandB = item.Y + this.Height;
                bool vertikaleOverlap = (bovenrandA >= onderrandB && onderrandA <= bovenrandB);

                overlap = (horizontaleOverlap && vertikaleOverlap)|| overlap;               
            }
            return overlap;
        }
        
        public void DetecteerBotsing(List<GameObject> botsingObjecten)
        {
            botsingObjecten.Remove(this);  //doe het object zelf weg uit de lijst

            List<GameObject> botsingLokaleLijst = new List<GameObject>();
            foreach (GameObject item in botsingObjecten) //copy by value
            {
                botsingLokaleLijst.Add(item);
            }

            bool botsing = false;
            while(botsingLokaleLijst.Count > 0  && !botsing) // stop de loop na een botsing zodat het object zelf niet kan blijven vasthangen
            {
                // http://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
                //A is gameobject en B is botsingObject
                int linkerrandA = this.X;
                int rechterrandA = this.X + this.Width;
                int linkerrandB = botsingLokaleLijst[0].X;
                int rechterrandB = botsingLokaleLijst[0].X + botsingLokaleLijst[0].Width;
                bool horizontaleOverlap = (rechterrandA >= linkerrandB && linkerrandA <= rechterrandB);

                int onderrandA = this.Y;
                int bovenrandA = this.Y + this.Height;
                int onderrandB = botsingLokaleLijst[0].Y;
                int bovenrandB = botsingLokaleLijst[0].Y + this.Height;
                bool vertikaleOverlap = (bovenrandA >= onderrandB && onderrandA <= bovenrandB);
                botsing = horizontaleOverlap && vertikaleOverlap;

                if (botsing)
                {
                    //botsing
                    //laat we de andere kant uitbewegen
                    this.xStepSize = -this.xStepSize;
                    this.yStepSize = -this.yStepSize;
                    botsingLokaleLijst[0].xStepSize = -botsingLokaleLijst[0].xStepSize;
                    botsingLokaleLijst[0].yStepSize = -botsingLokaleLijst[0].yStepSize;

                    //verander kleur

                    //this.Leven = !(this.GetType() == botsingObject.GetType());
                    //botsingObject.Leven = !(this.GetType() == botsingObject.GetType());

                    if (!(this.GetType() == botsingLokaleLijst[0].GetType()) && this.Leven && botsingLokaleLijst[0].Leven)
                    {
                        this.Leven = false;
                        botsingLokaleLijst[0].Leven = false;
                    }
                    else if ((this.GetType() == botsingLokaleLijst[0].GetType()) && (this.Leven || botsingLokaleLijst[0].Leven))
                    {
                        this.Leven = true;
                        botsingLokaleLijst[0].Leven = true;
                    }

                    botsingObjecten.Remove(botsingLokaleLijst[0]); //vermijd dat bij driedubbele botsing de twee laatste bollen in elkaar blijven hangen
                }
                botsingLokaleLijst.Remove(botsingLokaleLijst[0]); 
            }
        }


        public void DisplayOn(Canvas drawingCanvas)
        {
            drawingCanvas.Children.Add(this.objectShape);
        }        
        public abstract void UpdateElement();
        

    }
}
