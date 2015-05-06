using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    public class Leerling : Persoon
    {
        //  eigenschappen
        private AlleGebruikers alleGebruikers;
        private string klas;

        //  constructor
        public Leerling(string naam, string voornaam, string geboorteDatum, string passwoord, string klas, AlleGebruikers allegebruikers)
            :base(naam, voornaam, geboorteDatum, passwoord)
        {
            string[] geboorteDatumSplitted = geboorteDatum.Split('/');
            if (geboorteDatumSplitted[0].Length == 1)
            {
                geboorteDatumSplitted[0].PadLeft(1, '0');
            }
            base.id = naam.Substring(0, 1) + voornaam.Substring(0, 1) + geboorteDatumSplitted[0] + geboorteDatumSplitted[1];
            this.alleGebruikers = allegebruikers;
            this.klas = klas;
        }

        //  methoden
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", this.klas, base.id, base.paswoord, base.naam, base.voornaam, base.geboorteDatum);
        }

        public void SlaOp()
        {
            alleGebruikers.SlaStudentOp(this);  
        }


        public override string GeefGebruikersType()
        {
            return "leerling";
        }
    }
}
