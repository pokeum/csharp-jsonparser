using System.Collections;
using System.Text;

namespace JsonParser.ConsoleApp.Demo.ExtensionMethods;

public static class ListExtensions
{
    public static string ToStringEx(this IList list)
    {
        if (list == null)
        {
            return "null";
        }

        bool isFirst = true;
        StringBuilder builder = new StringBuilder();
        
        builder.Append("[");
        foreach (var item in list)
        {
            if (!isFirst) builder.Append(", ");
            
            if (item == null)
            {
                builder.Append("null");
            }
            else if (item is IList)
            {
                builder.Append(((IList)item).ToStringEx());
            }
            else if (item is IDictionary)
            {
                builder.Append(((IDictionary)item).ToStringEx());
            }
            else
            {
                builder.Append(item);
            }

            isFirst = false;
        }
        builder.Append("]");

        return builder.ToString();
    }
}