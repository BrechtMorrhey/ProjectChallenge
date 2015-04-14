using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    public class Klas
    {
        //  eigenschappen
        private string klasID;
        private Leerkracht leerkracht;
        private List<Leerling> leerlingen;

        //  constructor
        public Klas(string klasID)
        {
            this.klasID = klasID;
        }

        //  methoden
        public void VoegStudentToe(Leerling leerling) 
        {
            leerlingen.Add(leerling);
        }

        public void LeerlingenOverzicht()
        {
            //  ???
        }

        // properties
        public string KlasID
        {
            get
            { 
                return klasID; 
            }
            set
            {
                klasID = value;
            }
        }

        public Leerkracht Leerkracht
        {
            get
            {
                return leerkracht;
            }
            set
            {
                leerkracht = value;
            }
        }

        public int KlasGrootte
        {
            get
            {
                return leerlingen.Count();
            }
        }
    }
}
