using System;

namespace MagesGuild
{
    public class Spell
    {
        public string Name { get; set; }
        public SpellType Type { get; set; }
        public int ManaCost { get; set; }
        public int Cooldown { get; set; }
        public int Effect { get; set; }

        public Spell(string name, SpellType type, int manaCost, int cooldown, int effect)
        {
            Name = name;
            Type = type;
            ManaCost = manaCost;
            Cooldown = cooldown;
            Effect = effect;
        }

        public override string ToString()
        {
            return $"Spell: {Name}, Type: {Type}, Mana Cost: {ManaCost}, Cooldown: {Cooldown}, Effect: {Effect}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
} 