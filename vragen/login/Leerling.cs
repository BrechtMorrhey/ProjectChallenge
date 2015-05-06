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

        //  constructor
        public Leerling(string naam, string voornaam, /* datetime maken */ string geboorteDatum, string passwoord, AlleGebruikers allegebruikers)
            :base(naam, voornaam, geboorteDatum, passwoord)
        {
            base.id = naam.Substring(0, 1) + voornaam.Substring(0, 1) + geboorteDatum.Substring(0,2) + geboorteDatum.Substring(3,2);
            this.alleGebruikers = allegebruikers;
        }

        //  methoden
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", base.id, base.paswoord, base.naam, base.voornaam, base.geboorteDatum);
        }

        public void SlaOp()
        {
            alleGebruikers.SlaStudentOp(this);  
        }
        
    }
}
