using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//Author: Brecht Morrhey

namespace ProjectChallenge
{
    class ScoreLezer : BestandsLezer
    {
        public ScoreLezer():base() { }
        public ScoreLezer(string bestandsNaam):base(bestandsNaam) { }

        public string VoorNaam {
            get
            {
                Reset();
                string line = VolgendeRegel;                
                return line.Split(',')[0];
            }
        }
        public string Naam
        {
            get
            {
                Reset();
                string line = VolgendeRegel;                
                return line.Split(',')[1];
            }
        }
        public string Klas
        {
            get
            {
                string fileName = System.IO.Path.GetFileName(BestandsNaam);
                return fileName.Split('_')[0];
            }
        }
        public string UserId
        {
            get
            {
                string fileName = System.IO.Path.GetFileName(BestandsNaam);
                return fileName.Split('_')[1];
            }
        }
        public string Vraag
        {
            get
            {
                string fileName = System.IO.Path.GetFileName(BestandsNaam);
                return fileName.Split('_')[3];
            }
        }
        public double Score
        {
            get
            {
                Reset();
                string line = VolgendeRegel;
                line = VolgendeRegel;
                return Convert.ToDouble((line.Split(':')[2]).Split('%')[0]); //verwijder procent teken en converteer naar double
            }
        }
        public List<string> Resultaten
        {
            get
            {
                List<string> resultaten = new List<string>();
                Reset();
                string line = VolgendeRegel;
                line = VolgendeRegel;
                int regelCounter=0;
                line = VolgendeRegel;
                while(line != null && regelCounter<10000){
                    resultaten.Add(line);
                    regelCounter++;
                    line = VolgendeRegel;
                }
                if (regelCounter >= 10000)
                {
                    throw new BestandTeGrootException("Bestand " + System.IO.Path.GetFileName(BestandsNaam) + " is te groot.");
                }
                return resultaten;
            }
        }

    }
}
