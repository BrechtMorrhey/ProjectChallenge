using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author: Brecht Morrhey
namespace ProjectChallenge
{
    class MeerkeuzeVraag : Vraag
    {
        private string opgave;       
        private string ingevuld; // ingevulde antwoord
        private List<string> antwoordenLijst;

         // constructor

        public MeerkeuzeVraag(string opgave, List<string> antwoordenLijst)
        {
            this.opgave = opgave;            
            this.antwoordenLijst = antwoordenLijst;
        }
        //properties
        public override VraagType TypeVraag { get { return VraagType.meerkeuze; } }

        public override string ToString()
        {
            string s = "meerkeuze," + opgave + ",";
            foreach (string antwoord in antwoordenLijst)
            {
                s += antwoord +"|";
            }
            return s;
        }

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
                return antwoordenLijst[0];
            }
            set
            {
                antwoordenLijst[0] = value;
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
                return (ingevuld != null && ingevuld !="");
            }
        }
        public override bool IsJuist
        {
            get
            {
                return antwoordenLijst[0] == ingevuld;
                // We vergelijken niet de ingevulde index met de antwoordindex maar de geselecteerde string, zo kan de volgorde van de antwoorden willekeurig gemaakt worden
            }
        }
        public List<string> AntwoordenLijst { get { return antwoordenLijst; } set { antwoordenLijst = value; } }

    }
}
