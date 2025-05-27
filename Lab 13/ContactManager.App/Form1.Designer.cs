namespace ContactManager.App;

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
        components = new System.ComponentModel.Container();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        saveToToolStripMenuItem = new ToolStripMenuItem();
        saveXmlToolStripMenuItem = new ToolStripMenuItem();
        loadFromToolStripMenuItem = new ToolStripMenuItem();
        loadXmlToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        exitToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        dataGridView1 = new DataGridView();
        firstNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
        lastNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
        phoneDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
        emailDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
        discordDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
        contactBindingSource = new BindingSource(components);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)contactBindingSource).BeginInit();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToToolStripMenuItem, loadFromToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "&File";
        // 
        // saveToToolStripMenuItem
        // 
        saveToToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveXmlToolStripMenuItem });
        saveToToolStripMenuItem.Name = "saveToToolStripMenuItem";
        saveToToolStripMenuItem.Size = new Size(180, 22);
        saveToToolStripMenuItem.Text = "Save to";
        // 
        // saveXmlToolStripMenuItem
        // 
        saveXmlToolStripMenuItem.Name = "saveXmlToolStripMenuItem";
        saveXmlToolStripMenuItem.Size = new Size(180, 22);
        saveXmlToolStripMenuItem.Text = "XML";
        saveXmlToolStripMenuItem.Click += SaveXmlToolStripMenuItem_Click;
        // 
        // loadFromToolStripMenuItem
        // 
        loadFromToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadXmlToolStripMenuItem });
        loadFromToolStripMenuItem.Name = "loadFromToolStripMenuItem";
        loadFromToolStripMenuItem.Size = new Size(180, 22);
        loadFromToolStripMenuItem.Text = "Load from";
        // 
        // loadXmlToolStripMenuItem
        // 
        loadXmlToolStripMenuItem.Name = "loadXmlToolStripMenuItem";
        loadXmlToolStripMenuItem.Size = new Size(180, 22);
        loadXmlToolStripMenuItem.Text = "XML";
        loadXmlToolStripMenuItem.Click += LoadXmlToolStripMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(177, 6);
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(180, 22);
        exitToolStripMenuItem.Text = "E&xit";
        exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        helpToolStripMenuItem.Size = new Size(44, 20);
        helpToolStripMenuItem.Text = "&Help";
        // 
        // dataGridView1
        // 
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { firstNameDataGridViewTextBoxColumn, lastNameDataGridViewTextBoxColumn, phoneDataGridViewTextBoxColumn, emailDataGridViewTextBoxColumn, discordDataGridViewTextBoxColumn });
        dataGridView1.DataSource = contactBindingSource;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 24);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new Size(800, 426);
        dataGridView1.TabIndex = 1;
        // 
        // firstNameDataGridViewTextBoxColumn
        // 
        firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
        firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
        firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
        // 
        // lastNameDataGridViewTextBoxColumn
        // 
        lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
        lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
        lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
        // 
        // phoneDataGridViewTextBoxColumn
        // 
        phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
        phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
        phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
        // 
        // emailDataGridViewTextBoxColumn
        // 
        emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
        emailDataGridViewTextBoxColumn.HeaderText = "Email";
        emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
        // 
        // discordDataGridViewTextBoxColumn
        // 
        discordDataGridViewTextBoxColumn.DataPropertyName = "Discord";
        discordDataGridViewTextBoxColumn.HeaderText = "Discord";
        discordDataGridViewTextBoxColumn.Name = "discordDataGridViewTextBoxColumn";
        // 
        // contactBindingSource
        // 
        contactBindingSource.DataSource = typeof(Shared.Contact);
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dataGridView1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Contact Manager";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ((System.ComponentModel.ISupportInitialize)contactBindingSource).EndInit();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem saveToToolStripMenuItem;
    private ToolStripMenuItem loadFromToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private DataGridView dataGridView1;
    private BindingSource contactBindingSource;
    private ToolStripMenuItem saveXmlToolStripMenuItem;
    private ToolStripMenuItem loadXmlToolStripMenuItem;
    private DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn discordDataGridViewTextBoxColumn;
}
