using System;

public class Dragon : Character
{
    public int ExperienceReward { get; set; }
    public event Action<int> OnFireBreath;

    public Dragon() : base()
    {
        Name = "Ancient Dragon";
        Level = 5;
        Strength = 25;
        Intelligence = 15;
        MaxHealth = 200;
        CurrentHealth = MaxHealth;
        BaseDamage = 20;
        DamageResistance = 8;
        ExperienceReward = 1000;
    }

    public int BreatheFire()
    {
        int fireIntensity = BaseDamage + (Intelligence * 2) + (Level * 5);
        OnFireBreath?.Invoke(fireIntensity);
        return fireIntensity;
    }

    public override int GetDamagePerRound()
    {
        return base.GetDamagePerRound() + (Level * 3);
    }

    public override string ToString()
    {
        return $"🐲 {Name} (Level {Level})\nHP: {CurrentHealth}/{MaxHealth}\nPower: {GetDamagePerRound()}";
    }
} 