using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C# LINQ和函数式编程示例
    /// LINQ是C#的强大特性，提供统一的查询语法
    /// </summary>
    public class LinqAndFunctional
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# LINQ和函数式编程示例 ===\n");
            
            // 1. 基本LINQ查询
            BasicLinqQueries();
            
            // 2. LINQ方法语法 vs 查询语法
            MethodVsQuerySyntax();
            
            // 3. 复杂LINQ操作
            ComplexLinqOperations();
            
            // 4. 分组和聚合
            GroupingAndAggregation();
            
            // 5. 函数式编程概念
            FunctionalProgramming();
        }
        
        private static void BasicLinqQueries()
        {
            Console.WriteLine("--- 基本LINQ查询 ---");
            
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine($"原始数据: [{string.Join(", ", numbers)}]");
            
            // Where - 筛选
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"偶数: [{string.Join(", ", evenNumbers)}]");
            
            // Select - 投影/转换
            var squares = numbers.Select(n => n * n);
            Console.WriteLine($"平方: [{string.Join(", ", squares)}]");
            
            // 链式调用
            var evenSquares = numbers
                .Where(n => n % 2 == 0)
                .Select(n => n * n);
            Console.WriteLine($"偶数的平方: [{string.Join(", ", evenSquares)}]");
            
            // First, Last, Single
            var firstEven = numbers.First(n => n % 2 == 0);
            var lastOdd = numbers.Last(n => n % 2 == 1);
            Console.WriteLine($"第一个偶数: {firstEven}, 最后一个奇数: {lastOdd}");
            
            // Any, All
            bool hasLargeNumbers = numbers.Any(n => n > 5);
            bool allPositive = numbers.All(n => n > 0);
            Console.WriteLine($"有大于5的数: {hasLargeNumbers}, 都是正数: {allPositive}");
            
            // Java对比: Stream API (Java 8+)
            // numbers.stream()
            //     .filter(n -> n % 2 == 0)
            //     .map(n -> n * n)
            //     .collect(Collectors.toList());
            
            // Kotlin对比: 集合操作函数
            // numbers.filter { it % 2 == 0 }
            //     .map { it * it }
            
            Console.WriteLine();
        }
        
        private static void MethodVsQuerySyntax()
        {
            Console.WriteLine("--- 方法语法 vs 查询语法 ---");
            
            var students = new[]
            {
                new { Name = "张三", Age = 20, Grade = 85 },
                new { Name = "李四", Age = 22, Grade = 92 },
                new { Name = "王五", Age = 19, Grade = 78 },
                new { Name = "赵六", Age = 21, Grade = 88 }
            };
            
            // 方法语法 (Method Syntax)
            var highGradeStudentsMethod = students
                .Where(s => s.Grade >= 85)
                .OrderBy(s => s.Age)
                .Select(s => new { s.Name, s.Age, s.Grade });
            
            Console.WriteLine("方法语法结果:");
            foreach (var student in highGradeStudentsMethod)
            {
                Console.WriteLine($"  {student.Name} - 年龄: {student.Age}, 成绩: {student.Grade}");
            }
            
            // 查询语法 (Query Syntax) - 类似SQL
            var highGradeStudentsQuery = from s in students
                                       where s.Grade >= 85
                                       orderby s.Age
                                       select new { s.Name, s.Age, s.Grade };
            
            Console.WriteLine("查询语法结果:");
            foreach (var student in highGradeStudentsQuery)
            {
                Console.WriteLine($"  {student.Name} - 年龄: {student.Age}, 成绩: {student.Grade}");
            }
            
            // 复杂查询语法示例
            var complexQuery = from s in students
                             where s.Age >= 20
                             orderby s.Grade descending, s.Age
                             select new 
                             { 
                                 StudentInfo = $"{s.Name} ({s.Age}岁)",
                                 Performance = s.Grade >= 90 ? "优秀" : 
                                             s.Grade >= 80 ? "良好" : "一般"
                             };
            
            Console.WriteLine("复杂查询结果:");
            foreach (var item in complexQuery)
            {
                Console.WriteLine($"  {item.StudentInfo} - {item.Performance}");
            }
            
            Console.WriteLine();
        }
        
        private static void ComplexLinqOperations()
        {
            Console.WriteLine("--- 复杂LINQ操作 ---");
            
            var products = new[]
            {
                new Product { Id = 1, Name = "笔记本电脑", Category = "电子产品", Price = 5000, InStock = true },
                new Product { Id = 2, Name = "鼠标", Category = "电子产品", Price = 50, InStock = true },
                new Product { Id = 3, Name = "键盘", Category = "电子产品", Price = 200, InStock = false },
                new Product { Id = 4, Name = "办公椅", Category = "家具", Price = 800, InStock = true },
                new Product { Id = 5, Name = "书桌", Category = "家具", Price = 1200, InStock = true }
            };
            
            // Skip 和 Take - 分页
            var page1 = products.Skip(0).Take(2);
            var page2 = products.Skip(2).Take(2);
            Console.WriteLine($"第1页: [{string.Join(", ", page1.Select(p => p.Name))}]");
            Console.WriteLine($"第2页: [{string.Join(", ", page2.Select(p => p.Name))}]");
            
            // Distinct - 去重
            var categories = products.Select(p => p.Category).Distinct();
            Console.WriteLine($"产品类别: [{string.Join(", ", categories)}]");
            
            // Union, Intersect, Except - 集合操作
            var expensiveProducts = products.Where(p => p.Price > 500).Select(p => p.Id);
            var inStockProducts = products.Where(p => p.InStock).Select(p => p.Id);
            
            var expensiveAndInStock = expensiveProducts.Intersect(inStockProducts);
            Console.WriteLine($"昂贵且有库存的产品ID: [{string.Join(", ", expensiveAndInStock)}]");
            
            // SelectMany - 扁平化
            var orders = new[]
            {
                new { OrderId = 1, Items = new[] { "商品A", "商品B" } },
                new { OrderId = 2, Items = new[] { "商品C" } },
                new { OrderId = 3, Items = new[] { "商品D", "商品E", "商品F" } }
            };
            
            var allItems = orders.SelectMany(o => o.Items);
            Console.WriteLine($"所有订单商品: [{string.Join(", ", allItems)}]");
            
            // Zip - 合并两个序列
            var names = new[] { "产品1", "产品2", "产品3" };
            var prices = new[] { 100, 200, 300 };
            var nameWithPrices = names.Zip(prices, (name, price) => $"{name}: ¥{price}");
            Console.WriteLine($"产品价格: [{string.Join(", ", nameWithPrices)}]");
            
            Console.WriteLine();
        }
        
        private static void GroupingAndAggregation()
        {
            Console.WriteLine("--- 分组和聚合 ---");
            
            var sales = new[]
            {
                new Sale { Product = "笔记本", Category = "电子", Amount = 5000, Date = new DateTime(2024, 1, 15) },
                new Sale { Product = "鼠标", Category = "电子", Amount = 50, Date = new DateTime(2024, 1, 16) },
                new Sale { Product = "椅子", Category = "家具", Amount = 800, Date = new DateTime(2024, 1, 17) },
                new Sale { Product = "键盘", Category = "电子", Amount = 200, Date = new DateTime(2024, 2, 1) },
                new Sale { Product = "桌子", Category = "家具", Amount = 1200, Date = new DateTime(2024, 2, 2) }
            };
            
            // GroupBy - 按类别分组
            var salesByCategory = sales.GroupBy(s => s.Category);
            
            Console.WriteLine("按类别分组的销售:");
            foreach (var group in salesByCategory)
            {
                Console.WriteLine($"  {group.Key}:");
                foreach (var sale in group)
                {
                    Console.WriteLine($"    {sale.Product}: ¥{sale.Amount}");
                }
                Console.WriteLine($"    小计: ¥{group.Sum(s => s.Amount)}");
            }
            
            // 复杂分组 - 按月份分组
            var salesByMonth = sales.GroupBy(s => new { s.Date.Year, s.Date.Month });
            
            Console.WriteLine("按月份分组的销售:");
            foreach (var group in salesByMonth)
            {
                Console.WriteLine($"  {group.Key.Year}年{group.Key.Month}月: ¥{group.Sum(s => s.Amount)}");
            }
            
            // 聚合函数
            var totalSales = sales.Sum(s => s.Amount);
            var averageSale = sales.Average(s => s.Amount);
            var maxSale = sales.Max(s => s.Amount);
            var minSale = sales.Min(s => s.Amount);
            var salesCount = sales.Count();
            
            Console.WriteLine($"总销售额: ¥{totalSales}");
            Console.WriteLine($"平均销售额: ¥{averageSale:F2}");
            Console.WriteLine($"最大销售额: ¥{maxSale}");
            Console.WriteLine($"最小销售额: ¥{minSale}");
            Console.WriteLine($"销售笔数: {salesCount}");
            
            // 条件聚合
            var electronicsSales = sales.Where(s => s.Category == "电子").Sum(s => s.Amount);
            Console.WriteLine($"电子产品销售额: ¥{electronicsSales}");
            
            Console.WriteLine();
        }
        
        private static void FunctionalProgramming()
        {
            Console.WriteLine("--- 函数式编程概念 ---");
            
            var numbers = Enumerable.Range(1, 10).ToArray();
            Console.WriteLine($"原始数据: [{string.Join(", ", numbers)}]");
            
            // 高阶函数 - 接受函数作为参数的函数
            var doubled = ApplyOperation(numbers, x => x * 2);
            Console.WriteLine($"翻倍: [{string.Join(", ", doubled)}]");
            
            var squared = ApplyOperation(numbers, x => x * x);
            Console.WriteLine($"平方: [{string.Join(", ", squared)}]");
            
            // 函数组合
            Func<int, int> addOne = x => x + 1;
            Func<int, int> multiplyByTwo = x => x * 2;
            Func<int, int> composed = x => multiplyByTwo(addOne(x));
            
            var composedResult = numbers.Select(composed);
            Console.WriteLine($"组合函数(+1然后*2): [{string.Join(", ", composedResult)}]");
            
            // 柯里化 (Currying) 概念
            Func<int, Func<int, int>> curriedAdd = x => y => x + y;
            var addFive = curriedAdd(5);
            var addedFive = numbers.Select(addFive);
            Console.WriteLine($"柯里化加5: [{string.Join(", ", addedFive)}]");
            
            // 惰性求值 - LINQ查询是惰性的
            Console.WriteLine("演示惰性求值:");
            var lazyQuery = numbers
                .Where(x => { Console.WriteLine($"  检查: {x}"); return x % 2 == 0; })
                .Select(x => { Console.WriteLine($"  转换: {x}"); return x * x; });
            
            Console.WriteLine("查询定义完成，但还未执行");
            Console.WriteLine("现在执行查询:");
            var lazyResult = lazyQuery.Take(2).ToList(); // 只处理前两个匹配项
            Console.WriteLine($"结果: [{string.Join(", ", lazyResult)}]");
            
            // 不可变性概念
            var originalList = new List<int> { 1, 2, 3 };
            var newList = originalList.Select(x => x * 2).ToList(); // 创建新列表，不修改原列表
            Console.WriteLine($"原列表: [{string.Join(", ", originalList)}]");
            Console.WriteLine($"新列表: [{string.Join(", ", newList)}]");
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// 高阶函数示例 - 接受函数作为参数
        /// </summary>
        private static IEnumerable<int> ApplyOperation(IEnumerable<int> numbers, Func<int, int> operation)
        {
            return numbers.Select(operation);
        }
    }
    
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
    
    /// <summary>
    /// 销售记录类
    /// </summary>
    public class Sale
    {
        public string Product { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
