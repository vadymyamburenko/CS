using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_1a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a multiplicand please:");
            int multiplicand = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter a multiplier please:");
            int multiplier = Int32.Parse(Console.ReadLine());
            Console.WriteLine(Multiplication(multiplicand, multiplier));
            Console.ReadKey();
        }

        static string FinishStringWithZeros(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; i++)
                head += "0";
            return head + val;
        }

        static long Multiplication(int multiplic, int multiplier)
        {
            bool isMultiplierLessThenZero = false;
            if (multiplier < 0)
            {
                isMultiplierLessThenZero = true;
                multiplier = ~multiplier + 1;
                Console.WriteLine("if multiplier < 0: multiplier = {0}", multiplier);
            }
            long product = 0;
            long multiplicand = multiplic;
            for (int i = 0; i < 32; i++)
            {
                if ((multiplier & 1) == 1)
                    product += multiplicand;
                multiplier >>= 1;
                multiplicand <<= 1;
                Console.WriteLine("product = {0},         multiplier shift left, multiplicand shift right", FinishStringWithZeros(Convert.ToString(product, 2)));
            }

            if (isMultiplierLessThenZero)
            {
                product = ~product + 1;
            }
            return product;
        }
    }
}
