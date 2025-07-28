using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#集合和泛型示例
    /// 对比Java/Kotlin语法差异
    /// </summary>
    public class CollectionsAndGenerics
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 集合和泛型示例 ===\n");
            
            // 1. 数组操作
            ArrayExamples();
            
            // 2. List集合
            ListExamples();
            
            // 3. Dictionary字典
            DictionaryExamples();
            
            // 4. 泛型类和方法
            GenericExamples();
            
            // 5. 集合操作和LINQ预览
            CollectionOperations();
        }
        
        private static void ArrayExamples()
        {
            Console.WriteLine("--- 数组操作 ---");
            
            // C#: 数组声明和初始化
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] names = new string[3] { "张三", "李四", "王五" };
            
            // Java对比:
            // int[] numbers = {1, 2, 3, 4, 5};
            // String[] names = new String[]{"张三", "李四", "王五"};
            
            // Kotlin对比:
            // val numbers = intArrayOf(1, 2, 3, 4, 5)
            // val names = arrayOf("张三", "李四", "王五")
            
            Console.WriteLine($"数组长度: {numbers.Length}");
            Console.WriteLine($"第一个元素: {numbers[0]}");
            
            // 遍历数组
            Console.Write("数组元素: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
            
            // 多维数组
            int[,] matrix = {
                {1, 2, 3},
                {4, 5, 6}
            };
            
            Console.WriteLine($"矩阵[0,1]: {matrix[0, 1]}");
            
            // 锯齿数组（数组的数组）
            int[][] jaggedArray = new int[2][];
            jaggedArray[0] = new int[3] { 1, 2, 3 };
            jaggedArray[1] = new int[2] { 4, 5 };
            
            Console.WriteLine($"锯齿数组[1][0]: {jaggedArray[1][0]}\n");
        }
        
        private static void ListExamples()
        {
            Console.WriteLine("--- List集合 ---");
            
            // C#: List<T>泛型集合
            var numbers = new List<int> { 1, 2, 3 };
            var names = new List<string>();
            
            // Java对比:
            // List<Integer> numbers = new ArrayList<>(Arrays.asList(1, 2, 3));
            // List<String> names = new ArrayList<>();
            
            // Kotlin对比:
            // val numbers = mutableListOf(1, 2, 3)
            // val names = mutableListOf<String>()
            
            // 添加元素
            numbers.Add(4);
            numbers.AddRange(new[] { 5, 6, 7 });
            
            names.Add("张三");
            names.AddRange(new[] { "李四", "王五" });
            
            Console.WriteLine($"List大小: {numbers.Count}");
            Console.WriteLine($"包含3: {numbers.Contains(3)}");
            
            // 访问元素
            Console.WriteLine($"第一个: {numbers[0]}, 最后一个: {numbers[^1]}"); // ^1是C# 8.0的索引语法
            
            // 遍历
            Console.Write("List元素: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
            
            // 移除元素
            numbers.Remove(3);        // 移除值为3的元素
            numbers.RemoveAt(0);      // 移除索引0的元素
            
            Console.WriteLine($"移除后: [{string.Join(", ", numbers)}]");
            
            // List方法
            numbers.Sort();
            Console.WriteLine($"排序后: [{string.Join(", ", numbers)}]");
            
            numbers.Reverse();
            Console.WriteLine($"反转后: [{string.Join(", ", numbers)}]\n");
        }
        
        private static void DictionaryExamples()
        {
            Console.WriteLine("--- Dictionary字典 ---");
            
            // C#: Dictionary<TKey, TValue>
            var ages = new Dictionary<string, int>
            {
                {"张三", 25},
                {"李四", 30},
                {"王五", 35}
            };
            
            // Java对比:
            // Map<String, Integer> ages = new HashMap<>();
            // ages.put("张三", 25);
            // ages.put("李四", 30);
            
            // Kotlin对比:
            // val ages = mutableMapOf(
            //     "张三" to 25,
            //     "李四" to 30,
            //     "王五" to 35
            // )
            
            // 添加和访问
            ages["赵六"] = 28;
            Console.WriteLine($"张三的年龄: {ages["张三"]}");
            
            // 安全访问
            if (ages.TryGetValue("李四", out int age))
            {
                Console.WriteLine($"李四的年龄: {age}");
            }
            
            // 检查键是否存在
            Console.WriteLine($"包含王五: {ages.ContainsKey("王五")}");
            
            // 遍历字典
            Console.WriteLine("所有人员:");
            foreach (var kvp in ages)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}岁");
            }
            
            // 只遍历键或值
            Console.WriteLine($"所有姓名: [{string.Join(", ", ages.Keys)}]");
            Console.WriteLine($"所有年龄: [{string.Join(", ", ages.Values)}]");
            
            // 移除元素
            ages.Remove("王五");
            Console.WriteLine($"移除王五后，剩余人数: {ages.Count}\n");
        }
        
        private static void GenericExamples()
        {
            Console.WriteLine("--- 泛型类和方法 ---");
            
            // 使用泛型类
            var intBox = new GenericBox<int>(42);
            var stringBox = new GenericBox<string>("Hello");
            
            Console.WriteLine($"Int Box: {intBox.GetValue()}");
            Console.WriteLine($"String Box: {stringBox.GetValue()}");
            
            // 使用泛型方法
            int[] numbers = { 3, 1, 4, 1, 5 };
            string[] words = { "banana", "apple", "cherry" };
            
            Console.WriteLine($"交换前数组: [{string.Join(", ", numbers)}]");
            Swap(ref numbers[0], ref numbers[2]);
            Console.WriteLine($"交换后数组: [{string.Join(", ", numbers)}]");
            
            Console.WriteLine($"最大数字: {GetMax(10, 20, 5)}");
            Console.WriteLine($"最大字符串: {GetMax("apple", "banana", "cherry")}");
            
            // 泛型约束示例
            var calculator = new NumberCalculator<double>();
            Console.WriteLine($"计算结果: {calculator.Add(3.14, 2.86)}");
            
            Console.WriteLine();
        }
        
        private static void CollectionOperations()
        {
            Console.WriteLine("--- 集合操作 ---");
            
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            // 基本筛选和转换（不使用LINQ）
            var evenNumbers = new List<int>();
            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                {
                    evenNumbers.Add(num);
                }
            }
            Console.WriteLine($"偶数: [{string.Join(", ", evenNumbers)}]");
            
            // 使用LINQ进行相同操作（预览，后面会详细介绍）
            var evenNumbersLinq = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine($"偶数(LINQ): [{string.Join(", ", evenNumbersLinq)}]");
            
            // 查找操作
            int firstEven = numbers.First(n => n % 2 == 0);
            Console.WriteLine($"第一个偶数: {firstEven}");
            
            bool hasLargeNumber = numbers.Any(n => n > 5);
            Console.WriteLine($"是否有大于5的数: {hasLargeNumber}");
            
            // 聚合操作
            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();
            int min = numbers.Min();
            
            Console.WriteLine($"总和: {sum}, 平均值: {average:F2}, 最大值: {max}, 最小值: {min}");
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// 泛型方法 - 交换两个值
        /// </summary>
        public static void Swap<T>(ref T a, ref T b)
        {
            // C#: 泛型方法语法
            // public static <T> void swap(T[] array, int i, int j) // Java
            // fun <T> swap(a: T, b: T): Pair<T, T> // Kotlin (返回新值)
            
            T temp = a;
            a = b;
            b = temp;
        }
        
        /// <summary>
        /// 泛型方法 - 获取最大值
        /// </summary>
        public static T GetMax<T>(T a, T b, T c) where T : IComparable<T>
        {
            // C#: 泛型约束 where T : IComparable<T>
            // Java对比: public static <T extends Comparable<T>> T getMax(T a, T b, T c)
            // Kotlin对比: fun <T : Comparable<T>> getMax(a: T, b: T, c: T): T
            
            T max = a;
            if (b.CompareTo(max) > 0) max = b;
            if (c.CompareTo(max) > 0) max = c;
            return max;
        }
    }
    
    /// <summary>
    /// 泛型类示例
    /// </summary>
    /// <typeparam name="T">泛型类型参数</typeparam>
    public class GenericBox<T>
    {
        private T _value;
        
        public GenericBox(T value)
        {
            _value = value;
        }
        
        public T GetValue()
        {
            return _value;
        }
        
        public void SetValue(T value)
        {
            _value = value;
        }
        
        // Java对比:
        // public class GenericBox<T> {
        //     private T value;
        //     public GenericBox(T value) { this.value = value; }
        //     public T getValue() { return value; }
        //     public void setValue(T value) { this.value = value; }
        // }
        
        // Kotlin对比:
        // class GenericBox<T>(private var value: T) {
        //     fun getValue(): T = value
        //     fun setValue(newValue: T) { value = newValue }
        // }
    }
    
    /// <summary>
    /// 带约束的泛型类
    /// </summary>
    /// <typeparam name="T">必须是数值类型</typeparam>
    public class NumberCalculator<T> where T : struct, IComparable<T>
    {
        // C#: 多个泛型约束
        // where T : struct - T必须是值类型
        // where T : IComparable<T> - T必须实现IComparable接口
        
        public T Add(T a, T b)
        {
            // 注意：这里为了简化，使用dynamic
            // 实际项目中可能需要更复杂的处理
            dynamic da = a;
            dynamic db = b;
            return da + db;
        }
        
        public bool IsGreater(T a, T b)
        {
            return a.CompareTo(b) > 0;
        }
    }
}
