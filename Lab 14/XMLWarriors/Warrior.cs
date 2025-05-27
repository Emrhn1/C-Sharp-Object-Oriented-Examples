using System;

namespace XMLWarriors
{
    public class Warrior
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public string Monster { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
} 