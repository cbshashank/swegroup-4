using System;
using System.Data;
using System.Data.SqlClient;
namespace populateDB
{
    public class createDB
    {
        public static void Main(string[] args)
        {
            System.Data.SqlClient.SqlConnection tmpConn;
            string sqlCreateDBQuery;
            // SqlConnection tmpConn = new SqlConnection();
            // tmpConn = new SqlConnection("server=SQLExpress;");

            tmpConn = new SqlConnection("user id=username;" + "password=password;" + "server=.\\SQLExpress;" + "Trusted_Connection=yes;");

            // tmpConn.ConnectionString = "SERVER = SQLExpress;";
            sqlCreateDBQuery = "CREATE DATABASE OWL";

            SqlCommand myCommand = new SqlCommand(sqlCreateDBQuery, tmpConn);
            try
            {
                tmpConn.Open();
                Console.WriteLine(sqlCreateDBQuery);
                myCommand.ExecuteNonQuery();
                Console.WriteLine("Database has been created successfully!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                tmpConn.Close();
            }
            return;
        }
    }
}