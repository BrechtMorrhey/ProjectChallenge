using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectChallenge
{
    class Exception
    {
        public static void Handle(System.Exception e)
        {
            MessageBox.Show("Unhandled Exception:" + e.ToString());
            Application.Current.Shutdown();
        }
        // overloaded Handle methods om verschillende Exceptions te handelen
    }
}
