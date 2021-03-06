using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LaundryManagement.Domain
{
    public class Configuration
    {
        private JObject configuration;
        public Configuration()
        {
            string configurationFilePath = Path.GetFullPath(@"..\..\..\") + @"appsettings.json";
            string json = "";
            using (StreamReader r = new StreamReader(configurationFilePath))
            {
                json = r.ReadToEnd();
            }
            configuration = JsonConvert.DeserializeObject<JObject>(json);
        }

        public T GetValue<T>(string property)
        {
            var value = configuration[property]?.Value<string>() ?? "";
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
