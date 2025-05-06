using System;
using System.Windows.Forms;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;

namespace LibraryManagement.App.Forms
{
    public partial class EditBookForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly Book _book;

        private readonly TextBox _txtTitle;
        private readonly TextBox _txtAuthor;
        private readonly TextBox _txtISBN;
        private readonly NumericUpDown _numYear;
        private readonly TextBox _txtPublisher;
        private readonly TextBox _txtGenre;
        private readonly CheckBox _chkAvailable;

        public EditBookForm(ILibraryService libraryService, Book book)
        {
            _libraryService = libraryService;
            _book = book;

            // Initialize controls
            _txtTitle = new TextBox();
            _txtAuthor = new TextBox();
            _txtISBN = new TextBox();
            _numYear = new NumericUpDown();
            _txtPublisher = new TextBox();
            _txtGenre = new TextBox();
            _chkAvailable = new CheckBox();

            InitializeUI();
            LoadBookData();
        }

        private void InitializeUI()
        {
            Text = "Edit Book";
            Size = new System.Drawing.Size(400, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RowCount = 8,
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

            // Available
            panel.Controls.Add(new Label { Text = "Available:" }, 0, 6);
            _chkAvailable.Dock = DockStyle.Fill;
            panel.Controls.Add(_chkAvailable, 1, 6);

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

            panel.Controls.Add(buttonPanel, 1, 7);

            Controls.Add(panel);
        }

        private void LoadBookData()
        {
            _txtTitle.Text = _book.Title ?? "";
            _txtAuthor.Text = _book.Author ?? "";
            _txtISBN.Text = _book.ISBN ?? "";
            _numYear.Value = _book.PublicationYear;
            _txtPublisher.Text = _book.Publisher ?? "";
            _txtGenre.Text = _book.Genre ?? "";
            _chkAvailable.Checked = _book.IsAvailable;
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
                _book.Title = _txtTitle.Text;
                _book.Author = _txtAuthor.Text;
                _book.ISBN = _txtISBN.Text;
                _book.PublicationYear = (int)_numYear.Value;
                _book.Publisher = _txtPublisher.Text;
                _book.Genre = _txtGenre.Text;
                _book.IsAvailable = _chkAvailable.Checked;

                _libraryService.UpdateBook(_book);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving book: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
} 