using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Odev06W.Library;

namespace Odev06W.App
{
    public class MainForm : Form
    {
        private Character[] heroes;
        private Dragon dragon;
        private int potionCount = 3;

        // UI Elements
        private Label[] heroLabels;
        private ProgressBar[] healthBars;
        private Button[] potionButtons;
        private TextBox[] experienceInputs;
        private Button[] addExperienceButtons;
        private Label dragonLabel;
        private ProgressBar dragonHealthBar;
        private Label potionCountLabel;
        private Button fightRoundButton;
        private Button resetButton;
        private Label battleLogLabel;

        public MainForm()
        {
            InitializeComponents();
            InitializeCharacters();
            UpdateUI();
        }

        private void InitializeComponents()
        {
            this.Text = "Dragon Battle Simulator";
            this.Size = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Initialize UI arrays
            heroLabels = new Label[3];
            healthBars = new ProgressBar[3];
            potionButtons = new Button[3];
            experienceInputs = new TextBox[3];
            addExperienceButtons = new Button[3];

            // Create hero UI elements
            for (int i = 0; i < 3; i++)
            {
                heroLabels[i] = new Label
                {
                    Location = new Point(20, 20 + i * 120),
                    Size = new Size(200, 40),
                    Font = new Font("Arial", 10)
                };

                healthBars[i] = new ProgressBar
                {
                    Location = new Point(20, 60 + i * 120),
                    Size = new Size(200, 20)
                };

                potionButtons[i] = new Button
                {
                    Location = new Point(230, 60 + i * 120),
                    Size = new Size(100, 23),
                    Text = "Use Potion"
                };

                experienceInputs[i] = new TextBox
                {
                    Location = new Point(20, 90 + i * 120),
                    Size = new Size(100, 23)
                };

                addExperienceButtons[i] = new Button
                {
                    Location = new Point(130, 90 + i * 120),
                    Size = new Size(100, 23),
                    Text = "Add XP"
                };

                this.Controls.AddRange(new Control[] {
                    heroLabels[i],
                    healthBars[i],
                    potionButtons[i],
                    experienceInputs[i],
                    addExperienceButtons[i]
                });

                int index = i;
                potionButtons[i].Click += (s, e) => UsePotion(index);
                addExperienceButtons[i].Click += (s, e) => AddExperience(index);
            }

            // Create dragon UI
            dragonLabel = new Label
            {
                Location = new Point(400, 20),
                Size = new Size(200, 40),
                Font = new Font("Arial", 10)
            };

            dragonHealthBar = new ProgressBar
            {
                Location = new Point(400, 60),
                Size = new Size(200, 20)
            };

            // Potion counter
            potionCountLabel = new Label
            {
                Location = new Point(20, 380),
                Size = new Size(200, 20),
                Text = $"Remaining Potions: {potionCount}"
            };

            // Fight button
            fightRoundButton = new Button
            {
                Location = new Point(400, 380),
                Size = new Size(100, 30),
                Text = "Fight Round"
            };
            fightRoundButton.Click += FightRound;

            // Reset button
            resetButton = new Button
            {
                Location = new Point(510, 380),
                Size = new Size(100, 30),
                Text = "Reset Battle"
            };
            resetButton.Click += ResetBattle;

            // Battle log
            battleLogLabel = new Label
            {
                Location = new Point(20, 420),
                Size = new Size(700, 100),
                Font = new Font("Arial", 10)
            };

            this.Controls.AddRange(new Control[] {
                dragonLabel,
                dragonHealthBar,
                potionCountLabel,
                fightRoundButton,
                resetButton,
                battleLogLabel
            });
        }

        private void InitializeCharacters()
        {
            heroes = new Character[]
            {
                new Warrior("Warrior"),
                new Archer("Archer"),
                new Wizard("Wizard")
            };

            dragon = new Dragon();

            // Subscribe to dragon's fire breath event
            dragon.OnFireBreath += (fireIntensity) =>
            {
                foreach (var hero in heroes)
                {
                    if (!hero.IsDead)
                    {
                        hero.TakeDamage(fireIntensity);
                    }
                }
                UpdateUI();
                LogBattle($"Dragon breathes fire for {fireIntensity} damage!");
            };
        }

        private void UsePotion(int heroIndex)
        {
            if (potionCount > 0 && !heroes[heroIndex].IsDead)
            {
                heroes[heroIndex].DrinkHealthPotion();
                potionCount--;
                UpdateUI();
                LogBattle($"{heroes[heroIndex].Name} used a healing potion!");

                if (potionCount == 0)
                {
                    foreach (var button in potionButtons)
                    {
                        button.Enabled = false;
                    }
                }
            }
        }

        private void AddExperience(int heroIndex)
        {
            if (int.TryParse(experienceInputs[heroIndex].Text, out int xp))
            {
                heroes[heroIndex].GainExperience(xp);
                experienceInputs[heroIndex].Text = "";
                UpdateUI();
                LogBattle($"{heroes[heroIndex].Name} gained {xp} experience points!");
            }
        }

        private void FightRound(object sender, EventArgs e)
        {
            // Heroes' attacks
            foreach (var hero in heroes)
            {
                if (!hero.IsDead && !dragon.IsDead)
                {
                    int damage = hero.GetDamagePerRound();
                    dragon.TakeDamage(damage);
                    LogBattle($"{hero.Name} deals {damage} damage to the dragon!");
                }
            }

            // Dragon's attack
            if (!dragon.IsDead)
            {
                dragon.BreatheFire();
            }

            UpdateUI();
            CheckBattleEnd();
        }

        private void ResetBattle(object sender, EventArgs e)
        {
            InitializeCharacters();
            potionCount = 3;
            foreach (var button in potionButtons)
            {
                button.Enabled = true;
            }
            fightRoundButton.Enabled = true;
            battleLogLabel.Text = "Battle reset!";
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Update hero information
            for (int i = 0; i < heroes.Length; i++)
            {
                heroLabels[i].Text = heroes[i].ToString();
                healthBars[i].Maximum = heroes[i].MaxHealth;
                healthBars[i].Value = heroes[i].CurrentHealth;
            }

            // Update dragon information
            dragonLabel.Text = dragon.ToString();
            dragonHealthBar.Maximum = dragon.MaxHealth;
            dragonHealthBar.Value = dragon.CurrentHealth;

            // Update potion count
            potionCountLabel.Text = $"Remaining Potions: {potionCount}";
        }

        private void CheckBattleEnd()
        {
            bool allHeroesDead = heroes.All(h => h.IsDead);
            if (dragon.IsDead || allHeroesDead)
            {
                fightRoundButton.Enabled = false;
                string result = dragon.IsDead ?
                    "Heroes won! The dragon has been defeated!" :
                    "Game Over! All heroes have fallen!";
                LogBattle(result);
            }
        }

        private void LogBattle(string message)
        {
            battleLogLabel.Text = message + "\n" + battleLogLabel.Text;
            if (battleLogLabel.Text.Length > 500)
            {
                battleLogLabel.Text = battleLogLabel.Text.Substring(0, 500) + "...";
            }
        }
    }
} 