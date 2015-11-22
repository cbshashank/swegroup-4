using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Logger class
/// </summary>
public class Logger
{
    private const string Error_Path = "c:\\OWL_Error.txt";
    private const string Log_Path = "c:\\OWL_Log.txt";

    public Logger()
    {
      
    }

    /// <summary>
    /// Write an error message to the log
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static bool WriteError(string error)
    {
        try
        { 
            using (StreamWriter TW = new StreamWriter(Error_Path,true))
            {
                TW.WriteLine(System.DateTime.Now.ToString() + ":\t" + error);
                return true;
            }
        }
        catch(Exception e)
        {
            return false;      
        }
    }

    /// <summary>
    /// Write a log message to the log
    /// </summary>
    /// <param name="logging"></param>
    /// <returns></returns>
    public static bool WriteLog(string logging)
    {
        try
        {
            using (StreamWriter TW = new StreamWriter(Log_Path,true))
            {
                TW.WriteLine(System.DateTime.Now.ToString() + ":\t" + logging);
                return true;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}