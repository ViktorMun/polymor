using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace toString_Task
{
    class MyClass : Object
    {
        private int _a;
        public MyClass(int a)
        {
            _a = a;
        }
        // Попробуйте раскомментировать этот метод и запустить программу
         public override string ToString() =>  _a.ToString();
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyClass obj = new MyClass(10);
            Console.WriteLine(obj);
            Console.ReadKey();
        }
    }
}; 
