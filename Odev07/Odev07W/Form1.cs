using System;
using System.Windows.Forms;
using MagesGuild;

namespace MagesGuildApp
{
    public partial class MainForm : Form
    {
        private readonly MageGuild _guild;

        public MainForm()
        {
            InitializeComponent();
            _guild = CreateGuild();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize controls
            InitializeControls();
        }

        private void InitializeControls()
        {
            // ListBox
            listBoxResults = new ListBox();
            listBoxResults.Location = new System.Drawing.Point(400, 20);
            listBoxResults.Size = new System.Drawing.Size(380, 550);
            this.Controls.Add(listBoxResults);

            // Initialize all controls
            int y = 20;
            int buttonWidth = 200;
            int buttonHeight = 30;
            int spacing = 10;

            // All Mages
            btnAllMages = new Button();
            btnAllMages.Location = new System.Drawing.Point(20, y);
            btnAllMages.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnAllMages.Text = "Show All Mages";
            btnAllMages.Click += btnAllMages_Click;
            this.Controls.Add(btnAllMages);
            y += buttonHeight + spacing;

            // Experienced Mages
            btnExperiencedMages = new Button();
            txtMinLevel = new TextBox();
            btnExperiencedMages.Location = new System.Drawing.Point(20, y);
            btnExperiencedMages.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            btnExperiencedMages.Text = "Show Experienced Mages";
            btnExperiencedMages.Click += btnExperiencedMages_Click;
            this.Controls.Add(btnExperiencedMages);
            txtMinLevel.Location = new System.Drawing.Point(230, y);
            txtMinLevel.Size = new System.Drawing.Size(50, buttonHeight);
            this.Controls.Add(txtMinLevel);
            y += buttonHeight + spacing;

            // Add other controls similarly...
            // (Continue adding all other controls with their positions and event handlers)
        }

        private MageGuild CreateGuild()
        {
            var guild = new MageGuild();

            // Create some example mages
            var gandalf = new Mage("Gandalf", 10, 1000, 15, 12, 25, 100, 150, 20, 10, 15, 15, 5);
            var saruman = new Mage("Saruman", 8, 800, 10, 15, 22, 80, 120, 15, 8, 20, 10, 10);
            var merlin = new Mage("Merlin", 12, 1200, 12, 18, 28, 90, 180, 25, 12, 12, 18, 8);

            // Add spells to their spellbooks
            gandalf.Spellbook.AddRange(new[]
            {
                new Spell("Fireball", SpellType.Offensive, 20, 3, 50),
                new Spell("Lightning", SpellType.Offensive, 30, 5, 70),
                new Spell("Shield", SpellType.Defensive, 15, 4, 40)
            });

            saruman.Spellbook.AddRange(new[]
            {
                new Spell("Ice Storm", SpellType.Offensive, 25, 4, 60),
                new Spell("Heal", SpellType.Healing, 20, 3, 45),
                new Spell("Magic Barrier", SpellType.Defensive, 20, 5, 50)
            });

            merlin.Spellbook.AddRange(new[]
            {
                new Spell("Meteor", SpellType.Offensive, 40, 8, 100),
                new Spell("Greater Heal", SpellType.Healing, 35, 6, 80),
                new Spell("Time Stop", SpellType.Defensive, 50, 10, 90),
                new Spell("Lightning", SpellType.Offensive, 30, 5, 70)
            });

            guild.AddMage(gandalf);
            guild.AddMage(saruman);
            guild.AddMage(merlin);

            return guild;
        }

        private void DisplayResults(object results)
        {
            listBoxResults.Items.Clear();

            if (results == null)
                return;

            if (results is string str)
            {
                listBoxResults.Items.Add(str);
                return;
            }

            if (results is bool b)
            {
                listBoxResults.Items.Add(b.ToString());
                return;
            }

            foreach (var item in (System.Collections.IEnumerable)results)
            {
                listBoxResults.Items.Add(item.ToString());
                listBoxResults.Items.Add(string.Empty); // Empty line for readability
            }
        }

        private void btnAllMages_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetAllMages());
        }

        private void btnExperiencedMages_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMinLevel.Text, out int minLevel))
                DisplayResults(_guild.GetExperiencedMages(minLevel));
        }

        private void btnTalentedMages_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaxLevel.Text, out int maxLevel))
                DisplayResults(_guild.GetTalentedMages(maxLevel));
        }

        private void btnCombatPotential_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetCombatMagesTotalMana());
        }

        private void btnLargestArsenal_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMinSpells.Text, out int minSpells))
                DisplayResults(_guild.GetMagesWithLargeArsenal(minSpells));
        }

        private void btnVersatileMages_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetVersatileMages());
        }

        private void btnMostSpells_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetMagesWithMostSpells());
        }

        private void btnAllSpells_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetAllUniqueSpells());
        }

        private void btnSpellsByType_Click(object sender, EventArgs e)
        {
            if (Enum.TryParse<SpellType>(txtSpellType.Text, true, out SpellType type))
                DisplayResults(_guild.GetSpellsByType(type));
        }

        private void btnMageSpellsByType_Click(object sender, EventArgs e)
        {
            if (Enum.TryParse<SpellType>(txtSpellType2.Text, true, out SpellType type))
                DisplayResults(_guild.GetMageSpellsByType(txtMageName.Text, type));
        }

        private void btnSpellCountByType_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetSpellCountByType());
        }

        private void btnMageSpellCountByType_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.GetMageSpellCountByType(txtMageName2.Text));
        }

        private void btnPowerfulMages_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSkip.Text, out int skip) && int.TryParse(txtTake.Text, out int take))
                DisplayResults(_guild.GetMostPowerfulMages(skip, take));
        }

        private void btnCheckReady_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.AreMagesReady());
        }

        private void btnCheckUnconscious_Click(object sender, EventArgs e)
        {
            DisplayResults(_guild.IsAnyMageUnconscious());
        }

        private void btnMostResistant_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMinLevel2.Text, out int minLevel))
                DisplayResults(_guild.GetMostResistantMages(minLevel));
        }
    }
}