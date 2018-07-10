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
        //Utilities functions for handling json data


        //Load json from a path
        public static JObject LoadJSON(string path)
        {
            JObject jsonObject;
            //using streamreader and the path
            using (StreamReader file = File.OpenText(@path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                 jsonObject = (JObject)JToken.ReadFrom(reader);
            }

            return jsonObject;
        }
        //Convert a JSON Object to Array
        public static JArray ToArray(JObject obj)
        {
            //string data = obj.ToString()
            JArray array = JArray.Parse((obj.ToString()));
            return array;
        }

        //Map a JSONs Object to C# Object
        public static MenuItem MapToPizzaObject(JObject data)
        {
         //retreiving the list of items in veg and non-veg menu   
            JArray vegMenu = (JArray)data["veg"];
            JArray nonVegMenu = (JArray)data["nonveg"];
            //Instansiating MenuItem class
            MenuItem menu = new MenuItem()
            {
                veg = new List<Pizza>(),
                nonveg = new List<Pizza>()
            };
            //Retreiving the Veg Menu from the JSON
            foreach(JObject vegItem in vegMenu)
            {
                //mapping to object
                Pizza item = new Pizza()
                {
                    id = (string)vegItem["id"],
                    name = (string)vegItem["name"],
                    desc = (string)vegItem["desc"],
                    price = (int)vegItem["price"],
                    image = (string)vegItem["image"]
                };
                //Adding to the list
                menu.veg.Add(item);
            }

            //Retreiving the Non Veg Menu from the JSON
            foreach (JObject nonVegItem in nonVegMenu)
            {
                //mapping to object
                Pizza item = new Pizza()
                {
                    id = (string)nonVegItem["id"],
                    name = (string)nonVegItem["name"],
                    desc = (string)nonVegItem["desc"],
                    price = (int)nonVegItem["price"],
                    image = (string)nonVegItem["image"]
                };
                //Adding to the list
                menu.nonveg.Add(item);
            }
            return menu;
        }



        public static AddonItems MapToAddonObject(JObject data)
        {
            //retreiving the list of items in addons
            JArray addons = (JArray)data["addons"];
            //Instansiating AddonItems class
            AddonItems menu = new AddonItems()
            {
                addons = new List<Addon>()
            };
            //Retreiving the Menu from the JSON
            foreach (JObject addon in addons)
            {
                //mapping to object
                Addon item = new Addon()
                {
                    id = (string)addon["id"],
                    name = (string)addon["name"],
                    desc = (string)addon["desc"],
                    price = (int)addon["price"],
                    image = (string)addon["image"]
                };
                //Adding to the list
                menu.addons.Add(item);
            }
            return menu;
        }
    }
}