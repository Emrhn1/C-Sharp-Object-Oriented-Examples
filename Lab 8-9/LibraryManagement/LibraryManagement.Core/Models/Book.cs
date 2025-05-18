using System;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Core.Models
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string? Publisher { get; set; }
        public bool IsAvailable { get; set; }
        public string? Genre { get; set; }
        public DateTime DateAdded { get; set; }

        public Book()
        {
            DateAdded = DateTime.Now;
            IsAvailable = true;
        }
    }
} 