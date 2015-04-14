using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vragen
{
    class MeerkeuzeVraag : Vraag
    {
        private string opgave, antwoord; // lijst antwoorden + index juiste antwoord
        private string ingevuld;
        private bool isIngevuld, isJuist;

         // constructor

        public MeerkeuzeVraag(string opgave, string antwoord)
        {
            this.opgave = opgave;
            this.antwoord = antwoord;
           
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
