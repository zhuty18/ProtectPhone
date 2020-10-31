using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission
{
    class Infoset
    {
        private Infoset()
        {

        }
        public static Infoset getInfoset()
        {
            return new Infoset();
        }
        public void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Infoset: ");
        }
    }
}
