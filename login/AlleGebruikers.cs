﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectChallenge
{
    //  Deze klasse leest de gebruikers in en slaat ze op
    //  Ook de klassen worden hierin opgeslagen en ingelezen
    //  En ook het grootste deel van de mappenstructuur wordt
    //  Hierin aangemaakt.
    //
    //  Author: Stijn Stas
     


    public class AlleGebruikers
    {
        //  eigenschappen
        private List<Leerling> leerlingen;
        private List<Leerkracht> leerkrachten;
        private List<string> klassen;
        private string programmaDirPath, aceGebruikersPath, leerlingPath, leerkrachtPath, klassenPath;
        private StreamWriter opslaanStudent = null;
        private StreamWriter opslaanLeerkracht = null;
        private StreamWriter writeKlassenStream = null;
        private StreamReader studentenStream = null;
        private StreamReader leerkrachtenStream = null;
        private StreamReader readKlassenStream = null;


        //  Constructor
        public AlleGebruikers()
        {
            programmaDirPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Challenger");
            aceGebruikersPath = programmaDirPath + "\\Gebruikers";
            leerlingPath = aceGebruikersPath + "\\leerlingen.txt";
            leerkrachtPath = aceGebruikersPath + "\\leerkrachten.txt";
            klassenPath = aceGebruikersPath + "\\klassen.txt";

            leerlingen = new List<Leerling>();
            leerkrachten = new List<Leerkracht>();
            klassen = new List<string>();
            
            if (!Directory.Exists(programmaDirPath))
            {
                Directory.CreateDirectory(programmaDirPath);    
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
                LeesKlassenIn();
            }
            catch (FileNotFoundException)
            {
                if (readKlassenStream != null)
                {
                    readKlassenStream.Close();
                }
                File.CreateText(klassenPath);
                klassen.Add("testKlas");
            }
            finally
            {
                if (readKlassenStream != null)
                {
                    readKlassenStream.Close();
                }
            }
        }
        
        //  Methoden

        //  Student word opgeslagen in de leerlingenlijst
        //  en in een .txt bestand.
        //
        //  Author: Stijn Stas

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

        //  Leerkracht word opgeslagen in de leerkrachtenlijst
        //  en in een .txt bestand.
        //
        //  Author: Stijn Stas

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

        //  klassen word opgeslagen in de klassenlijst
        //  en in een .txt bestand.
        //
        //  Author: Stijn Stas

        public void SlaKlasOp(string klas)
        {
            klassen.Add(klas);
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
                writeKlassenStream.WriteLine(klas);
                writeKlassenStream.Close();
            }
        }

        //  Klassen worden uit een .txt bestand 
        //  ingelezen en in een klassenlijst 
        //  geplaatst.
        //
        //  Author: Stijn Stas

        public void LeesKlassenIn()
        {
            bool testKlasAanwezig = false;

            string regel = readKlassenStream.ReadLine();
            while (regel != null)
            {
                klassen.Add(regel);
                if (regel.CompareTo("testKlas") == 0)
                {
                    testKlasAanwezig = true;
                }
                regel = readKlassenStream.ReadLine();
            }
            if (!testKlasAanwezig)
            {
                klassen.Add("testKlas");
            }
        }

        //  Studenten worden uit een .txt bestand 
        //  ingelezen en in een leerlingenlijst 
        //  geplaatst.
        //
        //  Author: Stijn Stas

        private void LeesStudentenIn() 
        {
            string regel = studentenStream.ReadLine();
            while (regel != null)
            {
                LeesGebruikerIn(regel, soortGebruiker.leerling);
                regel = studentenStream.ReadLine();
            }
        }

        //  Leerkrachten worden uit een .txt bestand 
        //  ingelezen en in een leerkrachtenlijst 
        //  geplaatst.
        //
        //  Author: Stijn Stas

        private void LeesLeerkrachtenIn()
        {
            string regel = leerkrachtenStream.ReadLine();
            while (regel != null)
            {
                LeesGebruikerIn(regel, soortGebruiker.leerkracht);
                regel = leerkrachtenStream.ReadLine();
            }
        }

        //  gebruikers worden uit een .txt bestand 
        //  ingelezen en in een de respectievelijke 
        //  lijst geplaatst. Deze methode word gebruikt
        //  in LeesLeerkrachtenIn() en in LeesStudentenIn()
        //
        //  Author: Stijn Stas

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
