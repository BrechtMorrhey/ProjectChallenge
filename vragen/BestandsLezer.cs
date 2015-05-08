using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// http://stackoverflow.com/questions/2053206/return-streamreader-to-beginning
namespace ProjectChallenge
{
    public abstract class BestandsLezer
    {
        private string bestandsNaam;
        private FileStream s;
        private StreamReader inputStream;

        public BestandsLezer() { }
        public BestandsLezer(string bestandsNaam)
        {
            this.bestandsNaam = bestandsNaam;
            inputStream = null;
            s = null;
        }
        public void Initialise()
        {
            s = File.OpenRead(bestandsNaam);
            inputStream = new StreamReader(s);
        }
        public void Close()
        {
            if (s != null)
            {
                s.Dispose();
            }
            if (inputStream != null)
            {
                inputStream.Close();
            }
        }
        public void Reset()
        {
            if (s != null)
            {
            s.Position = 0;
            inputStream = new StreamReader(s);
            }
            else
            {
                throw new LezerNotInitialisedException();
            }
        }
        public string BestandsNaam { 
            get { return bestandsNaam; } 
            set 
            { 
                bestandsNaam = value;
                inputStream = null;
                s = null;                
            } 
        }
        public string VolgendeRegel
        {
            get
            {
                if (inputStream != null)
                {
                return inputStream.ReadLine();
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
