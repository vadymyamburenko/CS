using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_2c
{
    class Program
    {
        static void Main(string[] args)
        {
            Int16 dividend, divisor;
            Console.WriteLine("Enter 16 bits signed dividend:");
            dividend = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Enter 16 bits signed divisor:");
            divisor = Int16.Parse(Console.ReadLine());
            Task2(dividend, divisor);
            Console.ReadKey();
        }

        static void Task2(Int16 dividend, Int16 divisor)
        {
            Int64 register = 0 | dividend;
            int remainder_register_bits = Convert.ToInt32("11111111111111110000000000000000", 2),
            quotient_register_bits = Convert.ToInt32("1111111111111111", 2),
            shifted_divisor = divisor << 16,
            shifted_minus_divisor = -divisor << 16;

            const int remainder_bits_amount = 17,
                quotient_bits_amount = 16,
                register_bits_amount = 33;

            Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
            for (int i = 0; i < 16; ++i)
            {
                register <<= 1;
                Console.WriteLine("\tRegister shift left:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));

                if ((register >> 32 & 1) == 0)
                {
                    Console.WriteLine("Substract divisor: {0}", RegisterPartToBinaryString(shifted_minus_divisor, remainder_bits_amount, true));
                    register += shifted_minus_divisor;
                    Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
                }
                else
                {
                    Console.WriteLine("      Add shifted divisor: {0}", RegisterPartToBinaryString(shifted_divisor, remainder_bits_amount, true));
                    register += shifted_divisor;
                    Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
                }

                if ((register >> 32 & 1) == 0)
                {
                    register |= 1;
                    Console.WriteLine("\tSet last quotient bit to 1:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
                }
                else
                    Console.WriteLine("\tSet last quotient bit to 0:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
            }

            if ((register >> 32 & 1) == 1)
            {
                Console.WriteLine("      Add divisor: {0}", RegisterPartToBinaryString(shifted_divisor, remainder_bits_amount, true));
                register += shifted_divisor;
                Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, register_bits_amount));
            }

            Console.WriteLine("\tAnswer:");
            Console.WriteLine("\t\tRemainder:\t    {0} (in decimal: {1})",
                RegisterPartToBinaryString(register & remainder_register_bits, remainder_bits_amount, true),
                (register & remainder_register_bits) >> 16);
            Console.WriteLine("\t\tQuotient:\t      {0} (in decimal: {1})",
                RegisterPartToBinaryString(register & quotient_register_bits, quotient_bits_amount),
                register & quotient_register_bits);
        }
        static string RegisterPartToBinaryString(Int64 register, byte bits_amount, bool is_divisor = false)
        {
            string result = string.Empty;

            int last_index = is_divisor ? 15 : -1;
            for (int i = bits_amount - 1 + (is_divisor ? 16 : 0); i > last_index; --i)
                result += (register >> i & 1) + (i % 4 == 0 && i != 0 ? " " : "");

            return result;
        }

    }
}
