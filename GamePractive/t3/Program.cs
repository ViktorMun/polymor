using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///  Дан фрагмент программы. Произвести действия (описание по коду)
/// </summary>
namespace t3
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
  {
    {"four",4 },
    {"two",2 },
    { "one",1 },
    {"three",3 },
  };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            //а. Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
            var newD = dict.OrderBy(x => x.Value);
              
             
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine("-------------------------");
            foreach (var pair in newD)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();

        }
    }
}
