using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AS.Findbox.Scraper.Cars.Utils
{
    internal static class JsonExtractor
    {
        public static async Task<T> Parse<T>(this HttpContent content, string name)
        {
            var html = await content.ReadAsStringAsync();
            var regex = new Regex($"{{\"{name}.+}}");
            var json = regex.Match(html).Value;
            var jObject = JsonConvert.DeserializeObject<JObject>(json);
            var result = jObject.GetValue(name).ToObject<T>();
            return result;
        }
    }
}
