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
        void TestR1()
        {
            Console.WriteLine("Testing R1");
            try
            {
                GenerateGetRequest();
                Console.WriteLine("R1 Success");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("R1 Failed");
            }
        }

        /// <summary>
        /// Test the database fields to make sure they exist
        /// </summary>
        void TestR2()
        {
            Console.WriteLine("Testing R2");
            try
            {
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
                GETRequest.Method = "PUT";

                string json = "{\"Location\":\"Northeast US\"," +
                                 "\"Setting\":\"city\"," +
                                 "\"Color\":\"red\"," +
                                 "\"Shape\":\"Value\"" +
                                 "\"Texture\":\"Rough\"" +
                                 "\"Smell\":\"Tropical\"" +
                                 "\"Pattern\":\"Spotted\"" +
                                 "\"Plant Growth\":\"Vine\"" +
                                 "}";

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
                Console.WriteLine(resultjson);

                if(resultjson == json)
                {
                    Console.WriteLine("R2 Success");
                }
                else
                {
                    Console.WriteLine("R2 Failed");
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("R2 Failed");
            }
        }

        void TestR3()
        {

        }

        public void Main(string[] args)
        {
            
            TestR1();
            TestR2();
            Console.ReadLine();
        }
    }
}
