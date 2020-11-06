using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission;

namespace Runnable
{
    class Program
    {
        static void Main(string[] args)
        {
            Mission.Mission myMission = new Mission.Mission(1,5);
            myMission.deBug();
        }
    }
}
