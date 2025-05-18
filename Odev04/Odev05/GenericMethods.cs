using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev05
{
    public static class GenericMethods
    {
       public static void Swap<T>(ref T value1, ref T value2)
        {
            T temp = value1;
            value1 = value2;
            value2 = temp;
        }
        public static void DisplayAndReset<T1, T2>(T1 param1, T2 param2)
        {
            Console.WriteLine($"Param1 - Type: {param1.GetType().Name}, Value: {param1}");
            Console.WriteLine($"Param2 - Type: {param2.GetType().Name}, Value: {param2}");

            param1 = default;
            param2 = default;
        }

        public static T CreateInstance<T>() where T : new()
        {
            return new T();
        }

      
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

       
        public static List<T> SortParams<T>(params T[] items) where T : IComparable<T>
        {
            List<T> list = new List<T>(items);
            list.Sort();
            return list;
        }


        public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(TKey key, TValue value)
         where TKey : struct
         where TValue : class
        {
            var dict = new Dictionary<TKey, TValue>();
            dict.Add(key, value);
            return dict;
        }


        public static void DisplayDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
        }
        public static IEnumerable<T> CreateCollection<T>(params T[] items)
        {
            if (items.Length < 3)
            {
                var queue = new Queue<T>();
                foreach (var item in items)
                {
                    queue.Enqueue(item);
                }
                return queue;
            }
            else
            {
                var stack = new Stack<T>();
                foreach (var item in items)
                {
                    stack.Push(item);
                }
                return stack;
            }
        }


        public static IEnumerable<T> GetCollection<T>(params T[] items)
        {
            if (items.Length < 3)
            {
                return new Queue<T>(items);
            }
            else
            {
                return new Stack<T>(items);
            }
        }

    }
 }   
