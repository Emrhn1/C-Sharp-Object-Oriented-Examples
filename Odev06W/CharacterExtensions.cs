using System;

public static class CharacterExtensions
{
    public static void DrinkHealthPotion(this Character character)
    {
        character.CurrentHealth = character.MaxHealth;
    }

    public static void RegenerateMana(this Wizard wizard, int amount)
    {
        wizard.CurrentMana = Math.Min(wizard.MaxMana, wizard.CurrentMana + amount);
    }

    public static void Disarm(this Character character, int damageDecrease)
    {
        character.BaseDamage = Math.Max(0, character.BaseDamage - damageDecrease);
    }

    public static void RemoveArmor(this Character character, int resistanceDecrease)
    {
        character.DamageResistance = Math.Max(0, character.DamageResistance - resistanceDecrease);
    }
} 