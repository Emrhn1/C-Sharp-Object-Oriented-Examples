using System;

public abstract class Character : IComparable<Character>
{
    public string Name { get; set; }
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int Strength { get; set; } = 10;
    public int Dexterity { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int CurrentHealth { get; set; } = 100;
    public int MaxHealth { get; set; } = 100;
    public int BaseDamage { get; set; } = 10;
    public int DamageResistance { get; set; } = 5;
    public bool IsUnconscious => CurrentHealth <= 0;

    protected Character() { }

    protected Character(string name, int strength, int dexterity, int intelligence, int maxHealth, int baseDamage, int damageResistance)
    {
        Name = name;
        Strength = strength;
        Dexterity = dexterity;
        Intelligence = intelligence;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        BaseDamage = baseDamage;
        DamageResistance = damageResistance;
    }

    public virtual string ToString()
    {
        return $"{Name} (Level {Level})\nHP: {CurrentHealth}/{MaxHealth}\nSTR: {Strength} DEX: {Dexterity} INT: {Intelligence}";
    }

    public virtual void EquipWeapon(int damageIncrease)
    {
        BaseDamage += damageIncrease;
    }

    public virtual void EquipArmor(int resistanceIncrease)
    {
        DamageResistance += resistanceIncrease;
    }

    public virtual int GetDamagePerRound()
    {
        return BaseDamage + (Strength / 2);
    }

    public virtual void TakeDamage(int damage)
    {
        int actualDamage = Math.Max(1, damage - DamageResistance);
        CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
    }

    public virtual void LevelUp()
    {
        Level++;
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        Strength += 2;
        Dexterity += 2;
        Intelligence += 2;
    }

    public virtual void AddExperience(int xp)
    {
        Experience += xp;
        while (Experience >= GetExperienceThreshold())
        {
            Experience -= GetExperienceThreshold();
            LevelUp();
        }
    }

    protected virtual int GetExperienceThreshold()
    {
        return Level * 1000;
    }

    public int CompareTo(Character other)
    {
        if (other == null) return 1;
        return Level.CompareTo(other.Level);
    }
}

public class Warrior : Character
{
    public int AttacksPerRound { get; set; }

    public Warrior(string name) : base(
        name,
        strength: 15,
        dexterity: 10,
        intelligence: 8,
        maxHealth: 120,
        baseDamage: 12,
        damageResistance: 7)
    {
        AttacksPerRound = 2;
    }

    public override int GetDamagePerRound()
    {
        return base.GetDamagePerRound() * AttacksPerRound;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        MaxHealth += 15;
        Strength += 3;
    }
}

public class Archer : Character
{
    public int DodgeChancePercent { get; set; }

    public Archer(string name) : base(
        name,
        strength: 12,
        dexterity: 15,
        intelligence: 10,
        maxHealth: 90,
        baseDamage: 15,
        damageResistance: 4)
    {
        DodgeChancePercent = 20;
    }

    public override void TakeDamage(int damage)
    {
        Random rand = new Random();
        if (rand.Next(100) < DodgeChancePercent)
        {
            return; // Dodge successful
        }
        base.TakeDamage(damage);
    }

    public override int GetDamagePerRound()
    {
        return base.GetDamagePerRound() + (Dexterity / 3);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Dexterity += 3;
        DodgeChancePercent += 2;
    }
}

public class Wizard : Character
{
    public int CurrentMana { get; set; }
    public int MaxMana { get; set; }

    public Wizard(string name) : base(
        name,
        strength: 8,
        dexterity: 9,
        intelligence: 18,
        maxHealth: 80,
        baseDamage: 8,
        damageResistance: 3)
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
        Intelligence += 3;
        MaxMana += 20;
        CurrentMana = MaxMana;
    }
} 