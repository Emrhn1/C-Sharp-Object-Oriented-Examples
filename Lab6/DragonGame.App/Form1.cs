using System;
using System.Windows.Forms;
using DragonGame.Library;

namespace DragonGame.App
{
    public partial class Form1 : Form
    {
        private Warrior warrior;
        private Archer archer;
        private Wizard wizard;
        private Dragon dragon;
        private int healthPotionsLeft = 3;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Create characters
            warrior = new Warrior("Aragorn");
            archer = new Archer("Legolas");
            wizard = new Wizard("Gandalf");
            dragon = new Dragon();

            // Subscribe to dragon's fire breath event
            dragon.OnFireBreath += (intensity) =>
            {
                warrior.TakeDamage(intensity);
                archer.TakeDamage(intensity);
                wizard.TakeDamage(intensity);
                UpdateHealthBars();
            };

            // Update UI
            UpdateHealthBars();
            UpdatePotionCount();
            UpdateButtons();
        }

        private void UpdateHealthBars()
        {
            // Update character health bars
            progressBarWarrior.Maximum = warrior.MaxHealth;
            progressBarWarrior.Value = warrior.CurrentHealth;
            labelWarrior.Text = $"{warrior.Name} (HP: {warrior.CurrentHealth}/{warrior.MaxHealth})";

            progressBarArcher.Maximum = archer.MaxHealth;
            progressBarArcher.Value = archer.CurrentHealth;
            labelArcher.Text = $"{archer.Name} (HP: {archer.CurrentHealth}/{archer.MaxHealth})";

            progressBarWizard.Maximum = wizard.MaxHealth;
            progressBarWizard.Value = wizard.CurrentHealth;
            labelWizard.Text = $"{wizard.Name} (HP: {wizard.CurrentHealth}/{wizard.MaxHealth})";

            progressBarDragon.Maximum = dragon.MaxHealth;
            progressBarDragon.Value = dragon.CurrentHealth;
            labelDragon.Text = $"{dragon.Name} (HP: {dragon.CurrentHealth}/{dragon.MaxHealth})";
        }

        private void UpdatePotionCount()
        {
            labelPotions.Text = $"Health Potions: {healthPotionsLeft}";
        }

        private void UpdateButtons()
        {
            // Update potion buttons
            btnWarriorPotion.Enabled = !warrior.IsDead && healthPotionsLeft > 0;
            btnArcherPotion.Enabled = !archer.IsDead && healthPotionsLeft > 0;
            btnWizardPotion.Enabled = !wizard.IsDead && healthPotionsLeft > 0;

            // Update combat button
            btnCombatRound.Enabled = !IsPartyDefeated() && !dragon.IsDead;

            // Update experience input controls
            btnWarriorExp.Enabled = !warrior.IsDead;
            btnArcherExp.Enabled = !archer.IsDead;
            btnWizardExp.Enabled = !wizard.IsDead;
        }

        private bool IsPartyDefeated()
        {
            return warrior.IsDead && archer.IsDead && wizard.IsDead;
        }

        private void ExecuteCombatRound()
        {
            // Party attacks
            if (!warrior.IsDead)
                dragon.TakeDamage(warrior.CalculateDamagePerRound());
            if (!archer.IsDead)
                dragon.TakeDamage(archer.CalculateDamagePerRound());
            if (!wizard.IsDead)
                dragon.TakeDamage(wizard.CalculateDamagePerRound());

            // Dragon attacks (fire breath event will handle damage)
            if (!dragon.IsDead)
                dragon.CalculateDamagePerRound();

            // Update UI
            UpdateHealthBars();
            UpdateButtons();

            // Check battle outcome
            if (dragon.IsDead)
            {
                MessageBox.Show("Victory! The dragon has been defeated!");
                btnReset.Enabled = true;
            }
            else if (IsPartyDefeated())
            {
                MessageBox.Show("Defeat! The entire party has fallen!");
                btnReset.Enabled = true;
            }
        }

        private void DrinkPotion(Character character)
        {
            if (healthPotionsLeft > 0 && !character.IsDead)
            {
                character.DrinkHealthPotion();
                healthPotionsLeft--;
                UpdateHealthBars();
                UpdatePotionCount();
                UpdateButtons();
            }
        }

        private void AddExperience(Character character, TextBox expInput)
        {
            if (int.TryParse(expInput.Text, out int exp) && exp > 0)
            {
                character.GainExperience(exp);
                UpdateHealthBars();
                expInput.Clear();
            }
        }

        private void btnCombatRound_Click(object sender, EventArgs e)
        {
            ExecuteCombatRound();
        }

        private void btnWarriorPotion_Click(object sender, EventArgs e)
        {
            DrinkPotion(warrior);
        }

        private void btnArcherPotion_Click(object sender, EventArgs e)
        {
            DrinkPotion(archer);
        }

        private void btnWizardPotion_Click(object sender, EventArgs e)
        {
            DrinkPotion(wizard);
        }

        private void btnWarriorExp_Click(object sender, EventArgs e)
        {
            AddExperience(warrior, txtWarriorExp);
        }

        private void btnArcherExp_Click(object sender, EventArgs e)
        {
            AddExperience(archer, txtArcherExp);
        }

        private void btnWizardExp_Click(object sender, EventArgs e)
        {
            AddExperience(wizard, txtWizardExp);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InitializeGame();
            btnReset.Enabled = false;
        }
    }
}
