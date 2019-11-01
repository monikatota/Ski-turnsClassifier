using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client proxy = new ServiceReference1.Service1Client();
            Console.WriteLine("calculate sum of 2 and 4");
            int a = 2, b = 4;
            int output = proxy.GetSum(a, b);
            Console.WriteLine(output);
            Console.ReadKey();
            proxy.Close();
        }
    }
}
