using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for DatabaseComm
/// </summary>
public class DatabaseComm
{
    private string conn_string;

    public DatabaseComm(string connection)
    {
        conn_string = connection;
    }

    public void Insert(FloraObj FLO)
    {
        //using parametirized query
        string sqlInserString =
           "INSERT INTO Flora (Name, Color, LeafCount, Extra) VALUES (@Name, @Color, @LeafCount, @Extra)";

        SqlConnection conn = new SqlConnection(conn_string);

        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.Connection.Open();
        command.CommandText = sqlInserString;

        SqlParameter Name = new SqlParameter("@Name", FLO.Name);
        SqlParameter Color = new SqlParameter("@Color", FLO.Color);
        SqlParameter LeafCount = new SqlParameter("@LeafCount", FLO.LeafCount);
        SqlParameter Extra = new SqlParameter("@Extra", FLO.Extra);

        command.Parameters.AddRange(new SqlParameter[]{
                Name,Color,LeafCount,Extra});
        command.ExecuteNonQuery();
        command.Connection.Close();

    }

    /// <summary>
    /// Query the database based upon the values in the FloraObj
    /// </summary>
    /// <param name="FLO"></param>
    /// <returns></returns>
    public IList<FloraObj> Query(FloraObj FLO)
    {
        IList<FloraObj> ItemsFound = null;

        //---Form the query based on the FloraObj

        //---Send the Query

        //---Get the Query objects back

        //---Store Query results in FloraObjs
        
        //---Return list of found items
        return ItemsFound;
    }
}