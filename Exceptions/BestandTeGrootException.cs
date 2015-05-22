using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    class BestandTeGrootException: ApplicationException
    {
         public BestandTeGrootException() { }
         public BestandTeGrootException(string message)
            : base(message)
        {          
        }
    }
}
