using System.Text.Json;
using System.Reflection;

namespace LabProject.Helpers
{
    public class Utils
    {
        public static Utils Instance { get; } = new Utils();

        private Utils() { }

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public string ToJson<T>(List<T> data, List<string>? selectedColumns = null)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T item in data)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (selectedColumns == null || selectedColumns.Count == 0)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        dict[prop.Name] = prop.GetValue(item);
                    }
                }
                else
                {
                    foreach (PropertyInfo prop in props)
                    {
                        if (selectedColumns.Contains(prop.Name))
                        {
                            dict[prop.Name] = prop.GetValue(item);
                        }
                    }
                }
                result.Add(dict);
            }

            return JsonSerializer.Serialize(result, _jsonOptions);
        }
    }
}
