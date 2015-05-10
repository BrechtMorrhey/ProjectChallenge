﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author Brecht Morrhey

namespace ProjectChallenge
{
    public class BasisVraag : Vraag
    {
        //variables
        private string opgave;
        private string antwoord, ingevuld;
        //private bool isIngevuld, isJuist;

        //constructors
        public BasisVraag()
        {

        }
        public BasisVraag(string opgave, string antwoord)
        {
            this.opgave = opgave;
            this.antwoord = antwoord;
        }

        //methods
        public override string ToString()
        {
            return "basis," + opgave + "," + antwoord;
        }

        //properties
        public override VraagType TypeVraag { get { return VraagType.basis; } }
        public override string Opgave
        {
            get
            {
                return opgave;
            }
            set
            {
                opgave = value;

            }
        }
        public override string Antwoord
        {
            get
            {
                return antwoord;
            }
            set
            {
                antwoord = value;

            }
        }
        public override string Ingevuld
        {
            get
            {
                return ingevuld;
            }
            set
            {
                ingevuld = value;

            }
        }
        public override bool IsIngevuld
        {
            get
            {
                return (ingevuld != null && ingevuld != "");
            }
        }
        public override bool IsJuist
        {
            get
            {
                if (IsIngevuld)
                {
                    return antwoord.Trim() == ingevuld.Trim();
                }
                else
                {
                    return (false);
                }
            }
        }

    }
}
