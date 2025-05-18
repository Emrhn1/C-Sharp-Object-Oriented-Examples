namespace Odev06W.Library
{
    public class Warrior : Character
    {
        public int AttacksPerRound { get; set; }

        public Warrior(string name) : base(name, strength: 15, dexterity: 10, intelligence: 8, maxHealth: 100, damageDealt: 8, damageResistance: 5)
        {
            AttacksPerRound = 2;
        }

        public override int GetDamagePerRound()
        {
            return base.GetDamagePerRound() * AttacksPerRound + (Strength / 2);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            MaxHealth += 15;
            Strength += 3;
            if (Level % 3 == 0)
            {
                AttacksPerRound++;
            }
        }
    }
} 