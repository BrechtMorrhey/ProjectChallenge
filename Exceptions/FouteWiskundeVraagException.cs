﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Brecht Morrhey
namespace ProjectChallenge
{
    class FouteWiskundeVraagException : ApplicationException
    {
         public FouteWiskundeVraagException() { }
         public FouteWiskundeVraagException(string message)
            : base(message)
        {          
        }
    }
}
