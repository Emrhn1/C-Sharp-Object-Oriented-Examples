using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
   public class InvalidVolumeException: Exception
    {
        public InvalidVolumeException(int volumeNumber, int totalVolumes)
        : base($"Invalid volume number: {volumeNumber}. cannot exceed the total number of volumes ({totalVolumes}).")
        {
        }

    }
}
