using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Odev1
{
   public abstract class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public int salary { get; set; }

        public Employee(int id,string name,int age,int salary)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.salary = salary;
        }
        
        public virtual void IncreaseSalary(int salary)
        {
            this.salary += salary;
        }
        public override string ToString()
        {
            return $"ID: {id}, Name: {name}, Age: {age}, Salary: {salary}";
        }
    }
}
