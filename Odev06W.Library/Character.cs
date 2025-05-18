using System;

namespace Odev06W.Library
{
    public abstract class Character : IComparable<Character>
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int DamageDealt { get; set; }
        public int DamageResistance { get; set; }

        protected Character()
        {
            Level = 1;
            ExperiencePoints = 0;
        }

        protected Character(string name, int strength, int dexterity, int intelligence, int maxHealth, int damageDealt, int damageResistance)
        {
            Name = name;
            Level = 1;
            ExperiencePoints = 0;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            DamageDealt = damageDealt;
            DamageResistance = damageResistance;
        }

        public virtual string ToString()
        {
            return $"{GetType().Name} {Name} (Level {Level}): HP {CurrentHealth}/{MaxHealth}";
        }

        public virtual void EquipWeapon(int damageIncrease)
        {
            DamageDealt += damageIncrease;
        }

        public virtual void EquipArmor(int resistanceIncrease)
        {
            DamageResistance += resistanceIncrease;
        }

        public virtual int GetDamagePerRound()
        {
            return DamageDealt;
        }

        public virtual void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - DamageResistance);
            CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
        }

        public bool IsDead => CurrentHealth <= 0;

        public virtual void LevelUp()
        {
            Level++;
            MaxHealth += 10;
            Strength += 2;
            Dexterity += 2;
            Intelligence += 2;
            CurrentHealth = MaxHealth;
        }

        public virtual void GainExperience(int amount)
        {
            ExperiencePoints += amount;
            while (ExperiencePoints >= GetExperienceThreshold())
            {
                ExperiencePoints -= GetExperienceThreshold();
                LevelUp();
            }
        }

        protected virtual int GetExperienceThreshold()
        {
            return Level * 100;
        }

        public int CompareTo(Character other)
        {
            if (other == null) return 1;
            return Level.CompareTo(other.Level);
        }
    }
} 