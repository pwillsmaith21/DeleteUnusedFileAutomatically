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
        public DataAccess dataAccess;
         
        public FileAccess(string path)
        {
            this.Path = path;
            dataAccess = new DataAccess();

        }
        private void VerifyFile(ref List<FileInformation> files)
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
        public List<FileInformation> RetriveData()
        {
            List<FileInformation> files = null;
            List<DirectoryInfo> directories;
            try
            {
                directories = RetrieveDirectories();
                files = RetrieveFiles(directories);
                return files;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return files;
            }
        }
        public long ConvertSize(long size)
        {
            size = size / 8;
            return size;
        }
        public List<FileInformation> RetrieveFiles(List<DirectoryInfo> directories)
        {
            return (from dir in directories
                    let tempFiles = dir.GetFiles()
                    from file in tempFiles
                    where file.Name.Contains(".dat")
                    // add time deleted
                    select new FileInformation(file.Name, file.FullName, file.DirectoryName, ConvertSize(file.Length))).ToList();
        }

        public void DeleteFiles()
        {
            List<FileInformation> listOfDeletedFiles = new List<FileInformation>();
            List<FileInformation> files = RetriveData();
            ConvertSize(2);
            VerifyFile(ref files);
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
            dataAccess.SaveResult(listOfDeletedFiles);
        }

        //private void OutputResult(List<FileInformation> listOfDeletedFiles)
        //{
        //    string filePath = @"C:\Users\HP\source\repos\DeleteUnusedFileAutomatically\DeleteUnusedFileAutomatically\Output.txt";
        //    File.WriteAllText(filePath, "Delete Files\n");
        //    if (File.Exists(filePath)){
        //        foreach (FileInformation file in listOfDeletedFiles)
        //        {
        //            File.AppendAllText(filePath, file.ToString());
        //        }
        //    }
        //}
    }
}
