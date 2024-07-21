# C# Json Parser

[![Generic badge](https://img.shields.io/badge/GitHub-Unity--Technologies/UnityCsReference-blue?logo=github&logoColor=white)](https://github.com/Unity-Technologies/UnityCsReference)

Extensions of Unity Technologies [MiniJSON](https://github.com/Unity-Technologies/UnityCsReference/blob/master/External/JsonParsers/MiniJson/MiniJSON.cs)


## Creating a json parser project

![csharp](https://img.shields.io/badge/.NET-6.0-CBC3E3)

```bash
dotnet new sln
```

Creating a class library project
```bash
dotnet new classlib -o JsonParser -f net6.0 && dotnet sln add JsonParser
```

<details>
<summary><i>Creating other projects</i></summary>
  
- Creating a unit testing project
  ```bash
  dotnet new xunit -o JsonParser.Tests.Unit -f net6.0 && dotnet sln add JsonParser.Tests.Unit
  ```

- Creating a console app project
  ```bash
  dotnet new console -o JsonParser.ConsoleApp -f net6.0 && dotnet sln add JsonParser.ConsoleApp
  ```

</details>
