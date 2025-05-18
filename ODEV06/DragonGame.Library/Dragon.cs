using System;

namespace DragonGame.Library
{
    public class Dragon : Character
    {
        public int ExperienceReward { get; set; }
        
        // Event delegate and event declaration
        public delegate void FireBreathHandler(int intensity);
        public event FireBreathHandler OnFireBreath;

        public Dragon()
        {
            // Set dragon-specific initial stats
            Name = "Ancient Dragon";
            Level = 10;
            Strength = 25;
            Dexterity = 15;
            Intelligence = 20;
            MaxHealth = 500;
            CurrentHealth = MaxHealth;
            DamageDealt = 30;
            DamageResistance = 10;
            ExperienceReward = 1000;
        }

        public int BreatheFireAttack()
        {
            // Calculate fire intensity based on intelligence and level
            int fireIntensity = (Intelligence * 2) + (Level * 5);
            
            // Trigger the event if there are subscribers
            OnFireBreath?.Invoke(fireIntensity);
            
            return fireIntensity;
        }

        public override int CalculateDamagePerRound()
        {
            // Dragons deal physical damage plus fire damage
            return base.CalculateDamagePerRound() + BreatheFireAttack();
        }

        public override void LevelUp()
        {
            base.LevelUp();
            // Dragons gain more of everything on level up
            Strength += 5;
            Dexterity += 3;
            Intelligence += 4;
            MaxHealth += 50;
            CurrentHealth = MaxHealth;
            ExperienceReward += 200;
        }
    }
} 