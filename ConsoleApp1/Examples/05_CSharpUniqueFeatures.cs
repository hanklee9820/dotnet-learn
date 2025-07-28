using System;
using System.Collections.Generic;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#独有特性示例 - 属性、委托、事件、扩展方法等
    /// 这些特性在Java/Kotlin中没有直接对应的语法
    /// </summary>
    public class CSharpUniqueFeatures
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 独有特性示例 ===\n");
            
            // 1. 属性 (Properties)
            PropertyExamples();
            
            // 2. 委托 (Delegates)
            DelegateExamples();
            
            // 3. 事件 (Events)
            EventExamples();
            
            // 4. 扩展方法 (Extension Methods)
            ExtensionMethodExamples();
            
            // 5. 分部类 (Partial Classes)
            PartialClassExamples();
            
            // 6. 本地函数 (Local Functions)
            LocalFunctionExamples();
        }
        
        private static void PropertyExamples()
        {
            Console.WriteLine("--- 属性 (Properties) ---");
            
            var person = new PersonWithProperties("张三", 25);
            
            // 使用属性就像使用字段一样
            Console.WriteLine($"姓名: {person.Name}");
            Console.WriteLine($"年龄: {person.Age}");
            Console.WriteLine($"全名: {person.FullName}");
            
            // 修改属性
            person.Name = "李四";
            person.Age = 30;
            Console.WriteLine($"修改后: {person.FullName}");
            
            // 只读属性
            Console.WriteLine($"创建时间: {person.CreatedAt}");
            
            // 自动属性
            person.Email = "lisi@example.com";
            Console.WriteLine($"邮箱: {person.Email}");
            
            // Java对比: 需要显式的getter/setter方法
            // public String getName() { return name; }
            // public void setName(String name) { this.name = name; }
            
            // Kotlin对比: 有属性语法，但实现方式不同
            // var name: String = name
            //     get() = field
            //     set(value) { field = value }
            
            Console.WriteLine();
        }
        
        private static void DelegateExamples()
        {
            Console.WriteLine("--- 委托 (Delegates) ---");
            
            // 声明委托变量
            MathOperation operation;
            
            // 将方法赋值给委托
            operation = Add;
            Console.WriteLine($"加法: {operation(5, 3)}");
            
            operation = Multiply;
            Console.WriteLine($"乘法: {operation(5, 3)}");
            
            // 多播委托 - 可以包含多个方法
            MathOperation multicast = Add;
            multicast += Multiply;
            multicast += Subtract;
            
            Console.WriteLine("多播委托执行:");
            multicast(10, 2); // 会依次调用所有方法
            
            // 匿名方法
            operation = delegate(int a, int b) { return a % b; };
            Console.WriteLine($"取模: {operation(10, 3)}");
            
            // Lambda表达式
            operation = (a, b) => a * a + b * b;
            Console.WriteLine($"平方和: {operation(3, 4)}");
            
            // Action和Func委托
            Action<string> printMessage = message => Console.WriteLine($"消息: {message}");
            printMessage("Hello Delegate!");
            
            Func<int, int, int> calculator = (x, y) => x + y;
            Console.WriteLine($"Func计算: {calculator(7, 8)}");
            
            // Java对比: 使用函数式接口
            // Function<Integer, Integer> square = x -> x * x;
            // Consumer<String> printer = System.out::println;
            
            // Kotlin对比: 使用函数类型
            // val operation: (Int, Int) -> Int = { a, b -> a + b }
            // val printer: (String) -> Unit = { println(it) }
            
            Console.WriteLine();
        }
        
        private static void EventExamples()
        {
            Console.WriteLine("--- 事件 (Events) ---");
            
            var publisher = new EventPublisher();
            var subscriber = new EventSubscriber();
            
            // 订阅事件
            publisher.SomethingHappened += subscriber.HandleEvent;
            publisher.SomethingHappened += (sender, e) => 
                Console.WriteLine($"Lambda处理: {e.Message}");
            
            // 触发事件
            publisher.DoSomething("第一个事件");
            publisher.DoSomething("第二个事件");
            
            // 取消订阅
            publisher.SomethingHappened -= subscriber.HandleEvent;
            publisher.DoSomething("第三个事件"); // 只有Lambda处理器会响应
            
            // Java对比: 使用观察者模式或监听器接口
            // publisher.addListener(new EventListener() {
            //     public void onEvent(Event e) { ... }
            // });
            
            // Kotlin对比: 可以使用函数类型或接口
            // publisher.onEvent = { event -> println(event.message) }
            
            Console.WriteLine();
        }
        
        private static void ExtensionMethodExamples()
        {
            Console.WriteLine("--- 扩展方法 (Extension Methods) ---");
            
            // 为现有类型添加新方法
            string text = "hello world";
            Console.WriteLine($"原始: {text}");
            Console.WriteLine($"首字母大写: {text.ToTitleCase()}");
            Console.WriteLine($"单词数量: {text.WordCount()}");
            
            // 为int类型添加扩展方法
            int number = 5;
            Console.WriteLine($"{number} 的阶乘: {number.Factorial()}");
            Console.WriteLine($"{number} 是偶数: {number.IsEven()}");
            
            // 为List添加扩展方法
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine($"列表: [{string.Join(", ", numbers)}]");
            Console.WriteLine($"第二大的数: {numbers.SecondLargest()}");
            
            // Java对比: 无法为现有类添加方法，需要创建工具类
            // public class StringUtils {
            //     public static String toTitleCase(String str) { ... }
            // }
            // StringUtils.toTitleCase(text);
            
            // Kotlin对比: 也支持扩展函数
            // fun String.toTitleCase(): String { ... }
            // text.toTitleCase()
            
            Console.WriteLine();
        }
        
        private static void PartialClassExamples()
        {
            Console.WriteLine("--- 分部类 (Partial Classes) ---");
            
            var calc = new Calculator();
            Console.WriteLine($"加法: {calc.Add(10, 5)}");
            Console.WriteLine($"减法: {calc.Subtract(10, 5)}");
            Console.WriteLine($"乘法: {calc.Multiply(10, 5)}");
            Console.WriteLine($"除法: {calc.Divide(10, 5)}");
            
            // 分部类允许将一个类的定义分散到多个文件中
            // 这在代码生成场景中特别有用（如Windows Forms设计器）
            
            // Java/Kotlin对比: 没有分部类概念，一个类必须在一个文件中定义
            
            Console.WriteLine();
        }
        
        private static void LocalFunctionExamples()
        {
            Console.WriteLine("--- 本地函数 (Local Functions) ---");
            
            // 本地函数定义在方法内部
            int CalculateFactorial(int n)
            {
                if (n < 0) throw new ArgumentException("数字不能为负数");
                if (n <= 1) return 1;
                return n * CalculateFactorial(n - 1);
            }
            
            // 使用本地函数
            Console.WriteLine($"5的阶乘: {CalculateFactorial(5)}");
            
            // 本地函数可以访问外部方法的变量
            string prefix = "结果是: ";
            
            string FormatResult(int value)
            {
                return $"{prefix}{value}";
            }
            
            Console.WriteLine(FormatResult(42));
            
            // 复杂示例：快速排序
            int[] array = { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine($"排序前: [{string.Join(", ", array)}]");
            
            void QuickSort(int[] arr, int low, int high)
            {
                if (low < high)
                {
                    int pi = Partition(arr, low, high);
                    QuickSort(arr, low, pi - 1);
                    QuickSort(arr, pi + 1, high);
                }
            }
            
            int Partition(int[] arr, int low, int high)
            {
                int pivot = arr[high];
                int i = low - 1;
                
                for (int j = low; j < high; j++)
                {
                    if (arr[j] < pivot)
                    {
                        i++;
                        (arr[i], arr[j]) = (arr[j], arr[i]); // 元组交换
                    }
                }
                (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
                return i + 1;
            }
            
            QuickSort(array, 0, array.Length - 1);
            Console.WriteLine($"排序后: [{string.Join(", ", array)}]");
            
            // Java对比: 没有本地函数，需要使用私有方法或匿名类
            // Kotlin对比: 支持本地函数
            // fun outerFunction() {
            //     fun localFunction() { ... }
            // }
            
            Console.WriteLine();
        }
        
        // 委托类型定义
        public delegate int MathOperation(int a, int b);
        
        // 委托使用的方法
        private static int Add(int a, int b)
        {
            int result = a + b;
            Console.WriteLine($"  加法: {a} + {b} = {result}");
            return result;
        }
        
        private static int Multiply(int a, int b)
        {
            int result = a * b;
            Console.WriteLine($"  乘法: {a} * {b} = {result}");
            return result;
        }
        
        private static int Subtract(int a, int b)
        {
            int result = a - b;
            Console.WriteLine($"  减法: {a} - {b} = {result}");
            return result;
        }
    }
    
    /// <summary>
    /// 演示属性的类
    /// </summary>
    public class PersonWithProperties
    {
        private string _name;
        private int _age;
        
        // 完整属性定义
        public string Name
        {
            get { return _name; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("姓名不能为空");
                _name = value; 
            }
        }
        
        // 带验证的属性
        public int Age
        {
            get { return _age; }
            set 
            { 
                if (value < 0 || value > 150)
                    throw new ArgumentException("年龄必须在0-150之间");
                _age = value; 
            }
        }
        
        // 只读属性
        public DateTime CreatedAt { get; }
        
        // 计算属性
        public string FullName => $"{Name} ({Age}岁)";
        
        // 自动属性
        public string Email { get; set; } = "";
        
        public PersonWithProperties(string name, int age)
        {
            _name = "";
            Name = name;
            Age = age;
            CreatedAt = DateTime.Now;
        }
    }
    
    /// <summary>
    /// 事件发布者
    /// </summary>
    public class EventPublisher
    {
        // 事件定义
        public event EventHandler<CustomEventArgs>? SomethingHappened;
        
        public void DoSomething(string message)
        {
            Console.WriteLine($"执行操作: {message}");
            
            // 触发事件
            OnSomethingHappened(new CustomEventArgs(message));
        }
        
        protected virtual void OnSomethingHappened(CustomEventArgs e)
        {
            SomethingHappened?.Invoke(this, e);
        }
    }
    
    /// <summary>
    /// 自定义事件参数
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }
        
        public CustomEventArgs(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
    
    /// <summary>
    /// 事件订阅者
    /// </summary>
    public class EventSubscriber
    {
        public void HandleEvent(object? sender, CustomEventArgs e)
        {
            Console.WriteLine($"  事件处理: {e.Message} (时间: {e.Timestamp:HH:mm:ss})");
        }
    }
    
    /// <summary>
    /// 分部类示例 - 第一部分
    /// </summary>
    public partial class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        
        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }
    
    /// <summary>
    /// 分部类示例 - 第二部分
    /// 在实际项目中，这通常会在另一个文件中
    /// </summary>
    public partial class Calculator
    {
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        
        public double Divide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException();
            return (double)a / b;
        }
    }
    
    /// <summary>
    /// 扩展方法类 - 必须是静态类
    /// </summary>
    public static class StringExtensions
    {
        // 扩展方法必须是静态方法，第一个参数使用this关键字
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            
            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }
        
        public static int WordCount(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            return input.Split(new[] { ' ', '\t', '\n', '\r' }, 
                StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    
    /// <summary>
    /// 数字扩展方法
    /// </summary>
    public static class NumberExtensions
    {
        public static long Factorial(this int number)
        {
            if (number < 0) throw new ArgumentException("数字不能为负数");
            if (number <= 1) return 1;
            
            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
        
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
    }
    
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class CollectionExtensions
    {
        public static T SecondLargest<T>(this IList<T> list) where T : IComparable<T>
        {
            if (list.Count < 2) throw new ArgumentException("列表至少需要两个元素");
            
            T largest = list[0];
            T secondLargest = list[0];
            
            foreach (T item in list)
            {
                if (item.CompareTo(largest) > 0)
                {
                    secondLargest = largest;
                    largest = item;
                }
                else if (item.CompareTo(secondLargest) > 0 && item.CompareTo(largest) < 0)
                {
                    secondLargest = item;
                }
            }
            
            return secondLargest;
        }
    }
}
