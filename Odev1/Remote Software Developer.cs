using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1
{
   public class Remote_Software_Developer: Developer
    {
        public int distance { get; set; }
        public Remote_Software_Developer(int id, string name, int age, int salary, int technologyCount, int distance)
        : base(id, name, age, salary, technologyCount)
        {
            this.distance = distance;
        }
        public override void IncreaseSalary(int salary)
        {
            salary += distance * 2;
            base.IncreaseSalary(salary);
            

        }
        override public string ToString()
        {
            return base.ToString() + $", Distance: {distance} km";
        }

    }
}
