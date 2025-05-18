using System;

namespace DragonGame.Library
{
    public class Archer : Character
    {
        public int DodgeChancePercent { get; protected set; }
        private Random random = new Random();

        public Archer(string name) : base()
        {
            Name = name;
            // Set archer-specific initial stats
            Strength = 10;
            Dexterity = 15;
            Intelligence = 10;
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            DamageDealt = 15;
            DamageResistance = 3;
            DodgeChancePercent = 20;
        }

        public override int CalculateDamagePerRound()
        {
            // Archers deal more damage based on their dexterity
            return DamageDealt + (Dexterity / 2);
        }

        public override void TakeDamage(int damage)
        {
            // Check for dodge
            if (random.Next(1, 101) <= DodgeChancePercent)
            {
                // Dodge successful, no damage taken
                return;
            }

            base.TakeDamage(damage);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            // Archers gain more dexterity on level up
            Dexterity += 3;
            DodgeChancePercent = Math.Min(DodgeChancePercent + 2, 50); // Cap dodge chance at 50%
        }
    }
} 