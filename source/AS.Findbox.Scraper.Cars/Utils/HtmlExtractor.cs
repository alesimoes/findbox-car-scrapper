using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AS.Findbox.Scraper.Cars.Utils
{
    internal static class HtmlExtractor
    {

        public static async Task<string> GetInputValue(this HttpContent content, string name)
        {
            return await content.GetHtmlValue(name, "input");
        }

        public static async Task<string> GetSpanText(this HttpContent content, string className)
        {
            var html = await content.ReadAsStringAsync();
            var regexElement = new Regex($"((<span.*class=\".*{className}\">?\\s).*?\\s+(<\\/span>))|((<span.*class=\".*{className}\">).*(<\\/span>))");
            var htmlElement = regexElement.Match(html).Value;
            var regexSpan = new Regex($"(<span.*class=\"{className}\">)");
            var spanTag = regexSpan.Match(htmlElement).Value;

            if (string.IsNullOrEmpty(htmlElement))
            {
                return String.Empty;
            }

            htmlElement = htmlElement.Replace(spanTag, "").Replace("</span>", "").Trim();
            return htmlElement;
        }

        public static async Task<List<Tuple<string, string>>> GetSelectOptions(this HttpContent content, string dataActivityKey)
        {
            var list = new List<Tuple<string, string>>();
            var html = await content.ReadAsStringAsync();
            var regexElement = new Regex($"<select .*data-activitykey=\"{dataActivityKey}\".*>");
            var htmlElement = regexElement.Match(html).Value;
            var regexOptions = new Regex($"(<option.?.value=\"[A-z- ]*\">).([A-z- ]*)(<\\/option>)");
            var optionsHtml = regexOptions.Matches(htmlElement);

            foreach (Match option in optionsHtml)
            {
                var regexValue = new Regex($"value=\"[A-z ]*\"");
                var value = new string(regexValue.Match(option.Value).Value.Replace("value=\"", "").SkipLast(1).ToArray());
                var regexDescription = new Regex($"([A-z- ]*)(<\\/option>)");
                var description = regexDescription.Match(option.Value).Value.Replace("</option>", "");
                list.Add(new Tuple<string, string>(value, description));
            }

            return list;
        }

        public static async Task<string> GetHtmlValue(this HttpContent content, string name, string tag)
        {
            var html = await content.ReadAsStringAsync();
            var regexElement = new Regex($"(<{tag}.*name=\"{name}\".*>)|(value=)");
            var htmlElement = regexElement.Match(html).Value;
            var regexValue = new Regex("value=\"\\w.*\"");
            return regexValue.Match(htmlElement).Value.Replace("value=\"", "").Replace("\"", "");
        }

        public static async Task<List<string>> GetExpression(this HttpContent content, string regex)
        {
            var html = await content.ReadAsStringAsync();
            var regexElement = new Regex(regex);
            return regexElement.Matches(html).Select(x => x.Value).ToList();
        }

        public static string GetImageLink(string html)
        {
            var regexElement = new Regex($"(https:\\/\\/([\\w-.\\/$&])+)");
            return regexElement.Match(html).Value;
        }

        public static string GetGuid(string html)
        {
            var regexElement = new Regex($"id=\"([A-z-0-9]+)");
            return new string(regexElement.Match(html).Value.Replace("id=\"", "").Take(36).ToArray());
        }

        public static string GetTagText(string html)
        {
            var regexElement = new Regex($"[A-z-0-9 ,.]+<");
            return new string(regexElement.Match(html).Value.SkipLast(1).ToArray());
        }
    }
}
