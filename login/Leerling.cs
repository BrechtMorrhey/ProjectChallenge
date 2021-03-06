﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    //  Leerling klasse die alle gegevens
    //  van een leerling bezit en die gebruik
    //  maakt van de klasse Persoon
    //
    //  Author: Stijn Stas

    public class Leerling : Persoon
    {
        //  eigenschappen
        private AlleGebruikers alleGebruikers;
        private string klas;

        //  constructor
        public Leerling(string naam, string voornaam, string geboorteDatum, string passwoord, string klas, AlleGebruikers allegebruikers)
            :base(naam, voornaam, passwoord)
        {
            string[] geboorteDatumSplitted = geboorteDatum.Split('/');
            if (geboorteDatumSplitted[0].Length == 1)
            {
                geboorteDatum = "0" + geboorteDatum;
            }
            base.geboorteDatum = geboorteDatum;
            string idCijfer = Convert.ToString(allegebruikers.Leerlingen.Count() + 1);
            base.id = "s" + naam.Substring(0, 1) + voornaam.Substring(0, 1) + idCijfer.PadLeft(3,'0');
            this.alleGebruikers = allegebruikers;
            this.klas = klas;
        }

        //  methoden

        //  ToString() geeft de gegevens van de leerling
        //  terug in string formaat
        //
        //  Author: Stijn Stas

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", this.klas, base.id, base.paswoord, base.naam, base.voornaam, base.geboorteDatum);
        }

        //  SlaOp maakt gebruik van 
        //  de alleGebruikers class
        //  om leerlingen op te slaan
        //
        //  Author: Stijn Stas

        public void SlaOp()
        {
            alleGebruikers.SlaStudentOp(this);  
        }

        //  Geef het type gebruiker terug
        //
        //  Author: Stijn Stas

        public override string GeefGebruikersType()
        {
            return "leerling";
        }

        //  properties

        public string Klas
        {
            get
            {
                return klas;
            }
        }
    }
}
