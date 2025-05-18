using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
   public abstract class Pet
    {
        public string name { get; set; }
        public int age { get; set; }
        public int weigth { get; set; }

        protected Pet(string name,int age,int weigth)
        {
            this.name = name;
            this.age = age;
            this.weigth = weigth;
        }
        public string getName()
        {
            return name;
        }
    }
}
