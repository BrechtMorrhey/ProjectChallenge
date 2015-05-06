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
        private List<string> klassen;
        private string aceDirPath, aceGebruikersPath, leerlingPath, leerkrachtPath, klassenPath;
        private StreamWriter opslaanStudent = null;
        private StreamWriter opslaanLeerkracht = null;
        private StreamWriter writeKlassenStream = null;
        private StreamReader studentenStream = null;
        private StreamReader leerkrachtenStream = null;
        private StreamReader readKlassenStream = null;


        //  methoden
        public AlleGebruikers()
        {
            aceDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ace");
            aceGebruikersPath = aceDirPath + "\\aceGebruikers";
            leerlingPath = aceGebruikersPath + "\\leerlingen.txt";
            leerkrachtPath = aceGebruikersPath + "\\leerkrachten.txt";
            klassenPath = aceGebruikersPath + "\\klassen.txt";

            leerlingen = new List<Leerling>();
            leerkrachten = new List<Leerkracht>();
            klassen = new List<string>();
            
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
            }
            finally {
                if (studentenStream != null)
                {
                    studentenStream.Close();
                }
            }

            try
            {
                leerkrachtenStream = File.OpenText(leerkrachtPath);
                LeesLeerkrachtenIn();
            }
            catch (FileNotFoundException)
            {
                File.CreateText(leerkrachtPath);
            }
            finally
            {
                if (leerkrachtenStream != null)
                {
                    leerkrachtenStream.Close();
                }
            }

            try
            {
                readKlassenStream = File.OpenText(klassenPath);
            }
            catch (FileNotFoundException)
            {
                if (readKlassenStream != null)
                {
                    readKlassenStream.Close();
                }
                File.CreateText(klassenPath);
            }
            finally
            {
                readKlassenStream = File.OpenText(klassenPath);
                LeesKlassenIn();
                readKlassenStream.Close();
            }
            
            if (klassen.Count == 0)
            {
                SlaKlasOp("testKlas");
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
            }
            catch
            {
                opslaanLeerkracht = File.CreateText(leerkrachtPath); 
            }
            finally
            {
                opslaanLeerkracht.WriteLine(leerkracht.ToString());
                opslaanLeerkracht.Close();
            }
        }

        public void SlaKlasOp(string klas)
        {
            try
            {
                writeKlassenStream = File.AppendText(klassenPath);
            }
            catch(FileNotFoundException)
            {
                writeKlassenStream = File.CreateText(klassenPath);
            }
            finally
            {
                klassen.Add(klas);
                writeKlassenStream.WriteLine(klas);
                writeKlassenStream.Close();
            }
        }

        public void LeesKlassenIn()
        {
            string regel = readKlassenStream.ReadLine();
            while (regel != null)
            {
                regel = readKlassenStream.ReadLine();
            }
        }

        private void LeesStudentenIn() 
        {
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
                Leerling leerling = new Leerling(gebruikersGegevens[3], gebruikersGegevens[4], gebruikersGegevens[5], gebruikersGegevens[2], gebruikersGegevens[0], this);
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

        public List<string> Klassen
        {
            get
            {
                return klassen;
            }
        }

        enum soortGebruiker
        {
            leerling, leerkracht
        }
    }
}
