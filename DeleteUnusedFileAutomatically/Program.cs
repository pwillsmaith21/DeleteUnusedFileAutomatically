using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteUnusedFileAutomatically
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\HP\Downloads\FATE.The.Cursed.King";
            FileAccess fileAccess = new FileAccess(path);
            fileAccess.DeleteFiles();
            Console.WriteLine("Done");
            Console.ReadLine();

        }
    }
}
