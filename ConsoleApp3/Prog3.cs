using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Prog3
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return;

            foreach (var number in numbers)
            {
                if (number > 0)
                {
                    Console.WriteLine(number);
                }
            }
        }
    }
}
