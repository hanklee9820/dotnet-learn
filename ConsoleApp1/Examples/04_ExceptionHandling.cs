using System;
using System.IO;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#异常处理示例
    /// 对比Java/Kotlin语法差异
    /// </summary>
    public class ExceptionHandling
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 异常处理示例 ===\n");
            
            // 1. 基本异常处理
            BasicExceptionHandling();
            
            // 2. 多种异常类型处理
            MultipleExceptionTypes();
            
            // 3. 自定义异常
            CustomExceptions();
            
            // 4. finally块和using语句
            FinallyAndUsing();
            
            // 5. 异常过滤器
            ExceptionFilters();
        }
        
        private static void BasicExceptionHandling()
        {
            Console.WriteLine("--- 基本异常处理 ---");
            
            try
            {
                // 可能抛出异常的代码
                int result = DivideNumbers(10, 0);
                Console.WriteLine($"结果: {result}");
            }
            catch (DivideByZeroException ex)
            {
                // C#: 捕获特定异常类型
                Console.WriteLine($"除零错误: {ex.Message}");
                
                // Java对比:
                // catch (ArithmeticException e) {
                //     System.out.println("除零错误: " + e.getMessage());
                // }
                
                // Kotlin对比:
                // catch (e: ArithmeticException) {
                //     println("除零错误: ${e.message}")
                // }
            }
            catch (Exception ex)
            {
                // 捕获所有其他异常
                Console.WriteLine($"其他错误: {ex.Message}");
            }
            finally
            {
                // 无论是否发生异常都会执行
                Console.WriteLine("清理资源");
            }
            
            Console.WriteLine();
        }
        
        private static void MultipleExceptionTypes()
        {
            Console.WriteLine("--- 多种异常类型处理 ---");
            
            string[] testInputs = { "10", "abc", null, "0" };
            
            foreach (string input in testInputs)
            {
                try
                {
                    int number = ParseAndProcess(input);
                    Console.WriteLine($"处理结果: {number}");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"空参数错误: {ex.ParamName}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"格式错误: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"参数错误: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"未知错误: {ex.GetType().Name} - {ex.Message}");
                }
            }
            
            Console.WriteLine();
        }
        
        private static void CustomExceptions()
        {
            Console.WriteLine("--- 自定义异常 ---");
            
            try
            {
                var account = new BankAccount(1000);
                account.Withdraw(1500); // 这会抛出自定义异常
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"余额不足: {ex.Message}");
                Console.WriteLine($"当前余额: {ex.CurrentBalance:C}");
                Console.WriteLine($"尝试提取: {ex.AttemptedAmount:C}");
            }
            
            Console.WriteLine();
        }
        
        private static void FinallyAndUsing()
        {
            Console.WriteLine("--- finally块和using语句 ---");
            
            // 传统的资源管理方式
            FileStream? fileStream = null;
            try
            {
                fileStream = new FileStream("test.txt", FileMode.Create);
                // 使用文件流...
                Console.WriteLine("文件操作完成");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"文件操作错误: {ex.Message}");
            }
            finally
            {
                // 确保资源被释放
                fileStream?.Dispose();
                Console.WriteLine("文件流已关闭");
            }
            
            // C#: using语句 - 自动资源管理
            try
            {
                using (var fs = new FileStream("test2.txt", FileMode.Create))
                {
                    // 使用文件流...
                    Console.WriteLine("使用using语句的文件操作");
                } // 自动调用Dispose()
                
                // C# 8.0+: using声明
                using var fs2 = new FileStream("test3.txt", FileMode.Create);
                Console.WriteLine("使用using声明的文件操作");
                // 在方法结束时自动调用Dispose()
            }
            catch (IOException ex)
            {
                Console.WriteLine($"文件操作错误: {ex.Message}");
            }
            
            // Java对比: try-with-resources
            // try (FileInputStream fis = new FileInputStream("test.txt")) {
            //     // 使用文件流...
            // } catch (IOException e) {
            //     // 处理异常
            // }
            
            // Kotlin对比: use函数
            // try {
            //     FileInputStream("test.txt").use { fis ->
            //         // 使用文件流...
            //     }
            // } catch (e: IOException) {
            //     // 处理异常
            // }
            
            Console.WriteLine();
        }
        
        private static void ExceptionFilters()
        {
            Console.WriteLine("--- 异常过滤器 (C#独有特性) ---");
            
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (i == 0) throw new ArgumentException("第一个错误");
                    if (i == 1) throw new ArgumentException("第二个错误");
                    if (i == 2) throw new InvalidOperationException("操作错误");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("第一个"))
                {
                    // C#: when关键字进行异常过滤
                    Console.WriteLine($"处理第一个参数错误: {ex.Message}");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("第二个"))
                {
                    Console.WriteLine($"处理第二个参数错误: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"处理其他错误: {ex.Message}");
                }
            }
            
            // Java/Kotlin对比: 没有异常过滤器，需要在catch块内使用if语句
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// 可能抛出异常的方法
        /// </summary>
        private static int DivideNumbers(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("除数不能为零");
            }
            return a / b;
        }
        
        /// <summary>
        /// 解析和处理输入的方法
        /// </summary>
        private static int ParseAndProcess(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "输入不能为null");
            }
            
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("输入不能为空或空白字符", nameof(input));
            }
            
            // 这可能抛出FormatException
            int number = int.Parse(input);
            
            if (number < 0)
            {
                throw new ArgumentException("数字不能为负数", nameof(input));
            }
            
            return number * 2;
        }
    }
    
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public class InsufficientFundsException : Exception
    {
        public decimal CurrentBalance { get; }
        public decimal AttemptedAmount { get; }
        
        public InsufficientFundsException(decimal currentBalance, decimal attemptedAmount)
            : base($"余额不足。当前余额: {currentBalance:C}, 尝试提取: {attemptedAmount:C}")
        {
            CurrentBalance = currentBalance;
            AttemptedAmount = attemptedAmount;
        }
        
        public InsufficientFundsException(string message) : base(message) { }
        
        public InsufficientFundsException(string message, Exception innerException) 
            : base(message, innerException) { }
        
        // Java对比:
        // public class InsufficientFundsException extends Exception {
        //     private final BigDecimal currentBalance;
        //     private final BigDecimal attemptedAmount;
        //     
        //     public InsufficientFundsException(BigDecimal currentBalance, BigDecimal attemptedAmount) {
        //         super(String.format("余额不足。当前余额: %s, 尝试提取: %s", currentBalance, attemptedAmount));
        //         this.currentBalance = currentBalance;
        //         this.attemptedAmount = attemptedAmount;
        //     }
        // }
        
        // Kotlin对比:
        // class InsufficientFundsException(
        //     val currentBalance: BigDecimal,
        //     val attemptedAmount: BigDecimal
        // ) : Exception("余额不足。当前余额: $currentBalance, 尝试提取: $attemptedAmount")
    }
    
    /// <summary>
    /// 银行账户类 - 用于演示自定义异常
    /// </summary>
    public class BankAccount
    {
        private decimal _balance;
        
        public decimal Balance => _balance;
        
        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("初始余额不能为负数", nameof(initialBalance));
            }
            _balance = initialBalance;
        }
        
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("提取金额必须大于零", nameof(amount));
            }
            
            if (amount > _balance)
            {
                throw new InsufficientFundsException(_balance, amount);
            }
            
            _balance -= amount;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("存款金额必须大于零", nameof(amount));
            }
            
            _balance += amount;
        }
    }
}
