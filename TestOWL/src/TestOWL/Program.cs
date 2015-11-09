using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestOWL
{
    public class Program
    {
       private string url = "https://localhost:32297";
        private FloraObj TestObj;
        private int error;
        private int success;
        private int total;

        void TestGarbage()
        {
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "POST";

                Console.WriteLine("Sending POST Request");
                using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
                {
                    string json = "FAILVALUE";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();

                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);

                Console.WriteLine("Response from Server");
                string text = sr.ReadToEnd();
                Console.WriteLine("Response from Server");
                Console.WriteLine(text);

                if (text == "Invalid Message!")
                {
                    total++;
                    success++;
                    Console.WriteLine("Invalid Data test SUCCESS");
                }
            }
            catch(Exception e)
            {
                total++;
                error++;
                Console.WriteLine("Invalid Data test FAILURE");
            }
        }


        void GenerateGetRequest()
        {
            
            HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
            GETRequest.Method = "POST";
            
            Console.WriteLine("Sending POST Request");
            using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
            {
                string json = "{\"LeafCount\":\"4\"," +
                              "\"Color\":\"Green\","  +
                              "\"Name\":\"Ted\"," +
                              "\"Extra\":\"\"" +
                              "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            
            HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();
            
            

            Stream GETResponseStream = GETResponse.GetResponseStream();
            StreamReader sr = new StreamReader(GETResponseStream);
            string text = sr.ReadToEnd();
            Console.WriteLine("Response from Server");
            Console.WriteLine(text);

         

        }
        

        /// <summary>
        /// Given a list, find the given plant name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public FloraObj FindItem(string name, IList<FloraObj> list)
        {
            int idx = 0;
            for(int ii = 0; ii < list.Count; ii++)
            {
                if(list[ii].Name == name)
                {
                    idx = ii;
                    break;
                }
            }

            return list[idx];

        }

        /// <summary>
        /// Test the connection to the database
        /// </summary>
        void TestConnection()
        {
            Console.WriteLine("Testing Connection");
            try
            {
                success++;
                GenerateGetRequest();
                Console.WriteLine("Connection Success");
            }
            catch(Exception e)
            {
                error++;
                Console.WriteLine(e.ToString());
                Console.WriteLine("Connection Failed");
            }
            total++;
        }
        
        void TestQueryState(string state, int queryval)
        {
            Console.WriteLine("Testing Query State:  " + state);
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "POST";

                string json = "{\"" + state + "\":" + queryval + "," + "}";
              
                using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();

                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);

                string resultjson = sr.ReadToEnd();
                IList<FloraObj> FLO = JsonConvert.DeserializeObject<List<FloraObj>>(resultjson);

                if (FLO == null || FLO.Count < 0)
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }
                else if (CompareObjects(TestObj, FindItem(TestObj.Name,FLO),false))
                {
                    Console.WriteLine("TestQuery " + state + ": SUCCESS");
                    success++;
                }
                else
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("TestQuery " + state + ": FAILED");
                error++;
            }
            total++;
        }

        /// <summary>
        /// Test the database fields to make sure they exist
        /// </summary>
        void TestQueryState(string state, string queryval)
        {
            Console.WriteLine("Testing Query State:  " + state);
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "POST";

                string json = "{\"" + state +"\":\""+ queryval + "\"," +"}";

                using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();

                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);

                string resultjson = sr.ReadToEnd();
                IList<FloraObj> FLO = JsonConvert.DeserializeObject<List<FloraObj>>(resultjson);

                if (FLO == null || FLO.Count <= 0)
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }
                else if (CompareObjects(TestObj, FindItem(TestObj.Name, FLO), false))
                {
                    Console.WriteLine("TestQuery " + state + ": SUCCESS");
                    success++;
                }
                else
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("TestQuery " + state + ": FAILED");
                error++;
            }
            total++;
        }

        void TestQueryFields()
        {
            TestQueryState("PlantId", TestObj.PlantId);
            TestQueryState("Name", TestObj.Name);
            TestQueryState("ColorFlower", TestObj.ColorFlower);
            TestQueryState("ColorFoliage", TestObj.ColorFoliage);
            TestQueryState("ColorFruitSeed", TestObj.ColorFruitSeed);
            TestQueryState("TextureFoliage", TestObj.TextureFoliage);
            TestQueryState("Shape", TestObj.Shape);
            TestQueryState("Pattern", TestObj.Pattern);
            TestQueryState("USState", TestObj.USState);
            TestQueryState("Type", TestObj.Type);
        }

        void TestORFields()
        {
            TestQueryState("USState", TestObj.USState + ",FA,CA");
            TestQueryState("Type", TestObj.Type + "vine,shrub");
        }

        void TestImageLaunch()
        {
            Console.WriteLine("Launching image in browser window...");
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "POST";

                string json = "{\"PlantId\":\"" + TestObj.PlantId + "\"," + "}";

                using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();

                Stream GETResponseStream = GETResponse.GetResponseStream();
                StreamReader sr = new StreamReader(GETResponseStream);

                string resultjson = sr.ReadToEnd();
                IList<FloraObj> FLO = JsonConvert.DeserializeObject<List<FloraObj>>(resultjson);
                if (FLO != null && FLO.Count > 0)
                {
                    Process.Start("HTTP://WWW." + FLO[0].ImageURL);
                    Process.Start(FLO[0].GoogleURL);
                    Process.Start(FLO[0].GoogleImageURL);
                }
            }
            catch
            {
                Console.WriteLine("Image Launch failed.");
            }

        }


        //These are the URLs for the plant jpgs from USDA
        public static string imageURL(string plant_id)
        {
            return "http://plants.usda.gov/gallery/pubs/" + plant_id + "_001_pvp.jpg";
        }

        bool CompareObjects(FloraObj FO, FloraObj FO2, bool bFullQuery)
        {
            bool bMatch = FO.PlantId.Trim().ToLower() == FO2.PlantId.Trim().ToLower();
            bMatch = bMatch && (FO.Name.Trim().ToLower() == FO2.Name.Trim().ToLower());
            bMatch = bMatch && (FO.Pattern.Trim().ToLower() == FO2.Pattern.Trim().ToLower());
            bMatch = bMatch &&  FO.Shape.Trim().ToLower() == FO2.Shape.Trim().ToLower();
            bMatch = bMatch && (FO.TextureFoliage.Trim().ToLower() == FO2.TextureFoliage.Trim().ToLower());
            bMatch = bMatch && (FO.ColorFlower.Trim().ToLower() == FO2.ColorFlower.Trim().ToLower());
            bMatch = bMatch && (FO.ColorFoliage.Trim().ToLower() == FO2.ColorFoliage.Trim().ToLower() && FO.ColorFruitSeed.Trim().ToLower() == FO2.ColorFruitSeed.Trim().ToLower());

            if (bMatch && bFullQuery)
            {
                string[] types1 = FO.Type.Split(',');
                string[] types2 = FO2.Type.Split(',');

                int length1 = types1.GetLength(0);
                int length2 = types2.GetLength(0);
                bMatch = compareSplit(types1, types2, length1, length2);
            }

            if (bMatch && bFullQuery)
            {
                string[] state1 = FO.USState.Split(',');
                string[] state2 = FO2.USState.Split(',');

                int length1 = state1.GetLength(0);
                int length2 = state2.GetLength(0);
                bMatch = compareSplit(state1, state2, length1, length2);
            }

            if(bMatch)
            {
              //  bMatch = (FO2.ImageURL == imageURL(FO.PlantId));
            }

            return bMatch;
        }


        private bool compareSplit(string[] str1, string[] str2, int length1, int length2)
        {
            bool bMatch = true;

            if (length1 != length2)
                bMatch = false;
            else
            {

                for (int ii = 0; ii < length1; ii++)
                {
                    if (str1[ii].Trim().ToLower() != str2[ii].Trim().ToLower())
                    {
                        bMatch = false;
                        break;
                    }
                }
            }

            return bMatch;
        }


        private void FillTestObject()
        {
            TestObj = new FloraObj();
            TestObj.PlantId = "ABGR4";
            TestObj.Name = "Abelia ??grandiflora";
            TestObj.ColorFlower = "Purple";
           TestObj.ColorFoliage = "Dark Green";
           TestObj.ColorFruitSeed = "Brown";
           TestObj.TextureFoliage = "Medium";
           TestObj.Shape = "Semi-Erect";
           TestObj.Pattern = "Dicot";
           TestObj.USState = "FL";
           TestObj.Type = "Shrub";
            TestObj.ImageURL = "";
        }


        public void Main(string[] args)
        {
            error = 0;
            success = 0;
            total = 0;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            //---Set up the comparison object
            FillTestObject();

            TestGarbage();

            //---Test insert
            TestConnection();

            //---Test the query
            TestQueryFields();

            //---Test the comple queries
            TestORFields();

            //---Test the image launching code
            TestImageLaunch();
 

            Console.WriteLine("There were " + error + " errors and " + success + " successful tests out of " + total + " total tests.");

            Console.ReadLine();
        }
    }
}
