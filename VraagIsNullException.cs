using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    class VraagIsNullException : ApplicationException
    {
         public VraagIsNullException() { }
         public VraagIsNullException(string message)
            : base(message)
        {          
        }
    }
}
