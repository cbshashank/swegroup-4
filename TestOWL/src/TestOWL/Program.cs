using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestOWL
{
    public class Program
    {
       private string url = "http://localhost:32297";
        private FloraObj TestObj;
        private int error;
        private int success;
        private int total;

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

            Console.WriteLine("Response from Server");
            Console.WriteLine(sr.ReadToEnd());
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

                if(FLO == null || FLO.Count < 0)
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }
                else if (CompareObjects(TestObj, FLO[0]))
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

                if (FLO == null || FLO.Count < 0)
                {
                    Console.WriteLine("TestQuery " + state + ": FAILED");
                    error++;
                }
                else if (CompareObjects(TestObj, FLO[0]))
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
            TestQueryState("PlantId", 1);
            TestQueryState("Name", "testvalue");
            TestQueryState("ColorFlower", "red");
            TestQueryState("ColorFoliage", "green");
            TestQueryState("ColorFruitSeed", "blue");
            TestQueryState("TextureFoliage", "foil");
            TestQueryState("Shape", "square");
            TestQueryState("Pattern", "spotted");
            TestQueryState("USState", "MA,CA");
            TestQueryState("Type", "vine");
        }

        void TestORFields()
        {
            TestQueryState("USState", "MA,CA,ME");
            TestQueryState("Type", "vine,table,wine");
        }


        bool CompareObjects(FloraObj FO, FloraObj FO2)
        {
            bool bMatch = FO.PlantId == FO2.PlantId;
            bMatch = bMatch && (FO.Name.Trim() == FO2.Name.Trim());
            bMatch = bMatch && (FO.Pattern.Trim() == FO2.Pattern.Trim());
            bMatch = bMatch && (FO.ImageURL.Trim() == FO2.ImageURL.Trim() && FO.Shape.Trim() == FO2.Shape.Trim());
            bMatch = bMatch && (FO.TextureFoliage.Trim() == FO2.TextureFoliage.Trim());
            bMatch = bMatch && (FO.ColorFlower.Trim() == FO2.ColorFlower.Trim());
            bMatch = bMatch && (FO.ColorFoliage.Trim() == FO2.ColorFoliage.Trim() && FO.ColorFruitSeed.Trim() == FO2.ColorFruitSeed.Trim());

            if (bMatch)
            {
                string[] types1 = FO.Type.Split(',');
                string[] types2 = FO2.Type.Split(',');

                int length1 = types1.GetLength(0);
                int length2 = types2.GetLength(0);
                bMatch = compareSplit(types1, types2, length1, length2);
            }

            if (bMatch)
            {
                string[] state1 = FO.USState.Split(',');
                string[] state2 = FO2.USState.Split(',');

                int length1 = state1.GetLength(0);
                int length2 = state2.GetLength(0);
                bMatch = compareSplit(state1, state2, length1, length2);
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
                    if (str1[ii].Trim() != str2[ii].Trim())
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
            TestObj.PlantId = 1;
            TestObj.Name = "testvalue";
            TestObj.ColorFlower = "red";
           TestObj.ColorFoliage = "green";
           TestObj.ColorFruitSeed = "blue";
           TestObj.TextureFoliage = "foil";
           TestObj.Shape = "square";
           TestObj.Pattern = "spotted";
           TestObj.USState = "MA,CA";
           TestObj.Type = "vine";
            TestObj.ImageURL = "test.com";
        }


        public void Main(string[] args)
        {
            error = 0;
            success = 0;
            total = 0;

            //---Set up the comparison object
            FillTestObject();

            //---Test insert
            TestConnection();

            //---Test the query
            TestQueryFields();

            //---Test the comple queries
            TestORFields();

            Console.WriteLine("There were " + error + " errors and " + success + " successful tests out of " + total + " total tests.");

            Console.ReadLine();
        }
    }
}
