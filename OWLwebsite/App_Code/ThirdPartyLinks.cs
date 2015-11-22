using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ThirdPartyLinks
/// </summary>
public class ThirdPartyLinks
{
    public ThirdPartyLinks()
    {
    }
       
    public static string GoogleLink(string query)
    {
        string googlelink = "https://www.google.com/search?q/";
        googlelink += query;
        return googlelink;
    }

    public static string GoogleImageLink(string query)
    {
        string googlelink = "https://www.google.com/search?q/";
        googlelink += query;
        googlelink += "&source=lnms&tbm=isch";
        return googlelink;
    }


}