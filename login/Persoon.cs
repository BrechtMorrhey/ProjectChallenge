using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    public abstract class Persoon
    {
        //  eigenschappen
        protected string id;
        protected string paswoord;
        protected string naam;
        protected string voornaam;
        protected string geboorteDatum;

        //  constructor
        public Persoon(string naam, string voornaam,  string paswoord)
        {
            this.naam = naam;
            this.voornaam = voornaam;
            this.paswoord = paswoord;
        }

        //  methoden 
        public abstract string GeefGebruikersType();
        

        //  properties
        public string Naam
        {
            get 
            { 
                return naam; 
            }
        }

        public string Voornaam
        {
            get
            {
                return voornaam;
            }
        }

        public string GeboorteDatum
        {
            get
            {
                return geboorteDatum;
            }
            set
            {
                geboorteDatum = value;
            }
        }
        public string ID
        {
            get
            {
                return id;
            }
        }

        public string Paswoord
        {
            get
            {
                return paswoord;
            }
        }
    }
}
