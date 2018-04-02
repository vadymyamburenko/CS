using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace cslab1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double entropy=0, infoamount = 0, length=0;
           // string path = @"C:\EXAM170612\13Base64.txt";
           string path = @"C:\EXAM170612\PCIBase642.txt";
           //string path = @"C:\EXAM170612\RepaBase64.txt";
            string text = File.ReadAllText(path, Encoding.Default);       
            length = text.Length;
            Dictionary<char, int> mydict = new Dictionary<char, int>();
            foreach (char ch in text)
            {
                if (mydict.ContainsKey(ch))
                    mydict[ch] += 1;
                else
                    mydict.Add(ch, 1);
            }
            foreach (KeyValuePair<char, int> kvp in mydict)
            {
                entropy -= (kvp.Value / length) * Math.Log(kvp.Value / length, 2);
                Console.WriteLine(kvp.Key + " - "  + kvp.Value / length + "% \n");
            }
            infoamount = entropy * length / 8;
            Console.WriteLine("Ентропiя = " + entropy + "\n" + "Кiлькiсть iнформацiї: " + infoamount + "\nРозмір файлу" + new FileInfo(path).Length);
            Console.ReadKey();
        }
    }
}