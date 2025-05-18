using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class MemberHistoryForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly Member _member;
        private DataGridView _gridHistory;

        public MemberHistoryForm(ILibraryService libraryService, Member member)
        {
            _libraryService = libraryService;
            _member = member;

            InitializeUI();
            LoadHistory();
        }

        private void InitializeUI()
        {
            Text = $"Borrowing History - {_member.FullName}";
            Size = new System.Drawing.Size(800, 400);
            StartPosition = FormStartPosition.CenterParent;

            // Create grid
            _gridHistory = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            _gridHistory.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Book Title", DataPropertyName = "Book.Title", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", DataPropertyName = "BorrowDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", DataPropertyName = "DueDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ReturnDate", HeaderText = "Return Date", DataPropertyName = "ReturnDate", Width = 120 },
                new DataGridViewCheckBoxColumn { Name = "IsReturned", HeaderText = "Returned", DataPropertyName = "IsReturned", Width = 70 },
                new DataGridViewTextBoxColumn { Name = "LateFee", HeaderText = "Late Fee", DataPropertyName = "LateFee", Width = 100 }
            });

            Controls.Add(_gridHistory);

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

            Controls.Add(buttonPanel);
        }

        private void LoadHistory()
        {
            var history = _libraryService.GetBorrowHistory(_member.Id)
                .OrderByDescending(h => h.BorrowDate)
                .ToList();

            _gridHistory.DataSource = history;
        }
    }
} 