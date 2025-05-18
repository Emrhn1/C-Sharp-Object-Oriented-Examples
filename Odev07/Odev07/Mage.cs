using System;

namespace MagesGuild
{
    public class Mage
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentMana { get; set; }
        public int MaxMana { get; set; }
        public int DamageDealt { get; set; }
        public int PhysicalResistance { get; set; }
        public int FireResistance { get; set; }
        public int FrostResistance { get; set; }
        public int PoisonResistance { get; set; }
        public SpellBook Spellbook { get; set; }

        public Mage(string name, int level, int experiencePoints, int strength, int dexterity, int intelligence,
            int maxHealth, int maxMana, int damageDealt, int physicalResistance, int fireResistance,
            int frostResistance, int poisonResistance)
        {
            Name = name;
            Level = level;
            ExperiencePoints = experiencePoints;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            MaxMana = maxMana;
            CurrentMana = maxMana;
            DamageDealt = damageDealt;
            PhysicalResistance = physicalResistance;
            FireResistance = fireResistance;
            FrostResistance = frostResistance;
            PoisonResistance = poisonResistance;
            Spellbook = new SpellBook();
        }

        public override string ToString()
        {
            return $"Mage: {Name}\n" +
                   $"Level: {Level} (XP: {ExperiencePoints})\n" +
                   $"Stats: STR {Strength}, DEX {Dexterity}, INT {Intelligence}\n" +
                   $"Health: {CurrentHealth}/{MaxHealth}\n" +
                   $"Mana: {CurrentMana}/{MaxMana}\n" +
                   $"Damage: {DamageDealt}\n" +
                   $"Resistances: Physical {PhysicalResistance}, Fire {FireResistance}, " +
                   $"Frost {FrostResistance}, Poison {PoisonResistance}\n" +
                   $"Spellbook:\n{Spellbook}";
        }
    }
} 