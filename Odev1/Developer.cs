using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1
{
   public class Developer: Employee
    {
        public int UsedTechnologyCount { get; set; }
        public Developer(int id,string name,int age,int salary,int UsedTechnologyCount) : base(id, name, age, salary)
        {
            this.UsedTechnologyCount = UsedTechnologyCount;
        }
        override public void IncreaseSalary(int salary)
        {
            salary += UsedTechnologyCount * 30;
            base.IncreaseSalary(salary);
        }
        override public string ToString()
        {
            return base.ToString() + $", Used Technology Count: {UsedTechnologyCount}";
        }
    }
}
