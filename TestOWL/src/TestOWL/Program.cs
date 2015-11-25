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
        
        private FloraObj Query(string plantid)
        {
            FloraObj r;
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "POST";

                string json = "{\"PlantId\":\"" + plantid + "\"," + "}";

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
                    Console.WriteLine("PlantId: FAILED");
                    return null;
                }
                else if (FLO[0] != null)
                {
                    Console.WriteLine("PlantId:  SUCCESS");
                    return FLO[0];
                }
                else
                {
                    Console.WriteLine("PlantId: FAILED");
                    return null;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("PlantId: FAILED (exception)");
                error++;
            }
            return null;
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

        private FloraObj FillInsertObject(string plantid)
        {
            FloraObj FL = new FloraObj();
            FL.PlantId = plantid;
            FL.Name = "Abelia ??grandiflora";
            FL.ColorFlower = "Purple";
            FL.ColorFoliage = "Dark Green";
            FL.ColorFruitSeed = "Brown";
            FL.TextureFoliage = "Medium";
            FL.Shape = "Semi-Erect";
            FL.Pattern = "Dicot";
            FL.USState = "FL";
            FL.Type = "Shrub";
            FL.ImageURL = "Test";
            FL.UserName = "Admin";
            FL.Password = "Admin";
            return FL;
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

        string Delete(FloraObj FLO)
        {
            Console.WriteLine("Testing Delete");
            try
            {
                string json = JsonConvert.SerializeObject(FLO);
                
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "DELETE";

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
                FLO = JsonConvert.DeserializeObject<FloraObj>(resultjson);
                return FLO.Result;
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Test Delete:  FAILED");
                error++;
                return "Exception";
            }
            total++;
        }

        /// <summary>
        /// Insert the flora object
        /// </summary>
        /// <param name="Flora">Insert the given flora object</param>
        FloraObj Insert(FloraObj Flora)
        {
            Console.WriteLine("Testing Insert");
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "PUT";

                string json = JsonConvert.SerializeObject(Flora);

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
                FloraObj FLO = JsonConvert.DeserializeObject<FloraObj>(resultjson);

                if (FLO.Result == "Inserted Entry")
                {
                    Console.WriteLine("Test Insert:  SUCCESS");
                    success++;
                }
                else
                {
                    Console.WriteLine("Test Insert:  FAILED");
                    error++;
                }

                total++;
                return FLO;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Test Insert:  FAILED");
                error++;
                return null;
            }
            total++;
            
        }

        private string TestInsert()
        {
            //---Test a basic insert
            FloraObj Flora = FillInsertObject("test");
            Flora = Insert(Flora);

            FloraObj Compare = Query(Flora.PlantId);

            if(CompareObjects(Flora,Compare, false))
            {
                Console.WriteLine("Insert First Compare:  Success");
                success++;
                total++;
            }
            else
            {
                Console.WriteLine("Insert First Compare:  Failure");
                error++;
                total++;
            }

            //---Test no plantid
            Flora = FillInsertObject("");
            Flora = Insert(Flora);

            Compare = Query(Flora.PlantId);

            if (CompareObjects(Flora, Compare, false))
            {
                Console.WriteLine("Insert Second Compare:  Success");
                success++;
                total++;
            }
            else
            {
                Console.WriteLine("Insert Second Compare:  Failure");
                error++;
                total++;
            }

            //---Test duplicate plantid
            Flora = FillInsertObject("test");
            Flora = Insert(Flora);

            Compare = Query(Flora.PlantId);

            if (CompareObjects(Flora, Compare, false))
            {
                Console.WriteLine("Insert Third Compare:  Success");
                success++;
                total++;
            }
            else
            {
                Console.WriteLine("Insert Third Compare:  Failure");
                error++;
                total++;
            }

            //---Test duplicate plant name
            Flora = FillInsertObject("test");
            Flora = Insert(Flora);

            if (CompareObjects(Flora, Compare, false))
            {
                Console.WriteLine("Insert Fourth Compare:  Success");
                success++;
                total++;
            }
            else
            {
                Console.WriteLine("Insert Fourth Compare:  Failure");
                error++;
                total++;
            }
            return Flora.PlantId;
        }


        private void TestDelete(string plantid)
        {
            FloraObj FLO = new FloraObj();
            FLO.PlantId = plantid;
            FLO.UserName = "Admin";
            FLO.Password = "Admin";


            //---Test delete by plant id
            string result = Delete(FLO);

            if (result == "Deleted Entry")
            {
                Console.WriteLine("Test Delete:  SUCCESS");
                success++;
            }
            else
            {
                Console.WriteLine("Test Delete:  FAILED");
                error++;
            }
            total++;

            FLO.PlantId = "Not in Database";

            //---Test delete by plant id that doesn't exist
            result = Delete(FLO);

            if (result == "Invalid plantID")
            {
                Console.WriteLine("Test Delete:  SUCCESS");
                success++;
            }
            else
            {
                Console.WriteLine("Test Delete:  FAILED");
                error++;
            }
            total++;

            FLO.PlantId = "";

            //---Test delete by plant id that doesn't exist
            result = Delete(FLO);

            if (result == "Authenticated")
            {
                Console.WriteLine("Test Delete:  SUCCESS");
                success++;
            }
            else
            {
                Console.WriteLine("Test Delete:  FAILED");
                error++;
            }
            total++;
        }


        private string Login(string username, string password)
        {
            FloraObj Flora = new FloraObj();
            Flora.UserName = username;
            Flora.Password = password;

            Console.WriteLine("Testing Login");
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "PUT";

                string json = JsonConvert.SerializeObject(Flora);

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
                FloraObj FLO = JsonConvert.DeserializeObject<FloraObj>(resultjson);

          
                return FLO.Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Test Login:  FAILED");
                error++;
                return "";
            }
            total++;

        }


        private void TestLogin()
        {
            string result;
            //---Test invalid login
            result = Login("Fail", "Admin");
            if(result == "Authenticated")
            {
                Console.WriteLine("Test Login:  FAILED");
                error++;
                total++;
            }
            else
            {
                Console.WriteLine("Test Login:  SUCCESS");
                success++;
                total++;
            }

            //---Test valid login
            result = Login("Admin", "Admin");
            if (result == "Authenticated")
            {
                Console.WriteLine("Test Login:  SUCCESS");
                success++;
                total++;
            }
            else
            {
                Console.WriteLine("Test Login:  FAILED");
                error++;
                total++;
            }

            //---Test empty object
            result = Login("", "");
            if (result == "Authenticated")
            {
                Console.WriteLine("Test Login:  FAILED");
                error++;
                total++;
            }
            else
            {
                Console.WriteLine("Test Login:  SUCCESS");
                success++;
                total++;
            }

            //---Test long strings
            result = Login("asdffffffffdfsdafsafsdfadsfdsafdasfasdfadsfsdafadssdfadfsafsda", "afdsasfdfsdafdasfadsafdsfdsafadsfadsfdasfadsfsdaadfsfadsfasd");
            if (result == "Authenticated")
            {
                Console.WriteLine("Test Login:  FAILED");
                error++;
                total++;
            }
            else
            {
                Console.WriteLine("Test Login:  SUCCESS");
                success++;
                total++;
            }

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
            //---No longer necessary - we can't send more than one type or location
            //TestORFields();

            //---Test logging in
            TestLogin();

            //---Try Inserting some values
            string plantid = TestInsert();

            //---Try Deleting
            TestDelete(plantid);


            //---Test the image launching code
            TestImageLaunch();

           
 
            Console.WriteLine("There were " + error + " errors and " + success + " successful tests out of " + total + " total tests.");

            Console.ReadLine();
        }
    }
}
