using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission
{
    public class Pack
    {
        private List<Resource> resources;
        public Pack()
        {
            resources = new List<Resource>();
        }
        public void addResource(int index, int n)
        {
            resources.Add(new Resource(index, n));
        }
        public void addResource(Resource res)
        {
            resources.Add(res);
        }
        public List<Resource> getResources()
        {
            return resources;
        }
        public void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Pack: ");
            foreach (Resource res in resources)
            {
                res.deBug();
            }
        }
    }
}
