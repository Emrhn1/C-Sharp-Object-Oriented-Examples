using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
   public class Book: Document
    {
        public Book()
        {
            this.author = "";
        }
        public Book(string Isbn,string title,int PublicationYear,int NumberofPages,string author)
            :base(Isbn, title, PublicationYear, NumberofPages)
        {
           this.author = author;
        }
        public string author { get; set; }

        public override string Print()
        {
            return base.Print();
            return $"Printing a document of type: {this.GetType().Name}, Title: {title}, Author: {author}";
        }
    }
}
