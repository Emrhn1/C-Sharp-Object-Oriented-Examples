using System;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class AddBookForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly TextBox _txtTitle;
        private readonly TextBox _txtAuthor;
        private readonly TextBox _txtISBN;
        private readonly NumericUpDown _numYear;
        private readonly TextBox _txtPublisher;
        private readonly TextBox _txtGenre;

        public AddBookForm(ILibraryService libraryService)
        {
            _libraryService = libraryService;

            // Initialize controls
            _txtTitle = new TextBox();
            _txtAuthor = new TextBox();
            _txtISBN = new TextBox();
            _numYear = new NumericUpDown();
            _txtPublisher = new TextBox();
            _txtGenre = new TextBox();

            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Add New Book";
            Size = new System.Drawing.Size(400, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 7,
                ColumnCount = 2
            };

            // Title
            panel.Controls.Add(new Label { Text = "Title:" }, 0, 0);
            _txtTitle.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtTitle, 1, 0);

            // Author
            panel.Controls.Add(new Label { Text = "Author:" }, 0, 1);
            _txtAuthor.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtAuthor, 1, 1);

            // ISBN
            panel.Controls.Add(new Label { Text = "ISBN:" }, 0, 2);
            _txtISBN.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtISBN, 1, 2);

            // Publication Year
            panel.Controls.Add(new Label { Text = "Publication Year:" }, 0, 3);
            _numYear.Dock = DockStyle.Fill;
            _numYear.Minimum = 1000;
            _numYear.Maximum = DateTime.Now.Year;
            _numYear.Value = DateTime.Now.Year;
            panel.Controls.Add(_numYear, 1, 3);

            // Publisher
            panel.Controls.Add(new Label { Text = "Publisher:" }, 0, 4);
            _txtPublisher.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtPublisher, 1, 4);

            // Genre
            panel.Controls.Add(new Label { Text = "Genre:" }, 0, 5);
            _txtGenre.Dock = DockStyle.Fill;
            panel.Controls.Add(_txtGenre, 1, 5);

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

            panel.Controls.Add(buttonPanel, 1, 6);

            Controls.Add(panel);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtTitle.Text) || 
                string.IsNullOrWhiteSpace(_txtAuthor.Text) || 
                string.IsNullOrWhiteSpace(_txtISBN.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                var book = new Book
                {
                    Title = _txtTitle.Text,
                    Author = _txtAuthor.Text,
                    ISBN = _txtISBN.Text,
                    PublicationYear = (int)_numYear.Value,
                    Publisher = _txtPublisher.Text,
                    Genre = _txtGenre.Text
                };

                _libraryService.AddBook(book);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
} 