using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_3a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a = ");
            float a = float.Parse(Console.ReadLine());
            Console.Write("Enter b = ");
            float b = float.Parse(Console.ReadLine());

            string a_string = SingleToBinaryString(a), b_string = SingleToBinaryString(b);
            int a_sign = int.Parse(a_string[0].ToString()), b_sign = int.Parse(b_string[0].ToString());
            int a_ex = Convert.ToInt32(a_string.Substring(1, 8), 2), b_ex = Convert.ToInt32(b_string.Substring(1, 8), 2);
            int a_mantisa = Convert.ToInt32(a_string.Substring(9), 2), b_mantisa = Convert.ToInt32(b_string.Substring(9), 2);
            int result_sign = 0, result_ex = a_ex, result_mantisa = 0;

            a_mantisa = a_mantisa + (1 << 23);
            b_mantisa = b_mantisa + (1 << 23);

            //get large exp and shift mantisa
            if (a_ex > b_ex)
            {
                result_ex = a_ex;
                b_mantisa = b_mantisa >> (a_ex - b_ex);
            }
            if (b_ex > a_ex)
            {
                result_ex = b_ex;
                a_mantisa = a_mantisa >> (b_ex - a_ex);
            }
            //Align binary points

            Console.WriteLine("Result exponent: " + new string('0', 8 - Convert.ToString(result_ex, 2).Length) + Convert.ToString(result_ex, 2));
            Console.WriteLine("shifted a-mantisa: " + new string('0', 32 - Convert.ToString(a_mantisa, 2).Length) + Convert.ToString(a_mantisa, 2));
            Console.WriteLine("shifted b-mantisa: " + new string('0', 32 - Convert.ToString(b_mantisa, 2).Length) + Convert.ToString(b_mantisa, 2));

            //get sign and +/- mantisa
            if (a_sign == b_sign)
            {
                result_sign = a_sign;
                result_mantisa = a_mantisa + b_mantisa;
            }
            else if (a_mantisa > b_mantisa && a_sign == 0 && b_sign == 1)
            {
                result_sign = 0;
                result_mantisa = a_mantisa - b_mantisa;
            }
            else if (b_mantisa > a_mantisa && a_sign == 0 && b_sign == 1)
            {
                result_sign = 1;
                result_mantisa = b_mantisa - a_mantisa;
            }
            else if (a_mantisa > b_mantisa && a_sign == 1 && b_sign == 0)
            {
                result_sign = 1;
                result_mantisa = a_mantisa - b_mantisa;
            }
            else if (b_mantisa > a_mantisa && a_sign == 1 && b_sign == 0)
            {
                result_sign = 0;
                result_mantisa = b_mantisa - a_mantisa;
            }


            //significants
            Console.WriteLine("add significants:  " +
                new string('0', 32 - Convert.ToString(result_mantisa, 2).Length) + Convert.ToString(result_mantisa, 2));


            while ((result_mantisa >> 24) > 0)
            {
                result_mantisa = result_mantisa >> 1;
                result_ex++;
            }
            while ((result_mantisa & (1 << 23)) != (1 << 23))
            {
                result_mantisa = result_mantisa << 1;
                result_ex--;

            }

            result_mantisa = result_mantisa & ((1 << 23) - 1);
            string result_ex_string = new string('0', 8 - Convert.ToString(result_ex, 2).Length) + Convert.ToString(result_ex, 2);
            string result_mantisa_string = new string('0', 23 - Convert.ToString(result_mantisa, 2).Length) + Convert.ToString(result_mantisa, 2);
            string result_string = result_sign.ToString() + result_ex_string + result_mantisa_string;



            Console.WriteLine("Result_ex = " + Convert.ToString(result_ex, 2));
            Console.WriteLine("Result_mantisa = " + Convert.ToString(result_mantisa, 2));

            Console.WriteLine("Result bits   :" + result_string + "          (in decimal: {0})", BinaryStringToSingle(result_string));

            Console.ReadKey();
        }
        public static string SingleToBinaryString(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            int i = BitConverter.ToInt32(b, 0);
            string result = Convert.ToString(i, 2);
            return new String('0', 32 - result.Length) + result;
        }

        public static float BinaryStringToSingle(string s)
        {
            int i = Convert.ToInt32(s, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);

        }
    }
}
