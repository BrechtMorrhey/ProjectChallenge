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
        private string aceDirPath, aceGebruikersPath, leerlingPath, leerkrachtPath;
        private StreamWriter opslaanStudent = null;
        private StreamWriter opslaanLeerkracht = null;
        private StreamReader studentenStream = null;
        private StreamReader leerkrachtenStream = null;

        //  methoden
        public AlleGebruikers()
        {
            aceDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ace");
            leerlingPath = aceGebruikersPath + "\\leerlingen.txt";
            leerkrachtPath = aceGebruikersPath + "\\leerkrachten.txt";
            aceGebruikersPath = aceDirPath + "\\aceGebruikers";

            leerlingen = new List<Leerling>();
            leerkrachten = new List<Leerkracht>();
            
            if (!Directory.Exists(aceDirPath))
            {
                Directory.CreateDirectory(aceDirPath);    
            }

            if (!Directory.Exists(aceGebruikersPath))
            {
                Directory.CreateDirectory(aceGebruikersPath);
            }

            try
            {
                studentenStream = File.OpenText(leerlingPath);
                LeesStudentenIn();
            }
            catch (FileNotFoundException)
            {
                File.CreateText(leerlingPath);
                studentenStream = File.OpenText(leerlingPath);
            }
            finally {
                studentenStream.Close();
            }

            try
            {
                leerkrachtenStream = File.OpenText(leerkrachtPath);
                LeesLeerkrachtenIn();
            }
            catch (FileNotFoundException)
            {
                File.CreateText(leerkrachtPath);
                leerkrachtenStream = File.OpenText(leerkrachtPath);
            }
            finally
            {
                leerkrachtenStream.Close();
            }
            
        }

        public void SlaStudentOp(Leerling leerling)
        {
            leerlingen.Add(leerling);
            try
            {
                opslaanStudent = File.AppendText(leerlingPath);
                opslaanStudent.WriteLine(leerling.ToString());
            }
            catch
            {
                opslaanStudent = File.CreateText(leerlingPath);
                opslaanStudent.WriteLine(leerling.ToString());
            }
            finally
            {
                opslaanStudent.Close();
            }
        }
        public void SlaLeerkrachtOp(Leerkracht leerkracht)
        {
            leerkrachten.Add(leerkracht);
            try
            {
                opslaanLeerkracht = File.AppendText(leerkrachtPath);
                opslaanLeerkracht.WriteLine(leerkracht.ToString());
            }
            catch
            {
                opslaanLeerkracht = File.CreateText(leerkrachtPath);
                opslaanLeerkracht.WriteLine(leerkracht.ToString());
            }
            finally
            {
                opslaanLeerkracht.Close();
            }
        }


        private void LeesStudentenIn() 
        {
            studentenStream = File.OpenText(leerlingPath);
            string regel = studentenStream.ReadLine();
            while (regel != null)
            {
                LeesGebruikerIn(regel, soortGebruiker.leerling);
                regel = studentenStream.ReadLine();
            }
        }

        private void LeesLeerkrachtenIn()
        {
            string regel = leerkrachtenStream.ReadLine();
            while (regel != null)
            {
                LeesGebruikerIn(regel, soortGebruiker.leerkracht);
                regel = leerkrachtenStream.ReadLine();
            }
        }

        private void LeesGebruikerIn(string regel, soortGebruiker gebruikerSoort)
        {
            string[] gebruikersGegevens = regel.Split(',');

            if (gebruikerSoort == soortGebruiker.leerkracht)
            {
                Leerkracht leerkracht = new Leerkracht(gebruikersGegevens[2], gebruikersGegevens[3], gebruikersGegevens[4], gebruikersGegevens[1], this);
                leerkrachten.Add(leerkracht);
            }
            else
            {
                Leerling leerling = new Leerling(gebruikersGegevens[2], gebruikersGegevens[3], gebruikersGegevens[4], gebruikersGegevens[1], this);
                leerlingen.Add(leerling);
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

        enum soortGebruiker
        {
            leerling, leerkracht
        }
    }
}
