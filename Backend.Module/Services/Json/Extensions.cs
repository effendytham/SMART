using System;
using Newtonsoft.Json;

namespace Backend.Module.Services.Json {
   public static class Extensions {
      public static string Minify(this string json){
         object deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
         return Newtonsoft.Json.JsonConvert.SerializeObject(deserializedJson, Formatting.None); //This will serialize as one line json
      }
   }
}