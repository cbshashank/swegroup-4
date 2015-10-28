using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// The FloraObj which we send to the database, mapping to the tables
/// </summary>
public class FloraObj
{
    private string plant_id;
    private string name;
    private string color_flower;
    private string color_foliage;
    private string color_fruit_seed;
    private string texture_foliage;
    private string shape;
    private string pattern;
    private string imageURL;
    private string description;
    private string us_state;
    private string type;
    private string googleURL;
    private string googleImageURL;

    /// <summary>
    /// The constructor for the FloraObj - is currently empty, JSON.net fills in the fields
    /// </summary>
    public FloraObj()
    {

    }

    /// <summary>
    /// The primary key and plant id, typically a three character string
    /// </summary>
    public string PlantId
    {
        get
        {
            return plant_id;
        }
        set
        {
            plant_id = value;
        }
    }

    /// <summary>
    /// The plant Name
    /// </summary>
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

    /// <summary>
    /// The color of plant flower value
    /// </summary>
    public string ColorFlower
    {
        get
        {
            return color_flower;
        }
        set
        {
            color_flower = value;
        }
    }

    /// <summary>
    /// The color of an foliage on the plant
    /// </summary>
    public string ColorFoliage
    {
        get
        {
            return color_foliage;
        }
        set
        {
            color_foliage = value;
        }
    }

    /// <summary>
    /// Color of any fruit seeds on the plant
    /// </summary>
    public string ColorFruitSeed
    {
        get
        {
            return color_fruit_seed;
        }
        set
        {
            color_fruit_seed = value;
        }
    }

    /// <summary>
    /// Texture of the foliage on the plant
    /// </summary>
   public string TextureFoliage
    {
        get
        {
            return texture_foliage;
        }
        set
        {
            texture_foliage = value;
        }
    }

    /// <summary>
    /// Shape of the plant
    /// </summary>
    public string Shape
    {
        get
        {
            return shape;
        }
        set
        {
            shape = value;
        }
    }

    /// <summary>
    /// Pattern on the plant
    /// </summary>
    public string Pattern
    {
        get
        {
            return pattern;
        }
        set
        {
            pattern = value;
        }
    }

    /// <summary>
    /// URL of the image
    /// </summary>
    public string ImageURL
    {
        get
        {
            return imageURL;
        }
        set
        {
            imageURL = value;
        }
    }

    /// <summary>
    /// The google URL
    /// </summary>
    public string GoogleURL
    {
        get
        {
            return googleURL;
        }
        set
        {
            googleURL = value;
        }
    }

    /// <summary>
    /// The image URL from Google
    /// </summary>
    public string GoogleImageURL
    {
        get
        {
            return googleImageURL;
        }
        set
        {
            googleImageURL = value;
        }
    }

    /// <summary>
    /// State(s) where this plant is active
    /// </summary>
    public string USState
    {
        get
        {
            return us_state;
        }
        set
        {
            us_state = value;
        }
    }

    /// <summary>
    /// Type of plant (vine, tree, shrub)
    /// </summary>
    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    /// <summary>
    /// Descrpition of the plant
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

   
}