using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DeleteUnusedFileAutomatically
{
   public class DataAccess
    {
        public DataAccess()
        {

        }
        public bool SaveResult(List<FileInformation> listOfDeletedFile)
        {
            try
            {
                SaveToDataBase(listOfDeletedFile);
                return true;
            }
            catch( Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        private void SaveToDataBase(List<FileInformation> listOfDeletedFile)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResultData"].ConnectionString))
            {
                connection.Open();
                string StoredProcedure = "InsertDataIntoDeleteFileResult";
                SqlCommand command = new SqlCommand(StoredProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (FileInformation file in listOfDeletedFile)
                {
                    try
                    {
                        command.Parameters.Clear();
                        command.Parameters.Add(new SqlParameter("@filename", SqlDbType.VarChar, 100)
                        {
                            Value = file.name
                        });
                        command.Parameters.Add(new SqlParameter("@filepath", SqlDbType.VarChar, 100)
                        {
                            Value = file.fullPath
                        }); 
                        command.Parameters.Add(new SqlParameter("@dir", SqlDbType.VarChar, 100)
                        {
                            Value = file.dir
                        });
                        command.Parameters.Add(new SqlParameter("@size", SqlDbType.BigInt)
                        {
                            Value = file.size
                        });
                        command.ExecuteNonQuery();

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
