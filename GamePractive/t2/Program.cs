using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 2.	Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции

/// </summary>
namespace t2
{
    class Program
    {
        /// a.	для целых чисел;
        static void CountInt(List<int> list)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var item in list)
            {
                if (dict.ContainsKey(item))
                { dict[item]++; }
                else
                {
                    dict.Add(item, 1);
                }
            }
            foreach (var item in dict)
            { Console.WriteLine(item); }
        }
        ///b.  * для обобщенной коллекции;
        static void Count<T>(List<T> elements)
        {
            Dictionary<T, int> dict = new Dictionary<T, int>();
            foreach (var item in elements)
            {
                if (dict.ContainsKey(item))
                { dict[item]++; }
                else
                { dict.Add(item, 1); }
            }
            foreach (var item in dict)
            { Console.WriteLine(item); }
        }
        static void Main(string[] args)
        {
            List<int> elements = new List<int> { 5, 42, 1, 2, 3, 3, 10, 20, 40, 8, 7, 4, 5 };
            CountInt(elements);
            for (int i = 0; i < 2; i++)
            { Console.WriteLine(); }
            Count<int>(elements);
            for (int i = 0; i < 2; i++)
            { Console.WriteLine(); }

            Console.ReadKey();
        }
    }
}
