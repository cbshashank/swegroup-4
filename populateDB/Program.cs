﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace populateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("user id=username;" + "password=password;" + "server=.\\SQLExpress;" + "Trusted_Connection=yes;" + "database=OWL;" + "connection timeout=10");
            conn.Open();
            createTables(conn);
            loadtable(makePlantdt(conn), makeLocationDt(conn), makeplantTypedt(conn), conn);

            conn.Close();
            
            
           
        }


        public static void createTables(SqlConnection conn)
        {

            
            StringBuilder query = new StringBuilder();
            //These are attributes of Plant Table
            String[] plantColumns = { "plant_id", "name", "color_flower", "color_foliage", "color_fruit_seed", "texture_foliage", "shape", "pattern", "image" };
            String[] plantColumnTypes = { "VARCHAR(30) NOT NULL PRIMARY KEY", "VARCHAR(200)", "VARCHAR(100)", "VARCHAR(200)", "VARCHAR(100)", "VARCHAR(100)", "VARCHAR(150)", "VARCHAR(150)", "VARCHAR(5000)" };

            //These are attributes of Location Table
            String[] locationColumns = { "plant_id", "us_state" };
            String[] locationColumnTypes = { "VARCHAR(20)", "VARCHAR(20)" };

            //These are attributes of Plant Type Table
            String[] plantTypeColumns = { "plant_id", "type" };
            String[] ptColumnTypes = { "VARCHAR(20)", "VARCHAR(50)" };

            //This creates the Plant table
            query.Append("CREATE TABLE plant (");
            for (int i = 0; i < plantColumns.Length; i++)
            {
                query.Append(plantColumns[i]);
                query.Append(" ");
                query.Append(plantColumnTypes[i]);
                query.Append(",");
            }
            query.Length -= 1;
            query.Append(")");
            try
            {
                SqlCommand sqlplantQuery = new SqlCommand(query.ToString(), conn);
                sqlplantQuery.ExecuteNonQuery();
                Console.WriteLine("Executed: " + query.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            query.Clear();


            //This creates the Location table
            query.Append("CREATE TABLE location (");
            for (int i = 0; i < locationColumns.Length; i++)
            {
                query.Append(locationColumns[i]);
                query.Append(" ");
                query.Append(locationColumnTypes[i]);
                query.Append(",");
            }
            query.Length -= 1;
            query.Append(")");
            try
            {
                SqlCommand sqllocQuery = new SqlCommand(query.ToString(), conn);
                sqllocQuery.ExecuteNonQuery();
                Console.WriteLine("Executed: "+query.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            query.Clear();

            //This creates the Plant Type table
            query.Append("CREATE TABLE plantType (");
            for (int i = 0; i < plantTypeColumns.Length; i++)
            {
                query.Append(plantTypeColumns[i]);
                query.Append(" ");
                query.Append(ptColumnTypes[i]);
                query.Append(",");
            }
            query.Length -= 1;
            query.Append(")");
            try
            {
                SqlCommand sqlptQuery = new SqlCommand(query.ToString(), conn);
                sqlptQuery.ExecuteNonQuery();
                Console.WriteLine("Executed: " + query.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            query.Clear();

        }
        //This creates a c# datatable for Plant from the csv file
        public static DataTable makePlantdt(SqlConnection conn)
        {
            
            DataTable dtPlant = new DataTable();

            try
            {
                string[] Lines = File.ReadAllLines("TABLE_plant.csv");
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                    dtPlant.Columns.Add(Fields[i].ToLower(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dtPlant.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dtPlant.Rows.Add(Row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return dtPlant;

        }
        public static DataTable makeLocationDt(SqlConnection conn)
        {

            DataTable locdt = new DataTable();

            try
            {
                string[] Lines = File.ReadAllLines("TABLE_Location.csv");
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                    locdt.Columns.Add(Fields[i].ToLower(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = locdt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    locdt.Rows.Add(Row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return locdt;

        }
        //This makes a plantType Datatable 
        public static DataTable makeplantTypedt(SqlConnection conn)
        {

            DataTable plantTypedt = new DataTable();

            try
            {
                string[] Lines = File.ReadAllLines("TABLE_plant_type.csv");
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                    plantTypedt.Columns.Add(Fields[i].ToLower(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = plantTypedt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    plantTypedt.Rows.Add(Row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return plantTypedt;

        }
        //loads the datatables onto the DB
        public static void loadtable(DataTable plant, DataTable location, DataTable plantType, SqlConnection conn)
        {
           StringBuilder Plantsquery = new StringBuilder();
           StringBuilder Locationquery = new StringBuilder();
           StringBuilder Ptquery = new StringBuilder();

            for (int i=0; i<plant.Rows.Count;i++)
            {
                    Plantsquery.Append("INSERT INTO plant(plant_id,name,color_flower,color_foliage,color_fruit_seed,texture_foliage,shape,pattern,image) VALUES('" + plant.Rows[i][0].ToString().Trim() + "','" + plant.Rows[i][1].ToString().Trim() + "','" + plant.Rows[i][3].ToString().Trim() + "','" + plant.Rows[i][4].ToString().Trim() + "','" + plant.Rows[i][6].ToString().Trim() + "','" + plant.Rows[i][5].ToString().Trim() + "','" + plant.Rows[i][7].ToString().Trim() + "','" + plant.Rows[i][2].ToString().Trim() + "','plants.usda.gov/gallery/standard/" + plant.Rows[i][0].ToString().Trim() + "_001_shp.jpg');");
            }
            for (int i = 0; i < location.Rows.Count; i++)
            {
                Locationquery.Append("INSERT INTO location(plant_id, us_state) VALUES('" + location.Rows[i][0].ToString().Trim() + "','" + location.Rows[i][1].ToString().Trim()+"');");
            }
            for (int i = 0; i < plantType.Rows.Count; i++)
            {
                Ptquery.Append("INSERT INTO plantType(plant_id, type) VALUES('" + plantType.Rows[i][0].ToString().Trim() + "','" + plantType.Rows[i][1].ToString().Trim()+"');");
            }
            try {
                SqlCommand pQuery = new SqlCommand(Plantsquery.ToString(), conn);
                pQuery.ExecuteNonQuery();
                Console.WriteLine("Loaded all data into Table plant");
                SqlCommand lQuery = new SqlCommand(Locationquery.ToString(), conn);
                lQuery.ExecuteNonQuery();
                Console.WriteLine("Loaded all data into Table location");
                SqlCommand ptQuery = new SqlCommand(Ptquery.ToString(), conn);
                ptQuery.ExecuteNonQuery();
                Console.WriteLine("Loaded all data into Table plantType");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        
    }
}