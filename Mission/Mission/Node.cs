using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission
{
    public abstract class Node
    {
        protected Character nodeNPC;
        protected const int NodeTypes = 4;
        protected const int typeSubmit = 1;
        protected const int typeShow = 2;
        protected const int typeDeliver = 3;
        protected const int typeAsk = 4;
        private static Random rander=new Random();
        public static Node ranNode()
        {
            int t = rander.Next(1, NodeTypes + 1);
            switch (t)
            {
                case typeSubmit:
                    return new SubmitNode();
                case typeShow:
                    return new ShowNode();
                case typeDeliver:
                    return new DeliverNode();
                default:
                    return new AskNode();

            }
        }
        protected Node()
        {
            nodeNPC = Character.getCharacter();
        }
        public abstract int getType();
        public abstract void deBug();
    }
    public class SubmitNode : Node
    {
        Resource needResource;
        public SubmitNode() : base()
        {
            needResource = Resource.expendResource();
        }
        public override int getType()
        {
            return typeSubmit;
        }
        public override void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Submit Node: ");
            nodeNPC.deBug();
            needResource.deBug();
        }
    }
    public class ShowNode : Node
    {
        Resource trustResourse;
        public ShowNode() : base()
        {
            trustResourse = Resource.missionResource();
        }
        public override int getType()
        {
            return typeShow;
        }
        public override void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Show Node: ");
            nodeNPC.deBug();
            trustResourse.deBug();
        }
    }
    public class DeliverNode : Node
    {
        Character findNPC;
        public DeliverNode() : base()
        {
            findNPC = Character.getCharacter(nodeNPC);
        }
        public override int getType()
        {
            return typeDeliver;
        }
        public override void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Deliver Node: ");
            nodeNPC.deBug();
            findNPC.deBug();
        }
    }
    public class AskNode : Node
    {
        Infoset sentence;
        public AskNode() : base()
        {
            sentence = Infoset.getInfoset();
        }
        public override int getType()
        {
            return typeAsk;
        }
        public override void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Ask Node: ");
            nodeNPC.deBug();
            sentence.deBug();
        }
    }
}
