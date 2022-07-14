using Global.ZennoLab.Json.Linq;
using Global.ZennoLab.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser
{
    internal class JsonFile
    {
        internal static T Load<T>(string filePath)
        {
            try
            {
                string content = File.ReadAllText(filePath);
                T jsonObject = JToken.Parse(content).ToObject<T>();
                return jsonObject;
            }
            catch
            {
                return default;
            }
        }

        internal static void Save<T>(string filePath, T jsonObject)
        {
            string jsonContent = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}
