using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mission
{
    public class Mission
    {
        private List<Node> MissionChain;
        public Pack payPack;
        private Mission(int length = 1)
        {
            MissionChain = new List<Node>();
            payPack = new Pack();
            for (int i = 0; i < length; i++)
            {
                MissionChain.Add(Node.ranNode());
            }
        }
        public Mission(Resource res, int length = 1) : this(length)
        {
            payPack.addResource(res);
        }
        public Mission(int missionResourceid,int length = 1) : this(length)
        {
            payPack.addResource(Resource.missionResource(missionResourceid));
        }
        public void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Mission: ");
            payPack.deBug();
            foreach (Node d in MissionChain)
            {
                d.deBug();
            }
        }
    }
}
