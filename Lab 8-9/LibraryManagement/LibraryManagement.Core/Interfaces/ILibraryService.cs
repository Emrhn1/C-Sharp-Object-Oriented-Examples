using System;
using System.Collections.Generic;
using LibraryManagement.Core.Models;

namespace LibraryManagement.Core.Interfaces
{
    public interface ILibraryService
    {
        // Book operations
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        IEnumerable<Book> SearchBooks(Func<Book, bool> predicate);

        // Member operations
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int id);
        IEnumerable<Member> SearchMembers(Func<Member, bool> predicate);

        // Borrowing operations
        void BorrowBook(int memberId, int bookId);
        void ReturnBook(int memberId, int bookId);
        IEnumerable<BorrowRecord> GetBorrowHistory(int memberId);
        IEnumerable<BorrowRecord> GetOverdueBooks();
        
        // Save changes
        void SaveChanges();
    }
} 