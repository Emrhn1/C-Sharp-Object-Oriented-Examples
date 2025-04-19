namespace MagesGuildApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1103, 600);
            Name = "MainForm";
            Text = "Mages Guild Manager";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Button btnAllMages;
        private System.Windows.Forms.Button btnExperiencedMages;
        private System.Windows.Forms.TextBox txtMinLevel;
        private System.Windows.Forms.Button btnTalentedMages;
        private System.Windows.Forms.TextBox txtMaxLevel;
        private System.Windows.Forms.Button btnCombatPotential;
        private System.Windows.Forms.Button btnLargestArsenal;
        private System.Windows.Forms.TextBox txtMinSpells;
        private System.Windows.Forms.Button btnVersatileMages;
        private System.Windows.Forms.Button btnMostSpells;
        private System.Windows.Forms.Button btnAllSpells;
        private System.Windows.Forms.Button btnSpellsByType;
        private System.Windows.Forms.TextBox txtSpellType;
        private System.Windows.Forms.Button btnMageSpellsByType;
        private System.Windows.Forms.TextBox txtMageName;
        private System.Windows.Forms.TextBox txtSpellType2;
        private System.Windows.Forms.Button btnSpellCountByType;
        private System.Windows.Forms.Button btnMageSpellCountByType;
        private System.Windows.Forms.TextBox txtMageName2;
        private System.Windows.Forms.Button btnPowerfulMages;
        private System.Windows.Forms.TextBox txtSkip;
        private System.Windows.Forms.TextBox txtTake;
        private System.Windows.Forms.Button btnCheckReady;
        private System.Windows.Forms.Button btnCheckUnconscious;
        private System.Windows.Forms.Button btnMostResistant;
        private System.Windows.Forms.TextBox txtMinLevel2;
    }
}