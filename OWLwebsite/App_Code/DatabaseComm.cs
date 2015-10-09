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

    /// <summary>
    /// Given a partially complete FloraObj, query the database for a complete one.
    /// </summary>
    /// <param name="FLO"></param>
    /// <returns></returns>
    public IList<FloraObj> Query(FloraObj FLO)
    {
        IList<FloraObj> floraObjList = new List<FloraObj>();

        string sqlQueryString =
          "SELECT Name, Color, LeafCount, Extra FROM FLORA";

        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        
    
        int count = 0;  
        if(!string.IsNullOrEmpty(FLO.Name))
        {
            sqlQueryString += " WHERE Name = @Name";
            SqlParameter Name = new SqlParameter("@Name", FLO.Name);
            command.Parameters.Add(Name);
            count++;
        }
        if(!string.IsNullOrEmpty(FLO.Color))
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";
            sqlQueryString += "Color=@Color";
            SqlParameter Color = new SqlParameter("@Color", FLO.Color);
            command.Parameters.Add(Color);
            count++;

        }
        if (FLO.LeafCount != -1)
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";

            sqlQueryString += "LeafCount=@LeafCount";
            SqlParameter LeafCount = new SqlParameter("@LeafCount", FLO.LeafCount);
            command.Parameters.Add(LeafCount);
            count++;

        }
        if (!string.IsNullOrEmpty(FLO.Extra))
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";

            sqlQueryString += "Extra = @Extra";
            SqlParameter Extra = new SqlParameter("@Extra", FLO.Extra);
            command.Parameters.Add(Extra);
            count++;
        }
       

        
        command.Connection.Open();

        command.CommandText = sqlQueryString;

       SqlDataReader ReturnResult = command.ExecuteReader();

        while(ReturnResult.Read())
        {
            FloraObj AddObj = new FloraObj();
            AddObj.Name = ReturnResult.GetString(0);
            AddObj.Color = ReturnResult.GetString(1);
            AddObj.LeafCount = ReturnResult.GetInt16(2);
            AddObj.Extra = ReturnResult.GetString(3);
            floraObjList.Add(AddObj);
        }
        
        
        command.Connection.Close();
        return floraObjList;

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

   
}