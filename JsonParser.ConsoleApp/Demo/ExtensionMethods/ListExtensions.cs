namespace JsonParser.ConsoleApp.Demo.ExtensionMethods;

public static class ListExtensions
{
    public static string ToStringEx<T>(this IList<T> list)
    {
        if (list == null) 
            return "null";
        
        var items = list.Select(item => (item?.ToString() ?? "null"));
        return "[" + string.Join(",", items) + "]";
    }
}