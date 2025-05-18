using System;
using System.Windows.Forms;
using System.IO;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Services;
using LibraryManagement.Core.Models;
using LibraryManagement.App.Forms;

namespace LibraryManagement.App
{
    public partial class MainForm : Form
    {
        private readonly ILibraryService _libraryService;
        private readonly JsonDataService _dataService;
        private readonly string _dataDirectory;

        public MainForm()
        {
            // Initialize form
            Text = "Library Management System";
            WindowState = FormWindowState.Maximized;

            // Setup data directory
            _dataDirectory = Path.Combine(Application.StartupPath, "Data");
            Directory.CreateDirectory(_dataDirectory); // Ensure directory exists
            _dataService = new JsonDataService(_dataDirectory);

            // Initialize repositories
            var bookRepo = new JsonRepository<Book>(_dataService.LoadBooks(), _dataService.SaveBooks);
            var memberRepo = new JsonRepository<Member>(_dataService.LoadMembers(), _dataService.SaveMembers);
            var borrowRepo = new JsonRepository<BorrowRecord>(_dataService.LoadBorrowRecords(), _dataService.SaveBorrowRecords);

            // Initialize library service
            _libraryService = new LibraryService(bookRepo, memberRepo, borrowRepo);

            InitializeUI();
        }

        private void InitializeUI()
        {
            // Create menu strip
            var menuStrip = new MenuStrip();
            
            // File menu
            var fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("Save", null, SaveData_Click);
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => Close());
            
            // Books menu
            var booksMenu = new ToolStripMenuItem("Books");
            booksMenu.DropDownItems.Add("View All Books", null, ViewBooks_Click);
            booksMenu.DropDownItems.Add("Add New Book", null, AddBook_Click);
            
            // Members menu
            var membersMenu = new ToolStripMenuItem("Members");
            membersMenu.DropDownItems.Add("View All Members", null, ViewMembers_Click);
            membersMenu.DropDownItems.Add("Add New Member", null, AddMember_Click);
            
            // Borrowing menu
            var borrowingMenu = new ToolStripMenuItem("Borrowing");
            borrowingMenu.DropDownItems.Add("Borrow Book", null, BorrowBook_Click);
            borrowingMenu.DropDownItems.Add("Return Book", null, ReturnBook_Click);
            borrowingMenu.DropDownItems.Add("View Overdue Books", null, ViewOverdueBooks_Click);

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, booksMenu, membersMenu, borrowingMenu });
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void SaveData_Click(object? sender, EventArgs e)
        {
            try
            {
                _libraryService.SaveChanges();
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewBooks_Click(object? sender, EventArgs e)
        {
            var form = new BooksForm(_libraryService);
            form.ShowDialog();
        }

        private void AddBook_Click(object? sender, EventArgs e)
        {
            var form = new AddBookForm(_libraryService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ViewMembers_Click(object? sender, EventArgs e)
        {
            var form = new MembersForm(_libraryService);
            form.ShowDialog();
        }

        private void AddMember_Click(object? sender, EventArgs e)
        {
            var form = new AddMemberForm(_libraryService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BorrowBook_Click(object? sender, EventArgs e)
        {
            var form = new BorrowBookForm(_libraryService);
            form.ShowDialog();
        }

        private void ReturnBook_Click(object? sender, EventArgs e)
        {
            var form = new ReturnBookForm(_libraryService);
            form.ShowDialog();
        }

        private void ViewOverdueBooks_Click(object? sender, EventArgs e)
        {
            var form = new OverdueBooksForm(_libraryService);
            form.ShowDialog();
        }
    }
} 