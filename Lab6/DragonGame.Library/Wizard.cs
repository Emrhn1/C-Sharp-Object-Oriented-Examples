using System;

namespace DragonGame.Library
{
    public class Wizard : Character
    {
        public int CurrentMana { get; protected set; }
        public int MaxMana { get; protected set; }

        public Wizard(string name) : base()
        {
            Name = name;
            // Set wizard-specific initial stats
            Strength = 8;
            Dexterity = 10;
            Intelligence = 15;
            MaxHealth = 80;
            CurrentHealth = MaxHealth;
            DamageDealt = 8;
            DamageResistance = 2;
            MaxMana = 100;
            CurrentMana = MaxMana;
        }

        public override int CalculateDamagePerRound()
        {
            // Wizards deal more damage based on their intelligence
            return DamageDealt + Intelligence;
        }

        public override void LevelUp()
        {
            base.LevelUp();
            // Wizards gain more intelligence and mana on level up
            Intelligence += 3;
            MaxMana += 20;
            CurrentMana = MaxMana;
        }

        public void RegenerateMana(int amount)
        {
            CurrentMana = Math.Min(CurrentMana + amount, MaxMana);
        }
    }
} 