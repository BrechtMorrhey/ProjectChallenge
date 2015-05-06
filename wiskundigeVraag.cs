using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vragen
{
    class wiskundigeVraag : Vraag
    {

        private string opgave, antwoord, ingevuld, bewerking;
        //private bool isIngevuld, isJuist;
        private int getal1, getal2;
        
        // constructors
        public wiskundigeVraag()
        {
            GenereerOpgave(); 
        }

        public wiskundigeVraag(int min, int max)
        {
            GenereerOpgave(min, max);
        }

        public wiskundigeVraag(int min, int max, string bewerking)
        {
            GenereerOpgave(min, max, bewerking);
        }


        //Genereeropgave op 3 manieren
        

        public void GenereerOpgave()
        {
            Random random = new Random();
            getal1 = random.Next(0, 20);
            getal2 = random.Next(0, 20);
            int index = random.Next(0, 3);
            BewaarBewerking(index);


            string[] opgave = new string[3];
            opgave[0] = "" + getal1 + " + " + getal2;
            opgave[1] = "" + getal1 + " - " + getal2;
            opgave[2] = "" + getal1 + " * " + getal2;
            opgave[3] = "" + getal1 + " / " + getal2;

            this.opgave = opgave[index];

        }

        public void GenereerOpgave(int min, int max)
        {
            Random random = new Random();
            getal1 = random.Next(min, max);
            getal2 = random.Next(min, max);
            int index = random.Next(0, 3);
            BewaarBewerking(index);


            string[] opgave = new string[3];
            opgave[0] = "" + getal1 + " + " + getal2;
            opgave[1] = "" + getal1 + " - " + getal2;
            opgave[2] = "" + getal1 + " * " + getal2;
            opgave[3] = "" + getal1 + " / " + getal2;

            this.opgave = opgave[index];

        }

        public void GenereerOpgave(int min, int max, string bewerking)
        {
            Random random = new Random();
            getal1 = random.Next(min, max);
            getal2 = random.Next(min, max);

            int index = BerekenIndex(bewerking);
            BewaarBewerking(index);


            string[] opgave = new string[3];
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

        // index berekenen indien de bewerking door de leerkracht word opgegeven
        public int BerekenIndex(string bewerking) 
        {
            int index = -1;
            switch(bewerking)
            {
                case "+": index = 0;
                    break;
                case "-": index = 1;
                    break;
                case "*": index = 2;
                    break;
                case "/": index = 3;
                    break;
            }               
            return index;
        }

        // bereken antwoord
        public void BerekenAntwoord(int getal1 , int getal2, string bewerking)
        {
           

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
