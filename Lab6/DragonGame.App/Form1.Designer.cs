namespace DragonGame.App;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        labelWarrior = new System.Windows.Forms.Label();
        labelArcher = new System.Windows.Forms.Label();
        labelWizard = new System.Windows.Forms.Label();
        labelDragon = new System.Windows.Forms.Label();
        progressBarWarrior = new System.Windows.Forms.ProgressBar();
        progressBarArcher = new System.Windows.Forms.ProgressBar();
        progressBarWizard = new System.Windows.Forms.ProgressBar();
        progressBarDragon = new System.Windows.Forms.ProgressBar();
        btnWarriorPotion = new System.Windows.Forms.Button();
        btnArcherPotion = new System.Windows.Forms.Button();
        btnWizardPotion = new System.Windows.Forms.Button();
        labelPotions = new System.Windows.Forms.Label();
        btnCombatRound = new System.Windows.Forms.Button();
        btnReset = new System.Windows.Forms.Button();
        txtWarriorExp = new System.Windows.Forms.TextBox();
        txtArcherExp = new System.Windows.Forms.TextBox();
        txtWizardExp = new System.Windows.Forms.TextBox();
        btnWarriorExp = new System.Windows.Forms.Button();
        btnArcherExp = new System.Windows.Forms.Button();
        btnWizardExp = new System.Windows.Forms.Button();
        SuspendLayout();

        // Form settings
        this.Text = "Dragon Battle";
        this.Size = new System.Drawing.Size(800, 600);

        // Labels
        labelPotions.Location = new System.Drawing.Point(20, 20);
        labelPotions.Size = new System.Drawing.Size(200, 20);
        labelPotions.Text = "Health Potions: 3";

        labelWarrior.Location = new System.Drawing.Point(20, 60);
        labelWarrior.Size = new System.Drawing.Size(200, 20);

        labelArcher.Location = new System.Drawing.Point(20, 140);
        labelArcher.Size = new System.Drawing.Size(200, 20);

        labelWizard.Location = new System.Drawing.Point(20, 220);
        labelWizard.Size = new System.Drawing.Size(200, 20);

        labelDragon.Location = new System.Drawing.Point(20, 300);
        labelDragon.Size = new System.Drawing.Size(200, 20);

        // Progress bars
        progressBarWarrior.Location = new System.Drawing.Point(20, 80);
        progressBarWarrior.Size = new System.Drawing.Size(300, 30);

        progressBarArcher.Location = new System.Drawing.Point(20, 160);
        progressBarArcher.Size = new System.Drawing.Size(300, 30);

        progressBarWizard.Location = new System.Drawing.Point(20, 240);
        progressBarWizard.Size = new System.Drawing.Size(300, 30);

        progressBarDragon.Location = new System.Drawing.Point(20, 320);
        progressBarDragon.Size = new System.Drawing.Size(300, 30);

        // Potion buttons
        btnWarriorPotion.Location = new System.Drawing.Point(340, 80);
        btnWarriorPotion.Size = new System.Drawing.Size(100, 30);
        btnWarriorPotion.Text = "Use Potion";
        btnWarriorPotion.Click += btnWarriorPotion_Click;

        btnArcherPotion.Location = new System.Drawing.Point(340, 160);
        btnArcherPotion.Size = new System.Drawing.Size(100, 30);
        btnArcherPotion.Text = "Use Potion";
        btnArcherPotion.Click += btnArcherPotion_Click;

        btnWizardPotion.Location = new System.Drawing.Point(340, 240);
        btnWizardPotion.Size = new System.Drawing.Size(100, 30);
        btnWizardPotion.Text = "Use Potion";
        btnWizardPotion.Click += btnWizardPotion_Click;

        // Experience input controls
        txtWarriorExp.Location = new System.Drawing.Point(460, 80);
        txtWarriorExp.Size = new System.Drawing.Size(100, 30);

        txtArcherExp.Location = new System.Drawing.Point(460, 160);
        txtArcherExp.Size = new System.Drawing.Size(100, 30);

        txtWizardExp.Location = new System.Drawing.Point(460, 240);
        txtWizardExp.Size = new System.Drawing.Size(100, 30);

        btnWarriorExp.Location = new System.Drawing.Point(570, 80);
        btnWarriorExp.Size = new System.Drawing.Size(100, 30);
        btnWarriorExp.Text = "Add XP";
        btnWarriorExp.Click += btnWarriorExp_Click;

        btnArcherExp.Location = new System.Drawing.Point(570, 160);
        btnArcherExp.Size = new System.Drawing.Size(100, 30);
        btnArcherExp.Text = "Add XP";
        btnArcherExp.Click += btnArcherExp_Click;

        btnWizardExp.Location = new System.Drawing.Point(570, 240);
        btnWizardExp.Size = new System.Drawing.Size(100, 30);
        btnWizardExp.Text = "Add XP";
        btnWizardExp.Click += btnWizardExp_Click;

        // Combat and reset buttons
        btnCombatRound.Location = new System.Drawing.Point(20, 380);
        btnCombatRound.Size = new System.Drawing.Size(150, 40);
        btnCombatRound.Text = "Execute Combat Round";
        btnCombatRound.Click += btnCombatRound_Click;

        btnReset.Location = new System.Drawing.Point(180, 380);
        btnReset.Size = new System.Drawing.Size(100, 40);
        btnReset.Text = "Reset Game";
        btnReset.Click += btnReset_Click;
        btnReset.Enabled = false;

        // Add controls to form
        Controls.AddRange(new System.Windows.Forms.Control[] {
            labelPotions,
            labelWarrior, labelArcher, labelWizard, labelDragon,
            progressBarWarrior, progressBarArcher, progressBarWizard, progressBarDragon,
            btnWarriorPotion, btnArcherPotion, btnWizardPotion,
            txtWarriorExp, txtArcherExp, txtWizardExp,
            btnWarriorExp, btnArcherExp, btnWizardExp,
            btnCombatRound, btnReset
        });

        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label labelWarrior;
    private System.Windows.Forms.Label labelArcher;
    private System.Windows.Forms.Label labelWizard;
    private System.Windows.Forms.Label labelDragon;
    private System.Windows.Forms.Label labelPotions;
    private System.Windows.Forms.ProgressBar progressBarWarrior;
    private System.Windows.Forms.ProgressBar progressBarArcher;
    private System.Windows.Forms.ProgressBar progressBarWizard;
    private System.Windows.Forms.ProgressBar progressBarDragon;
    private System.Windows.Forms.Button btnWarriorPotion;
    private System.Windows.Forms.Button btnArcherPotion;
    private System.Windows.Forms.Button btnWizardPotion;
    private System.Windows.Forms.Button btnCombatRound;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.TextBox txtWarriorExp;
    private System.Windows.Forms.TextBox txtArcherExp;
    private System.Windows.Forms.TextBox txtWizardExp;
    private System.Windows.Forms.Button btnWarriorExp;
    private System.Windows.Forms.Button btnArcherExp;
    private System.Windows.Forms.Button btnWizardExp;
}
