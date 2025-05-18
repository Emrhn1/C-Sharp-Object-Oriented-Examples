using System;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class BooksForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly DataGridView _gridBooks;
        private readonly TextBox _txtSearch;

        public BooksForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;

            // Initialize controls
            _gridBooks = new DataGridView();
            _txtSearch = new TextBox();
            
            InitializeUI();
            LoadBooks();
        }

        private void InitializeUI()
        {
            Text = "Books Management";
            Size = new System.Drawing.Size(800, 600);
            StartPosition = FormStartPosition.CenterParent;

            // Create search box
            _txtSearch.Dock = DockStyle.Top;
            _txtSearch.PlaceholderText = "Search books by title or author...";
            _txtSearch.TextChanged += TxtSearch_TextChanged;
            Controls.Add(_txtSearch);

            // Create grid
            _gridBooks.Dock = DockStyle.Fill;
            _gridBooks.AutoGenerateColumns = false;
            _gridBooks.AllowUserToAddRows = false;
            _gridBooks.AllowUserToDeleteRows = false;
            _gridBooks.ReadOnly = true;
            _gridBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridBooks.MultiSelect = false;

            _gridBooks.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id" },
                new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Title", DataPropertyName = "Title", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Author", DataPropertyName = "Author", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "ISBN", HeaderText = "ISBN", DataPropertyName = "ISBN", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "PublicationYear", HeaderText = "Year", DataPropertyName = "PublicationYear", Width = 70 },
                new DataGridViewTextBoxColumn { Name = "Publisher", HeaderText = "Publisher", DataPropertyName = "Publisher", Width = 150 },
                new DataGridViewCheckBoxColumn { Name = "IsAvailable", HeaderText = "Available", DataPropertyName = "IsAvailable", Width = 70 },
                new DataGridViewTextBoxColumn { Name = "Genre", HeaderText = "Genre", DataPropertyName = "Genre", Width = 100 }
            });

            Controls.Add(_gridBooks);

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

            Controls.Add(buttonPanel);
        }

        private void LoadBooks()
        {
            _gridBooks.DataSource = _libraryService.GetAllBooks().ToList();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var searchText = _txtSearch.Text.ToLower();
            var filteredBooks = _libraryService.SearchBooks(b =>
                (b.Title?.ToLower() ?? "").Contains(searchText) ||
                (b.Author?.ToLower() ?? "").Contains(searchText) ||
                (b.ISBN?.ToLower() ?? "").Contains(searchText));
            
            _gridBooks.DataSource = filteredBooks.ToList();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_gridBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = (Book)_gridBooks.SelectedRows[0].DataBoundItem;
            var form = new EditBookForm(_libraryService, book);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_gridBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = (Book)_gridBooks.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Are you sure you want to delete '{book.Title}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _libraryService.DeleteBook(book.Id);
                    LoadBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
} 