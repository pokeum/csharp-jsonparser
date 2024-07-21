#define UPDATED_VERSION
// #undef UPDATED_VERSION

using System.Collections;
using JsonParser.ConsoleApp.Demo.ExtensionMethods;

namespace JsonParser.ConsoleApp.Demo.Serialize;

public class TypeInference
{
    public static void Demo()
    {
        ArraysDemo();
        IndexableCollectionsDemo();
    }

    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays
    private static void ArraysDemo()
    {
        // Declare and set single-dimensional array
        int[] array = new int[] { 1, 2, 3, 4, 5, 6 };
        SerializeValue(array);

        // Declare and set two-dimensional array
        int[,] multiDimensionalArray = { { 1, 2, 3 }, { 4, 5, 6 } };
        SerializeValue(multiDimensionalArray);

        // Declare a jagged array.
        int[][] jaggedArray = new int[6][];
        // Set the values of the first array in the jagged array structure.
        jaggedArray[0] = new int[] { 1, 2, 3, 4 };
        SerializeValue(jaggedArray);
    }

    private static void IndexableCollectionsDemo()
    {
        // Array
        int[] numbers = { 1, 1, 2, 3, 5, 8, 13 };
        IEnumerable enumerableNumbers = numbers;
        SerializeValue(numbers);
        SerializeValue(enumerableNumbers);

        // List
        var words = new List<string> { "one", "two" };
        SerializeValue(words);

        // Stack: Last in, First Out (LIFO)
        var names = new string[] { "Ankit", "Marius", "Raffaele" };
        Stack<string> nameStack = new Stack<string>(names); // "Raffaele", "Marius", "Ankit"
        nameStack.Push("Alice"); // "Alice", "Raffaele", "Marius", "Ankit"
        nameStack.Push("Bob"); // "Bob", "Raffaele", "Marius", "Ankit"
        nameStack.Push("Charlie"); // "Charlie", "Bob", "Raffaele", "Marius", "Ankit"
        nameStack.Pop(); // "Bob", "Raffaele", "Marius", "Ankit"
        SerializeValue(nameStack);

        // Queue: First in, First Out (FIFO)
        Queue<string> nameQueue = new Queue<string>(names); // "Ankit", "Marius", "Raffaele"
        nameQueue.Enqueue("Alice"); // "Ankit", "Marius", "Raffaele", "Alice"
        nameQueue.Enqueue("Bob"); // "Ankit", "Marius", "Raffaele", "Alice", "Bob"
        nameQueue.Enqueue("Charlie"); // "Ankit", "Marius", "Raffaele", "Alice", "Bob", "Charlie"
        nameQueue.Dequeue(); // "Marius", "Raffaele", "Alice", "Bob", "Charlie"
        SerializeValue(nameQueue);

        // Linked List
        var linkedList = new LinkedList<int>();
        var n2 = linkedList.AddFirst(2); // 2
        linkedList.AddFirst(1); // 1, 2
        linkedList.AddLast(5); // 1, 2, 5
        linkedList.AddLast(7); // 1, 2, 5, 7
        linkedList.AddAfter(n2, 3); // 1, 2, 3, 5, 7
        SerializeValue(linkedList);

        // HashSet
        HashSet<int> hashSetNumbers = new HashSet<int>(numbers);
        SerializeValue(hashSetNumbers);
    }

    private static void SerializeValue(object value)
    {
#if UPDATED_VERSION
#else
        IList asList;
#endif
        IDictionary asDict;
        string asStr;

        if (value == null || value is Delegate)
        {
            Console.WriteLine("# Is Null");
        }
        else if ((asStr = value as string) != null)
        {
            Console.WriteLine($"# Is String: {asStr}");
        }
        else if (value is bool)
        {
            Console.WriteLine($"# Is Boolean: {((bool)value ? "true" : "false")}");
        }
#if UPDATED_VERSION
        else if (IsList(value, out var asList))
        {
            Console.WriteLine($"# Is List: {asList.ToStringEx()}");
        }
#else
        else if ((asList = value as IList) != null)
        {
            var list = new List<object>();
            foreach (var element in asList)
            {
                list.Add(element);
            }

            Console.WriteLine($"# Is List: {list.ToStringEx()}");
        }
#endif
        else if ((asDict = value as IDictionary) != null)
        {
            var dict = new Dictionary<object, object>();
            foreach (DictionaryEntry kvp in asDict)
            {
                dict.Add(kvp.Key, kvp.Value);
            }

            Console.WriteLine($"# Is Dictionary: {dict.ToStringEx()}");
        }
        else if (value is char)
        {
            Console.WriteLine($"# Is Character: {(char)value}");
        }
        else
        {
            Console.WriteLine("# Is Other");
        }
    }

    /// <summary>
    /// Attempts to determine if the provided object is an enumerable collection
    /// with a single generic type argument and converts it to a List&lt;object&gt;.
    /// </summary>
    /// <param name="value"> The object to be checked and converted.</param>
    /// <param name="result"> An output parameter that will contain the List&lt;object&gt; if the conversion is successful, otherwise it will be null.</param>
    /// <returns> Returns true if the object is a valid enumerable with a single generic type argument and is successfully converted to a List&lt;object&gt;,
    /// otherwise returns false.</returns>
    private static bool IsList(object value, out List<object> result)
    {
        IEnumerable asEnumerable;

        if ((asEnumerable = value as IEnumerable) != null)
        {
            Type type = asEnumerable.GetType();
            if (!type.IsGenericType ||
                (type.IsGenericType && type.GetGenericArguments().Length == 1))
            {
                var list = new List<object>();
                IEnumerator enumerator = asEnumerable.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        list.Add(enumerator.Current);
                    }
                }
                finally
                {
                    if (enumerator is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                result = list;
                return true;
            }
        }

        result = null;
        return false;
    }
}