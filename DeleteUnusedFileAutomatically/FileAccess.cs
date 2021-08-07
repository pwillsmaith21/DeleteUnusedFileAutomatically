using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

namespace DeleteUnusedFileAutomatically
{
    
    public class FileAccess
    {
        public string Path { get; set; }
         
        public FileAccess(string path)
        {
            this.Path = path;


        }
        private void VerifyFile(in List<FileInformation> files)
        {
            foreach (FileInformation file in files) {
                if (!File.Exists(file.fullPath)) {

                    files.Remove(file);
                }
            }


        }
     
        public List<DirectoryInfo> RetrieveDirectories()
        {
            List<DirectoryInfo> directories = new List<DirectoryInfo>();
            Directory.SetCurrentDirectory(Path);
            foreach( string dir in Directory.GetDirectories(Path, "*", SearchOption.AllDirectories))
            {
                directories.Add(new DirectoryInfo(dir));
            }
            return directories;
        }
        public List<FileInformation> RetriveFiles()
        {
            List<DirectoryInfo> directories = RetrieveDirectories();
            List<FileInformation> files;
            files = (from dir in directories
                     let tempFiles = dir.GetFiles()
                     from file in tempFiles
                     where file.Name.Contains(".dat") 
                     select new FileInformation(file.Name, file.FullName, file.DirectoryName,file.Length)).ToList();
            return files;
        }
        public void DeleteFiles()
        {
            List<FileInformation> listOfDeletedFiles = new List<FileInformation>();
            List<FileInformation> files = RetriveFiles();
            VerifyFile(in files);
            foreach (FileInformation file in files)
            {
                try
                {
                    FileSystem.DeleteFile(file.fullPath,
                       Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                    listOfDeletedFiles.Add(file);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
            OutputResult(listOfDeletedFiles);
        }

        private void OutputResult(List<FileInformation> listOfDeletedFiles)
        {
            string filePath = @"C:\Users\HP\source\repos\DeleteUnusedFileAutomatically\DeleteUnusedFileAutomatically\Output.txt";
            File.WriteAllText(filePath, "Delete Files\n");
            if (File.Exists(filePath)){
                foreach (FileInformation file in listOfDeletedFiles)
                {
                    File.AppendAllText(filePath, file.ToString());
                }
            }
        }
    }
}
