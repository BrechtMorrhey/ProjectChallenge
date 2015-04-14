using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    abstract public class Vraag
    {
        // dit is de abstracte hoofdklasse waar elke soort vraag een deelklasse van is
        //private string opgave;
        //private bool isIngevuld, isJuist;

        public virtual VraagType TypeVraag { get { return VraagType.vraag; } }

        public abstract string Opgave { get; set; }
        public abstract string Ingevuld { get; set; }
        public abstract string Antwoord { get; set; }

        public abstract bool IsIngevuld { get; }
        public abstract bool IsJuist { get; }
        
    }

    public enum VraagType{
        basis,
        meerkeuze,
        wiskunde,
        vraag
    }
}
