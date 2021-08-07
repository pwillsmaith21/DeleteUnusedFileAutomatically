using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace DeleteUnusedFileAutomatically
{
    
    public class FileAccess
    {
        public string Path { get; set; }
        public FileAccess(string path)
        {
            this.Path = path;
            
        }
        private FileInfo LocateFile()
        {
            FileInfo file;
            if(File.Exists(Path))
            {
                file = new FileInfo(Path);
                return file;
            }
            else
            {
                //Error
                throw new FileNotFoundException();
            }
           
        }
        public void DeleteFiles()
        {
            try
            {
                FileInfo file = LocateFile();
                string fileName = file.Name;
                string fileLocation = file.DirectoryName;
                long fileSize = file.Length;
                FileSystem.DeleteFile(file.FullName,
                    Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                //file.Delete();
                Console.WriteLine($"File: {fileName} from: {fileLocation} size: {fileSize} Deleted Successfully");
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

    }
}
