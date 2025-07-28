using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// 现代C#特性示例 - 记录类型、模式匹配、可空引用类型等
    /// C# 8.0, 9.0, 10.0+ 的新特性
    /// </summary>
    public class ModernCSharpFeatures
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== 现代C#特性示例 ===\n");
            
            // 1. 记录类型 (Records) - C# 9.0
            RecordTypes();
            
            // 2. 模式匹配 - C# 8.0+
            PatternMatching();
            
            // 3. 可空引用类型 - C# 8.0
            NullableReferenceTypes();
            
            // 4. Switch表达式 - C# 8.0
            SwitchExpressions();
            
            // 5. 目标类型new表达式 - C# 9.0
            TargetTypedNew();
            
            // 6. 顶级程序 - C# 9.0 (在Program.cs中演示)
            
            // 7. 全局using - C# 10.0
            
            // 8. 文件范围命名空间 - C# 10.0
        }
        
        private static void RecordTypes()
        {
            Console.WriteLine("--- 记录类型 (Records) ---");
            
            // 创建记录实例
            var person1 = new PersonRecord("张三", 25);
            var person2 = new PersonRecord("李四", 30);
            var person3 = new PersonRecord("张三", 25); // 与person1相同的值
            
            Console.WriteLine($"Person1: {person1}");
            Console.WriteLine($"Person2: {person2}");
            
            // 记录类型自动实现值相等性
            Console.WriteLine($"person1 == person3: {person1 == person3}"); // True
            Console.WriteLine($"person1 == person2: {person1 == person2}"); // False
            
            // with表达式 - 创建修改后的副本
            var olderPerson1 = person1 with { Age = 26 };
            Console.WriteLine($"原始: {person1}");
            Console.WriteLine($"修改后: {olderPerson1}");
            
            // 解构
            var (name, age) = person1;
            Console.WriteLine($"解构结果: 姓名={name}, 年龄={age}");
            
            // 位置记录 (Positional Records)
            var point1 = new Point(3, 4);
            var point2 = new Point(3, 4);
            Console.WriteLine($"Point1: {point1}");
            Console.WriteLine($"Points相等: {point1 == point2}");
            
            // 记录继承
            var student = new StudentRecord("王五", 20, "计算机科学");
            Console.WriteLine($"Student: {student}");
            
            // Java对比: 需要手动实现equals, hashCode, toString
            // Kotlin对比: data class提供类似功能
            // data class Person(val name: String, val age: Int)
            
            Console.WriteLine();
        }
        
        private static void PatternMatching()
        {
            Console.WriteLine("--- 模式匹配 ---");
            
            // 类型模式
            object[] items = { 42, "Hello", 3.14, new PersonRecord("测试", 25), null };
            
            foreach (object item in items)
            {
                string description = item switch
                {
                    int i when i > 0 => $"正整数: {i}",
                    int i => $"非正整数: {i}",
                    string s when s.Length > 5 => $"长字符串: {s}",
                    string s => $"短字符串: {s}",
                    double d => $"双精度数: {d}",
                    PersonRecord p => $"人员: {p.Name}",
                    null => "空值",
                    _ => "未知类型"
                };
                Console.WriteLine($"  {description}");
            }
            
            // 属性模式
            var people = new[]
            {
                new PersonRecord("张三", 25),
                new PersonRecord("李四", 17),
                new PersonRecord("王五", 35),
                new PersonRecord("赵六", 16)
            };
            
            Console.WriteLine("年龄分类:");
            foreach (var person in people)
            {
                string category = person switch
                {
                    { Age: < 18 } => "未成年",
                    { Age: >= 18 and < 30 } => "青年",
                    { Age: >= 30 } => "中年",
                    _ => "未知"
                };
                Console.WriteLine($"  {person.Name}: {category}");
            }
            
            // 元组模式
            var coordinates = new[] { (0, 0), (1, 0), (0, 1), (1, 1), (2, 3) };
            
            Console.WriteLine("坐标分析:");
            foreach (var (x, y) in coordinates)
            {
                string location = (x, y) switch
                {
                    (0, 0) => "原点",
                    (var a, 0) => $"X轴上的点 ({a}, 0)",
                    (0, var b) => $"Y轴上的点 (0, {b})",
                    (var a, var b) when a == b => $"对角线上的点 ({a}, {b})",
                    _ => $"普通点 ({x}, {y})"
                };
                Console.WriteLine($"  {location}");
            }
            
            // 关系模式 (C# 9.0)
            int temperature = 25;
            string weather = temperature switch
            {
                < 0 => "冰冻",
                >= 0 and < 10 => "寒冷",
                >= 10 and < 20 => "凉爽",
                >= 20 and < 30 => "温暖",
                >= 30 => "炎热"
            };
            Console.WriteLine($"温度 {temperature}°C: {weather}");
            
            Console.WriteLine();
        }
        
        private static void NullableReferenceTypes()
        {
            Console.WriteLine("--- 可空引用类型 ---");
            
            // 在项目文件中启用: <Nullable>enable</Nullable>
            
            // 非空引用类型
            string nonNullableString = "Hello";
            Console.WriteLine($"非空字符串: {nonNullableString}");
            
            // 可空引用类型
            string? nullableString = GetNullableString();
            Console.WriteLine($"可空字符串: {nullableString ?? "null"}");
            
            // 空值检查
            if (nullableString != null)
            {
                Console.WriteLine($"字符串长度: {nullableString.Length}");
            }
            
            // 空合并赋值 (C# 8.0)
            nullableString ??= "默认值";
            Console.WriteLine($"空合并赋值后: {nullableString}");
            
            // 空条件运算符
            string? result = nullableString?.ToUpper();
            Console.WriteLine($"安全调用结果: {result}");
            
            // 空断言运算符 (!) - 告诉编译器这里不会为null
            string definitelyNotNull = GetStringThatMightBeNull()!;
            Console.WriteLine($"断言非空: {definitelyNotNull}");
            
            // 处理可空集合
            List<string>? nullableList = GetNullableList();
            int count = nullableList?.Count ?? 0;
            Console.WriteLine($"列表项数: {count}");
            
            // Java对比: 使用Optional<T>
            // Optional<String> optional = Optional.ofNullable(getString());
            // String result = optional.orElse("default");
            
            // Kotlin对比: 内置可空类型
            // val nullableString: String? = null
            // val length = nullableString?.length ?: 0
            
            Console.WriteLine();
        }
        
        private static void SwitchExpressions()
        {
            Console.WriteLine("--- Switch表达式 ---");
            
            // 传统switch语句 vs switch表达式
            DayOfWeek today = DateTime.Now.DayOfWeek;
            
            // Switch表达式 (C# 8.0)
            string dayType = today switch
            {
                DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday 
                or DayOfWeek.Thursday or DayOfWeek.Friday => "工作日",
                DayOfWeek.Saturday or DayOfWeek.Sunday => "周末",
                _ => "未知"
            };
            Console.WriteLine($"今天是{today}: {dayType}");
            
            // 复杂switch表达式
            var shapes = new object[]
            {
                new CircleShape { Radius = 5 },
                new RectangleShape { Width = 4, Height = 6 },
                new Triangle { Base = 3, Height = 4 }
            };

            foreach (var shape in shapes)
            {
                double area = shape switch
                {
                    CircleShape { Radius: var r } => Math.PI * r * r,
                    RectangleShape { Width: var w, Height: var h } => w * h,
                    Triangle { Base: var b, Height: var h } => 0.5 * b * h,
                    _ => 0
                };
                Console.WriteLine($"{shape.GetType().Name} 面积: {area:F2}");
            }
            
            // 嵌套属性模式
            var orders = new[]
            {
                new Order { Customer = new Customer { Type = CustomerType.Premium }, Amount = 1000 },
                new Order { Customer = new Customer { Type = CustomerType.Regular }, Amount = 500 },
                new Order { Customer = new Customer { Type = CustomerType.New }, Amount = 200 }
            };
            
            foreach (var order in orders)
            {
                decimal discount = order switch
                {
                    { Customer.Type: CustomerType.Premium, Amount: > 500 } => 0.15m,
                    { Customer.Type: CustomerType.Premium } => 0.10m,
                    { Customer.Type: CustomerType.Regular, Amount: > 300 } => 0.05m,
                    { Customer.Type: CustomerType.New } => 0.02m,
                    _ => 0m
                };
                Console.WriteLine($"客户类型: {order.Customer.Type}, 订单金额: {order.Amount:C}, 折扣: {discount:P}");
            }
            
            Console.WriteLine();
        }
        
        private static void TargetTypedNew()
        {
            Console.WriteLine("--- 目标类型new表达式 ---");
            
            // C# 9.0: 可以省略new后面的类型
            PersonRecord person = new("张三", 25); // 等同于 new PersonRecord("张三", 25)
            Console.WriteLine($"Person: {person}");
            
            // 集合初始化
            List<int> numbers = new() { 1, 2, 3, 4, 5 };
            Dictionary<string, int> ages = new()
            {
                ["张三"] = 25,
                ["李四"] = 30
            };
            
            Console.WriteLine($"Numbers: [{string.Join(", ", numbers)}]");
            Console.WriteLine($"Ages: {string.Join(", ", ages.Select(kv => $"{kv.Key}:{kv.Value}"))}");
            
            // 数组
            int[] array = new[] { 1, 2, 3 }; // 这个在早期版本就支持
            Point[] points = { new(0, 0), new(1, 1) }; // C# 9.0新语法

            Console.WriteLine($"Points: [{string.Join(", ", points.Select(p => p.ToString()))}]");
            
            Console.WriteLine();
        }
        
        private static string? GetStringThatMightBeNull()
        {
            return "实际上不为null";
        }
        
        private static List<string>? GetNullableList()
        {
            return new List<string> { "item1", "item2" };
        }

        private static string? GetNullableString()
        {
            return null;
        }
    }
    
    // 记录类型定义
    public record PersonRecord(string Name, int Age);
    
    // 位置记录
    public record Point(int X, int Y);
    
    // 记录继承
    public record StudentRecord(string Name, int Age, string Major) : PersonRecord(Name, Age);
    
    // 形状类用于模式匹配
    public class CircleShape
    {
        public double Radius { get; set; }
    }

    public class RectangleShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
    
    public class Triangle
    {
        public double Base { get; set; }
        public double Height { get; set; }
    }
    
    // 订单和客户类
    public enum CustomerType
    {
        New,
        Regular,
        Premium
    }
    
    public class Customer
    {
        public CustomerType Type { get; set; }
    }
    
    public class Order
    {
        public Customer Customer { get; set; } = new();
        public decimal Amount { get; set; }
    }
}
