using Newtonsoft.Json;
using System;
using System.IO;

namespace ordermanagement.shared.cronus_edge_api.Extensions
{
    public static class StringExtensions
    {
        public static string NullSafeTrim(this string value) => string.IsNullOrEmpty(value) ? string.Empty : value.Trim();

        public static bool IsNullOrWhitespace(this string value) => string.IsNullOrWhiteSpace(value);

        public static T Deserialize<T>(this string s) => s.IsNullOrWhitespace() ? default : JsonConvert.DeserializeObject<T>(s);

        public static string Serialize<T>(this T o) => o == null ? default : JsonConvert.SerializeObject(o);

        public static T Deserialize<T>(this string s, JsonSerializerSettings settings) =>
                    s.IsNullOrWhitespace() ? default(T) : JsonConvert.DeserializeObject<T>(s, settings);

        public static bool Equals_IgnoreCase(this string s, string compare) =>
            String.Equals(s, compare, StringComparison.OrdinalIgnoreCase);

        public static bool Equals_TrimmedIgnoreCase(this string s, string compare) =>
            String.Equals(s?.Trim(), compare?.Trim(), StringComparison.CurrentCultureIgnoreCase);

        public static System.Collections.Generic.IEnumerable<string> SplitOnNewline(this string s)
        {
            if (s != null)
            {
                string line;
                using (var sr = new StringReader(s))
                    while ((line = sr.ReadLine()) != null)
                        yield return line;
            }
        }

        public static string RemoveEnd(this string value, int numCharsToRemove) =>
            value.Substring(0, value.Length - numCharsToRemove);
    }
}
