using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Models;
using LibraryManagement.Core.Exceptions;

namespace LibraryManagement.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<BorrowRecord> _borrowRepository;

        public LibraryService(
            IRepository<Book> bookRepository,
            IRepository<Member> memberRepository,
            IRepository<BorrowRecord> borrowRepository)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _borrowRepository = borrowRepository;
        }

        // Book operations
        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();

        public Book GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                throw new BookNotFoundException(id);
            return book;
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Book title is required.", nameof(book));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Book author is required.", nameof(book));

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("Book ISBN is required.", nameof(book));

            _bookRepository.Add(book);
            SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Book title is required.", nameof(book));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Book author is required.", nameof(book));

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("Book ISBN is required.", nameof(book));

            _bookRepository.Update(book);
            SaveChanges();
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
            SaveChanges();
        }

        public IEnumerable<Book> SearchBooks(Func<Book, bool> predicate)
        {
            return _bookRepository.GetAll().Where(predicate);
        }

        // Member operations
        public IEnumerable<Member> GetAllMembers() => _memberRepository.GetAll();

        public Member GetMemberById(int id)
        {
            var member = _memberRepository.GetById(id);
            if (member == null)
                throw new MemberNotFoundException(id);
            return member;
        }

        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (string.IsNullOrWhiteSpace(member.FirstName))
                throw new ArgumentException("Member first name is required.", nameof(member));

            if (string.IsNullOrWhiteSpace(member.LastName))
                throw new ArgumentException("Member last name is required.", nameof(member));

            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("Member email is required.", nameof(member));

            _memberRepository.Add(member);
            SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (string.IsNullOrWhiteSpace(member.FirstName))
                throw new ArgumentException("Member first name is required.", nameof(member));

            if (string.IsNullOrWhiteSpace(member.LastName))
                throw new ArgumentException("Member last name is required.", nameof(member));

            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("Member email is required.", nameof(member));

            _memberRepository.Update(member);
            SaveChanges();
        }

        public void DeleteMember(int id)
        {
            _memberRepository.Delete(id);
            SaveChanges();
        }

        public IEnumerable<Member> SearchMembers(Func<Member, bool> predicate)
        {
            return _memberRepository.GetAll().Where(predicate);
        }

        // Borrowing operations
        public void BorrowBook(int memberId, int bookId)
        {
            var member = GetMemberById(memberId);
            var book = GetBookById(bookId);

            if (!book.IsAvailable)
                throw new BookNotAvailableException(bookId);

            if (!member.IsActive)
                throw new LibraryManagement.Core.Exceptions.InvalidOperationException($"Member {member.FullName} is not active.");

            var borrowRecord = new BorrowRecord(book, member);
            book.IsAvailable = false;
            _borrowRepository.Add(borrowRecord);
            _bookRepository.Update(book);
            SaveChanges();
        }

        public void ReturnBook(int memberId, int bookId)
        {
            var borrowRecord = _borrowRepository.GetAll()
                .FirstOrDefault(br => br.BookId == bookId && br.MemberId == memberId && !br.IsReturned);

            if (borrowRecord == null)
                throw new LibraryManagement.Core.Exceptions.InvalidOperationException("No active borrow record found for this book and member.");

            var book = GetBookById(bookId);
            
            borrowRecord.ReturnDate = DateTime.Now;
            
            // Calculate late fee if returned after due date
            if (borrowRecord.ReturnDate > borrowRecord.DueDate)
            {
                var daysLate = (borrowRecord.ReturnDate.Value - borrowRecord.DueDate).Days;
                borrowRecord.LateFee = daysLate * 1.0m; // $1 per day late fee
            }

            book.IsAvailable = true;
            _bookRepository.Update(book);
            SaveChanges();
        }

        public IEnumerable<BorrowRecord> GetBorrowHistory(int memberId)
        {
            return _borrowRepository.GetAll().Where(br => br.MemberId == memberId);
        }

        public IEnumerable<BorrowRecord> GetOverdueBooks()
        {
            return _borrowRepository.GetAll()
                .Where(br => !br.IsReturned && br.DueDate < DateTime.Now);
        }

        public void SaveChanges()
        {
            _bookRepository.SaveChanges();
            _memberRepository.SaveChanges();
            _borrowRepository.SaveChanges();
        }
    }
} 