using System;

namespace DragonGame.Library
{
    public class Warrior : Character
    {
        public int AttacksPerRound { get; protected set; }

        public Warrior(string name) : base()
        {
            Name = name;
            // Set warrior-specific initial stats
            Strength = 15;
            Dexterity = 10;
            Intelligence = 8;
            MaxHealth = 120;
            CurrentHealth = MaxHealth;
            DamageDealt = 12;
            DamageResistance = 5;
            AttacksPerRound = 2;
        }

        public override int CalculateDamagePerRound()
        {
            // Warriors deal more damage based on their strength and get multiple attacks
            int baseDamage = DamageDealt + (Strength / 2);
            return baseDamage * AttacksPerRound;
        }

        public override void LevelUp()
        {
            base.LevelUp();
            // Warriors gain more strength and health on level up
            Strength += 3;
            MaxHealth += 15;
            CurrentHealth = MaxHealth;
            
            // Every 5 levels, gain an extra attack
            if (Level % 5 == 0)
            {
                AttacksPerRound++;
            }
        }
    }
} 