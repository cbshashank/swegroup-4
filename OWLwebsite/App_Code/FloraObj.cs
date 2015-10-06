using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for FloraObj
/// </summary>
public class FloraObj
{
    private int leafCount;
    private string color;
    private string name;
    private string extra;

    public int LeafCount
    {
        get
        {
            return leafCount;
        }
        set
        {
            leafCount = value;
        }
    }

    public string Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Extra
    {
        get { return extra; }
        set { extra = value; }
    }

    public FloraObj()
    {
        
    }
}