using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
