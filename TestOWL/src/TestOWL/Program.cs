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
        void GenerateGetRequest()
        {
            string url = "http://localhost:30992";
            HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(url);
            GETRequest.Method = "POST";
            
            Console.WriteLine("Sending POST Request");
            using (var streamWriter = new StreamWriter(GETRequest.GetRequestStream()))
            {
                string json = "{\"LeafCount\":\"4\"," +
                              "\"Color\":\"Green\","  +
                              "\"Name\":\"Ted\"," +
                              "\"Extra\":\"Notes\"" +
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
            Console.ReadLine();
        }

        public void Main(string[] args)
        {
            GenerateGetRequest();
        }
    }
}
