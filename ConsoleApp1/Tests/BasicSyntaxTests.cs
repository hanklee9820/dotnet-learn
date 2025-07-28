using System;
using Xunit;
using ConsoleApp1.Examples;

namespace ConsoleApp1.Tests
{
    /// <summary>
    /// 基础语法测试 - 验证C#基本功能
    /// 这些测试帮助您理解C#语法的正确性
    /// </summary>
    public class BasicSyntaxTests
    {
        [Fact]
        public void TestVariableDeclarations()
        {
            // 测试基本变量声明
            int number = 42;
            string text = "Hello C#";
            bool isActive = true;
            double price = 99.99;
            
            Assert.Equal(42, number);
            Assert.Equal("Hello C#", text);
            Assert.True(isActive);
            Assert.Equal(99.99, price);
        }
        
        [Fact]
        public void TestTypeInference()
        {
            // 测试var关键字的类型推断
            var autoNumber = 100;
            var autoText = "Auto type";
            var autoArray = new[] { 1, 2, 3 };
            
            Assert.IsType<int>(autoNumber);
            Assert.IsType<string>(autoText);
            Assert.IsType<int[]>(autoArray);
            Assert.Equal(3, autoArray.Length);
        }
        
        [Fact]
        public void TestStringInterpolation()
        {
            // 测试字符串插值
            string name = "张三";
            int age = 25;
            string result = $"姓名: {name}, 年龄: {age}";
            
            Assert.Equal("姓名: 张三, 年龄: 25", result);
        }
        
        [Fact]
        public void TestStringOperations()
        {
            // 测试字符串操作
            string text = "Hello World";
            
            Assert.Equal(11, text.Length);
            Assert.Equal("HELLO WORLD", text.ToUpper());
            Assert.Equal("hello world", text.ToLower());
            Assert.Contains("World", text);
            Assert.Equal("Hello", text.Substring(0, 5));
        }
        
        [Fact]
        public void TestArrayOperations()
        {
            // 测试数组操作
            int[] numbers = { 1, 2, 3, 4, 5 };
            
            Assert.Equal(5, numbers.Length);
            Assert.Equal(1, numbers[0]);
            Assert.Equal(5, numbers[^1]); // C# 8.0 索引语法
            
            // 测试数组遍历
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            Assert.Equal(15, sum);
        }
        
        [Fact]
        public void TestControlFlow()
        {
            // 测试if-else
            int score = 85;
            string grade;
            
            if (score >= 90)
                grade = "A";
            else if (score >= 80)
                grade = "B";
            else if (score >= 70)
                grade = "C";
            else
                grade = "F";
            
            Assert.Equal("B", grade);
        }
        
        [Fact]
        public void TestSwitchExpression()
        {
            // 测试switch表达式 (C# 8.0)
            int score = 85;
            string grade = score switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
            
            Assert.Equal("B", grade);
        }
        
        [Fact]
        public void TestForLoops()
        {
            // 测试for循环
            var result = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                result.Add(i);
            }
            
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, result);
        }
        
        [Fact]
        public void TestMethodParameters()
        {
            // 测试方法参数
            int sum = AddNumbers(10, 20);
            Assert.Equal(30, sum);
            
            // 测试可选参数
            string formatted = FormatMessage("Hello", "World");
            Assert.Equal("Hello World", formatted);
            
            string customFormatted = FormatMessage("Hello", "World", " - ");
            Assert.Equal("Hello - World", customFormatted);
        }
        
        [Fact]
        public void TestOutParameters()
        {
            // 测试out参数
            bool success = TryParseNumber("123", out int result);
            Assert.True(success);
            Assert.Equal(123, result);
            
            bool failure = TryParseNumber("abc", out int failResult);
            Assert.False(failure);
            Assert.Equal(0, failResult);
        }
        
        [Fact]
        public void TestRefParameters()
        {
            // 测试ref参数
            int value = 10;
            ModifyValue(ref value);
            Assert.Equal(20, value);
        }
        
        // 辅助方法
        private int AddNumbers(int a, int b)
        {
            return a + b;
        }
        
        private string FormatMessage(string first, string second, string separator = " ")
        {
            return $"{first}{separator}{second}";
        }
        
        private bool TryParseNumber(string input, out int result)
        {
            return int.TryParse(input, out result);
        }
        
        private void ModifyValue(ref int value)
        {
            value *= 2;
        }
    }
    
    /// <summary>
    /// 对比Java/Kotlin的语法差异测试
    /// </summary>
    public class SyntaxComparisonTests
    {
        [Fact]
        public void TestCSharpVsJavaVariables()
        {
            // C#: var关键字进行类型推断
            var number = 42;        // 推断为int
            var text = "Hello";     // 推断为string
            
            // Java对比: 
            // var number = 42;     // Java 10+
            // String text = "Hello";
            
            Assert.IsType<int>(number);
            Assert.IsType<string>(text);
        }
        
        [Fact]
        public void TestCSharpVsKotlinNullability()
        {
            // C#: 可空引用类型 (需要启用nullable)
            string nonNullable = "Hello";
            string? nullable = null;
            
            // Kotlin对比:
            // val nonNullable: String = "Hello"
            // val nullable: String? = null
            
            Assert.NotNull(nonNullable);
            Assert.Null(nullable);
            
            // 空合并运算符
            string result = nullable ?? "Default";
            Assert.Equal("Default", result);
        }
        
        [Fact]
        public void TestCSharpVsJavaStringFormatting()
        {
            string name = "张三";
            int age = 25;
            
            // C#: 字符串插值
            string csharpFormat = $"姓名: {name}, 年龄: {age}";
            
            // Java对比: String.format或连接
            // String javaFormat = String.format("姓名: %s, 年龄: %d", name, age);
            // String javaConcat = "姓名: " + name + ", 年龄: " + age;
            
            // Kotlin对比: 字符串模板
            // val kotlinFormat = "姓名: $name, 年龄: $age"
            
            Assert.Equal("姓名: 张三, 年龄: 25", csharpFormat);
        }
    }
}
