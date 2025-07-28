using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#异步编程示例 - async/await模式
    /// 对比Java/Kotlin的异步处理方式
    /// </summary>
    public class AsyncProgramming
    {
        private static readonly HttpClient httpClient = new HttpClient();
        
        public static async Task RunExamples()
        {
            Console.WriteLine("=== C# 异步编程示例 ===\n");
            
            // 1. 基本async/await用法
            await BasicAsyncAwait();
            
            // 2. 并行执行多个异步操作
            await ParallelAsyncOperations();
            
            // 3. 异步文件操作
            await AsyncFileOperations();
            
            // 4. 异步HTTP请求
            await AsyncHttpRequests();
            
            // 5. 取消令牌 (CancellationToken)
            await CancellationTokenExample();
            
            // 6. 异步流 (IAsyncEnumerable)
            await AsyncStreams();
        }
        
        private static async Task BasicAsyncAwait()
        {
            Console.WriteLine("--- 基本async/await用法 ---");
            
            Console.WriteLine("开始异步操作...");
            
            // 异步方法调用
            string result = await LongRunningOperationAsync("任务1");
            Console.WriteLine($"结果: {result}");
            
            // 多个异步操作按顺序执行
            Console.WriteLine("执行多个异步操作:");
            string result1 = await LongRunningOperationAsync("任务A");
            string result2 = await LongRunningOperationAsync("任务B");
            Console.WriteLine($"结果1: {result1}");
            Console.WriteLine($"结果2: {result2}");
            
            // Java对比: CompletableFuture
            // CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
            //     // 长时间运行的操作
            //     return "结果";
            // });
            // String result = future.get(); // 阻塞等待
            
            // Kotlin对比: suspend函数和协程
            // suspend fun longRunningOperation(): String {
            //     delay(1000)
            //     return "结果"
            // }
            // val result = longRunningOperation()
            
            Console.WriteLine();
        }
        
        private static async Task ParallelAsyncOperations()
        {
            Console.WriteLine("--- 并行执行多个异步操作 ---");
            
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // 方式1: Task.WhenAll - 等待所有任务完成
            Console.WriteLine("使用Task.WhenAll并行执行:");
            Task<string> task1 = LongRunningOperationAsync("并行任务1");
            Task<string> task2 = LongRunningOperationAsync("并行任务2");
            Task<string> task3 = LongRunningOperationAsync("并行任务3");
            
            string[] results = await Task.WhenAll(task1, task2, task3);
            
            stopwatch.Stop();
            Console.WriteLine($"所有任务完成，耗时: {stopwatch.ElapsedMilliseconds}ms");
            foreach (string result in results)
            {
                Console.WriteLine($"  {result}");
            }
            
            // 方式2: Task.WhenAny - 等待任何一个任务完成
            Console.WriteLine("\n使用Task.WhenAny等待第一个完成的任务:");
            stopwatch.Restart();
            
            Task<string> fastTask = QuickOperationAsync("快速任务");
            Task<string> slowTask = LongRunningOperationAsync("慢速任务");
            
            Task<string> completedTask = await Task.WhenAny(fastTask, slowTask);
            string firstResult = await completedTask;
            
            stopwatch.Stop();
            Console.WriteLine($"第一个完成的任务结果: {firstResult}，耗时: {stopwatch.ElapsedMilliseconds}ms");
            
            // 等待剩余任务完成（可选）
            if (completedTask == fastTask)
            {
                string slowResult = await slowTask;
                Console.WriteLine($"慢速任务也完成了: {slowResult}");
            }
            
            Console.WriteLine();
        }
        
        private static async Task AsyncFileOperations()
        {
            Console.WriteLine("--- 异步文件操作 ---");
            
            string fileName = "async_test.txt";
            string content = "这是异步写入的内容\n第二行内容\n第三行内容";
            
            try
            {
                // 异步写入文件
                Console.WriteLine("异步写入文件...");
                await File.WriteAllTextAsync(fileName, content);
                Console.WriteLine("文件写入完成");
                
                // 异步读取文件
                Console.WriteLine("异步读取文件...");
                string readContent = await File.ReadAllTextAsync(fileName);
                Console.WriteLine($"读取的内容:\n{readContent}");
                
                // 异步读取所有行
                string[] lines = await File.ReadAllLinesAsync(fileName);
                Console.WriteLine($"文件共有 {lines.Length} 行");
                
                // 清理文件
                File.Delete(fileName);
                Console.WriteLine("测试文件已删除");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"文件操作错误: {ex.Message}");
            }
            
            Console.WriteLine();
        }
        
        private static async Task AsyncHttpRequests()
        {
            Console.WriteLine("--- 异步HTTP请求 ---");
            
            try
            {
                // 单个HTTP请求
                Console.WriteLine("发送HTTP请求...");
                string url = "https://api.github.com/users/octocat";
                string response = await httpClient.GetStringAsync(url);
                Console.WriteLine($"响应长度: {response.Length} 字符");
                
                // 并行发送多个HTTP请求
                Console.WriteLine("\n并行发送多个HTTP请求:");
                var urls = new[]
                {
                    "https://httpbin.org/delay/1",
                    "https://httpbin.org/delay/2",
                    "https://httpbin.org/delay/1"
                };
                
                var tasks = new List<Task<string>>();
                foreach (string requestUrl in urls)
                {
                    tasks.Add(FetchUrlAsync(requestUrl));
                }
                
                var responses = await Task.WhenAll(tasks);
                for (int i = 0; i < responses.Length; i++)
                {
                    Console.WriteLine($"  请求{i + 1}响应长度: {responses[i].Length} 字符");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP请求错误: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"其他错误: {ex.Message}");
            }
            
            Console.WriteLine();
        }
        
        private static async Task CancellationTokenExample()
        {
            Console.WriteLine("--- 取消令牌示例 ---");
            
            // 创建取消令牌源
            using var cts = new CancellationTokenSource();
            
            // 设置5秒后自动取消
            cts.CancelAfter(TimeSpan.FromSeconds(5));
            
            try
            {
                Console.WriteLine("开始长时间运行的任务（5秒后自动取消）...");
                string result = await LongRunningCancellableOperationAsync("可取消任务", cts.Token);
                Console.WriteLine($"任务完成: {result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务被取消了");
            }
            
            // 手动取消示例
            using var cts2 = new CancellationTokenSource();
            
            // 启动任务
            var task = LongRunningCancellableOperationAsync("手动取消任务", cts2.Token);
            
            // 模拟用户在2秒后取消
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("用户请求取消任务...");
                cts2.Cancel();
            });
            
            try
            {
                string result = await task;
                Console.WriteLine($"任务完成: {result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务被用户取消了");
            }
            
            Console.WriteLine();
        }
        
        private static async Task AsyncStreams()
        {
            Console.WriteLine("--- 异步流 (IAsyncEnumerable) ---");
            
            Console.WriteLine("处理异步数据流:");
            
            // C# 8.0+: 异步流
            await foreach (int number in GenerateNumbersAsync())
            {
                Console.WriteLine($"  接收到数字: {number}");
            }
            
            Console.WriteLine("异步流处理完成");
            
            // 使用LINQ处理异步流 (需要System.Linq.Async包)
            Console.WriteLine("\n使用异步流进行数据处理:");
            await foreach (int evenNumber in GenerateNumbersAsync().Where(n => n % 2 == 0))
            {
                Console.WriteLine($"  偶数: {evenNumber}");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// 模拟长时间运行的异步操作
        /// </summary>
        private static async Task<string> LongRunningOperationAsync(string taskName)
        {
            Console.WriteLine($"  开始执行: {taskName}");
            
            // 模拟异步工作
            await Task.Delay(2000); // 等待2秒
            
            Console.WriteLine($"  完成执行: {taskName}");
            return $"{taskName} 完成于 {DateTime.Now:HH:mm:ss}";
        }
        
        /// <summary>
        /// 快速异步操作
        /// </summary>
        private static async Task<string> QuickOperationAsync(string taskName)
        {
            Console.WriteLine($"  开始快速任务: {taskName}");
            await Task.Delay(500); // 等待0.5秒
            Console.WriteLine($"  快速任务完成: {taskName}");
            return $"{taskName} 快速完成";
        }
        
        /// <summary>
        /// 可取消的长时间运行操作
        /// </summary>
        private static async Task<string> LongRunningCancellableOperationAsync(string taskName, CancellationToken cancellationToken)
        {
            Console.WriteLine($"  开始可取消任务: {taskName}");
            
            for (int i = 0; i < 10; i++)
            {
                // 检查取消请求
                cancellationToken.ThrowIfCancellationRequested();
                
                await Task.Delay(1000, cancellationToken); // 每秒检查一次
                Console.WriteLine($"    {taskName} 进度: {(i + 1) * 10}%");
            }
            
            return $"{taskName} 完全完成";
        }
        
        /// <summary>
        /// 异步HTTP请求
        /// </summary>
        private static async Task<string> FetchUrlAsync(string url)
        {
            try
            {
                var response = await httpClient.GetStringAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                return $"错误: {ex.Message}";
            }
        }
        
        /// <summary>
        /// 异步流生成器
        /// </summary>
        private static async IAsyncEnumerable<int> GenerateNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                // 模拟异步数据获取
                await Task.Delay(1000);
                yield return i;
            }
        }
    }
    
    /// <summary>
    /// 异步流的扩展方法（简化版）
    /// </summary>
    public static class AsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<T> Where<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
        {
            await foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
