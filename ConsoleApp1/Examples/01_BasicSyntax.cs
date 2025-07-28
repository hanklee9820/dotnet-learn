using System;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#基础语法示例 - 变量声明、类型推断、方法定义
    /// 对比Java/Kotlin语法差异
    /// </summary>
    public class BasicSyntax
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 基础语法示例 ===\n");
            
            // 1. 变量声明和类型推断
            VariableDeclarations();
            
            // 2. 方法定义和调用
            MethodExamples();
            
            // 3. 字符串操作
            StringOperations();
            
            // 4. 控制流语句
            ControlFlow();
        }
        
        /// <summary>
        /// 变量声明示例
        /// </summary>
        private static void VariableDeclarations()
        {
            Console.WriteLine("--- 变量声明和类型推断 ---");
            
            // C#: 显式类型声明
            int number = 42;
            string text = "Hello C#";
            bool isActive = true;
            double price = 99.99;
            
            // Java对比:
            // int number = 42;
            // String text = "Hello Java";
            // boolean isActive = true;
            // double price = 99.99;
            
            // Kotlin对比:
            // val number: Int = 42
            // val text: String = "Hello Kotlin"
            // val isActive: Boolean = true
            // val price: Double = 99.99
            
            Console.WriteLine($"Number: {number}");
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"IsActive: {isActive}");
            Console.WriteLine($"Price: {price}");
            
            // C#: var关键字 - 类型推断（编译时确定类型）
            var autoNumber = 100;        // 推断为int
            var autoText = "Auto type";  // 推断为string
            var autoArray = new[] { 1, 2, 3 }; // 推断为int[]
            
            // Java对比: Java 10+支持var，但使用较少
            // var autoNumber = 100;
            
            // Kotlin对比: 使用val/var进行类型推断
            // val autoNumber = 100
            // var autoText = "Auto type"
            
            Console.WriteLine($"Auto Number: {autoNumber} (Type: {autoNumber.GetType().Name})");
            Console.WriteLine($"Auto Text: {autoText} (Type: {autoText.GetType().Name})");
            
            // C#: 常量声明
            const int MAX_SIZE = 1000;
            
            // Java对比:
            // final int MAX_SIZE = 1000;
            // static final int MAX_SIZE = 1000; // 类常量
            
            // Kotlin对比:
            // const val MAX_SIZE = 1000 // 编译时常量
            // val maxSize = 1000 // 运行时常量
            
            Console.WriteLine($"Max Size: {MAX_SIZE}\n");
        }
        
        /// <summary>
        /// 方法定义示例
        /// </summary>
        private static void MethodExamples()
        {
            Console.WriteLine("--- 方法定义和调用 ---");
            
            // 调用各种类型的方法
            int sum = AddNumbers(10, 20);
            Console.WriteLine($"Sum: {sum}");
            
            string greeting = CreateGreeting("张三");
            Console.WriteLine(greeting);
            
            // 可选参数和命名参数
            string formatted = FormatMessage("Hello", "World", separator: " - ");
            Console.WriteLine(formatted);
            
            // out参数示例
            if (TryParseNumber("123", out int result))
            {
                Console.WriteLine($"Parsed number: {result}");
            }
            
            // ref参数示例
            int value = 10;
            ModifyValue(ref value);
            Console.WriteLine($"Modified value: {value}\n");
        }
        
        /// <summary>
        /// 基本方法 - 返回两个数的和
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之和</returns>
        private static int AddNumbers(int a, int b)
        {
            // C#: 方法定义语法
            // [访问修饰符] [static] 返回类型 方法名(参数列表)
            
            // Java对比:
            // private static int addNumbers(int a, int b) {
            //     return a + b;
            // }
            
            // Kotlin对比:
            // private fun addNumbers(a: Int, b: Int): Int {
            //     return a + b
            // }
            // 或简化为: private fun addNumbers(a: Int, b: Int) = a + b
            
            return a + b;
        }
        
        /// <summary>
        /// 字符串方法示例
        /// </summary>
        private static string CreateGreeting(string name)
        {
            // C#: 字符串插值 (C# 6.0+)
            return $"你好, {name}!";
            
            // Java对比:
            // return "你好, " + name + "!";
            // 或使用String.format: return String.format("你好, %s!", name);
            
            // Kotlin对比:
            // return "你好, $name!"
            // 或: return "你好, ${name}!"
        }
        
        /// <summary>
        /// 可选参数和命名参数示例
        /// </summary>
        private static string FormatMessage(string first, string second, string separator = " ")
        {
            // C#: 支持可选参数（默认值）
            return $"{first}{separator}{second}";
            
            // Java对比: 不支持可选参数，需要方法重载
            // public static String formatMessage(String first, String second) {
            //     return formatMessage(first, second, " ");
            // }
            // public static String formatMessage(String first, String second, String separator) {
            //     return first + separator + second;
            // }
            
            // Kotlin对比: 支持默认参数
            // fun formatMessage(first: String, second: String, separator: String = " "): String {
            //     return "$first$separator$second"
            // }
        }
        
        /// <summary>
        /// out参数示例 - 类似于返回多个值
        /// </summary>
        private static bool TryParseNumber(string input, out int result)
        {
            // C#: out参数允许方法返回多个值
            // out参数必须在方法返回前被赋值
            
            // Java对比: 没有out参数，通常返回包装类或使用数组
            // public static Integer tryParseNumber(String input) {
            //     try {
            //         return Integer.parseInt(input);
            //     } catch (NumberFormatException e) {
            //         return null;
            //     }
            // }
            
            // Kotlin对比: 可以返回Pair或自定义数据类
            // fun tryParseNumber(input: String): Pair<Boolean, Int?> {
            //     return try {
            //         Pair(true, input.toInt())
            //     } catch (e: NumberFormatException) {
            //         Pair(false, null)
            //     }
            // }
            
            return int.TryParse(input, out result);
        }
        
        /// <summary>
        /// ref参数示例 - 引用传递
        /// </summary>
        private static void ModifyValue(ref int value)
        {
            // C#: ref参数允许方法修改传入的变量
            value *= 2;
            
            // Java对比: 基本类型按值传递，对象按引用传递
            // 无法直接修改基本类型参数的值
            
            // Kotlin对比: 基本类型不可变，需要使用包装类
        }
        
        /// <summary>
        /// 字符串操作示例
        /// </summary>
        private static void StringOperations()
        {
            Console.WriteLine("--- 字符串操作 ---");
            
            string text = "Hello World";
            
            // C#: 字符串方法
            Console.WriteLine($"Length: {text.Length}");
            Console.WriteLine($"Upper: {text.ToUpper()}");
            Console.WriteLine($"Lower: {text.ToLower()}");
            Console.WriteLine($"Contains 'World': {text.Contains("World")}");
            Console.WriteLine($"Substring: {text.Substring(0, 5)}");
            
            // C#: 字符串是不可变的（与Java相同）
            string original = "Hello";
            string modified = original + " World";
            Console.WriteLine($"Original: {original}, Modified: {modified}");
            
            // C#: StringBuilder用于高效字符串构建
            var sb = new System.Text.StringBuilder();
            sb.Append("Hello");
            sb.Append(" ");
            sb.Append("World");
            Console.WriteLine($"StringBuilder result: {sb.ToString()}\n");
        }
        
        /// <summary>
        /// 控制流语句示例
        /// </summary>
        private static void ControlFlow()
        {
            Console.WriteLine("--- 控制流语句 ---");
            
            // if-else语句（与Java/Kotlin基本相同）
            int score = 85;
            if (score >= 90)
            {
                Console.WriteLine("优秀");
            }
            else if (score >= 80)
            {
                Console.WriteLine("良好");
            }
            else
            {
                Console.WriteLine("需要改进");
            }
            
            // switch语句 - C# 8.0+支持表达式语法
            string grade = score switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
            Console.WriteLine($"Grade: {grade}");
            
            // for循环
            Console.Write("For loop: ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            
            // foreach循环
            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.Write("Foreach loop: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("\n");
        }
    }
}
