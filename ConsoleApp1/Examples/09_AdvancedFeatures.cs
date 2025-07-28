using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#高级特性示例 - 特性(Attributes)、反射(Reflection)、匿名类型
    /// 对比Java/Kotlin语法差异
    /// </summary>
    public class AdvancedFeatures
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 高级特性示例 ===\n");
            
            // 1. 特性 (Attributes)
            AttributeExamples();
            
            // 2. 反射 (Reflection)
            ReflectionExamples();
            
            // 3. 匿名类型和匿名方法
            AnonymousTypesAndMethods();
            
            // 4. 动态类型 (dynamic)
            DynamicTypeExamples();
        }
        
        private static void AttributeExamples()
        {
            Console.WriteLine("--- 特性 (Attributes) ---");
            
            // 使用带有特性的类
            var product = new ProductWithAttributes
            {
                Id = 1,
                Name = "笔记本电脑",
                Price = 5000.99m,
                InternalCode = "INTERNAL_001"
            };
            
            Console.WriteLine($"产品: {product.Name}, 价格: {product.Price:C}");
            
            // 通过反射读取特性信息
            Type productType = typeof(ProductWithAttributes);
            
            // 检查类级别的特性
            var obsoleteAttr = productType.GetCustomAttribute<ObsoleteAttribute>();
            if (obsoleteAttr != null)
            {
                Console.WriteLine($"⚠️  类已过时: {obsoleteAttr.Message}");
            }
            
            // 检查属性的特性
            PropertyInfo nameProperty = productType.GetProperty("Name")!;
            var jsonPropertyAttr = nameProperty.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonPropertyAttr != null)
            {
                Console.WriteLine($"JSON属性名: {jsonPropertyAttr.Name}");
            }
            
            // 检查自定义特性
            PropertyInfo priceProperty = productType.GetProperty("Price")!;
            var validationAttr = priceProperty.GetCustomAttribute<RangeValidationAttribute>();
            if (validationAttr != null)
            {
                Console.WriteLine($"价格验证范围: {validationAttr.Min} - {validationAttr.Max}");
                bool isValid = validationAttr.IsValid(product.Price);
                Console.WriteLine($"当前价格是否有效: {isValid}");
            }
            
            // Java对比:
            // @Deprecated
            // @JsonProperty("product_name")
            // @Range(min = 0, max = 10000)
            // public class Product { ... }
            
            // Kotlin对比:
            // @Deprecated("Use NewProduct instead")
            // @JsonProperty("product_name")
            // @field:Range(min = 0, max = 10000)
            // class Product { ... }
            
            Console.WriteLine();
        }
        
        private static void ReflectionExamples()
        {
            Console.WriteLine("--- 反射 (Reflection) ---");
            
            // 获取类型信息
            Type stringType = typeof(string);
            Type listType = typeof(List<>);
            Type productType = typeof(ProductWithAttributes);
            
            Console.WriteLine($"字符串类型: {stringType.Name}");
            Console.WriteLine($"泛型List类型: {listType.Name}");
            Console.WriteLine($"产品类型: {productType.FullName}");
            
            // 获取类型的属性信息
            Console.WriteLine("\n产品类的属性:");
            PropertyInfo[] properties = productType.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                Console.WriteLine($"  {prop.Name}: {prop.PropertyType.Name}");
                
                // 检查属性是否有特性
                var attributes = prop.GetCustomAttributes();
                foreach (Attribute attr in attributes)
                {
                    Console.WriteLine($"    特性: {attr.GetType().Name}");
                }
            }
            
            // 动态创建对象
            Console.WriteLine("\n动态创建对象:");
            object productInstance = Activator.CreateInstance(productType)!;
            
            // 动态设置属性值
            PropertyInfo nameProperty = productType.GetProperty("Name")!;
            nameProperty.SetValue(productInstance, "动态创建的产品");
            
            PropertyInfo priceProperty = productType.GetProperty("Price")!;
            priceProperty.SetValue(productInstance, 999.99m);
            
            // 动态获取属性值
            string name = (string)nameProperty.GetValue(productInstance)!;
            decimal price = (decimal)priceProperty.GetValue(productInstance)!;
            
            Console.WriteLine($"动态设置的产品: {name}, 价格: {price:C}");
            
            // 获取和调用方法
            Console.WriteLine("\n动态调用方法:");
            MethodInfo toStringMethod = productType.GetMethod("ToString")!;
            string result = (string)toStringMethod.Invoke(productInstance, null)!;
            Console.WriteLine($"ToString结果: {result}");
            
            // 泛型类型处理
            Console.WriteLine("\n泛型类型处理:");
            Type genericListType = typeof(List<int>);
            Console.WriteLine($"泛型List<int>: {genericListType.Name}");
            Console.WriteLine($"是否为泛型: {genericListType.IsGenericType}");
            Console.WriteLine($"泛型参数: {string.Join(", ", genericListType.GetGenericArguments().Select(t => t.Name))}");
            
            // Java对比:
            // Class<?> clazz = String.class;
            // Field[] fields = clazz.getDeclaredFields();
            // Method method = clazz.getMethod("toString");
            // Object instance = clazz.newInstance();
            
            // Kotlin对比:
            // val clazz = String::class.java
            // val properties = clazz.kotlin.memberProperties
            // val instance = clazz.newInstance()
            
            Console.WriteLine();
        }
        
        private static void AnonymousTypesAndMethods()
        {
            Console.WriteLine("--- 匿名类型和匿名方法 ---");
            
            // 匿名类型
            var person = new { Name = "张三", Age = 25, City = "北京" };
            var product = new { Id = 1, Name = "笔记本", Price = 5000.0 };
            
            Console.WriteLine($"匿名人员对象: {person.Name}, {person.Age}岁, 来自{person.City}");
            Console.WriteLine($"匿名产品对象: {product.Name}, 价格: {product.Price:C}");
            
            // 匿名类型数组
            var people = new[]
            {
                new { Name = "张三", Age = 25 },
                new { Name = "李四", Age = 30 },
                new { Name = "王五", Age = 28 }
            };
            
            Console.WriteLine("匿名类型数组:");
            foreach (var p in people)
            {
                Console.WriteLine($"  {p.Name}: {p.Age}岁");
            }
            
            // 匿名方法 (C# 2.0)
            Console.WriteLine("\n匿名方法演示:");
            
            // 使用匿名方法
            Func<int, int> square = delegate(int x) { return x * x; };
            Console.WriteLine($"匿名方法计算5的平方: {square(5)}");
            
            // Lambda表达式 (C# 3.0) - 匿名方法的简化语法
            Func<int, int> cube = x => x * x * x;
            Console.WriteLine($"Lambda表达式计算5的立方: {cube(5)}");
            
            // 复杂的匿名方法
            Func<string, int, string> formatter = delegate(string name, int age)
            {
                return $"姓名: {name}, 年龄: {age}";
            };
            Console.WriteLine($"复杂匿名方法: {formatter("张三", 25)}");
            
            // 等价的Lambda表达式
            Func<string, int, string> formatterLambda = (name, age) => $"姓名: {name}, 年龄: {age}";
            Console.WriteLine($"等价Lambda: {formatterLambda("李四", 30)}");
            
            // 在LINQ中使用匿名类型
            var numbers = new[] { 1, 2, 3, 4, 5 };
            var numberInfo = numbers.Select(n => new 
            { 
                Number = n, 
                Square = n * n, 
                IsEven = n % 2 == 0 
            });
            
            Console.WriteLine("\nLINQ中的匿名类型:");
            foreach (var info in numberInfo)
            {
                Console.WriteLine($"  数字: {info.Number}, 平方: {info.Square}, 是偶数: {info.IsEven}");
            }
            
            // Java对比:
            // // Java 8+ Lambda
            // Function<Integer, Integer> square = x -> x * x;
            // // Java匿名类
            // Function<Integer, Integer> square = new Function<Integer, Integer>() {
            //     public Integer apply(Integer x) { return x * x; }
            // };
            
            // Kotlin对比:
            // val square: (Int) -> Int = { x -> x * x }
            // val square = { x: Int -> x * x }
            // // 匿名对象
            // val person = object {
            //     val name = "张三"
            //     val age = 25
            // }
            
            Console.WriteLine();
        }
        
        private static void DynamicTypeExamples()
        {
            Console.WriteLine("--- 动态类型 (dynamic) ---");
            
            // dynamic关键字 - 运行时类型检查
            dynamic dynamicVar = "Hello World";
            Console.WriteLine($"动态变量作为字符串: {dynamicVar}");
            Console.WriteLine($"字符串长度: {dynamicVar.Length}");
            
            // 改变动态变量的类型
            dynamicVar = 42;
            Console.WriteLine($"动态变量作为整数: {dynamicVar}");
            Console.WriteLine($"整数加法: {dynamicVar + 10}");
            
            // 动态对象
            dynamic dynamicObject = new System.Dynamic.ExpandoObject();
            dynamicObject.Name = "动态对象";
            dynamicObject.Age = 25;
            dynamicObject.SayHello = new Func<string>(() => $"Hello, I'm {dynamicObject.Name}");
            
            Console.WriteLine($"动态对象: {dynamicObject.Name}, {dynamicObject.Age}岁");
            Console.WriteLine($"动态方法调用: {dynamicObject.SayHello()}");
            
            // 动态类型的风险 - 运行时错误
            try
            {
                dynamic riskyVar = "字符串";
                int result = riskyVar.NonExistentMethod(); // 运行时会抛出异常
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                Console.WriteLine($"动态类型运行时错误: {ex.Message}");
            }
            
            // 与var的区别
            var staticVar = "编译时确定类型";  // 编译时确定为string
            dynamic dynamicVar2 = "运行时确定类型"; // 运行时确定类型
            
            Console.WriteLine($"var变量类型: {staticVar.GetType().Name}");
            Console.WriteLine($"dynamic变量类型: {dynamicVar2.GetType().Name}");
            
            // Java对比: 没有直接对应的dynamic类型，可以使用Object + 反射
            // Object obj = "Hello";
            // String str = (String) obj; // 需要显式转换
            
            // Kotlin对比: Any类型 + 智能转换
            // var any: Any = "Hello"
            // if (any is String) {
            //     println(any.length) // 智能转换
            // }
            
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 自定义特性示例 - 范围验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeValidationAttribute : Attribute
    {
        public double Min { get; }
        public double Max { get; }

        public RangeValidationAttribute(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public bool IsValid(object value)
        {
            if (value is double doubleValue)
            {
                return doubleValue >= Min && doubleValue <= Max;
            }
            if (value is decimal decimalValue)
            {
                return (double)decimalValue >= Min && (double)decimalValue <= Max;
            }
            return false;
        }

        // Java对比:
        // @Target(ElementType.FIELD)
        // @Retention(RetentionPolicy.RUNTIME)
        // public @interface RangeValidation {
        //     double min();
        //     double max();
        // }

        // Kotlin对比:
        // @Target(AnnotationTarget.PROPERTY)
        // @Retention(AnnotationRetention.RUNTIME)
        // annotation class RangeValidation(val min: Double, val max: Double)
    }

    /// <summary>
    /// 带有各种特性的产品类
    /// </summary>
    [Obsolete("此类已过时，请使用NewProduct类", false)]
    [Serializable]
    public class ProductWithAttributes
    {
        public int Id { get; set; }

        [JsonPropertyName("product_name")]
        public string Name { get; set; } = "";

        [RangeValidation(0, 10000)]
        public decimal Price { get; set; }

        [Obsolete("内部代码字段将被移除")]
        public string InternalCode { get; set; } = "";

        public override string ToString()
        {
            return $"Product: {Name} (¥{Price:F2})";
        }

        // Java对比:
        // @Deprecated
        // @JsonProperty("product_name")
        // @Range(min = 0, max = 10000)
        // private String name;

        // Kotlin对比:
        // @Deprecated("Use NewProduct instead")
        // @JsonProperty("product_name")
        // @field:Range(min = 0.0, max = 10000.0)
        // var name: String = ""
    }

    /// <summary>
    /// 演示方法特性的类
    /// </summary>
    public class MethodAttributeExample
    {
        [Obsolete("使用NewCalculate方法替代")]
        public int OldCalculate(int a, int b)
        {
            return a + b;
        }

        public int NewCalculate(int a, int b)
        {
            return a + b;
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void DebugMethod()
        {
            Console.WriteLine("这个方法只在DEBUG模式下执行");
        }
    }

    /// <summary>
    /// 演示特性继承的基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class CustomDocumentationAttribute : Attribute
    {
        public string Description { get; }
        public string Author { get; set; } = "";
        public string Version { get; set; } = "1.0";

        public CustomDocumentationAttribute(string description)
        {
            Description = description;
        }
    }

    /// <summary>
    /// 使用自定义文档特性的类
    /// </summary>
    [CustomDocumentation("这是一个示例类", Author = "开发者", Version = "2.0")]
    public class DocumentedClass
    {
        [CustomDocumentation("这是一个示例方法")]
        public void ExampleMethod()
        {
            Console.WriteLine("示例方法执行");
        }
    }
}
