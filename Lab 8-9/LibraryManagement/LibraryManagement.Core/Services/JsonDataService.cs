using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryManagement.Core.Models;

namespace LibraryManagement.Core.Services
{
    public class JsonDataService
    {
        private readonly string _dataDirectory;
        private const string BOOKS_FILE = "books.json";
        private const string MEMBERS_FILE = "members.json";
        private const string BORROWS_FILE = "borrows.json";

        public JsonDataService(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
            Directory.CreateDirectory(_dataDirectory);
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(_dataDirectory, fileName);
        }

        private void SaveToJson<T>(IEnumerable<T> data, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(GetFullPath(fileName), json);
        }

        private IEnumerable<T> LoadFromJson<T>(string fileName)
        {
            var path = GetFullPath(fileName);
            if (!File.Exists(path))
                return new List<T>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? new List<T>();
        }

        public void SaveBooks(IEnumerable<Book> books)
        {
            SaveToJson(books, BOOKS_FILE);
        }

        public void SaveMembers(IEnumerable<Member> members)
        {
            SaveToJson(members, MEMBERS_FILE);
        }

        public void SaveBorrowRecords(IEnumerable<BorrowRecord> borrowRecords)
        {
            SaveToJson(borrowRecords, BORROWS_FILE);
        }

        public IEnumerable<Book> LoadBooks()
        {
            return LoadFromJson<Book>(BOOKS_FILE);
        }

        public IEnumerable<Member> LoadMembers()
        {
            return LoadFromJson<Member>(MEMBERS_FILE);
        }

        public IEnumerable<BorrowRecord> LoadBorrowRecords()
        {
            return LoadFromJson<BorrowRecord>(BORROWS_FILE);
        }
    }
} 