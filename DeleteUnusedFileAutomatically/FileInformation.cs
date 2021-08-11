using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DeleteUnusedFileAutomatically
{
    public class FileInformation 
    {
        public string name { get; set; }
        public string fullPath { get; set; }
        public string dir { get; set; }
        public long size { get; set; }
        public FileInformation(string name , string fullPath, string dir , long size)
        {
            this.name = name;
            this.fullPath = fullPath;
            this.dir = dir;
            this.size = size;
        }
      
        public override string ToString()
        {
            string result = $"Name: {name}, location: {dir}, size: {size}\n";
            return result;
        }
    }
}
