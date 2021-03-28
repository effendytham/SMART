using System;
using Backend.Module.Services.Json;
using Newtonsoft.Json;

namespace Backend.Module.Services.NDJson
{
   public static class Extensions
    {
        public static void AddItem(this NDJsonContainer ndjson, string jsonstring){
            ndjson.Content += jsonstring.Minify() + Environment.NewLine; //As for NDJson spec, last record should have a new line specified
        }

        /*
        void RemoveItem(int index);
        void RemoveFirst();
        */
    }
}