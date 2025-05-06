using System;
using System.Collections.Generic;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Core.Models
{
    public class Member : IEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime MembershipDate { get; set; }
        public bool IsActive { get; set; }
        public List<BorrowRecord> BorrowHistory { get; set; }

        public Member()
        {
            MembershipDate = DateTime.Now;
            IsActive = true;
            BorrowHistory = new List<BorrowRecord>();
        }

        public string FullName => $"{FirstName} {LastName}";
    }
} 