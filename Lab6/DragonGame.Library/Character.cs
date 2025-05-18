using System;

namespace DragonGame.Library
{
    public abstract class Character : IComparable<Character>
    {
        // Basic properties
        public string Name { get; set; }
        public int Level { get; protected set; }
        public int ExperiencePoints { get; protected set; }
        public int Strength { get; protected set; }
        public int Dexterity { get; protected set; }
        public int Intelligence { get; protected set; }
        public int CurrentHealth { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int DamageDealt { get; protected set; }
        public int DamageResistance { get; protected set; }

        // Default constructor
        protected Character()
        {
            Level = 1;
            ExperiencePoints = 0;
        }

        // Constructor with all parameters
        protected Character(string name, int level, int exp, int str, int dex, int intel, 
            int currentHp, int maxHp, int damage, int resistance)
        {
            Name = name;
            Level = level;
            ExperiencePoints = exp;
            Strength = str;
            Dexterity = dex;
            Intelligence = intel;
            CurrentHealth = currentHp;
            MaxHealth = maxHp;
            DamageDealt = damage;
            DamageResistance = resistance;
        }

        // Required methods
        public override string ToString()
        {
            return $"{GetType().Name} {Name} (Level {Level})\n" +
                   $"HP: {CurrentHealth}/{MaxHealth}\n" +
                   $"STR: {Strength}, DEX: {Dexterity}, INT: {Intelligence}\n" +
                   $"Damage: {DamageDealt}, Resistance: {DamageResistance}";
        }

        public virtual void EquipWeapon(int damageIncrease)
        {
            DamageDealt += damageIncrease;
        }

        public virtual void EquipArmor(int resistanceIncrease)
        {
            DamageResistance += resistanceIncrease;
        }

        public virtual int CalculateDamagePerRound()
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
            return Level * 100; // Simple formula: each level requires Level * 100 XP
        }

        public int CompareTo(Character other)
        {
            if (other == null) return 1;
            return Level.CompareTo(other.Level);
        }
    }
} 