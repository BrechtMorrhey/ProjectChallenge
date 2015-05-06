using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectChallenge
{
    public class AlleGebruikers
    {
        //  eigenschappen
        private List<Leerling> leerlingen;
        private List<Leerkracht> leerkrachten;

        //  methoden
        public AlleGebruikers()
        {
            leerlingen = new List<Leerling>();
            leerkrachten = new List<Leerkracht>();
            //LeesStudentenIn();
            //LeesLeerkrachtenIn();
            //Paden van deze methoden moeten aangepast worden
        }

        public void SlaStudentOp(Leerling leerling)
        {
            leerlingen.Add(leerling);
            StreamWriter opslaanStudent = File.AppendText("leerlingen.txt");
            opslaanStudent.WriteLine(leerling.ToString());
            opslaanStudent.Close();
        }
        public void SlaLeerkrachtOp(Leerkracht leerkracht)
        {
            leerkrachten.Add(leerkracht);
            StreamWriter opslaanLeerkracht = File.AppendText("leerkrachten.txt");
            opslaanLeerkracht.WriteLine(leerkracht.ToString());
            opslaanLeerkracht.Close();
        }

        private void LeesStudentenIn() 
        {
            StreamReader studenten = File.OpenText("leerlingen.txt");
            string regel = studenten.ReadLine();
            while (regel != null)
            {
                string[] studentGegevens = regel.Split(',');
                Leerling student = new Leerling(studentGegevens[2],studentGegevens[3],studentGegevens[4],studentGegevens[1], this);
                leerlingen.Add(student);
                regel = studenten.ReadLine();
            }
        }

        private void LeesLeerkrachtenIn()
        {
            StreamReader leerkrachten = File.OpenText("leerkrachten.txt");
            string regel = leerkrachten.ReadLine();
            while (regel != null)
            {
                string[] leerkrachtGegevens = regel.Split(',');
                Leerkracht leerkracht = new Leerkracht(leerkrachtGegevens[2], leerkrachtGegevens[3], leerkrachtGegevens[4], leerkrachtGegevens[1], this);
                this.leerkrachten.Add(leerkracht);
                regel = leerkrachten.ReadLine();
            }
        }

        //  properties
        public List<Leerling> Leerlingen
        {
            get
            {
                return leerlingen;
            }
        }

        public List<Leerkracht> Leerkrachten
        {
            get
            {
                return leerkrachten;
            }
        }
    }
}
