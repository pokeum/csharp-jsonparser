namespace JsonParser.ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            MiniJsonTest();
        }

        static void MiniJsonTest()
        {
            var jsonString = "{ \"array\": [1.44,2,3], " +
                             "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                             "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
                             "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                             "\"int\": 65536, " +
                             "\"float\": 3.1415926, " +
                             "\"bool\": true, " +
                             "\"null\": null }";

            var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;

            Console.WriteLine("deserialized: " + dict.GetType());
            Console.WriteLine("dict['array'][0]: " + ((List<object>)dict["array"])[0]);
            Console.WriteLine("dict['string']: " + (string)dict["string"]);
            Console.WriteLine("dict['float']: " + (double)dict["float"]); // floats come out as doubles
            Console.WriteLine("dict['int']: " + (long)dict["int"]); // ints come out as longs
            Console.WriteLine("dict['unicode']: " + (string)dict["unicode"]);

            var str = Json.Serialize(dict);

            Console.WriteLine("serialized: " + str);
        }
    }
}