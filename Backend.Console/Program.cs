using System;
using System.IO;
using System.Net.Http;
using Backend.Module;
using Backend.Module.Services.ElasticSearch;
using Backend.Module.Services.NDJson;

namespace Backend.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9200");
            IDataHandler dataHandler = new DataHandler();
            string ndjson = dataHandler.ParseToElasticBulkFormat(File.ReadAllText("/Users/effendytham/Desktop/projects/elasticsearch/data/6removedspace.json"),"","rumah");

            StringContent content = new StringContent(ndjson);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage res = client.PostAsync("/_bulk", content).Result;
            System.Console.Write(res.Content.ReadAsStringAsync().Status);
            */

            
            string text = File.ReadAllText("/Users/effendytham/Desktop/projects/elasticsearch/data/3test.json");
            bool isOpen = false;
            int length = text.Length;
            int counter = 0;
            while (counter < length){
                try {
                    string chunk = text.Substring(counter,1);
                    if (chunk == "\""){
                        isOpen = !isOpen;
                        System.Console.WriteLine("Currently on char : " + counter + " , negative");
                        counter += 1;
                    }
                    else if (chunk == " ") {
                        if (!isOpen){
                            string newText = text.Substring(0, counter) + text.Substring(counter + 1);
                            text = newText;
                            length = text.Length;
                            System.Console.WriteLine("Currently on char : " + counter + " , YES YES FOUND IT !!!!!!");
                        }
                        else {
                            System.Console.WriteLine("Currently on char : " + counter + " , negative");
                            counter += 1;
                        }
                    }
                    else {
                        System.Console.WriteLine("Currently on char : " + counter + " , negative");
                        counter += 1;
                    }
                }
                catch (Exception ex){
                    throw ex;
                }
            }
            File.WriteAllText("/Users/effendytham/Desktop/projects/elasticsearch/data/3removedspace.json",text);
            
        }
    }
}
