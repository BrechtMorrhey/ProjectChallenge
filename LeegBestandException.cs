using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallenge
{
    class LeegBestandException : ApplicationException
    {
        public LeegBestandException() { }
        public LeegBestandException(string message)
            : base(message)
        {          
        }
    }
}
