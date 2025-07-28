using ConsoleApp1.Examples;

namespace ConsoleApp1;

/// <summary>
/// C# .NET 9 学习示例主程序
/// 为移动开发者（Java/Kotlin背景）设计的C#学习路径
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🎯 欢迎来到C# .NET 9学习之旅！");
        Console.WriteLine("📱 专为移动开发者（Java/Kotlin背景）设计");
        Console.WriteLine(new string('=', 60));

        try
        {
            // 显示菜单
            ShowMenu();

            while (true)
            {
                Console.Write("\n请选择要运行的示例 (输入数字，0退出): ");
                string? input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("👋 感谢使用C#学习示例！");
                    break;
                }

                await RunSelectedExample(input);

                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("按任意键继续...");
                Console.ReadKey();
                Console.Clear();
                ShowMenu();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 程序运行出错: {ex.Message}");
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("\n📚 C#学习示例菜单:");
        Console.WriteLine("1️⃣  基础语法 - 变量、方法、控制流");
        Console.WriteLine("2️⃣  面向对象编程 - 类、继承、多态、接口");
        Console.WriteLine("3️⃣  集合和泛型 - List、Dictionary、泛型类");
        Console.WriteLine("4️⃣  异常处理 - try-catch、自定义异常");
        Console.WriteLine("5️⃣  C#独有特性 - 属性、委托、事件、扩展方法");
        Console.WriteLine("6️⃣  LINQ和函数式编程 - 查询语法、方法链");
        Console.WriteLine("7️⃣  异步编程 - async/await、Task、并行处理");
        Console.WriteLine("8️⃣  现代C#特性 - 记录类型、模式匹配、可空引用");
        Console.WriteLine("9️⃣  高级特性 - 特性、反射、匿名类型、动态类型");
        Console.WriteLine("🔟  运行所有示例");
        Console.WriteLine("0️⃣  退出程序");
    }

    private static async Task RunSelectedExample(string? input)
    {
        try
        {
            switch (input)
            {
                case "1":
                    BasicSyntax.RunExamples();
                    break;
                case "2":
                    ObjectOriented.RunExamples();
                    break;
                case "3":
                    CollectionsAndGenerics.RunExamples();
                    break;
                case "4":
                    ExceptionHandling.RunExamples();
                    break;
                case "5":
                    CSharpUniqueFeatures.RunExamples();
                    break;
                case "6":
                    LinqAndFunctional.RunExamples();
                    break;
                case "7":
                    await AsyncProgramming.RunExamples();
                    break;
                case "8":
                    ModernCSharpFeatures.RunExamples();
                    break;
                case "9":
                    AdvancedFeatures.RunExamples();
                    break;
                case "10":
                    await RunAllExamples();
                    break;
                default:
                    Console.WriteLine("❌ 无效选择，请输入0-10之间的数字");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 运行示例时出错: {ex.Message}");
        }
    }

    private static async Task RunAllExamples()
    {
        Console.WriteLine("🚀 运行所有示例...\n");

        BasicSyntax.RunExamples();
        ObjectOriented.RunExamples();
        CollectionsAndGenerics.RunExamples();
        ExceptionHandling.RunExamples();
        CSharpUniqueFeatures.RunExamples();
        LinqAndFunctional.RunExamples();
        await AsyncProgramming.RunExamples();
        ModernCSharpFeatures.RunExamples();
        AdvancedFeatures.RunExamples();

        Console.WriteLine("✅ 所有示例运行完成！");
    }
}