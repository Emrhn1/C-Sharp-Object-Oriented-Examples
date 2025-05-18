using System;

namespace DragonGame.Library
{
    public static class CharacterExtensions
    {
        public static void DrinkHealthPotion(this Character character)
        {
            if (character != null)
            {
                character.GetType().GetProperty("CurrentHealth")?.SetValue(character, character.MaxHealth);
            }
        }

        public static void RegenerateManaPoints(this Wizard wizard, int amount)
        {
            if (wizard != null)
            {
                wizard.RegenerateMana(amount);
            }
        }

        public static void Unequip(this Character character, int damageDecrease)
        {
            if (character != null)
            {
                character.GetType().GetProperty("DamageDealt")?.SetValue(
                    character, 
                    Math.Max(0, character.DamageDealt - damageDecrease)
                );
            }
        }

        public static void UnequipArmor(this Character character, int resistanceDecrease)
        {
            if (character != null)
            {
                character.GetType().GetProperty("DamageResistance")?.SetValue(
                    character, 
                    Math.Max(0, character.DamageResistance - resistanceDecrease)
                );
            }
        }
    }
} 