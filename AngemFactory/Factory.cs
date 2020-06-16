using System;
using System.Collections.Generic;
using System.Linq;

namespace AngemFactory
{
    public class Factory
    {
        public string[] Types;

        public int[] Sizes;

        public string CommonTemplate;
        
        public readonly Dictionary<int, string> 
            ContentTemplates = new Dictionary<int, string>(), 
            ConversionTemplates = new Dictionary<int, string>();

        public string LibraryName;

        public Dictionary<string, (bool @explicit, string type)[]> Conversions 
            = new Dictionary<string, (bool @explicit, string type)[]>();
        
        public IEnumerable<(string name, string content)>Generate()
        {
            return Types
                .SelectMany(
                    type => Sizes.Select(
                        size => (
                            type + size, 
                            Complete(CommonTemplate, type, size))));
        }

        private string Complete(string template, string type, int size, bool stopRecursion = false)
        {
            template = template
                .Replace("$TYPE$", type + size)
                .Replace("$ROOT$", LibraryName)
                .Replace("$T$", type)
                .Replace(
                    "$CONVERSIONS$", 
                    Conversions.ContainsKey(type)
                        ? Conversions[type]
                            .Select(c => ConversionTemplates[size]
                                .Replace("$FROM$", type)
                                .Replace("$TO$", c.type)
                                .Replace("$KEYWORD$", c.@explicit ? "explicit" : "implicit"))
                            .Aggregate("", (current, sum) => current + "\n" + sum)
                        : "");

            return stopRecursion
                ? template
                : template.Replace(
                    "$CONTENT$",
                    Complete(ContentTemplates[size], type, size, true));
        }
    }
}