using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/// <summary>
/// Restful C# Service class, based upon tutorial located at www.codeproject.com/articles/112470/Developing-a-REST-Web-Service-using-C-A-walkthroug
/// </summary>
public class Service :  IHttpHandler
{
    //---Database object
    private DatabaseComm DAO;

    //String to connect to the database
    private const string Connect = "Data Source=.\\SQLEXPRESS;Initial Catalog=OWL;Integrated Security=True;MultipleActiveResultSets=True";

    public Service()
    {
        //---Initialize database
        DAO = new DatabaseComm(Connect);
    }

    bool IHttpHandler.IsReusable
    {
        get
        {
            return true;
        }
    }


    public void ProcessRequest(HttpContext context)
    {
        switch (context.Request.HttpMethod)
        {
            case "GET":
                //Perform READ Operation
                READ(context);
                break;
            case "POST":
                //Perform UPDATE Operation
                QUERY(context);
                break;
            case "PUT":
                //Perform CREATE Operation
                CREATE(context);
                break;
            case "DELETE":
                //Perform DELETE Operation
                DELETE(context);
                break;
            default:
                break;
        }
    }


    
    public void READ(HttpContext context)
    {
   
    }

    public void DELETE(HttpContext context)
    {
        
    }


    public void QUERY(HttpContext context)
    {
        string json = new StreamReader(context.Request.InputStream).ReadToEnd();
        try
        {

            FloraObj newObj = JsonConvert.DeserializeObject<FloraObj>(json);
            IList<FloraObj> FloraObjList = DAO.Query(newObj);
            context.Response.Write(JsonConvert.SerializeObject(FloraObjList));
        }
        catch (Exception e)
        {

            //---Do logging here about message - could be a post message for the server, could be a badly formed JSON
            context.Response.Write("Invalid Message!");
            
        }
    }


    public void CREATE(HttpContext context)
    {

        string json = new StreamReader(context.Request.InputStream).ReadToEnd();
        try
        { 
            
            FloraObj newObj = JsonConvert.DeserializeObject<FloraObj>(json);
            DAO.Insert(newObj);
            context.Response.Write(JsonConvert.SerializeObject(newObj));
        }
        catch(Exception e)
        {
            //---Do logging here about message - could be a post message for the server, could be a badly formed JSON
            context.Response.Write("Invalid Message!");
        }
        

    }

}
