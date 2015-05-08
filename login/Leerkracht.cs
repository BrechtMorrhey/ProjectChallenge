using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    public class Leerkracht : Persoon
    {
        //  eigenschappen
        private AlleGebruikers alleGebruikers;
        

        //  constructor
        public Leerkracht(string naam, string voornaam, /* datetime maken */ string geboorteDatum, string passwoord, AlleGebruikers allegebruikers)
            : base(naam, voornaam, passwoord)
        {
            string[] geboorteDatumSplitted = geboorteDatum.Split('/');
            if (geboorteDatumSplitted[0].Length == 1)
            {
                geboorteDatum = "0" + geboorteDatum;
            }
            base.geboorteDatum = geboorteDatum;
            string idCijfer = Convert.ToString(allegebruikers.Leerkrachten.Count() + 1);
            base.id = "l" + naam.Substring(0, 1) + voornaam.Substring(0, 1) + idCijfer.PadLeft(3,'0');
            this.alleGebruikers = allegebruikers;
        }

        //  methoden
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", base.id, base.paswoord, base.naam, base.voornaam, base.geboorteDatum);
        }

        public void SlaOp()
        {
            alleGebruikers.SlaLeerkrachtOp(this);
        }

        public override string GeefGebruikersType()
        {
            return "leerkracht";
        }
    }
}
