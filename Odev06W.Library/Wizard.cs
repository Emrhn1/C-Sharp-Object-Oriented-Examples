namespace Odev06W.Library
{
    public class Wizard : Character
    {
        public int CurrentMana { get; set; }
        public int MaxMana { get; set; }

        public Wizard(string name) : base(name, strength: 6, dexterity: 8, intelligence: 15, maxHealth: 60, damageDealt: 4, damageResistance: 2)
        {
            MaxMana = 100;
            CurrentMana = MaxMana;
        }

        public override int GetDamagePerRound()
        {
            return base.GetDamagePerRound() + Intelligence;
        }

        public override void LevelUp()
        {
            base.LevelUp();
            MaxHealth += 5;
            Intelligence += 3;
            MaxMana += 20;
            CurrentMana = MaxMana;
        }
    }
} 