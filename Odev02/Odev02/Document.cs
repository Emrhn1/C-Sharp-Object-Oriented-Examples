using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
    public abstract class Document
    {
        public Document() { }

        protected Document(string Isbn, string title, int PublicationYear, int NumberofPages)
        {
            this.Isbn = Isbn;
            this.title = title;
            this.PublicationYear = PublicationYear;
            this.NumberofPages = NumberofPages;
        }
        public string Isbn { get; set; }
        public string title { get; set; }
        public int PublicationYear { get; set; }
        public int NumberofPages { get; set; }

        public override string ToString()
        {
            return $"ISBN: {Isbn}, Title: {title}, Publication Year: {PublicationYear}, Number of Pages: {NumberofPages}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Document otherDocument)
            {
                return this.ToString() == otherDocument.ToString();
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public virtual string Print()
        {
            return $"Printing a document of type: {this.GetType().Name}, Title: {title}";
        }

    }
}

