using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev05
{
    public class Human: IEnumerable<Car>,IComparable<Human>
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        private List<Car> Cars { get; set; }

        public Human()
        {
            Cars = new List<Car>();
        }
        public Human(string firstName, string lastName, int age, List<Car> cars)
        {
            name = firstName;
            surname = lastName;
            this.age = age;
            Cars = cars ?? new List<Car>();
        }
        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public decimal TotalCarValue => Cars.Sum(c => c.price);

        public override string ToString()
        {
            return $"{name} {surname}, {age} years old. Cars: {string.Join(", ", Cars)}";
        }

        public IEnumerator<Car> GetEnumerator()
        {
            return Cars.GetEnumerator();
        }

        public int CompareTo(Human other)
        {
            return TotalCarValue.CompareTo(other?.TotalCarValue);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
