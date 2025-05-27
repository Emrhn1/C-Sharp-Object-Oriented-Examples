using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace XMLWarriors
{
    public partial class Form1 : Form
    {
        private List<Warrior> _warriors;
        private string _xmlFilePath = "warriors.xml";

        public Form1()
        {
            InitializeComponent();
            _warriors = new List<Warrior>();
            SetupInitialUI();
            CreateInitialXml();
        }

        private void SetupInitialUI()
        {
            // Menu setup
            var menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("File");
            var loadMenuItem = new ToolStripMenuItem("Load XML", null, LoadXml);
            var saveMenuItem = new ToolStripMenuItem("Save XML", null, SaveXml);
            
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { loadMenuItem, saveMenuItem });
            menuStrip.Items.Add(fileMenu);
            this.Controls.Add(menuStrip);
            menuStrip.Dock = DockStyle.Top;

            // Main layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(10),
                Margin = new Padding(0, menuStrip.Height, 0, 0)  // Add margin to avoid overlapping with menu
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            // DataGridView
            var gridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            gridView.DataSource = _warriors;

            // Controls group
            var controlsGroup = new GroupBox
            {
                Text = "Warrior Management",
                Dock = DockStyle.Fill
            };

            var controlsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 7,
                ColumnCount = 2,
                Padding = new Padding(10)
            };

            // Add controls
            var warriorCombo = new ComboBox { Dock = DockStyle.Fill };
            warriorCombo.SelectedIndexChanged += WarriorCombo_SelectedIndexChanged;
            warriorCombo.DataSource = _warriors;
            warriorCombo.DisplayMember = "Name";

            var nameLabel = new Label { Text = "Name:" };
            var nameTextBox = new TextBox { Name = "nameTextBox" };

            var genderLabel = new Label { Text = "Gender:" };
            var genderCombo = new ComboBox { Name = "genderCombo" };
            genderCombo.Items.AddRange(new[] { "Male", "Female" });

            var levelLabel = new Label { Text = "Level:" };
            var levelTextBox = new TextBox { Name = "levelTextBox" };

            var hpLabel = new Label { Text = "HP:" };
            var hpTextBox = new TextBox { Name = "hpTextBox" };

            var monsterLabel = new Label { Text = "Monster:" };
            var monsterTextBox = new TextBox { Name = "monsterTextBox" };

            var buttonsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill };
            var addButton = new Button { Text = "Add" };
            var updateButton = new Button { Text = "Update" };
            var removeButton = new Button { Text = "Remove" };
            var resetButton = new Button { Text = "Reset" };

            addButton.Click += AddButton_Click;
            updateButton.Click += UpdateButton_Click;
            removeButton.Click += RemoveButton_Click;
            resetButton.Click += ResetButton_Click;

            buttonsPanel.Controls.AddRange(new Control[] { addButton, updateButton, removeButton, resetButton });

            // Sorting controls
            var sortingGroup = new GroupBox { Text = "Sorting", Dock = DockStyle.Fill };
            var sortingLayout = new FlowLayoutPanel { Dock = DockStyle.Fill };
            
            var propertyCombo = new ComboBox { Name = "propertyCombo" };
            propertyCombo.DataSource = typeof(Warrior).GetProperties().Select(p => p.Name).ToList();

            var ascRadio = new RadioButton { Text = "Ascending", Checked = true };
            var descRadio = new RadioButton { Text = "Descending" };
            var sortButton = new Button { Text = "Sort" };
            sortButton.Click += SortButton_Click;

            sortingLayout.Controls.AddRange(new Control[] { propertyCombo, ascRadio, descRadio, sortButton });
            sortingGroup.Controls.Add(sortingLayout);

            // Add all controls to layout
            controlsLayout.Controls.AddRange(new Control[] {
                new Label { Text = "Select Warrior:" }, warriorCombo,
                nameLabel, nameTextBox,
                genderLabel, genderCombo,
                levelLabel, levelTextBox,
                hpLabel, hpTextBox,
                monsterLabel, monsterTextBox,
                buttonsPanel, sortingGroup
            });

            controlsGroup.Controls.Add(controlsLayout);
            mainLayout.Controls.Add(gridView);
            mainLayout.Controls.Add(controlsGroup);

            this.Controls.Add(mainLayout);
        }

        private void CreateInitialXml()
        {
            var xmlDoc = new XElement("guild",
                new XElement("warrior",
                    new XAttribute("gender", "Male"),
                    new XElement("name", "Arthur"),
                    new XElement("level", "10"),
                    new XElement("hp", "100"),
                    new XElement("monster", "Dragon")),
                new XElement("warrior",
                    new XAttribute("gender", "Female"),
                    new XElement("name", "Morgana"),
                    new XElement("level", "12"),
                    new XElement("hp", "95"),
                    new XElement("monster", "Griffin")),
                new XElement("warrior",
                    new XAttribute("gender", "Male"),
                    new XElement("name", "Lancelot"),
                    new XElement("level", "11"),
                    new XElement("hp", "110"),
                    new XElement("monster", "Hydra")));

            xmlDoc.Save(_xmlFilePath);
            LoadXml(null, null);
        }

        private void LoadXml(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Title = "Select XML File to Load";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _warriors.Clear();
                        var doc = XElement.Load(openFileDialog.FileName);
                        var warriors = doc.Elements("warrior").Select(w => new Warrior
                        {
                            Name = w.Element("name").Value,
                            Gender = w.Attribute("gender").Value,
                            Level = int.Parse(w.Element("level").Value),
                            HP = int.Parse(w.Element("hp").Value),
                            Monster = w.Element("monster").Value
                        });

                        _warriors.AddRange(warriors);
                        RefreshControls();
                        MessageBox.Show("XML file loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveXml(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.Title = "Save XML File";
                    saveFileDialog.DefaultExt = "xml";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.FileName = "warriors.xml";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var doc = new XElement("guild",
                            _warriors.Select(w => new XElement("warrior",
                                new XAttribute("gender", w.Gender),
                                new XElement("name", w.Name),
                                new XElement("level", w.Level),
                                new XElement("hp", w.HP),
                                new XElement("monster", w.Monster))));

                        doc.Save(saveFileDialog.FileName);

                        // XML dosyasını otomatik olarak varsayılan tarayıcıda açma seçeneği sun
                        if (MessageBox.Show("XML file saved successfully! Would you like to open it in your default browser?", 
                            "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = saveFileDialog.FileName,
                                UseShellExecute = true
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving XML file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshControls()
        {
            var gridView = Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<DataGridView>().First();
            var warriorCombo = GetWarriorCombo();

            gridView.DataSource = null;
            gridView.DataSource = _warriors;
            
            warriorCombo.DataSource = null;
            warriorCombo.DataSource = _warriors;
        }

        private ComboBox GetWarriorCombo()
        {
            return Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<GroupBox>().First()
                .Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<ComboBox>().First();
        }

        private void WarriorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            if (combo.SelectedItem == null) return;

            var warrior = (Warrior)combo.SelectedItem;
            var controls = GetControlsGroup();

            controls["nameTextBox"].Text = warrior.Name;
            controls["genderCombo"].Text = warrior.Gender;
            controls["levelTextBox"].Text = warrior.Level.ToString();
            controls["hpTextBox"].Text = warrior.HP.ToString();
            controls["monsterTextBox"].Text = warrior.Monster;
        }

        private Dictionary<string, Control> GetControlsGroup()
        {
            var controlsLayout = Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<GroupBox>().First()
                .Controls.OfType<TableLayoutPanel>().First();

            return new Dictionary<string, Control>
            {
                ["nameTextBox"] = controlsLayout.Controls.OfType<TextBox>().ElementAt(0),
                ["genderCombo"] = controlsLayout.Controls.OfType<ComboBox>().ElementAt(1),
                ["levelTextBox"] = controlsLayout.Controls.OfType<TextBox>().ElementAt(1),
                ["hpTextBox"] = controlsLayout.Controls.OfType<TextBox>().ElementAt(2),
                ["monsterTextBox"] = controlsLayout.Controls.OfType<TextBox>().ElementAt(3)
            };
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var controls = GetControlsGroup();
            var name = controls["nameTextBox"].Text;

            if (_warriors.Any(w => w.Name == name))
            {
                MessageBox.Show("A warrior with this name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var warrior = new Warrior
            {
                Name = name,
                Gender = controls["genderCombo"].Text,
                Level = int.Parse(controls["levelTextBox"].Text),
                HP = int.Parse(controls["hpTextBox"].Text),
                Monster = controls["monsterTextBox"].Text
            };

            _warriors.Add(warrior);
            RefreshControls();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var combo = GetWarriorCombo();
            if (combo.SelectedItem == null) return;

            var warrior = (Warrior)combo.SelectedItem;
            var controls = GetControlsGroup();

            warrior.Name = controls["nameTextBox"].Text;
            warrior.Gender = controls["genderCombo"].Text;
            warrior.Level = int.Parse(controls["levelTextBox"].Text);
            warrior.HP = int.Parse(controls["hpTextBox"].Text);
            warrior.Monster = controls["monsterTextBox"].Text;

            RefreshControls();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            var combo = GetWarriorCombo();
            if (combo.SelectedItem == null) return;

            var warrior = (Warrior)combo.SelectedItem;
            _warriors.Remove(warrior);
            RefreshControls();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            var controls = GetControlsGroup();
            controls["nameTextBox"].Text = "";
            controls["genderCombo"].Text = "";
            controls["levelTextBox"].Text = "";
            controls["hpTextBox"].Text = "";
            controls["monsterTextBox"].Text = "";
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            var sortingGroup = Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<GroupBox>().First()
                .Controls.OfType<TableLayoutPanel>().First()
                .Controls.OfType<GroupBox>().Last();

            var propertyCombo = sortingGroup.Controls.OfType<FlowLayoutPanel>().First()
                .Controls.OfType<ComboBox>().First();
            var ascRadio = sortingGroup.Controls.OfType<FlowLayoutPanel>().First()
                .Controls.OfType<RadioButton>().First();

            var propertyName = propertyCombo.Text;
            var ascending = ascRadio.Checked;
            var property = typeof(Warrior).GetProperty(propertyName);

            if (ascending)
                _warriors = _warriors.OrderBy(w => property.GetValue(w)).ToList();
            else
                _warriors = _warriors.OrderByDescending(w => property.GetValue(w)).ToList();

            RefreshControls();
        }
    }
}
