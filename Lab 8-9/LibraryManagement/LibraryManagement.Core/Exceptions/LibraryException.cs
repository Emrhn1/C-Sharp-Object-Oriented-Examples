using System;

namespace LibraryManagement.Core.Exceptions
{
    public class LibraryException : Exception
    {
        public LibraryException() : base() { }
        public LibraryException(string message) : base(message) { }
        public LibraryException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BookNotFoundException : LibraryException
    {
        public BookNotFoundException(int bookId) 
            : base($"Book with ID {bookId} was not found.") { }
    }

    public class MemberNotFoundException : LibraryException
    {
        public MemberNotFoundException(int memberId) 
            : base($"Member with ID {memberId} was not found.") { }
    }

    public class BookNotAvailableException : LibraryException
    {
        public BookNotAvailableException(int bookId) 
            : base($"Book with ID {bookId} is not available for borrowing.") { }
    }

    public class InvalidOperationException : LibraryException
    {
        public InvalidOperationException(string message) : base(message) { }
    }
} 