using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class OverdueBooksForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly DataGridView _gridOverdueBooks;

        public OverdueBooksForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            _gridOverdueBooks = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            InitializeUI();
            LoadOverdueBooks();
        }

        private void InitializeUI()
        {
            Text = "Overdue Books";
            Size = new System.Drawing.Size(800, 400);
            StartPosition = FormStartPosition.CenterParent;

            _gridOverdueBooks.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Book Title", DataPropertyName = "Book.Title", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Member", DataPropertyName = "Member.FullName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", DataPropertyName = "BorrowDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", DataPropertyName = "DueDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "DaysOverdue", HeaderText = "Days Overdue", Width = 100 }
            });

            Controls.Add(_gridOverdueBooks);

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

        private void LoadOverdueBooks()
        {
            var overdueBooks = _libraryService.GetOverdueBooks()
                .Select(b => new
                {
                    b.Book,
                    b.Member,
                    b.BorrowDate,
                    b.DueDate,
                    DaysOverdue = (DateTime.Now - b.DueDate).Days
                })
                .OrderByDescending(b => b.DaysOverdue)
                .ToList();

            _gridOverdueBooks.DataSource = overdueBooks;
        }
    }
} 