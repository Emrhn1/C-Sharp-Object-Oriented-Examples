using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
    public class Wolf : Pet, ICarnivorous
    {
        public Wolf(string name,int age, int weigth):base(name,age,weigth)
        {
            
        }
        public void EatMeat()
        {
            Console.WriteLine(getName(),"is eating meat");
        }

        public void FindFood()
        {
            Console.WriteLine(getName(), "is hunting");
        }
    }
}
