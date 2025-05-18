using System;
using System.Collections.Generic;
using System.Linq;

namespace MagesGuild
{
    public record MageSpellCount(string Name, int SpellCount, int ManaPoints);
    public record MageStats(string Name, int Level, double AverageStats);
    public record SpellTypeCount(SpellType Type, int Count);
    public record MagePowerLevel(string Name, int Level);
    public record MageResistances(string Name, int Level, int TotalResistance, int Physical, int Fire, int Frost, int Poison);

    public class MageGuild
    {
        private readonly List<Mage> _mages;

        public MageGuild()
        {
            _mages = new List<Mage>();
        }

        public void AddMage(Mage mage)
        {
            _mages.Add(mage);
        }

        public override string ToString()
        {
            return string.Join("\n\n", _mages.Select(m => m.ToString()));
        }

        // 1) List of all mages sorted by name
        public IEnumerable<Mage> GetAllMages()
        {
            return _mages.OrderBy(m => m.Name);
        }

        // 2) Experienced mages above certain level
        public IEnumerable<Mage> GetExperiencedMages(int minLevel)
        {
            return _mages.Where(m => m.Level > minLevel).OrderBy(m => m.Level);
        }

        // 3) Talented mages with high intelligence
        public IEnumerable<Mage> GetTalentedMages(int maxLevel)
        {
            return _mages.Where(m => m.Intelligence > 20 && m.Level <= maxLevel)
                        .OrderByDescending(m => m.Intelligence);
        }

        // 4) Total mana of combat mages
        public int GetCombatMagesTotalMana()
        {
            return _mages.Where(m => m.Level > 2 && m.Dexterity > 10)
                        .Sum(m => m.MaxMana);
        }

        // 5) Mages with largest spell arsenal
        public IEnumerable<MageSpellCount> GetMagesWithLargeArsenal(int minSpells)
        {
            return _mages.Where(m => m.Spellbook.Count >= minSpells)
                        .Select(m => new MageSpellCount(m.Name, m.Spellbook.Count, m.MaxMana))
                        .OrderByDescending(m => m.SpellCount);
        }

        // 6) Most versatile mages
        public IEnumerable<MageStats> GetVersatileMages()
        {
            return _mages.Select(m => new MageStats(
                m.Name,
                m.Level,
                (m.Strength + m.Dexterity + m.Intelligence) / 3.0
            )).OrderByDescending(m => m.AverageStats);
        }

        // 7) Mages with most spells
        public IEnumerable<string> GetMagesWithMostSpells()
        {
            var maxSpells = _mages.Max(m => m.Spellbook.Count);
            return _mages.Where(m => m.Spellbook.Count == maxSpells)
                        .Select(m => m.Name);
        }

        // 8) All unique spells
        public IEnumerable<Spell> GetAllUniqueSpells()
        {
            return _mages.SelectMany(m => m.Spellbook).Distinct();
        }

        // 9) All spells of specific type
        public IEnumerable<Spell> GetSpellsByType(SpellType type)
        {
            return _mages.SelectMany(m => m.Spellbook)
                        .Where(s => s.Type == type)
                        .Distinct();
        }

        // 10) Specific mage's spells of specific type
        public IEnumerable<Spell> GetMageSpellsByType(string mageName, SpellType type)
        {
            return _mages.Single(m => m.Name == mageName)
                        .Spellbook.Where(s => s.Type == type);
        }

        // 11) Count of spells by type
        public IEnumerable<SpellTypeCount> GetSpellCountByType()
        {
            return _mages.SelectMany(m => m.Spellbook)
                        .Distinct()
                        .GroupBy(s => s.Type)
                        .Select(g => new SpellTypeCount(g.Key, g.Count()));
        }

        // 12) Specific mage's spell count by type
        public IEnumerable<SpellTypeCount> GetMageSpellCountByType(string mageName)
        {
            return _mages.Single(m => m.Name == mageName)
                        .Spellbook
                        .GroupBy(s => s.Type)
                        .Select(g => new SpellTypeCount(g.Key, g.Count()));
        }

        // 13) Most powerful mages excluding some
        public IEnumerable<MagePowerLevel> GetMostPowerfulMages(int skip, int take)
        {
            return _mages.OrderByDescending(m => m.Level)
                        .ThenByDescending(m => m.ExperiencePoints)
                        .Skip(skip)
                        .Take(take)
                        .Select(m => new MagePowerLevel(m.Name, m.Level));
        }

        // 14) Check if all mages are ready
        public bool AreMagesReady()
        {
            return _mages.All(m => m.CurrentHealth == m.MaxHealth && m.CurrentMana == m.MaxMana);
        }

        // 15) Check if any mage is unconscious
        public bool IsAnyMageUnconscious()
        {
            return _mages.Any(m => m.CurrentHealth == 0);
        }

        // 16) Get most resistant mages
        public IEnumerable<MageResistances> GetMostResistantMages(int minLevel)
        {
            return _mages.Where(m => m.Level > minLevel)
                        .Select(m => new MageResistances(
                            m.Name,
                            m.Level,
                            m.PhysicalResistance + m.FireResistance + m.FrostResistance + m.PoisonResistance,
                            m.PhysicalResistance,
                            m.FireResistance,
                            m.FrostResistance,
                            m.PoisonResistance
                        ))
                        .OrderByDescending(m => m.TotalResistance)
                        .Take(3);
        }
    }
} 