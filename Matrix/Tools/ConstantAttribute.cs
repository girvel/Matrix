using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Matrix.Tools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConstantAttribute : Attribute
    {
        private static readonly JObject FileData;

        static ConstantAttribute()
        {
            FileData = JObject.Parse(File.ReadAllText("../../Constants.json"));
        }
        
        

        public static void UpdateConstants(object instance)
        {
            Debug.Assert(instance != null);
            
            foreach (
                var field 
                in instance
                    .GetType()
                    .GetFields()
                    .Where(f => f.GetCustomAttribute<ConstantAttribute>() != null))
            {
                field.SetValue(
                    instance, 
                    FileData[instance.GetType().Name][field.Name].ToObject(field.FieldType));
            }
        }
    }
}