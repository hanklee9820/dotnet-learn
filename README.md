# C# .NET 9 学习指南

> 专为移动开发者（Java/Kotlin背景）设计的C#学习项目

## 📋 项目概述

这个项目为有Java、Kotlin、Objective-C、Swift、Dart经验的移动开发者提供了一个全面的C#学习路径。通过对比语法差异和突出C#独有特性，帮助您快速掌握C#编程。

## 🎯 学习目标

- 掌握C#核心语法和概念
- 理解C#与Java/Kotlin的语法差异
- 学习C#独有的强大特性
- 掌握现代C#编程最佳实践
- 了解.NET 9的新特性

## 📁 项目结构

```
ConsoleApp1/
├── Examples/                          # 示例代码
│   ├── 01_BasicSyntax.cs             # 基础语法
│   ├── 02_ObjectOriented.cs          # 面向对象编程
│   ├── 03_CollectionsAndGenerics.cs  # 集合和泛型
│   ├── 04_ExceptionHandling.cs       # 异常处理
│   ├── 05_CSharpUniqueFeatures.cs    # C#独有特性
│   ├── 06_LinqAndFunctional.cs       # LINQ和函数式编程
│   ├── 07_AsyncProgramming.cs        # 异步编程
│   ├── 08_ModernCSharpFeatures.cs    # 现代C#特性
│   └── 09_AdvancedFeatures.cs        # 高级特性
├── Tests/                             # 测试代码
│   └── BasicSyntaxTests.cs           # 基础语法测试
├── Program.cs                         # 主程序
├── ConsoleApp1.csproj                # 项目文件
└── README.md                          # 本文件
```

## 🚀 快速开始

### 前置要求

- .NET 9 SDK
- Visual Studio 2022 或 Visual Studio Code
- C# Dev Kit 扩展（VS Code用户）

### 运行项目

1. 克隆或下载项目
2. 在项目根目录打开终端
3. 运行项目：

```bash
dotnet run
```

4. 按照菜单提示选择要学习的内容

### 运行测试

```bash
# 添加测试框架（如果需要）
dotnet add package xunit
dotnet add package xunit.runner.visualstudio

# 运行测试
dotnet test
```

## 📚 学习路径

### 1. 基础语法 (01_BasicSyntax.cs)
- **变量声明和类型推断**
  - C#: `var name = "张三";`
  - Java: `String name = "张三";`
  - Kotlin: `val name = "张三"`

- **方法定义**
  - C#支持可选参数和命名参数
  - out/ref参数用于返回多个值

- **字符串操作**
  - 字符串插值：`$"Hello {name}"`
  - 与Java的String.format()对比

### 2. 面向对象编程 (02_ObjectOriented.cs)
- **类和继承**
  - 语法与Java相似，但有细微差异
  - 虚方法和抽象方法

- **接口**
  - 支持默认实现（C# 8.0+）
  - 属性在接口中的定义

- **多态**
  - 方法重写和隐藏
  - 类型检查和转换

### 3. 集合和泛型 (03_CollectionsAndGenerics.cs)
- **常用集合**
  - `List<T>`, `Dictionary<TKey, TValue>`, `Array`
  - 与Java Collections的对比

- **泛型**
  - 泛型约束：`where T : IComparable<T>`
  - 与Java泛型的差异

### 4. 异常处理 (04_ExceptionHandling.cs)
- **try-catch-finally**
  - 与Java异常处理的相似性
  - C#独有的异常过滤器

- **using语句**
  - 自动资源管理
  - 与Java try-with-resources对比

### 5. C#独有特性 (05_CSharpUniqueFeatures.cs)

#### 🌟 属性 (Properties)
```csharp
public string Name { get; set; }  // 自动属性
public string FullName => $"{FirstName} {LastName}";  // 计算属性
```
**Java对比**: 需要显式getter/setter方法
**Kotlin对比**: 有类似概念但实现不同

#### 🌟 委托 (Delegates)
```csharp
public delegate int MathOperation(int a, int b);
MathOperation op = (a, b) => a + b;
```
**Java对比**: 函数式接口
**Kotlin对比**: 函数类型

#### 🌟 事件 (Events)
```csharp
public event EventHandler<CustomEventArgs> SomethingHappened;
```
**Java对比**: 观察者模式
**Kotlin对比**: 回调函数

#### 🌟 扩展方法 (Extension Methods)
```csharp
public static string ToTitleCase(this string input) { ... }
"hello world".ToTitleCase();  // 为现有类型添加方法
```
**Java对比**: 无此功能，需要工具类
**Kotlin对比**: 也支持扩展函数

#### 🌟 分部类 (Partial Classes)
```csharp
public partial class Calculator { ... }  // 可以分散在多个文件中
```
**Java/Kotlin对比**: 无此概念

### 6. LINQ和函数式编程 (06_LinqAndFunctional.cs)

