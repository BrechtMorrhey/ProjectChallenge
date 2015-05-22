using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author: Brecht Morrhey
namespace ProjectChallenge
{
    class LezerNotInitialisedException : ApplicationException
    {
        public LezerNotInitialisedException() { }
        public LezerNotInitialisedException(string message) : base(message) { }

    }
}
