using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    class wiskundigeVraag : Vraag
    {
        public override VraagType TypeVraag { get { return VraagType.wiskunde; } }
        private string opgave, antwoord, ingevuld, bewerking;
        //private bool isIngevuld, isJuist;
        private double getal1, getal2;
        
        // constructors
        public wiskundigeVraag()
        {
            GenereerOpgave();
            BerekenAntwoord();
        }

        public wiskundigeVraag(int min, int max, string bewerking)
        {
            this.bewerking = bewerking;
            GenereerOpgave(min, max);
            BerekenAntwoord();
        }

        public wiskundigeVraag(double getal1, double getal2, string bewerking)
        {
            this.getal1 = getal1;
            this.getal2 = getal2;
            this.bewerking = bewerking;
            this.opgave = "" + getal1 + " " + bewerking + " " + getal2;
            BerekenAntwoord();
        }


        //Genereeropgave op 3 manieren
        

        public void GenereerOpgave()
        {
            Random random = new Random();
            int index = random.Next(0, 4);
            BewaarBewerking(index);
            getal1 = random.Next(0, 20);
            // nooit delen door 0
            if (bewerking == "/")
            {
                getal2 = random.Next(1, 20);
            }
            else
            {
                getal2 = random.Next(0, 20);
            }
            

            string[] opgave = new string[4];
            opgave[0] = "" + getal1 + " + " + getal2;
            opgave[1] = "" + getal1 + " - " + getal2;
            opgave[2] = "" + getal1 + " * " + getal2;
            opgave[3] = "" + getal1 + " / " + getal2;

            this.opgave = opgave[index];

        }
        // opslaan van de soort bewerking
        public void BewaarBewerking(int index)
        {
            switch (index)
            {
                case 0: this.bewerking = "+";
                    break;
                case 1: this.bewerking = "-";
                    break;
                case 2: this.bewerking = "*";
                    break;
                case 3: this.bewerking = "/";
                    break;
            }

        }

        // bereken antwoord
        public void BerekenAntwoord()
        {
            if (bewerking == "+")
            {
                antwoord = Convert.ToString(getal1 + getal2);
            }
            else if (bewerking == "-")
            {
                antwoord = Convert.ToString(getal1 - getal2);
            }
            else if (bewerking == "*")
            {
                antwoord = Convert.ToString(getal1 * getal2);
            }
            else if (bewerking == "/")
            {
                antwoord = Convert.ToString(((int)((getal1 / getal2) * 1000 + 0.5) / 1000.0));
            }

        }


        public void GenereerOpgave(int min, int max)
        {
            Random random = new Random();
            getal1 = random.Next(min, max+1);

            // nooit delen door 0
            if (bewerking == "/")
            {
                getal2 = random.Next(min, max+1);
                while (getal2 == 0)
                {
                    getal2 = random.Next(min, max+1);
                }
            }
            else
            {
                getal2 = random.Next(min, max+1);
            }
            

            int index = BerekenIndex(bewerking);
            BewaarBewerking(index);

            string[] opgave = new string[4];
            opgave[0] = "" + getal1 + " + " + getal2;
            opgave[1] = "" + getal1 + " - " + getal2;
            opgave[2] = "" + getal1 + " * " + getal2;
            opgave[3] = "" + getal1 + " / " + getal2;

            this.opgave = opgave[index];

        }

        // index berekenen indien de bewerking door de leerkracht word opgegeven
        public int BerekenIndex(string bewerking)
        {
            int index = -1;
            switch (bewerking)
            {
                case "+": 
                    index = 0;
                    break;
                case "-":
                    index = 1;
                    break;
                case "*": 
                    index = 2;
                    break;
                case "/": 
                    index = 3;
                    break;
                default:
                    Random random = new Random(); // bij foute ingave van de bewerking word er een random bewerking gekozen
                    index = random.Next(0, 4);
                    break;
            }
            return index;
        }


        public override string ToString()
        {
            return "wiskunde," + getal1 + "," + getal2 + "," + bewerking;
        } 


        //properties
        public override string Opgave
        {
            get
            {
                return opgave;
            }
            set
            {
                opgave = value;

            }
        }
        public override string Antwoord
        {
            get
            {
                return antwoord;
            }
            set
            {
                antwoord = value;

            }
        }
        public override string Ingevuld
        {
            get
            {
                return ingevuld;
            }
            set
            {
                ingevuld = value;

            }
        }
        public override bool IsIngevuld
        {
            get
            {
                return ingevuld != null;
            }
        }
        public override bool IsJuist
        {
            get
            {
                return antwoord == ingevuld;
            }
        }

    }
}
