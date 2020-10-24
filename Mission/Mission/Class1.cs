using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mission
{
    public class Resource
    {
        private int ID;
        private int num;
        public Resource(int index, int n)
        {
            ID = index;
            num = n;
        }
        public int getId()
        {
            return ID;
        }
    }
    public class Pack
    {
        private List<Resource> resources;
        Pack()
        {
            resources = new List<Resource>();
        }
        public void addResource(int index,int n)
        {
            resources.Add(new Resource(index, n));
        }
        public List<Resource> getResource()
        {
            return resources;
        }
    }
    public class Charater
    {
        private string name;
        Charater()
        {
            name = ranName();
        }
        static string ranName()
        {
            return "Alice";
        }
    }
    public class Mission{
        Charater NPC;
        Resource needResource;
        Pack payPack;
    }
}
