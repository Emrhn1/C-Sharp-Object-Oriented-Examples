using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
   public class Sheep:Pet , IPlants
    {
        public Sheep(string name,int age,int weigth):base(name,age,weigth)
        {
            
        }

        public void EatPlant()
        {
            Console.WriteLine(getName(), "is eating plant");
        }

        public void FindFood()
        {
            Console.WriteLine(getName(),"is hunting");
        }

    }
}
