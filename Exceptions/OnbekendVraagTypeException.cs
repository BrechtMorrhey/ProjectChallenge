using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author: Brecht Morrhey
namespace ProjectChallenge
{
    class OnbekendVraagTypeException: ApplicationException
    {
        public OnbekendVraagTypeException() { }
        public OnbekendVraagTypeException(string message)
            : base(message)
        {          
        }
    }
}
