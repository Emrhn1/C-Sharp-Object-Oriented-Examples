using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev05
{
    public class Quadruple<T1, T2, T3, T4> : IComparable<Quadruple<T1, T2, T3, T4>>
    where T1 : IComparable<T1>

    {
        public int CompareTo(Quadruple<T1, T2, T3, T4> other)
        {
            throw new NotImplementedException();
        }
        private T1 id;
        private T2 second;
        private T3 third;
        private T4 fourth;

        public T1 Id
        {
            get => id;
            set => id = value;
        }

        public T2 Second
        {
            get => second;
            set => second = value;
        }

        public T3 Third
        {
            get => third;
            set => third = value;
        }

        public T4 Fourth
        {
            get => fourth;
            set => fourth = value;
        }
        public Quadruple() { }
        public Quadruple(T1 id, T2 second, T3 third, T4 fourth)
        {
            this.id = id;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
        }
        public override string ToString()
        {
            return $"Quadruple[{id}, {second}, {third}, {fourth}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is Quadruple<T1, T2, T3, T4> other)
            {
                return id.Equals(other.id) &&
                       second.Equals(other.second) &&
                       third.Equals(other.third) &&
                       fourth.Equals(other.fourth);
            }
            return false;
        }
 
    }
}
