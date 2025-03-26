using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev02
{
   public class Volume: Document
    {
        public Volume()
        {
            this.VolumeNumber = 0;
            this.TotalVolumes = 0;
        }
        public Volume(string isbn, string title, int PublicationYear, int NumberofPages, int volumeNumber, int totalVolumes)
        : base(isbn, title, PublicationYear, NumberofPages)
        {
           this.VolumeNumber = volumeNumber;
           this.TotalVolumes = totalVolumes;
        }
        public int VolumeNumber { get; set; }
        public int TotalVolumes { get; set; }

        public override string Print()
        {
            return base.Print();
            return $"Printing a document of type: {this.GetType().Name}, Title: {title}, Volume Number: {VolumeNumber}, Total Volumes: {TotalVolumes}";
        }
    }
}
