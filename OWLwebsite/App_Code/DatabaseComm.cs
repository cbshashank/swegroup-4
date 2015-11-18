using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for DatabaseComm
/// </summary>
public class DatabaseComm
{
    /// <summary>
    /// The connection string
    /// </summary>
    private string conn_string;

    /// <summary>
    /// The constructor for the connection to the database
    /// </summary>
    /// <param name="connection"></param>
    public DatabaseComm(string connection)
    {
        conn_string = connection;
    }

    /// <summary>
    /// Given a partially complete FloraObj, query the database for a complete one.
    /// </summary>
    /// <param name="FLO"></param>
    /// <returns></returns>
    public string checkAdminLogin(FloraObj admLog) // admin log in test Abhi
    {
        string result = "check username & password";
        string sqlQueryString = "";
        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;


        if (!(string.IsNullOrEmpty(admLog.UserName) && string.IsNullOrEmpty(admLog.Password)))
        {
            sqlQueryString = "SELECT username,password  from AdminTb";



            command.Connection.Open();

            //---Run the Query
            command.CommandText = sqlQueryString;
            SqlDataReader ReturnResult = command.ExecuteReader();

            //---cycle through the return values and build a list of the floraobjs which match
            if (ReturnResult.HasRows)
            {
                ReturnResult.Read();
                string username = ReturnResult["username"].ToString().Trim();
                string password = ReturnResult["password"].ToString().Trim();
                if(username == admLog.UserName && password == admLog.Password)
                    result = "Authenticated";
                return (result);
            }
            else
            {
                result = "check username & password";

                return result;
            }
        }
        return result;
    }  
        //end login check 
    public IList<FloraObj> Query(FloraObj FLO)
    {
        IList<FloraObj> floraObjList = new List<FloraObj>();

        string sqlQueryString = "";
        bool bAdvancedQuery = false;
        int count;

        if (string.IsNullOrEmpty(FLO.Type) && string.IsNullOrEmpty(FLO.USState))
        {
            //****--assumes that all of the other fields are filled in.
            //---The basic query
            sqlQueryString =
          "SELECT Plant.plant_id, name, color_flower, color_foliage, color_fruit_seed, texture_foliage, shape, pattern, image FROM Plant";

            //---There are no WHERE statements
            count = 0;
        }
        else
        {
            bAdvancedQuery = true;
            //****--NEED WHERE CLAUSES.
            //---The advanced query
            sqlQueryString =
          "SELECT Plant.plant_id, name, color_flower, color_foliage, color_fruit_seed, texture_foliage, shape, pattern, image, us_state, type FROM Plant,Location,PlantType WHERE Plant.plant_id=Location.plant_id AND Plant.plant_id=PlantType.plant_id";
            //---There are two conditionals here
            count = 2;
        }

        //---set up the database connection
        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;


        //---Because we have 2 assignments, we set the count of fields we are checking to 2


        //---Build the query string - for each field, add a WHERE clause
        sqlQueryString = BuildSQLWhere("Plant.plant_id", FLO.PlantId, "@plantid", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("name", FLO.Name, "@name", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("color_flower", FLO.ColorFlower, "@color_flower", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("color_foliage", FLO.ColorFoliage, "@color_foliage", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("color_fruit_seed", FLO.ColorFruitSeed, "@color_fruit_seed", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("texture_foliage", FLO.TextureFoliage, "@texture_foliage", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("shape", FLO.Shape, "@shape", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("pattern", FLO.Pattern, "@pattern", count, sqlQueryString, command, out count);
        sqlQueryString = BuildSQLWhere("image", FLO.ImageURL, "@pattern", count, sqlQueryString, command, out count);

        if (bAdvancedQuery)
        {
            //---Special case query for US state and type
            sqlQueryString = BuildSQLWhereOr("us_state", FLO.USState, "@us_state", count, sqlQueryString, command, out count);
            sqlQueryString = BuildSQLWhereOr("type", FLO.Type, "@type", count, sqlQueryString, command, out count);
        }

        //---Open the connection
        command.Connection.Open();

        //---Run the Query
        command.CommandText = sqlQueryString;
        SqlDataReader ReturnResult = command.ExecuteReader();

        //---cycle through the return values and build a list of the floraobjs which match
        while (ReturnResult.Read())
        {

            //---Get the plant id to start
            string plant_id = ReturnResult.GetString(0);


            //---Determine if an object already exists
            FloraObj AddObj = null;//findPlant_Id(floraObjList, plant_id);
            if (AddObj == null)
            {
                AddObj = new FloraObj();
                AddObj.PlantId = plant_id;
                AddObj.Name = ReturnResult.GetString(1);
                AddObj.ColorFlower = ReturnResult.GetString(2);
                AddObj.ColorFoliage = ReturnResult.GetString(3);
                AddObj.ColorFruitSeed = ReturnResult.GetString(4);
                AddObj.TextureFoliage = ReturnResult.GetString(5);
                AddObj.Shape = ReturnResult.GetString(6);
                AddObj.Pattern = ReturnResult.GetString(7);
                AddObj.ImageURL = ReturnResult.GetString(8);

                if (bAdvancedQuery)
                {
                    AddObj.USState = ReturnResult.GetString(9);
                    AddObj.Type = ReturnResult.GetString(10);
                }
                //---Add this floraobj to our list of objects
                floraObjList.Add(AddObj);
            }
            else
            {
                //---This object exists.  All the values should be the same except for location or type
                int length;

                //---Check if this state value is here
                bool bFound = UpdateComplexField(AddObj.USState, ReturnResult.GetString(9), out length);
                if (!bFound && length > 0)
                {
                    AddObj.USState += "," + ReturnResult.GetString(9);
                }
                else if (length == 0)
                {
                    AddObj.USState = ReturnResult.GetString(9);
                }

                //---Check if the type value is here
                bFound = UpdateComplexField(AddObj.Type, ReturnResult.GetString(10), out length);
                if (!bFound && length > 0)
                {
                    AddObj.Type += "," + ReturnResult.GetString(10);
                }
                else if (length == 0)
                {
                    AddObj.Type = ReturnResult.GetString(10);
                }


            }
        }


        //---Close the database connection.  Do we want a persistent connection?  For now, we will just do connections on a case-by-case basis
        command.Connection.Close();
        return floraObjList;

    }

    //These are the URLs for the plant jpgs from USDA
    public static string imageURL(string plant_id)
    {
        return "http://plants.usda.gov/gallery/pubs/" + plant_id + "_001_pvp.jpg";
    }

    FloraObj findPlant_Id(IList<FloraObj> FLO, string plant_id)
    {
        FloraObj FO = null;

        foreach (FloraObj F in FLO)
        {
            if (F.PlantId == plant_id)
            {
                FO = F;
                break;
            }
        }

        return FO;
    }


    /// <summary>
    /// Build the query, putting in place an AND clause or a WHERE clause and filling in the parameter
    /// </summary>
    /// <param name="strField">The field we are checking</param>
    /// <param name="strValue">The value in the object corresponding to that field (may be empty or null)</param>
    /// <param name="parameter">The parameter format of the field</param>
    /// <param name="count">The number of fields we have so far</param>
    /// <param name="sqlQueryString">The query string as it is right now</param>
    /// <param name="command">The sql command we are building</param>
    /// <param name="newcount">The new count value after the function is complete - if we have added a new field or not</param>
    /// <returns>the updated sqlQueryString with the field if the value in the object is not null or empty</returns>
    private string BuildSQLWhere(string strField, string strValue, string parameter, int count, string sqlQueryString, SqlCommand command, out int newcount)
    {
        if (!string.IsNullOrEmpty(strValue))
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";
            sqlQueryString += strField + "=" + parameter;
            SqlParameter Param = new SqlParameter(parameter, strValue);
            command.Parameters.Add(Param);
            count++;

        }
        newcount = count;

        return sqlQueryString;
    }

    private string BuildSQLWhere(string strField, int nValue, string parameter, int count, string sqlQueryString, SqlCommand command, out int newcount)
    {
        if (nValue != null && nValue != 0)
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";
            sqlQueryString += strField + "=" + parameter;
            SqlParameter Param = new SqlParameter(parameter, nValue);
            command.Parameters.Add(Param);
            count++;

        }
        newcount = count;

        return sqlQueryString;
    }

    /// <summary>
    /// Check if a field in the database is like another field
    /// </summary>
    /// <param name="strField"></param>
    /// <param name="strValue"></param>
    /// <param name="parameter"></param>
    /// <param name="count"></param>
    /// <param name="sqlQueryString"></param>
    /// <param name="command"></param>
    /// <param name="newcount"></param>
    /// <returns></returns>
    private string BuildSQLWhereLike(string strField, string strValue, string parameter, int count, string sqlQueryString, SqlCommand command, out int newcount)
    {
        //---We face possible injection issues here if the user uses a string with special characters

        if (!string.IsNullOrEmpty(strValue))
        {
            if (count > 0)
                sqlQueryString += " AND ";
            else
                sqlQueryString += " WHERE ";
            sqlQueryString += strField + " LIKE " + parameter;
            SqlParameter Param = new SqlParameter(parameter, "%" + strValue + "%");
            command.Parameters.Add(Param);
            count++;

        }
        newcount = count;

        return sqlQueryString;
    }



    string BuildSQLWhereOr(string strField, string strValue, string parameter, int count, string sqlQueryString, SqlCommand command, out int newcount)
    {

        newcount = count;
        if (!string.IsNullOrEmpty(strValue))
        {
            int totalsplit = 0;
            string[] searchparameters = strValue.Split(',');

            totalsplit = searchparameters.GetLength(0);

            if (count > 0)
                sqlQueryString += " AND (";
            else
                sqlQueryString += " WHERE (";

            for (int ii = 0; ii < totalsplit; ii++)
            {
                if (ii > 0)
                {
                    sqlQueryString += " OR (";
                }
                else
                {
                    sqlQueryString += "(";
                }
                sqlQueryString += strField + " = " + parameter + ii;
                SqlParameter Param = new SqlParameter(parameter + ii, searchparameters[ii]);
                command.Parameters.Add(Param);
                count++;
                sqlQueryString += ")";
            }
            sqlQueryString += ")";
        }

        return sqlQueryString;
    }

    private bool UpdateComplexField(string Values, string newValue, out int length)
    {
        bool bFound = false;
        string[] values = Values.Split(',');
        length = values.GetLength(0);

        for (int ii = 0; ii < length; ii++)
        {
            if (newValue == values[ii])
            {
                //---Already present, don't add
                bFound = true;
                break;
            }
        }

        return bFound;
    }


    /// <summary>
    /// Insert an item into the database query
    /// </summary>
    /// <param name="FLO"></param>
    // method for creating a unique plant id

    public string getplantid(FloraObj FLO) // infinite loop for checking
    {
        int i=1;
        string plantid = "";
        if (FLO.Name.Length > 4)
        {

            plantid = FLO.Name.Substring(0, 4);
        }
        else
            plantid = FLO.Name;
            plantid = plantid + i;
        for (i=1;i<10; i++) //added the condition i <10 for preventing the crash
        {
            plantid = plantid + i;
            if (checkplantid(plantid) == false)
                return plantid;
        }
        return plantid;

    }

    public bool checkplantid(string plantid)
    {
        bool found = false;
        string sqlQueryString = "";
        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;


        if (string.IsNullOrEmpty(plantid))
        {

            sqlQueryString = "SELECT *  from plant";

            command.Connection.Open();

            //---Run the Query
            command.CommandText = sqlQueryString;
            SqlDataReader ReturnResult = command.ExecuteReader();

            //---cycle through the return values and build a list of the floraobjs which match
            if (ReturnResult.HasRows)
            {
                ReturnResult.Read();
                if
                  (ReturnResult["plantid"].ToString() == plantid)
                {
                    found = true;
                    
                    return found;

                }

            }
            else
                return found;
        }  //end login check
        return found;
    }



    public void Insert(FloraObj FLO)
    {
        string plantid;
        FLO.UserName = "Admin";
        FLO.Password = "Admin";

        if (checkAdminLogin(FLO) == "Authenticated")
        {

            
            // check user name & password here ok or not
            //using parametirized query
            string sqlInserString =
               "INSERT INTO plant (plant_id,Name, Color_flower,color_foliage,color_fruit_seed,texture_foliage, shape, pattern,image) VALUES (@plant_id,@Name, @Color_flower,@color_foliage,@color_fruit_seed,@texture_foliage,@shape,@pattern,@image)";

            SqlConnection conn = new SqlConnection(conn_string);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.Connection.Open();
            command.CommandText = sqlInserString;
            if (FLO.Type == null)
                FLO.Type = "";
            if (FLO.Location == null)
                FLO.Location = "";

            if (FLO.ColorFoliage == null)
                FLO.ColorFoliage = "";
            if (FLO.ColorFlower == null)
                FLO.ColorFlower = "";
            if (FLO.PlantId == null)
                FLO.PlantId = "";
            if (FLO.ColorFruitSeed == null)
                FLO.ColorFruitSeed  = "";
            if (FLO.TextureFoliage == null)
                FLO.TextureFoliage = "";
            if (FLO.Shape == null)
                FLO.Shape= "";
            if (FLO.Pattern == null)
                FLO.Pattern = "";
            if (FLO.ImageURL == null)
                FLO.ImageURL = "";
            if (FLO.Name == null)
                FLO.Name = "";

            if (FLO.Name.Length > 20)
                FLO.Name = FLO.Name.Substring(0, 20);
            if (FLO.Name.Length > 20)
                FLO.PlantId = FLO.PlantId.Substring(0, 20);
            if (FLO.Pattern.Length > 20)
                FLO.Pattern = FLO.Pattern.Substring(0, 20);
            if (FLO.Shape.Length > 20)
                FLO.Shape = FLO.Shape.Substring(0, 20);
            if (FLO.ImageURL.Length > 20)
                FLO.ImageURL = FLO.ImageURL.Substring(0, 20);
            if (FLO.ColorFlower.Length > 20)
                FLO.ColorFlower=FLO.ColorFlower.Substring(0, 20);
            if (FLO.ColorFoliage.Length > 20)
                FLO.ColorFoliage = FLO.ColorFoliage.Substring(0, 20);
            if (FLO.ColorFruitSeed.Length > 20)
                FLO.ColorFruitSeed = FLO.ColorFruitSeed.Substring(0, 20);
            if (FLO.TextureFoliage.Length > 20)
                FLO.TextureFoliage = FLO.TextureFoliage.Substring(0, 20);
            if (FLO.Type.Length > 20)
                FLO.Type = FLO.Type.Substring(0, 20);
            if (FLO.Location.Length > 20)
                FLO.Location = FLO.Location.Substring(0, 20); 

            plantid = getplantid(FLO);
            SqlParameter plant_id = new SqlParameter("@plant_id",plantid ); 
            SqlParameter Name = new SqlParameter("@Name", FLO.Name);
            SqlParameter Color_flower = new SqlParameter("@Color_flower", FLO.ColorFlower);
            SqlParameter Color_foliage = new SqlParameter("@Color_foliage", FLO.ColorFoliage);
            SqlParameter Color_fruit_seed = new SqlParameter("@color_fruit_seed", FLO.ColorFruitSeed);
            SqlParameter texture = new SqlParameter("@texture_foliage", FLO.TextureFoliage);
            SqlParameter shape = new SqlParameter("@shape", FLO.Shape);
            SqlParameter pattern = new SqlParameter("@pattern", FLO.Pattern);
            SqlParameter image = new SqlParameter("@image", FLO.ImageURL);
       
           
             


            command.Parameters.AddRange(new SqlParameter[]{
                plant_id,Name, Color_flower,Color_foliage,Color_fruit_seed,texture, shape, pattern,image});
            command.ExecuteNonQuery();
            command.Connection.Close();
            insertlocation(plantid, FLO.Location);
            insertplanttype(plantid, FLO.Type);

        }
        // Check Admin USer & pass. 
    }

    public void insertlocation(string plantid, string location)

    {
       
        string sqlInserString ="INSERT INTO location (plant_id,location) VALUES (@plant_id,@location)";

            SqlConnection conn = new SqlConnection(conn_string);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.Connection.Open();
            command.CommandText = sqlInserString;

            SqlParameter plant_id = new SqlParameter("@plant_id",plantid);
            SqlParameter Location = new SqlParameter("@location", location);
          
            command.Parameters.AddRange(new SqlParameter[]{plant_id,Location});
            command.ExecuteNonQuery();
            command.Connection.Close();

        }

    public void insertplanttype(string plantid, string type)

    {

        string sqlInserString = "INSERT INTO location (plant_id,type) VALUES (@plant_id,@type)";

        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.Connection.Open();
        command.CommandText = sqlInserString;

        SqlParameter plant_id = new SqlParameter("@plant_id", plantid);
        SqlParameter Type = new SqlParameter("@type", type);

        command.Parameters.AddRange(new SqlParameter[] { plant_id, Type });
        command.ExecuteNonQuery();
        command.Connection.Close();

    }

    public void Delete(FloraObj FLO) // delete for plant table.
    {
        //using parametirized query
        string sqlInserString = "DELETE  FROM plant WHERE plant_id=@plant_id ";

        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.Connection.Open();
        command.CommandText = sqlInserString;

        SqlParameter plant_id = new SqlParameter("@plant_id", FLO.PlantId);


        command.Parameters.AddRange(new SqlParameter[]{
                plant_id});
        command.ExecuteNonQuery();
        command.Connection.Close();
        Deletetype(FLO.PlantId);
        Deletelocation(FLO.PlantId);



    }

    public void Deletetype(string plantid) // delete for plant table.
    {
        //using parametirized query
        string sqlInserString = "DELETE  FROM type WHERE plant_id=@plant_id ";

        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.Connection.Open();
        command.CommandText = sqlInserString;

        SqlParameter plant_id = new SqlParameter("@plant_id", FLO.PlantId);


        command.Parameters.AddRange(new SqlParameter[]{
                plant_id});
        command.ExecuteNonQuery();
        command.Connection.Close();

    }

    public void Deletelocation(string plantid) // delete for plant table.
    {
        //using parametirized query
        string sqlInserString = "DELETE  FROM location WHERE plant_id=@plant_id ";

        SqlConnection conn = new SqlConnection(conn_string);
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        command.Connection.Open();
        command.CommandText = sqlInserString;

        SqlParameter plant_id = new SqlParameter("@plant_id", FLO.PlantId);


        command.Parameters.AddRange(new SqlParameter[]{
                plant_id});
        command.ExecuteNonQuery();
        command.Connection.Close();

    }

    public List<Question> TransferQuestionAns()
    {
        List<Question> questions = new List<Question>();

        //---set up the database connection
        SqlConnection conn = new SqlConnection(conn_string);
        conn.Open();

        // build sql command (do a sort in the question and answers)
        SqlCommand command = new SqlCommand("SELECT question, question_text, answer, url from questionans order by question, answer");
        command.Connection = conn;

        // execute sql command -> return object
        SqlDataReader reader = command.ExecuteReader();

        // check if object has rows
        if (reader.HasRows)
        {
            // keep track of the previous and current question
            string prev_question = "";
            string curr_question = "";

            // initialize
            Question q = new Question("", "", new List<string>(), new List<string>());

            // if so, while reading row after row of SQL table
            while (reader.Read())
            {
                // question column in as curr_question
                curr_question = reader.GetString(reader.GetOrdinal("question"));

                // if the curr_question is different from the prev_question
                // when you are seeing a new question
                if (curr_question != prev_question)
                {
                    if (prev_question == "") // at start of SQL table
                    {
                        // create a new question object
                        q = new Question(curr_question, reader.GetString(reader.GetOrdinal("question_text")), new List<string>(), new List<string>());
                        // put in question (= term)
                        q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
                        q.urls.Add(reader.GetString(reader.GetOrdinal("url")));

                        // then make prev_question = curr_question
                        prev_question = curr_question;
                    }
                    else // in middle of SQL table
                    {
                        // add the last question
                        questions.Add(q);

                        // create a new question object
                        q = new Question(curr_question, reader.GetString(reader.GetOrdinal("question_text")), new List<string>(), new List<string>());
                        q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
                        q.urls.Add(reader.GetString(reader.GetOrdinal("url")));

                        // then make prev_question = curr_question
                        prev_question = curr_question;
                    }

                }
                else
                {
                    // curr_question == prev_question

                    // if not, continue adding to the curr_question's set of
                    // options and those options' urls
                    q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
                    q.urls.Add(reader.GetString(reader.GetOrdinal("url")));
                }
            }

            // add the last question object to the list of questions
            questions.Add(q);
        }

        command.Connection.Close();

        return questions;
    }
}