using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission
{
    public class Resource
    {
        private int ID;
        private int num;
        private static Random rander = new Random();
        public Resource(int index, int n)
        {
            ID = index;
            num = n;
        }
        public int getId()
        {
            return ID;
        }
        public static Resource expendResource()
        {
            return new Resource(rander.Next(1, 11), rander.Next(1, 6));
        }
        public static Resource missionResource()
        {
            return new Resource(rander.Next(11, 21), 1);
        }
        public static Resource missionResource(int index)
        {
            return new Resource(index, 1);
        }
        public void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Resource: " + ID + " " + num);
        }
    }
}
