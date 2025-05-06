using System;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class AddMemberForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly TextBox _txtFirstName;
        private readonly TextBox _txtLastName;
        private readonly TextBox _txtEmail;
        private readonly TextBox _txtPhone;

        public AddMemberForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;

            // Initialize controls
            _txtFirstName = new TextBox();
            _txtLastName = new TextBox();
            _txtEmail = new TextBox();
            _txtPhone = new TextBox();

            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Add New Member";
            Size = new System.Drawing.Size(400, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 5,
                ColumnCount = 2
            };

            // First Name
            panel.Controls.Add(new Label { Text = "First Name:" }, 0, 0);
            _txtFirstName.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtFirstName, 1, 0);

            // Last Name
            panel.Controls.Add(new Label { Text = "Last Name:" }, 0, 1);
            _txtLastName.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtLastName, 1, 1);

            // Email
            panel.Controls.Add(new Label { Text = "Email:" }, 0, 2);
            _txtEmail.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtEmail, 1, 2);

            // Phone
            panel.Controls.Add(new Label { Text = "Phone:" }, 0, 3);
            _txtPhone.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtPhone, 1, 3);

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

            panel.Controls.Add(buttonPanel, 1, 4);

            Controls.Add(panel);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
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
                var member = new Member
                {
                    FirstName = _txtFirstName.Text,
                    LastName = _txtLastName.Text,
                    Email = _txtEmail.Text,
                    PhoneNumber = _txtPhone.Text
                };

                _libraryService.AddMember(member);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding member: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
} 