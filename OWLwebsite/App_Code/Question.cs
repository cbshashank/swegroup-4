using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Question
/// </summary>
public class Question
{
    string tex;
    string ter;
    private List<string> opt;
    private List<string> url;

    public string term
    {
        get
        {
            return ter;
        }
        set
        {
            ter = value;
        }
    }

    public string text
    {
        get
        {
            return tex;
        }
        set
        {
            tex = value;
        }
    }

    public List<string> options
    {
        get
        {
            return opt;
        }
        set
        {
            opt = value;
        }

    }

    public List<string> urls
    {
        get
        {
            return url;
        }
        set
        {
            url = value;
        }
    }

    public Question(string ter, string tex, List<string> opt, List<string> url)
    {
        term = ter;
        text = tex;
        options = opt;
        urls = url;
    }
}