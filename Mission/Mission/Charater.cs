using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission
{
    public class Character
    {
        private int characterId;
        private static int index = 0;
        private string name;
        private Character()
        {
            index++;
            characterId = index;
            name = ranName();
        }
        public static Character getCharacter()
        {
            return new Character();
        }
        public static Character getCharacter(Character notbe)
        {
            return new Character();
        }
        public static string ranName()
        {
            return "Alice";
        }
        public void deBug()
        {
            System.Diagnostics.Debug.WriteLine("Character: "+characterId+" "+name);
        }
    }
}
