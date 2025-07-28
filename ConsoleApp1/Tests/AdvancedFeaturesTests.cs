using System;
using System.Linq;
using System.Reflection;
using Xunit;
using ConsoleApp1.Examples;

namespace ConsoleApp1.Tests
{
    /// <summary>
    /// 高级特性测试 - 验证特性、反射、匿名类型等功能
    /// </summary>
    public class AdvancedFeaturesTests
    {
        [Fact]
        public void TestCustomAttribute()
        {
            // 测试自定义特性
            var productType = typeof(ProductWithAttributes);
            var priceProperty = productType.GetProperty("Price");
            
            Assert.NotNull(priceProperty);
            
            var rangeAttr = priceProperty!.GetCustomAttribute<RangeValidationAttribute>();
            Assert.NotNull(rangeAttr);
            Assert.Equal(0, rangeAttr!.Min);
            Assert.Equal(10000, rangeAttr.Max);
            
            // 测试验证逻辑
            Assert.True(rangeAttr.IsValid(5000m));
            Assert.False(rangeAttr.IsValid(15000m));
            Assert.False(rangeAttr.IsValid(-100m));
        }
        
        [Fact]
        public void TestObsoleteAttribute()
        {
            // 测试Obsolete特性
            var productType = typeof(ProductWithAttributes);
            var obsoleteAttr = productType.GetCustomAttribute<ObsoleteAttribute>();
            
            Assert.NotNull(obsoleteAttr);
            Assert.Contains("过时", obsoleteAttr!.Message);
            Assert.False(obsoleteAttr.IsError); // 应该是警告，不是错误
        }
        
        [Fact]
        public void TestReflectionBasics()
        {
            // 测试基本反射功能
            var productType = typeof(ProductWithAttributes);
            
            // 测试类型信息
            Assert.Equal("ProductWithAttributes", productType.Name);
            Assert.True(productType.IsClass);
            Assert.False(productType.IsInterface);
            
            // 测试属性信息
            var properties = productType.GetProperties();
            Assert.True(properties.Length >= 4); // Id, Name, Price, InternalCode
            
            var nameProperty = productType.GetProperty("Name");
            Assert.NotNull(nameProperty);
            Assert.Equal(typeof(string), nameProperty!.PropertyType);
        }
        
        [Fact]
        public void TestDynamicObjectCreation()
        {
            // 测试动态创建对象
            var productType = typeof(ProductWithAttributes);
            var instance = Activator.CreateInstance(productType);
            
            Assert.NotNull(instance);
            Assert.IsType<ProductWithAttributes>(instance);
            
            // 测试动态设置属性
            var nameProperty = productType.GetProperty("Name")!;
            nameProperty.SetValue(instance, "测试产品");
            
            var nameValue = nameProperty.GetValue(instance);
            Assert.Equal("测试产品", nameValue);
        }
        
        [Fact]
        public void TestAnonymousTypes()
        {
            // 测试匿名类型
            var person = new { Name = "张三", Age = 25, City = "北京" };
            
            Assert.Equal("张三", person.Name);
            Assert.Equal(25, person.Age);
            Assert.Equal("北京", person.City);
            
            // 匿名类型是只读的
            var personType = person.GetType();
            var nameProperty = personType.GetProperty("Name")!;
            Assert.False(nameProperty.CanWrite); // 匿名类型属性是只读的
        }
        
        [Fact]
        public void TestAnonymousTypesInLinq()
        {
            // 测试LINQ中的匿名类型
            var numbers = new[] { 1, 2, 3, 4, 5 };
            
            var numberInfo = numbers.Select(n => new 
            { 
                Number = n, 
                Square = n * n, 
                IsEven = n % 2 == 0 
            }).ToArray();
            
            Assert.Equal(5, numberInfo.Length);
            Assert.Equal(1, numberInfo[0].Number);
            Assert.Equal(1, numberInfo[0].Square);
            Assert.False(numberInfo[0].IsEven);
            
            Assert.Equal(4, numberInfo[3].Number);
            Assert.Equal(16, numberInfo[3].Square);
            Assert.True(numberInfo[3].IsEven);
        }
        
        [Fact]
        public void TestAnonymousMethods()
        {
            // 测试匿名方法
            Func<int, int> square = delegate(int x) { return x * x; };
            Assert.Equal(25, square(5));
            
            // 测试Lambda表达式
            Func<int, int> cube = x => x * x * x;
            Assert.Equal(125, cube(5));
            
            // 测试复杂匿名方法
            Func<string, int, string> formatter = delegate(string name, int age)
            {
                return $"姓名: {name}, 年龄: {age}";
            };
            
            string result = formatter("张三", 25);
            Assert.Equal("姓名: 张三, 年龄: 25", result);
        }
        
