// Utils.cs
using System.Text.Json;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Helpers
{
    public class Utils
    {
        private static readonly Utils _instance = new Utils();
        public static Utils Instance => _instance;
        private Utils() { }

        public string ToJson<T>(List<T> data, List<string>? selectedColumns = null)
        {
            var result = new List<Dictionary<string, object>>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in data)
            {
                var dict = new Dictionary<string, object>();
                if (selectedColumns == null || !selectedColumns.Any())
                {
                    foreach (var prop in props)
                    {
                        dict[prop.Name] = prop.GetValue(item);
                    }
                }
                else
                {
                    foreach (var prop in props)
                    {
                        if (selectedColumns.Contains(prop.Name))
                        {
                            dict[prop.Name] = prop.GetValue(item);
                        }
                    }
                }
                result.Add(dict);
            }

            return JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}