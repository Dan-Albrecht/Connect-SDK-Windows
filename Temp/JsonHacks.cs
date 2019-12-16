using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectSdk
{
    static class JsonHacks
    {
        public static JArray GetNamedArray(this JObject jObject, string name)
        {
            return (JArray)jObject[name];
        }

        public static bool GetNamedBoolean(this JObject jObject, string name)
        {
            return (bool)jObject[name];
        }

        public static int GetNamedNumber(this JObject jObject, string name, int dunno = 0)
        {
            return (int)jObject[name];
        }

        public static JObject GetNamedObject(this JObject jObject, string name, string dunno = "")
        {
            return (JObject)jObject[name];
        }

        public static JToken GetNamedValue(this JObject jObject, string name)
        {
            return jObject[name];
        }

        public static JObject GetObject(this JToken token)
        {
            return (JObject)token;
        }

        public static void SetNamedValue(this JObject jObject, string name, string value)
        {
            jObject[name] = value;
        }

        public static void SetNamedValue(this JObject jObject, string name, double value)
        {
            jObject[name] = value;
        }

        public static void SetNamedValue(this JObject jObject, string name, int value)
        {
            jObject[name] = value;
        }

        public static void SetNamedValue(this JObject jObject, string name, JObject value)
        {
            jObject[name] = value;
        }

        public static string Stringify(this JObject jObject)
        {
            return jObject.ToString();
        }

        public static bool TryParse(string value, out JObject result)
        {
            result = null;
            return false;
        }

        public static string GetNamedString(this JToken token, string name, string dunno = "")
        {
            return token[name].ToString();
        }

        public static bool GetNamedBoolean(this JObject jObject, string name, bool dunno)
        {
            return (bool)jObject[name];
        }
    }
}
