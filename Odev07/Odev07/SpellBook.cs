using System;
using System.Collections.Generic;
using System.Text;

namespace MagesGuild
{
    public class SpellBook : List<Spell>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Spellbook contains:");
            foreach (var spell in this)
            {
                sb.AppendLine(spell.ToString());
            }
            return sb.ToString();
        }
    }
} 