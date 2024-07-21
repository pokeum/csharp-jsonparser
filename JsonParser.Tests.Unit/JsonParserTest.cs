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