using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class MembersForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly DataGridView _gridMembers;
        private readonly TextBox _txtSearch;

        public MembersForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;

            // Initialize controls
            _gridMembers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            _txtSearch = new TextBox
            {
                Dock = DockStyle.Top,
                PlaceholderText = "Search members by name or email..."
            };

            InitializeUI();
            LoadMembers();
        }

        private void InitializeUI()
        {
            Text = "Members Management";
            Size = new System.Drawing.Size(800, 600);
            StartPosition = FormStartPosition.CenterParent;

            // Setup search box
            _txtSearch.TextChanged += TxtSearch_TextChanged;
            Controls.Add(_txtSearch);

            // Setup grid columns
            _gridMembers.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id" },
                new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "First Name", DataPropertyName = "FirstName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Last Name", DataPropertyName = "LastName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "PhoneNumber", HeaderText = "Phone", DataPropertyName = "PhoneNumber", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "MembershipDate", HeaderText = "Member Since", DataPropertyName = "MembershipDate", Width = 100 },
                new DataGridViewCheckBoxColumn { Name = "IsActive", HeaderText = "Active", DataPropertyName = "IsActive", Width = 70 }
            });

            Controls.Add(_gridMembers);

            // Create button panel
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                Padding = new Padding(5),
                FlowDirection = FlowDirection.RightToLeft
            };

            var btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel
            };
            buttonPanel.Controls.Add(btnClose);

            var btnDelete = new Button
            {
                Text = "Delete",
                Margin = new Padding(5, 0, 0, 0)
            };
            btnDelete.Click += BtnDelete_Click;
            buttonPanel.Controls.Add(btnDelete);

            var btnEdit = new Button
            {
                Text = "Edit",
                Margin = new Padding(5, 0, 0, 0)
            };
            btnEdit.Click += BtnEdit_Click;
            buttonPanel.Controls.Add(btnEdit);

            var btnViewHistory = new Button
            {
                Text = "View History",
                Margin = new Padding(5, 0, 0, 0)
            };
            btnViewHistory.Click += BtnViewHistory_Click;
            buttonPanel.Controls.Add(btnViewHistory);

            Controls.Add(buttonPanel);
        }

        private void LoadMembers()
        {
            _gridMembers.DataSource = _libraryService.GetAllMembers().ToList();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var searchText = _txtSearch.Text.ToLower();
            var filteredMembers = _libraryService.SearchMembers(m =>
                (m.FirstName?.ToLower() ?? "").Contains(searchText) ||
                (m.LastName?.ToLower() ?? "").Contains(searchText) ||
                (m.Email?.ToLower() ?? "").Contains(searchText));

            _gridMembers.DataSource = filteredMembers.ToList();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_gridMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var member = (Member)_gridMembers.SelectedRows[0].DataBoundItem;
            var form = new EditMemberForm(_libraryService, member);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMembers();
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_gridMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var member = (Member)_gridMembers.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Are you sure you want to delete member '{member.FullName}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _libraryService.DeleteMember(member.Id);
                    LoadMembers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnViewHistory_Click(object? sender, EventArgs e)
        {
            if (_gridMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to view history.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var member = (Member)_gridMembers.SelectedRows[0].DataBoundItem;
            var form = new MemberHistoryForm(_libraryService, member);
            form.ShowDialog();
        }
    }
} 