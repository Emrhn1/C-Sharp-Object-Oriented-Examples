using System;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Core.Models
{
    public class BorrowRecord : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
        public decimal? LateFee { get; set; }

        public BorrowRecord()
        {
            BorrowDate = DateTime.Now;
            DueDate = BorrowDate.AddDays(14); // Default loan period is 14 days
        }

        public BorrowRecord(Book book, Member member) : this()
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            BookId = book.Id;
            MemberId = member.Id;
        }
    }
} 