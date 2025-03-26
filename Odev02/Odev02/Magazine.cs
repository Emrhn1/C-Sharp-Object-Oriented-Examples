using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
    public class Magazine : Document
    {
     
        public int IssueNumber { get; set; }  
        public FrequencyType Frequency { get; set; } 

        public Magazine()
        {
            IssueNumber = 0;
            Frequency = FrequencyType.Unknown;
        }
        public Magazine(string Isbn, string title, int PublicationYear, int NumberofPages, int issueNumber, FrequencyType frequency)
            : base(Isbn,title,PublicationYear,NumberofPages)
        {
            IssueNumber = issueNumber;
            Frequency = frequency;
        }
        public override string Print()
        {
            return base.Print();
            return $"Printing a document of type: {this.GetType().Name}, Title: {title}, Issue Number: {IssueNumber}, Frequency: {Frequency}";
        }
    }
    public enum FrequencyType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Unknown // Varsayılan değer için
    }
    
}
