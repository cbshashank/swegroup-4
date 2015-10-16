using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace populateDB
{
    class populatePlantDB
    {
        //Tables: Plant, Location, Plant Type
        //Plant: plant_id, name, color_flower, color-foliage, color_fruit_seed, texture_foliage, shape, pattern, image
        //Location: plant_id, us_state
        //Plant_type: plant_id, type
        // TODO: Add constructor logic here
        
            
        //These are the URLs for the plant jpgs from USDA
        public static string imageURL(string plant_id)
        {
            return "http://plants.usda.gov/gallery/standard/" + plant_id + "_001_shp.jpg";
        }

    }
    

}







       