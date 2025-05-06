using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class ReturnBookForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly DataGridView _gridBorrowedBooks;
        private readonly ComboBox _cboMembers;

        public ReturnBookForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            
            // Initialize controls
            _gridBorrowedBooks = new DataGridView();
            _cboMembers = new ComboBox();

            InitializeUI();
            LoadMembers();
        }

        private void InitializeUI()
        {
            Text = "Return Book";
            Size = new System.Drawing.Size(800, 500);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 3,
                ColumnCount = 2
            };

            // Member selection
            mainPanel.Controls.Add(new Label { Text = "Member:" }, 0, 0);
            _cboMembers.Dock = DockStyle.Fill;
            _cboMembers.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboMembers.SelectedIndexChanged += CboMembers_SelectedIndexChanged;
            mainPanel.Controls.Add(_cboMembers, 1, 0);

            // Borrowed books grid
            _gridBorrowedBooks.Dock = DockStyle.Fill;
            _gridBorrowedBooks.AutoGenerateColumns = false;
            _gridBorrowedBooks.AllowUserToAddRows = false;
            _gridBorrowedBooks.AllowUserToDeleteRows = false;
            _gridBorrowedBooks.ReadOnly = true;
            _gridBorrowedBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridBorrowedBooks.MultiSelect = false;

            _gridBorrowedBooks.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Book Title", DataPropertyName = "Book.Title", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", DataPropertyName = "BorrowDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", DataPropertyName = "DueDate", Width = 120 }
            });

            mainPanel.SetColumnSpan(_gridBorrowedBooks, 2);
            mainPanel.Controls.Add(_gridBorrowedBooks, 0, 1);

            // Buttons
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            var btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel
            };
            buttonPanel.Controls.Add(btnClose);

            var btnReturn = new Button
            {
                Text = "Return Selected Book",
                Enabled = false
            };
            btnReturn.Click += BtnReturn_Click;
            buttonPanel.Controls.Add(btnReturn);

            mainPanel.SetColumnSpan(buttonPanel, 2);
            mainPanel.Controls.Add(buttonPanel, 0, 2);

            Controls.Add(mainPanel);

            // Enable/disable return button based on selection
            _gridBorrowedBooks.SelectionChanged += (s, e) =>
            {
                btnReturn.Enabled = _gridBorrowedBooks.SelectedRows.Count > 0;
            };
        }

        private void LoadMembers()
        {
            var members = _libraryService.GetAllMembers()
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.FirstName)
                .ToList();

            _cboMembers.DisplayMember = "FullName";
            _cboMembers.ValueMember = "Id";
            _cboMembers.DataSource = members;
        }

        private void CboMembers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_cboMembers.SelectedItem == null)
                return;

            var memberId = (int)_cboMembers.SelectedValue;
            var borrowedBooks = _libraryService.GetBorrowHistory(memberId)
                .Where(b => !b.IsReturned)
                .OrderBy(b => b.DueDate)
                .ToList();

            _gridBorrowedBooks.DataSource = borrowedBooks;
        }

        private void BtnReturn_Click(object? sender, EventArgs e)
        {
            if (_gridBorrowedBooks.SelectedRows.Count == 0)
                return;

            var borrowRecord = (BorrowRecord)_gridBorrowedBooks.SelectedRows[0].DataBoundItem;

            try
            {
                _libraryService.ReturnBook(borrowRecord.MemberId, borrowRecord.BookId);
                
                // Show late fee if applicable
                if (borrowRecord.LateFee.HasValue && borrowRecord.LateFee.Value > 0)
                {
                    MessageBox.Show($"Book returned successfully. Late fee: ${borrowRecord.LateFee.Value:F2}",
                        "Book Returned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Book returned successfully.", "Book Returned",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Refresh the grid
                CboMembers_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning book: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 