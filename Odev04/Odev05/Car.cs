using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev05
{
    public class Car
    {
        public string brand { get; set; }
        public int price { get; set; }

        public Car(string brand, decimal price)
        {
            brand = brand;
            price = price;
        }
        public int CompareTo(Car other)
        {
            if(other == null) return 1;

            return price.CompareTo(other.price);
        }

        public override string ToString()
        {
            
            return "Brand: " + brand + " Price: " + price;
        }
        
    }
}
