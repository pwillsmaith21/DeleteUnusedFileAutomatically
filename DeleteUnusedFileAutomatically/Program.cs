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
            string path = @"C:\Users\HP\Downloads\k8tw4.txt";
            FileAccess fileAccess = new FileAccess(path);
            fileAccess.DeleteFiles();
            Console.ReadLine();

        }
    }
}
