namespace JsonParser.ConsoleApp.Demo.ExtensionMethods;

public static class DictionaryExtensions
{
    public static string ToStringEx<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        if (dictionary == null) 
            return "null";

        var items = from kvp in dictionary 
            select (kvp.Key?.ToString() ?? "null") + "=" + (kvp.Value?.ToString() ?? "null");
        return "{" + string.Join(",", items) + "}";
    }
}