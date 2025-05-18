using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1
{
   public class Analyst: Employee
    {
        public int customerCount { get; set; }
        public Analyst(int id,string name,int age,int salary,int customerCount):base(id,name,age,salary)
        {
            this.customerCount = customerCount;
        }
        public override void IncreaseSalary(int salary)
        {
            salary += customerCount * 5;
            base.IncreaseSalary(salary);
            
        }
        public override string ToString()
        {
            return base.ToString() + $", Customer Count: {customerCount}";
        }
    }
}
