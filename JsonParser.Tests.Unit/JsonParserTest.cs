using Xunit.Abstractions;

namespace JsonParser.Tests.Unit
{
    public class JsonParserTest
    {
        private readonly ITestOutputHelper _output;

        public JsonParserTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Serialize_And_Deserialize_Single_Dimensional_Array()
        {
            // Serialize
            int[] array = new int[] { 1, 2, 3, 4, 5, 6 };
            var jsonString = Json.Serialize(array);
            _output.WriteLine(jsonString);
            
            // Deserialize
            var result = Json.Deserialize(jsonString) as List<object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
            
            Assert.Equal(6, result.Count);
            Assert.Equal(1, (long)result[0]);
            Assert.Equal(2, (long)result[1]);
            Assert.Equal(3, (long)result[2]);
            Assert.Equal(4, (long)result[3]);
            Assert.Equal(5, (long)result[4]);
            Assert.Equal(6, (long)result[5]);
        }
        
        [Fact]
        public void Serialize_And_Deserialize_Two_Dimensional_Array()
        {
            // Serialize
            int[,] multiDimensionalArray = { { 1, 2, 3 }, { 4, 5, 6 } };
            var jsonString = Json.Serialize(multiDimensionalArray);
            _output.WriteLine(jsonString);
            
            // Deserialize
            var result = Json.Deserialize(jsonString) as List<object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
            
            Assert.Equal(6, result.Count);
            Assert.Equal(1, (long)result[0]);
            Assert.Equal(2, (long)result[1]);
            Assert.Equal(3, (long)result[2]);
            Assert.Equal(4, (long)result[3]);
            Assert.Equal(5, (long)result[4]);
            Assert.Equal(6, (long)result[5]);
        }
        
        [Fact]
        public void Serialize_And_Deserialize_Jagged_Array()
        {
            // Serialize
            int[][] jaggedArray = new int[6][];
            jaggedArray[0] = new int[] { 1, 2, 3, 4 };
            var jsonString = Json.Serialize(jaggedArray);
            _output.WriteLine(jsonString);
            
            // Deserialize
            var result = Json.Deserialize(jsonString) as List<object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
            
            Assert.Equal(6, result.Count);
            
            var row0 = result[0] as List<object>;
            // Check row0 is not null
            Assert.NotNull(row0);
            if (row0 == null) return;
            Assert.Equal(1, (long)row0[0]);
            Assert.Equal(2, (long)row0[1]);
            Assert.Equal(3, (long)row0[2]);
            Assert.Equal(4, (long)row0[3]);

            for (int i = 1; i < result.Count; i++)
            {
                Assert.Null(result[i]);
            }
        }
        
        [Fact]
        public void Serialize_And_Deserialize_Dictionary()
        {
            // Serialize
            var dictionary = new Dictionary<string, object>
            {
                { "k1", "value1" },
                { "k2", 100 },
                {
                    "k3", new
                    {
                        latitude = 111.1,
                        longitude = 222.2,
                        altitude = 3.3,
                        speed = 4f
                    }
                }
            };
            var jsonString = Json.Serialize(dictionary);
            _output.WriteLine(jsonString);
            
            // Deserialize
            var result = Json.Deserialize(jsonString) as Dictionary<string, object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
            
            Assert.Equal("value1", (string)result["k1"]);
            Assert.Equal(100L, (long)result["k2"]);
                
            var k3 = result["k3"] as Dictionary<string, object>;
            // Check k3 is not null
            Assert.NotNull(k3);
            if (k3 == null) return;
            
            Assert.Equal(111.1, (double)k3["latitude"]);
            Assert.Equal(222.2, (double)k3["longitude"]);
            Assert.Equal(3.3, (double)k3["altitude"]);
            Assert.Equal(4.0, (double)k3["speed"]);
        }

        [Fact]
        public void Serialize_And_Deserialize_List()
        {
            // Serialize
            var list = new List<object>
            {
                new Tuple<string, List<string>>("k1",  new List<string> { "child1-1", "child1-2", "child1-3" }),
                new Tuple<string, List<string>>("k2",  new List<string> { "child2-1", "child2-2", "child2-3" })
            };
            var jsonString = Json.Serialize(list);
            _output.WriteLine(jsonString);
                  
            // Deserialize
            var result = Json.Deserialize(jsonString) as List<object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
            
            Assert.Equal(2, result.Count);
            
            var tuple1 = result[0] as Dictionary<string, object>;
            // Check tuple1 is not null
            Assert.NotNull(tuple1);
            if (tuple1 == null) return;
            
            Assert.Equal("k1", (string)tuple1["Item1"]);
            var tuple1Item2 = tuple1["Item2"] as List<object>;
            // Check tuple1-item2 is not null
            Assert.NotNull(tuple1Item2);
            if (tuple1Item2 == null) return;
            Assert.Equal("child1-1", (string)tuple1Item2[0]);
            Assert.Equal("child1-2", (string)tuple1Item2[1]);
            Assert.Equal("child1-3", (string)tuple1Item2[2]);
            
            var tuple2 = result[1] as Dictionary<string, object>;
            // Check tuple2 is not null
            Assert.NotNull(tuple2);
            if (tuple2 == null) return;
            
            Assert.Equal("k2", (string)tuple2["Item1"]);
            var tuple2Item2 = tuple2["Item2"] as List<object>;
            // Check tuple2-item2 is not null
            Assert.NotNull(tuple2Item2);
            if (tuple2Item2 == null) return;
            Assert.Equal("child2-1", (string)tuple2Item2[0]);
            Assert.Equal("child2-2", (string)tuple2Item2[1]);
            Assert.Equal("child2-3", (string)tuple2Item2[2]);
        }
        
        [Fact]
        public void Serialize_And_Deserialize_HashSet()
        {
            // Serialize
            var hashSet = new HashSet<object>() { 1, 1, 2, 3, 5, 8, 11 };
            var jsonString = Json.Serialize(hashSet);
            _output.WriteLine(jsonString);
            
            // Deserialize
            var result = Json.Deserialize(jsonString) as List<object>;
            // Check result is not null
            Assert.NotNull(result);
            if (result == null) return;
           
            Assert.Equal(6, result.Count);
            Assert.Equal(1, (long)result[0]);
            Assert.Equal(2, (long)result[1]);
            Assert.Equal(3, (long)result[2]);
            Assert.Equal(5, (long)result[3]);
            Assert.Equal(8, (long)result[4]);
            Assert.Equal(11, (long)result[5]);
        }
    }   
}