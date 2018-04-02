using System;
using System.Text;
using System.IO;

namespace cslab1_2
{
    class Program
    {
        private static char GetCharFromIndexTable(byte b)
        {
            char[] indexTable = new char[64] {
        'A','B','C','D','E','F','G','H','I','J','K','L','M',
        'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        'a','b','c','d','e','f','g','h','i','j','k','l','m',
        'n','o','p','q','r','s','t','u','v','w','x','y','z',
        '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return indexTable[b];
            }
            else
            {
                return ' ';
            }
        }
        public static char[] Base64Encoding(byte[] data)
        {
            int len, len2;
            int blockCount;
            int paddingCount;
            len = data.Length;

            if ((len % 3) == 0)
            {
                paddingCount = 0;
                blockCount = len / 3;
            }
            else
            {
                paddingCount = 3 - (len % 3);
                blockCount = (len + paddingCount) / 3;
            }

            len2 = len + paddingCount;

            byte[] source2 = new byte[len2];

            for (int x = 0; x < len2; x++)
            {
                if (x < len)
                {
                    source2[x] = data[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte b1, b2, b3;
            byte temp, temp1, temp2, temp3, temp4;
            byte[] buffer = new byte[blockCount * 4];
            char[] result = new char[blockCount * 4];

            for (int x = 0; x < blockCount; x++)
            {
                b1 = source2[x * 3];
                b2 = source2[x * 3 + 1];
                b3 = source2[x * 3 + 2];

                temp1 = (byte)((b1 & 252) >> 2);

                temp = (byte)((b1 & 3) << 4);
                temp2 = (byte)((b2 & 240) >> 4);
                temp2 += temp;

                temp = (byte)((b2 & 15) << 2);
                temp3 = (byte)((b3 & 192) >> 6);
                temp3 += temp;

                temp4 = (byte)(b3 & 63);

                buffer[x * 4] = temp1;
                buffer[x * 4 + 1] = temp2;
                buffer[x * 4 + 2] = temp3;
                buffer[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < blockCount * 4; x++)
            {
                result[x] = GetCharFromIndexTable(buffer[x]);
            }

            if (paddingCount == 1)
                result[blockCount * 4 - 1] = '=';
            if (paddingCount == 2)
            {
                result[blockCount * 4 - 1] = '=';
                result[blockCount * 4 - 2] = '=';
            }

            return result;
        }
        static void Main(string[] args)
        {
            byte[] data = new byte[0];
            string path = @"C:\EXAM170612\";
           // string name = @"13.txt.bz2";
           // string name = @"Repa.txt.bz2";
            string name = @"PCI.txt";

            data = Encoding.Default.GetBytes(File.ReadAllText(path + name, Encoding.Default));
          // data = Encoding.UTF8.GetBytes(File.ReadAllText(path + name, Encoding.UTF8));

            char[] value = Base64Encoding(data);
            Console.WriteLine(value);

            string sValue = "";
            for (int i = 0; i < value.LongLength; i++)
            {
                sValue += value[i].ToString();
            }

            File.WriteAllText(path + "PCIBase642.txt", sValue, Encoding.Default);

        }
    }
}
