using System;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class EditMemberForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly Member _member;

        private TextBox _txtFirstName;
        private TextBox _txtLastName;
        private TextBox _txtEmail;
        private TextBox _txtPhone;
        private CheckBox _chkActive;

        public EditMemberForm(ILibraryService libraryService, Member member)
        {
            _libraryService = libraryService;
            _member = member;

            InitializeUI();
            LoadMemberData();
        }

        private void InitializeUI()
        {
            Text = "Edit Member";
            Size = new System.Drawing.Size(400, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 6,
                ColumnCount = 2
            };

            // First Name
            panel.Controls.Add(new Label { Text = "First Name:" }, 0, 0);
            _txtFirstName = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(_txtFirstName, 1, 0);

            // Last Name
            panel.Controls.Add(new Label { Text = "Last Name:" }, 0, 1);
            _txtLastName = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(_txtLastName, 1, 1);

            // Email
            panel.Controls.Add(new Label { Text = "Email:" }, 0, 2);
            _txtEmail = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(_txtEmail, 1, 2);

            // Phone
            panel.Controls.Add(new Label { Text = "Phone:" }, 0, 3);
            _txtPhone = new TextBox { Dock = DockStyle.Fill };
            panel.Controls.Add(_txtPhone, 1, 3);

            // Active
            panel.Controls.Add(new Label { Text = "Active:" }, 0, 4);
            _chkActive = new CheckBox { Dock = DockStyle.Fill };
            panel.Controls.Add(_chkActive, 1, 4);

            // Buttons
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel
            };
            buttonPanel.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Text = "Save",
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            panel.Controls.Add(buttonPanel, 1, 5);

            Controls.Add(panel);
        }

        private void LoadMemberData()
        {
            _txtFirstName.Text = _member.FirstName;
            _txtLastName.Text = _member.LastName;
            _txtEmail.Text = _member.Email;
            _txtPhone.Text = _member.PhoneNumber;
            _chkActive.Checked = _member.IsActive;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtFirstName.Text) || 
                string.IsNullOrWhiteSpace(_txtLastName.Text) || 
                string.IsNullOrWhiteSpace(_txtEmail.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                _member.FirstName = _txtFirstName.Text;
                _member.LastName = _txtLastName.Text;
                _member.Email = _txtEmail.Text;
                _member.PhoneNumber = _txtPhone.Text;
                _member.IsActive = _chkActive.Checked;

                _libraryService.UpdateMember(_member);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving member: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
} 