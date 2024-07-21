using System.Reflection;

namespace JsonParser.ConsoleApp.Demo.Reflection;

class Test
{
    public static void TestMethod() { }
}

class ReflectionGetMembers
{
    public static void Demo()
    {
        // Test 클래스에 대한 Type 개체 가져오기
        Type type = typeof(Test);
        
        // 원하는 멤버를 조건에 따라 가져오기
        MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public);
        
        // 멤버 출력
        foreach (var member in members)
        {
            Console.WriteLine("{0}", member.Name);
        }
    }
}