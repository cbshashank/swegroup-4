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
    private string username;
    private string password;

    private const int plant_id_size = 30;
    private const int name_size = 200;
    private const int color_flower_size = 100;
    private const int color_foliage_size = 200;
    private const int color_fruit_seed_size = 100;
    private const int texture_foliage_size = 100;
    private const int shape_size = 150;
    private const int pattern_size = 150;
    private const int image_size = 5000;
    private const int usstate_size = 20;
    private const int type_size = 50;


    /// <summary>
    /// The constructor for the FloraObj - is currently empty, JSON.net fills in the fields
    /// </summary>
    public FloraObj()
    {

    }


    public string UserName
    {
        get
        {
            return username; 
        }
        set
        { username = value; }
    }

    public string Password
    {

        get

        { return password; }

        set
        { password = value; }
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

    public string Result
    {
        get;
        set;
    }

    /// <summary>
    /// IsLogin tells us if this object is a login object - are the username and password filled but nothing else
    /// </summary>
    /// <returns></returns>
    public bool IsLogin()
    {
        bool login = false;

        if (UserName.Length > 0 && Password.Length > 0)
        {
            if (string.IsNullOrEmpty(PlantId) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(GoogleURL) && 
            string.IsNullOrEmpty(GoogleImageURL) && string.IsNullOrEmpty(ColorFlower) && string.IsNullOrEmpty(ColorFoliage) 
            && string.IsNullOrEmpty(this.ColorFruitSeed) && string.IsNullOrEmpty(this.ImageURL) && 
            string.IsNullOrEmpty(this.USState) && string.IsNullOrEmpty(this.type))
            {            
                login = true;
            }
        }

        return login;

    }

    /// <summary>
    /// Are all the items in this object empty (is it an invalid object)
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        bool empty = false;

        if (string.IsNullOrEmpty(PlantId) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(GoogleURL) &&
            string.IsNullOrEmpty(GoogleImageURL) && string.IsNullOrEmpty(ColorFlower) && string.IsNullOrEmpty(ColorFoliage)
            && string.IsNullOrEmpty(this.ColorFruitSeed) && string.IsNullOrEmpty(this.ImageURL) &&
            string.IsNullOrEmpty(this.USState) && string.IsNullOrEmpty(this.type) && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
        {
            empty = true;
        }
        return empty;
    }


    /// <summary>
    /// Check to make sure all the strings in this object are not too long (or null) and will not cause a crash
    /// </summary>
    /// <returns></returns>
    public bool CheckStringSize()
    {
        bool stringIsValid = false;

            //---Check the strings for all the data types against their maximum values in the database
            if (checkString(PlantId, plant_id_size) && 
                checkString(Name, name_size) && 
                checkString(GoogleURL, image_size) &&
                checkString(GoogleImageURL, image_size) && 
                checkString(ColorFlower, color_flower_size) &&
                checkString(ColorFoliage,color_foliage_size) &&
                checkString(ColorFruitSeed,color_fruit_seed_size) && 
                checkString(ImageURL, image_size) &&
                checkString(USState,usstate_size) && 
                checkString(Type,type_size))
            {
                stringIsValid = true;
            }


        return stringIsValid;

    }

    /// <summary>
    /// Check a string to make sure it is a given size
    /// </summary>
    /// <param name="check"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private bool checkString(string check, int size)
    {
        bool validstring = true;
        if(!string.IsNullOrEmpty(check))
        {
            if(check.Length > size)
            {
                validstring = false;
            }
        }
        return validstring;
    }
   
}