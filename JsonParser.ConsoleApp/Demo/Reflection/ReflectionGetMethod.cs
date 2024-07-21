using System.Reflection;

namespace JsonParser.ConsoleApp.Demo.Reflection;

public class MemberClass
{
    public string Name { get; set; } = "John Doe";

    public string GetName()
    {
        return Name + ", " + DateTime.Now.ToShortTimeString();
    }
}

public class ReflectionGetMethod
{
    public static void Demo()
    {
        // 리플렉션 기능으로 특정 클래스의 멤버를 동적으로 호출
        MemberClass memberClass = new MemberClass();
        Type type = memberClass.GetType();
        
        // 속성 읽어 오기 및 속성 호출
        PropertyInfo nameProperty = type.GetProperty("Name");   // Name 속성
        Console.WriteLine("속성 호출 : {0}", nameProperty.GetValue(memberClass));
        
        // 메서드 읽어 오기 및 메서드 호출
        MethodInfo getNameMethod = type.GetMethod("GetName");   // GetName 메서드
        Console.WriteLine("메서드 호출 : {0}", getNameMethod.Invoke(memberClass, null));
        
        // // 참고: C# 4.0 이상에서는 dynamic 개체로 쉽게 멤버를 동적으로 호출
        // dynamic d = new MemberClass();
        // Console.WriteLine("속성 호출 : {0}", d.Name);
        // Console.WriteLine("메서드 호출 : {0}", d.GetName());
    }
}