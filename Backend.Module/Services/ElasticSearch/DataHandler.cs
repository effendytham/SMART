using System;
using System.Collections.Generic;
using Backend.Module.Services.NDJson;
using Backend.Module.Services.SMART;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend.Module.Services.ElasticSearch
{
   public class DataHandler : IDataHandler
   {
      public string ParseToElasticBulkFormat(string bulkJson, string idProperty, string index)
      {
            NDJsonContainer ndjson = new NDJsonContainer();
            List<PropertyContainer> properties = JsonConvert.DeserializeObject<List<PropertyContainer>>(bulkJson);
            Console.WriteLine("succes deserialize");
            int count = 0;
            foreach(PropertyContainer item in properties){
                count += 1;
                //if (count == 20) break;
                ElasticDataHeader header = new ElasticDataHeader(index, Guid.NewGuid().ToString());
                ndjson.AddItem(JsonConvert.SerializeObject(header));
                ndjson.AddItem(JsonConvert.SerializeObject(item));
            }
            return ndjson.Content;
      }
   }
}