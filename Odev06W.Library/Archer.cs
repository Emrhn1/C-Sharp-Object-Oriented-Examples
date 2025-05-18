using System;

namespace Odev06W.Library
{
    public class Archer : Character
    {
        public int DodgeChancePercent { get; set; }
        private Random random = new Random();

        public Archer(string name) : base(name, strength: 10, dexterity: 15, intelligence: 10, maxHealth: 80, damageDealt: 10, damageResistance: 3)
        {
            DodgeChancePercent = 20;
        }

        public override int GetDamagePerRound()
        {
            return base.GetDamagePerRound() + (Dexterity / 2);
        }

        public override void TakeDamage(int damage)
        {
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
            MaxHealth += 8;
            Dexterity += 3;
            DodgeChancePercent = Math.Min(50, DodgeChancePercent + 2);
        }
    }
} 