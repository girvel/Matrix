using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Matrix.Tools
{
    public class ConstantAttribute : Attribute
    {
        private static JObject FileData;

        static ConstantAttribute()
        {
            FileData = JObject.Parse(File.ReadAllText("../../Constants.json"));
        }
        
        

        public static void UpdateConstants(object instance)
        {
            foreach (
                var field 
                in instance
                    .GetType()
                    .GetFields()
                    .Where(f => f.GetCustomAttribute<ConstantAttribute>() != null))
            {
                field.SetValue(
                    instance, 
                    FileData[$"{instance.GetType().Name}.{field.Name}"].ToObject(field.FieldType));
            }
        }
    }
}