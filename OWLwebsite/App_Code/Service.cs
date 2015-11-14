﻿using System;
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
    

    private List<DisplayTestObj> FillDispList()
    {
        List<DisplayTestObj> DispList = new List<DisplayTestObj>();

        DispList.Add(new DisplayTestObj("Name", "What is the plant Name?", new string[] { "" }));
        DispList.Add(new DisplayTestObj("ImageURL", "Image URL", new string[] { "" }));
        DispList.Add(new DisplayTestObj("USState", "Where is the plant in the US",
            new string[] { "AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY",
                "LA", "ME", "MD","MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK",
                "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA",
                "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI" }));
        DispList.Add(new DisplayTestObj("Type", "What type of plant is it?", new string[] { "Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine" }));
        DispList.Add(new DisplayTestObj("ColorFlower", "What color are the flowers?", new string[] { "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow" }));
        DispList.Add(new DisplayTestObj("ColorFoliage", "What color are the leaves?", new string[] { "Dark Green", "Green", "Grey-Green", "Red", "White-Gray", "Yellow-Green" }));
        DispList.Add(new DisplayTestObj("ColorFruitSeed", "What color are the fruit or seeds?", new string[] { "Black", "Blue", "Brown", "Green", "Orange", "Purple", "Red", "White", "Yellow" }));
        DispList.Add(new DisplayTestObj("Shape", "What shape does the plant have?", new string[] { "Climbing", "Columnar", "Conical", "Decumbent", "Erect", "Irregular", "Oval", "Prostrate", "Rounded", "Semi-Erect", "Vase" }));
        DispList.Add(new DisplayTestObj("TextureFoliage", "What kind of texture do the leaves have?", new string[] { "Fine", "Medium", "Coarse" }));
        DispList.Add(new DisplayTestObj("Pattern", "What kind of pattern does the plant have?", new string[] { "Dicot", "Fern", "Green Alga", "Gymnosperm", "Hornwort", "Horsetail", "Lichen", "Liverwort", "Lycopod", "Monocot", "Moss", "Quillwort", "Red Algae", "Whisk-fern" }));
        return DispList;
    }


    public void READ(HttpContext context)
    {
        List<DisplayTestObj> DispList = FillDispList();
        context.Response.Write(JsonConvert.SerializeObject(DispList));



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
