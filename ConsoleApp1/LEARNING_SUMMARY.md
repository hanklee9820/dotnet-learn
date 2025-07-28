# C# .NET 9 学习总结

## 🎯 项目完成状态

✅ **项目创建成功** - 基于.NET 9的控制台应用程序
✅ **代码示例完成** - 9个完整的学习模块
✅ **编译通过** - 所有代码都能正常编译
✅ **交互式菜单** - 可以选择性运行不同示例
✅ **详细注释** - 每个示例都有中文注释和Java/Kotlin对比

## 📚 学习模块概览

### 1. 基础语法 (01_BasicSyntax.cs)
**核心概念**:
- 变量声明和类型推断 (`var` 关键字)
- 方法定义和参数 (可选参数、out/ref参数)
- 字符串插值 (`$"Hello {name}"`)
- 控制流语句 (if-else, switch表达式, 循环)

**与Java/Kotlin对比**:
```csharp
// C#
var name = "张三";                    // 类型推断
string greeting = $"Hello {name}";   // 字符串插值

// Java
String name = "张三";
String greeting = String.format("Hello %s", name);

// Kotlin  
val name = "张三"
val greeting = "Hello $name"
```

### 2. 面向对象编程 (02_ObjectOriented.cs)
**核心概念**:
- 类定义和继承
- 抽象类和接口
- 多态和方法重写
- 访问修饰符

**C#特色**:
- 接口可以有默认实现 (C# 8.0+)
- `virtual`/`override` 关键字明确标识
- 构造函数链式调用 (`: base()`)

### 3. 集合和泛型 (03_CollectionsAndGenerics.cs)
**核心概念**:
- `List<T>`, `Dictionary<TKey, TValue>`, 数组
- 泛型类和泛型方法
- 泛型约束 (`where T : IComparable<T>`)
- 集合操作方法

**与Java对比**:
```csharp
// C#
var numbers = new List<int> { 1, 2, 3 };
var ages = new Dictionary<string, int> { ["张三"] = 25 };

// Java
List<Integer> numbers = Arrays.asList(1, 2, 3);
Map<String, Integer> ages = Map.of("张三", 25);
```

### 4. 异常处理 (04_ExceptionHandling.cs)
**核心概念**:
- try-catch-finally 语句
- 自定义异常类
- using语句进行资源管理
- 异常过滤器 (`when` 关键字)

**C#独有特性**:
```csharp
// 异常过滤器
catch (ArgumentException ex) when (ex.Message.Contains("特定错误"))
{
    // 只处理特定条件的异常
}

// using语句自动资源管理
using var file = new FileStream("test.txt", FileMode.Create);
// 自动调用Dispose()
```

### 5. C#独有特性 (05_CSharpUniqueFeatures.cs)
**重点特性**:

#### 🌟 属性 (Properties)
```csharp
public string Name { get; set; }                    // 自动属性
public string FullName => $"{First} {Last}";        // 计算属性
public int Age { get; private set; }                // 只读属性
```

#### 🌟 委托 (Delegates)
```csharp
public delegate int MathOperation(int a, int b);
MathOperation op = (a, b) => a + b;
Func<int, int, int> func = (x, y) => x * y;
Action<string> action = message => Console.WriteLine(message);
```

#### 🌟 事件 (Events)
```csharp
public event EventHandler<CustomEventArgs> SomethingHappened;
SomethingHappened += (sender, e) => Console.WriteLine(e.Message);
```

#### 🌟 扩展方法 (Extension Methods)
```csharp
public static string ToTitleCase(this string input) { ... }
"hello world".ToTitleCase();  // 为现有类型添加方法
```

#### 🌟 分部类 (Partial Classes)
```csharp
public partial class Calculator { ... }  // 可以分散在多个文件中
```

### 6. LINQ和函数式编程 (06_LinqAndFunctional.cs)
**核心概念**:
- LINQ查询语法和方法语法
- 函数式编程概念
- 惰性求值
- 高阶函数

**LINQ示例**:
```csharp
// 查询语法
var result = from student in students
             where student.Grade >= 80
             orderby student.Name
             select student;

// 方法语法
var result = students
    .Where(s => s.Grade >= 80)
    .OrderBy(s => s.Name)
    .Select(s => s);
```

### 7. 异步编程 (07_AsyncProgramming.cs)
**核心概念**:
- async/await 模式
- Task 和 Task<T>
- 并行处理 (Task.WhenAll, Task.WhenAny)
- 取消令牌 (CancellationToken)
- 异步流 (IAsyncEnumerable)

**异步示例**:
```csharp
public async Task<string> GetDataAsync()
{
    var result = await SomeAsyncOperation();
    return result;
}

// 并行处理
var tasks = urls.Select(url => FetchAsync(url));
var results = await Task.WhenAll(tasks);
```

### 8. 现代C#特性 (08_ModernCSharpFeatures.cs)
**C# 8.0+ 新特性**:

#### 🌟 记录类型 (Records) - C# 9.0
```csharp
public record Person(string Name, int Age);
var person = new Person("张三", 25);
var older = person with { Age = 26 };  // 不可变更新
```

#### 🌟 模式匹配 - C# 8.0+
```csharp
var result = value switch
{
    int i when i > 0 => "正数",
    string s => $"字符串: {s}",
    _ => "其他"
};
```

#### 🌟 可空引用类型 - C# 8.0
```csharp
string nonNull = "Hello";     // 不可为null
string? nullable = null;      // 可以为null
```

### 9. 高级特性 (09_AdvancedFeatures.cs)
**核心概念**:
- 特性 (Attributes) 和自定义特性定义
- 反射 (Reflection) 和运行时类型操作
- 匿名类型和匿名方法
- 动态类型 (dynamic) 和运行时绑定

#### 🌟 特性 (Attributes)
```csharp
[Obsolete("此方法已过时")]
[JsonPropertyName("product_name")]
[RangeValidation(0, 10000)]
public string Name { get; set; }
```

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

#### 🌟 动态类型 (dynamic)
```csharp
dynamic obj = new ExpandoObject();
obj.Name = "动态属性";
obj.Method = new Func<string>(() => "动态方法");
```

## 🔍 关键语法对比

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

## 🚀 如何运行项目

1. **运行主程序**:
```bash
dotnet run --project ConsoleApp1
```

2. **选择学习模块**: 根据菜单选择1-9运行不同示例

3. **查看代码**: 每个示例文件都有详细注释和对比说明

## 💡 学习建议

1. **按顺序学习**: 从基础语法开始，逐步深入
2. **动手实践**: 修改示例代码，观察运行结果
3. **对比理解**: 重点关注与Java/Kotlin的差异
4. **重点掌握**: C#独有特性是学习重点
5. **实际应用**: 尝试用C#重写一些Java/Kotlin项目

## 📖 下一步学习

- **ASP.NET Core**: Web开发框架
- **Entity Framework**: ORM框架
- **Blazor**: 前端开发框架
- **Xamarin/MAUI**: 移动应用开发
- **WPF/WinUI**: 桌面应用开发

---

**恭喜您完成C# .NET 9的基础学习！** 🎉

从移动开发转向.NET开发，您已经掌握了核心概念。继续实践和深入学习，很快就能熟练使用C#进行各种类型的应用开发！
