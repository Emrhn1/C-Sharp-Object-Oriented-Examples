using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
   public class InvalidPublicationYearException: Exception
    {
        public InvalidPublicationYearException(int publicationYear)
        : base($"Invalid publication year: {publicationYear}.The invention of the printing press cannot be a year before 1440.")
        {
        }

    }
}
