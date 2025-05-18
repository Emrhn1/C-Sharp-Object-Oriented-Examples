using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
    public class Piggy : Pet, ICarnivorous, IPlants
    {
        public Piggy(string name,int age,int weigth):base(name, age, weigth)
        {
            
        }
        public void EatMeat()
        {
            Console.WriteLine(getName(),"is eating meat");
        }

        public void EatPlant()
        {
            Console.WriteLine(getName(),"is eating plant");
        }

        void IPlants.FindFood()
        {
            Console.WriteLine(getName(),"is looking for vegetables");
        }
        void ICarnivorous.FindFood()
        {
            Console.WriteLine(getName(),"is looking for meat");
        }

    }
}
