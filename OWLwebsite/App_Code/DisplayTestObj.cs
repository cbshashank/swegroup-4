using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisplayTestObj
/// </summary>
public class DisplayTestObj
{
    string tex;
    string ter;
    string[] opt;

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

    public string[] options
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

    public DisplayTestObj(string ter, string tex, string[] opt)
    {
        term = ter;
        text = tex;
        options = opt;
         
    }
}