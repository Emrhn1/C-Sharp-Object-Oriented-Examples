using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Exceptions;

namespace LibraryManagement.App.Forms
{
    public partial class BorrowBookForm : Form
    {
        private readonly ILibraryService _libraryService;
        private ComboBox _cboMembers;
        private ComboBox _cboBooks;

        public BorrowBookForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            Text = "Borrow Book";
            Size = new System.Drawing.Size(400, 200);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 3,
                ColumnCount = 2
            };

            // Member selection
            panel.Controls.Add(new Label { Text = "Member:" }, 0, 0);
            _cboMembers = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panel.Controls.Add(_cboMembers, 1, 0);

            // Book selection
            panel.Controls.Add(new Label { Text = "Book:" }, 0, 1);
            _cboBooks = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panel.Controls.Add(_cboBooks, 1, 1);

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

            var btnBorrow = new Button
            {
                Text = "Borrow",
                DialogResult = DialogResult.OK
            };
            btnBorrow.Click += BtnBorrow_Click;
            buttonPanel.Controls.Add(btnBorrow);

            panel.Controls.Add(buttonPanel, 1, 2);

            Controls.Add(panel);
        }

        private void LoadData()
        {
            // Load members
            var members = _libraryService.GetAllMembers()
                .Where(m => m.IsActive)
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.FirstName)
                .ToList();

            _cboMembers.DisplayMember = "FullName";
            _cboMembers.ValueMember = "Id";
            _cboMembers.DataSource = members;

            // Load available books
            var books = _libraryService.GetAllBooks()
                .Where(b => b.IsAvailable)
                .OrderBy(b => b.Title)
                .ToList();

            _cboBooks.DisplayMember = "Title";
            _cboBooks.ValueMember = "Id";
            _cboBooks.DataSource = books;
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {
            if (_cboMembers.SelectedItem == null || _cboBooks.SelectedItem == null)
            {
                MessageBox.Show("Please select both a member and a book.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                var memberId = (int)_cboMembers.SelectedValue;
                var bookId = (int)_cboBooks.SelectedValue;

                _libraryService.BorrowBook(memberId, bookId);
                DialogResult = DialogResult.OK;
            }
            catch (BookNotAvailableException)
            {
                MessageBox.Show("This book is no longer available.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                LoadData(); // Refresh the data
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error borrowing book: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
} 