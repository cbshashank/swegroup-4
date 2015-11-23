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
        //string googlelink = "https://www.google.com/search?q=";
        //string googlelink = "https://en.wikipedia.org/wiki/";
        string googlelink = "https://duckduckgo.com/?q=";
        googlelink += query;
        //googlelink += "&output=embed";
        return googlelink;
    }

    public static string GoogleImageLink(string query)
    {
        //string googlelink = "https://www.google.com/search?q=";
        string googlelink = "https://duckduckgo.com/?q=";
        googlelink += query;
        //googlelink += "&source=lnms&tbm=isch";
        googlelink += "&iax=1&ia=images";
        return googlelink;
    }


}