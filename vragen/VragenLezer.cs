using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectChallenge
{
    class VragenLezer: BestandsLezer
    {
        public VragenLezer():base() { }
        public VragenLezer(string bestandsNaam) : base(bestandsNaam) { }

        public List<Vraag> VragenLijst
        {
            get
            {
                int i;
                List<Vraag> vragenLijst = new List<Vraag>();
                List<string> antwoordenLijst;
                string antwoord;
                Vraag vraag = null;
                string line = VolgendeRegel;
                int regelCounter=0;
                while(line != null && regelCounter<10000)
                {
                    switch (line.Split(',')[0])
                    {
                        case "basis":
                            vraag = new BasisVraag(line.Split(',')[1], line.Split(',')[2]);
                            break;
                        case "meerkeuze":
                            antwoordenLijst = new List<string>();
                            antwoord = line.Split(',')[2];
                            i = 0;
                            while ((antwoord.Split('|')[i]).Trim() != "")   // maak de antwoordenlijst door elementen in te lezen zolang er geen lege waarde komt
                            {
                                antwoordenLijst.Add(antwoord.Split('|')[i]);
                                i++;
                            }
                            if (antwoordenLijst != null)
                            {
                                vraag = new MeerkeuzeVraag(line.Split(',')[1], antwoordenLijst);
                            }
                            else
                            {
                                MessageBox.Show("Lege antwoordenlijst");
                                // de vraag wordt niet ingelezen en het programma probeert verder te gaan
                            }
                            //code voor meerkeuze
                            break;

                        case "wiskunde":
                            //code voor wiskunde
                            break;
                        default: throw new OnbekendVraagTypeException("Onbekend Type Vraag voor vraag " + line.Split(',')[1]);
                    }
                    if (vraag != null)
                    {
                        vragenLijst.Add(vraag);
                    }
                    else
                    {
                        throw new VraagIsNullException("Vraag op regel "+regelCounter+" in bestand "+BestandsNaam+" is null.");
                    }
                    line = VolgendeRegel;
                    regelCounter++;
                }
                if (regelCounter >= 10000)
                {
                    throw new BestandTeGrootException("Bestand " + System.IO.Path.GetFileName(BestandsNaam) + " is te groot.");
                }
                // leeg bestand exception
                if (regelCounter == 0)
                {
                    throw new LeegBestandException("Bestand " + BestandsNaam + " bevat geen geldige vragen.");
                }
                return vragenLijst;
            }
        }

    }
}
