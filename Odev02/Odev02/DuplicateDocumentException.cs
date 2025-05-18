using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
    public class DuplicateDocumentException : Exception
    {
        public DuplicateDocumentException(string isbn)
        : base($"Document is already available: ISBN {isbn}")
        {
        }
    }
}
