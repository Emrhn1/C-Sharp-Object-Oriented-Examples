using System;

namespace Odev06W.Library
{
    public delegate void FireBreathHandler(int intensity);

    public class Dragon : Character
    {
        public int ExperienceReward { get; set; }
        public event FireBreathHandler OnFireBreath;

        public Dragon()
        {
            Name = "Ancient Dragon";
            Level = 10;
            Strength = 30;
            Dexterity = 15;
            Intelligence = 20;
            MaxHealth = 500;
            CurrentHealth = MaxHealth;
            DamageDealt = 25;
            DamageResistance = 15;
            ExperienceReward = 1000;
        }

        public int BreatheFire()
        {
            int intensity = (Intelligence * 2) + (Level * 5);
            OnFireBreath?.Invoke(intensity);
            return intensity;
        }

        public override int GetDamagePerRound()
        {
            return base.GetDamagePerRound() + (Strength / 2);
        }
    }
} 