        [Fact]
        public void TestDynamicType()
        {
            // 测试dynamic类型
            dynamic dynamicVar = "Hello World";
            Assert.Equal("Hello World", dynamicVar);
            Assert.Equal(11, dynamicVar.Length);
            
            // 改变类型
            dynamicVar = 42;
            Assert.Equal(42, dynamicVar);
            
            // 测试动态对象
            dynamic dynamicObject = new System.Dynamic.ExpandoObject();
            dynamicObject.Name = "动态对象";
            dynamicObject.Age = 25;
            
            Assert.Equal("动态对象", dynamicObject.Name);
            Assert.Equal(25, dynamicObject.Age);
        }
        
        [Fact]
        public void TestGenericTypeReflection()
        {
            // 测试泛型类型反射
            var listType = typeof(System.Collections.Generic.List<int>);
            
            Assert.True(listType.IsGenericType);
            Assert.False(listType.IsGenericTypeDefinition);
            
            var genericArgs = listType.GetGenericArguments();
            Assert.Single(genericArgs);
            Assert.Equal(typeof(int), genericArgs[0]);
            
            // 测试泛型类型定义
            var genericListType = typeof(System.Collections.Generic.List<>);
            Assert.True(genericListType.IsGenericTypeDefinition);
        }
        
        [Fact]
        public void TestMethodReflection()
        {
            // 测试方法反射
            var productType = typeof(ProductWithAttributes);
            var toStringMethod = productType.GetMethod("ToString");
            
            Assert.NotNull(toStringMethod);
            Assert.Equal(typeof(string), toStringMethod!.ReturnType);
            
            // 创建实例并调用方法
            var instance = new ProductWithAttributes 
            { 
                Name = "测试产品", 
                Price = 100m 
            };
            
            var result = toStringMethod.Invoke(instance, null);
            Assert.NotNull(result);
            Assert.Contains("测试产品", result!.ToString());
        }
        
        [Fact]
        public void TestAttributeInheritance()
        {
            // 测试特性继承
            var documentedType = typeof(DocumentedClass);
            var docAttr = documentedType.GetCustomAttribute<CustomDocumentationAttribute>();
            
            Assert.NotNull(docAttr);
            Assert.Equal("这是一个示例类", docAttr!.Description);
            Assert.Equal("开发者", docAttr.Author);
            Assert.Equal("2.0", docAttr.Version);
            
            // 测试方法上的特性
            var method = documentedType.GetMethod("ExampleMethod");
            var methodDocAttr = method!.GetCustomAttribute<CustomDocumentationAttribute>();
            
            Assert.NotNull(methodDocAttr);
            Assert.Equal("这是一个示例方法", methodDocAttr!.Description);
        }
    }
    
    /// <summary>
    /// 对比Java/Kotlin的高级特性测试
    /// </summary>
    public class AdvancedFeaturesComparisonTests
    {
        [Fact]
        public void TestCSharpVsJavaAttributes()
        {
            // C#: 特性使用方括号语法
            // [Obsolete("message")]
            // [JsonProperty("name")]
            
            // Java: 注解使用@语法
            // @Deprecated
            // @JsonProperty("name")
            
            var productType = typeof(ProductWithAttributes);
            var obsoleteAttr = productType.GetCustomAttribute<ObsoleteAttribute>();
            
            Assert.NotNull(obsoleteAttr);
            // C#的Obsolete特性包含消息和是否为错误的信息
            Assert.NotNull(obsoleteAttr!.Message);
            Assert.False(obsoleteAttr.IsError);
        }
        
        [Fact]
        public void TestCSharpVsKotlinAnonymousTypes()
        {
            // C#: 匿名类型
            var csharpAnonymous = new { Name = "张三", Age = 25 };
            
            // Kotlin: 匿名对象
            // val kotlinAnonymous = object {
            //     val name = "张三"
            //     val age = 25
            // }
            
            Assert.Equal("张三", csharpAnonymous.Name);
            Assert.Equal(25, csharpAnonymous.Age);
            
            // C#匿名类型的属性是只读的
            var type = csharpAnonymous.GetType();
            var nameProperty = type.GetProperty("Name")!;
            Assert.False(nameProperty.CanWrite);
        }
        
        [Fact]
        public void TestCSharpVsJavaReflection()
        {
            // C#: typeof(Type).GetProperty("Name")
            // Java: Class.forName("Type").getDeclaredField("name")
            
            var productType = typeof(ProductWithAttributes);
            var nameProperty = productType.GetProperty("Name");
            
            Assert.NotNull(nameProperty);
            Assert.Equal("Name", nameProperty!.Name);
            Assert.Equal(typeof(string), nameProperty.PropertyType);
            
            // C#反射更直观，Java需要处理字段和方法分别
        }
    }
}
