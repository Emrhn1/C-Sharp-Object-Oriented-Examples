using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1
{
   public class Project_Manager : Employee
    {
        public int ProjectCount { get; set; }
        public Project_Manager(int id,string name,int age,int salary,int projectCount):base(id,name,age,salary)
        {
            this.ProjectCount = projectCount;
        }

        public override void IncreaseSalary(int salary)
        {
            salary += ProjectCount * 10;
            base.IncreaseSalary(salary);
            
           
        }
        override public string ToString()
        {
            return base.ToString() + $", Project Count: {ProjectCount}";
        }
    }
   
}
