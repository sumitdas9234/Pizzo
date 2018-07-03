using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Pizzo.Utilities
{
    public class Utilities
    {
        public static JObject FromJSONtoObject()
        {
            JObject menu;
            using (StreamReader file = File.OpenText(@"c:\Users\Sumit Das\source\repos\Pizzo\Pizzo\Resources\menu.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                 menu = (JObject)JToken.ReadFrom(reader);
            }

            return menu;
        }
    }
}