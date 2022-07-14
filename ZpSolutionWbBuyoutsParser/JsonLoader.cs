using Global.ZennoLab.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser
{
    internal class JsonLoader
    {
        internal static T LoadJson<T>(string filePath)
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
    }
}
