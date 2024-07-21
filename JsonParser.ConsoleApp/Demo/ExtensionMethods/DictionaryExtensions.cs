using System.Collections;
using System.Text;

namespace JsonParser.ConsoleApp.Demo.ExtensionMethods;

public static class DictionaryExtensions
{
    public static string ToStringEx(this IDictionary dictionary)
    {
        if (dictionary == null)
        {
            return "null";
        }
        
        bool isFirst = true;
        StringBuilder builder = new StringBuilder();
        
        builder.Append("{");
        foreach (DictionaryEntry kvp in dictionary)
        {
            if (!isFirst) builder.Append(", ");
            
            builder.Append($"\"{kvp.Key}\": ");

            if (kvp.Value == null)
            {
                builder.Append("null");
            }
            else if (kvp.Value is IList)
            {
                builder.Append(((IList)kvp.Value).ToStringEx());
            }
            else if (kvp.Value is IDictionary)
            {
                builder.Append(((IDictionary)kvp.Value).ToStringEx());
            }
            else
            {
                builder.Append(kvp.Value);
            }

            isFirst = false;
        }
        builder.Append("}");

        return builder.ToString();
    }
}