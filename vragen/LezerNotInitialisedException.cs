using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectChallenge
{
    class LezerNotInitialisedException : ApplicationException
    {
        public LezerNotInitialisedException() { }
        public LezerNotInitialisedException(string message) : base(message) { }

    }
}