#### 🌟 LINQ查询语法
```csharp
var result = from student in students
             where student.Grade >= 80
             orderby student.Name
             select student;
```

#### 🌟 方法语法
```csharp
var result = students
    .Where(s => s.Grade >= 80)
    .OrderBy(s => s.Name)
    .Select(s => s);
```

**Java对比**: Stream API (Java 8+)
**Kotlin对比**: 集合操作函数

### 7. 异步编程 (07_AsyncProgramming.cs)

#### 🌟 async/await模式
```csharp
public async Task<string> GetDataAsync()
{
    var result = await SomeAsyncOperation();
    return result;
}
```

**Java对比**: CompletableFuture
**Kotlin对比**: suspend函数和协程

#### 🌟 并行处理
```csharp
var tasks = urls.Select(url => FetchAsync(url));
var results = await Task.WhenAll(tasks);
```

### 8. 现代C#特性 (08_ModernCSharpFeatures.cs)

#### 🌟 记录类型 (Records) - C# 9.0
```csharp
public record Person(string Name, int Age);
var person = new Person("张三", 25);
var older = person with { Age = 26 };  // 不可变更新
```

**Java对比**: 需要手动实现equals/hashCode/toString
**Kotlin对比**: data class

#### 🌟 模式匹配 - C# 8.0+
```csharp
var result = value switch
{
    int i when i > 0 => "正数",
    int i => "非正数",
    string s => $"字符串: {s}",
    _ => "其他"
};
```

#### 🌟 可空引用类型 - C# 8.0
```csharp
string nonNull = "Hello";     // 不可为null
string? nullable = null;      // 可以为null
```

**Java对比**: Optional<T>
**Kotlin对比**: 内置可空类型

## 🔍 语法对比总结

| 特性 | C# | Java | Kotlin |
|------|----|----- |--------|
| 变量声明 | `var name = "张三";` | `String name = "张三";` | `val name = "张三"` |
| 字符串插值 | `$"Hello {name}"` | `String.format("Hello %s", name)` | `"Hello $name"` |
| 属性 | `public string Name { get; set; }` | `getName()/setName()` | `var name: String` |
| 可空类型 | `string?` | `Optional<String>` | `String?` |
| 扩展方法 | ✅ | ❌ | ✅ |
| 委托/函数类型 | `Func<int, int>` | `Function<Integer, Integer>` | `(Int) -> Int` |
| 异步编程 | `async/await` | `CompletableFuture` | `suspend fun` |
| LINQ/查询 | ✅ 内置 | Stream API | 集合函数 |
| 特性/注解 | `[Obsolete]` | `@Deprecated` | `@Deprecated` |
| 反射 | `typeof(T).GetProperty()` | `Class.getDeclaredField()` | `T::class.memberProperties` |
| 匿名类型 | `new { Name = "张三" }` | 匿名类 | `object { val name = "张三" }` |
| 动态类型 | `dynamic` | `Object` + 反射 | `Any` + 智能转换 |

### 9. 高级特性 (09_AdvancedFeatures.cs)
**核心概念**:
- 特性 (Attributes) 和自定义特性
- 反射 (Reflection) 和动态类型操作
- 匿名类型和匿名方法
- 动态类型 (dynamic)

#### 🌟 特性 (Attributes)
```csharp
[Obsolete("此方法已过时")]
[JsonPropertyName("product_name")]
[RangeValidation(0, 10000)]
public string Name { get; set; }
```

**Java对比**: 注解 (@Deprecated, @JsonProperty)
**Kotlin对比**: 注解 (@Deprecated, @JsonProperty)

#### 🌟 反射 (Reflection)
```csharp
Type type = typeof(MyClass);
PropertyInfo[] properties = type.GetProperties();
object instance = Activator.CreateInstance(type);
```

#### 🌟 匿名类型
```csharp
var person = new { Name = "张三", Age = 25 };
var result = data.Select(x => new { x.Name, Square = x.Value * x.Value });
```

#### 🌟 动态类型
```csharp
dynamic obj = new ExpandoObject();
obj.Name = "动态属性";
obj.Method = new Func<string>(() => "动态方法");
```

## 🎓 学习建议

1. **按顺序学习**: 从基础语法开始，逐步深入
2. **动手实践**: 运行每个示例，修改代码观察结果
3. **对比理解**: 注意与Java/Kotlin的差异和相似点
4. **编写测试**: 通过测试验证理解
5. **查阅文档**: 遇到问题时查阅官方文档

## 📖 推荐资源

- [Microsoft C# 文档](https://docs.microsoft.com/zh-cn/dotnet/csharp/)
- [C# 编程指南](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/)
- [.NET API 参考](https://docs.microsoft.com/zh-cn/dotnet/api/)
- [C# 语言规范](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/)

## 🤝 贡献

欢迎提交问题和改进建议！

## 📄 许可证

MIT License

---

**祝您学习愉快！从移动开发到.NET开发的转换会很顺利的！** 🚀
