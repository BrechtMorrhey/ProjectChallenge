using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    //  Leerkracht klasse die alle gegevens
    //  van een leerkracht bezit en die gebruik
    //  maakt van de klasse Persoon
    //
    //  Author: Stijn Stas

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

        //  ToString() geeft de gegevens van de leerkracht
        //  terug in string formaat
        //
        //  Author: Stijn Stas

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", base.id, base.paswoord, base.naam, base.voornaam, base.geboorteDatum);
        }

        //  SlaOp maakt gebruik van 
        //  de alleGebruikers class
        //  om leerkrachten op te slaan
        //
        //  Author: Stijn Stas

        public void SlaOp()
        {
            alleGebruikers.SlaLeerkrachtOp(this);
        }

        //  Geef het type gebruiker terug
        //
        //  Author: Stijn Stas

        public override string GeefGebruikersType()
        {
            return "leerkracht";
        }
    }
}
