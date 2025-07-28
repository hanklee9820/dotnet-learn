using System;

namespace ConsoleApp1.Examples
{
    /// <summary>
    /// C#面向对象编程示例 - 类、继承、多态、接口
    /// 对比Java/Kotlin语法差异
    /// </summary>
    public class ObjectOriented
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== C# 面向对象编程示例 ===\n");
            
            // 1. 基本类使用
            ClassBasics();
            
            // 2. 继承和多态
            InheritanceAndPolymorphism();
            
            // 3. 接口使用
            InterfaceUsage();
            
            // 4. 抽象类
            AbstractClassUsage();
        }
        
        private static void ClassBasics()
        {
            Console.WriteLine("--- 基本类使用 ---");
            
            // 创建对象实例
            var person = new Person("张三", 25);
            Console.WriteLine(person.GetInfo());
            
            // 使用属性
            person.Name = "李四";
            person.Age = 30;
            Console.WriteLine($"修改后: {person.GetInfo()}");
            
            // 静态方法调用
            Console.WriteLine($"总人数: {Person.GetTotalCount()}\n");
        }
        
        private static void InheritanceAndPolymorphism()
        {
            Console.WriteLine("--- 继承和多态 ---");
            
            // 多态示例
            Animal[] animals = {
                new Dog("旺财"),
                new Cat("咪咪"),
                new Bird("小鸟")
            };
            
            foreach (Animal animal in animals)
            {
                animal.MakeSound(); // 多态调用
                animal.Move();      // 虚方法调用
                
                // 类型检查和转换
                if (animal is Dog dog)
                {
                    dog.Fetch();
                }
                else if (animal is Cat cat)
                {
                    cat.Climb();
                }
            }
            Console.WriteLine();
        }
        
        private static void InterfaceUsage()
        {
            Console.WriteLine("--- 接口使用 ---");
            
            // 接口实现
            IVehicle car = new Car("丰田", 4);
            IVehicle bike = new Bicycle("捷安特", 2);
            
            IVehicle[] vehicles = { car, bike };
            
            foreach (IVehicle vehicle in vehicles)
            {
                vehicle.Start();
                vehicle.Stop();
                Console.WriteLine($"轮子数量: {vehicle.WheelCount}");
            }
            Console.WriteLine();
        }
        
        private static void AbstractClassUsage()
        {
            Console.WriteLine("--- 抽象类使用 ---");
            
            Shape[] shapes = {
                new Circle(5),
                new Rectangle(4, 6)
            };
            
            foreach (Shape shape in shapes)
            {
                Console.WriteLine($"面积: {shape.CalculateArea():F2}");
                shape.Draw();
            }
            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// 基本类示例 - Person类
    /// </summary>
    public class Person
    {
        // C#: 私有字段（backing field）
        private string _name;
        private int _age;
        private static int _totalCount = 0;
        
        // C#: 构造函数
        public Person(string name, int age)
        {
            // Java对比:
            // public Person(String name, int age) {
            //     this.name = name;
            //     this.age = age;
            //     totalCount++;
            // }
            
            // Kotlin对比:
            // class Person(var name: String, var age: Int) {
            //     init {
            //         totalCount++
            //     }
            // }
            
            _name = name;
            _age = age;
            _totalCount++;
        }
        
        // C#: 属性（Properties） - C#独有特性
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        // C#: 自动属性（Auto-implemented Properties）
        public int Age { get; set; }
        
        // Java对比: 需要显式的getter/setter方法
        // public String getName() { return name; }
        // public void setName(String name) { this.name = name; }
        // public int getAge() { return age; }
        // public void setAge(int age) { this.age = age; }
        
        // Kotlin对比: 自动生成getter/setter
        // var name: String = name
        // var age: Int = age
        
        // C#: 只读属性
        public string FullInfo => $"{Name} ({Age}岁)";
        
        // C#: 静态方法
        public static int GetTotalCount()
        {
            return _totalCount;
        }
        
        // C#: 实例方法
        public string GetInfo()
        {
            return $"姓名: {Name}, 年龄: {Age}";
        }
        
        // C#: 方法重写ToString
        public override string ToString()
        {
            return GetInfo();
        }
    }
    
    /// <summary>
    /// 抽象基类 - Animal
    /// </summary>
    public abstract class Animal
    {
        protected string Name { get; set; }
        
        protected Animal(string name)
        {
            Name = name;
        }
        
        // C#: 抽象方法 - 必须在派生类中实现
        public abstract void MakeSound();
        
        // C#: 虚方法 - 可以在派生类中重写
        public virtual void Move()
        {
            Console.WriteLine($"{Name} 正在移动");
        }
        
        // Java对比:
        // public abstract void makeSound();
        // public void move() { ... } // 普通方法，可以被重写
        
        // Kotlin对比:
        // abstract fun makeSound()
        // open fun move() { ... } // open关键字表示可以被重写
    }
    
    /// <summary>
    /// 派生类 - Dog
    /// </summary>
    public class Dog : Animal
    {
        public Dog(string name) : base(name) { }
        
        // C#: 重写抽象方法
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} 汪汪叫");
        }
        
        // C#: 重写虚方法
        public override void Move()
        {
            Console.WriteLine($"{Name} 跑来跑去");
        }
        
        // 特有方法
        public void Fetch()
        {
            Console.WriteLine($"{Name} 去捡球");
        }
    }
    
    /// <summary>
    /// 派生类 - Cat
    /// </summary>
    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} 喵喵叫");
        }
        
        public void Climb()
        {
            Console.WriteLine($"{Name} 爬树");
        }
    }
    
    /// <summary>
    /// 派生类 - Bird
    /// </summary>
    public class Bird : Animal
    {
        public Bird(string name) : base(name) { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} 叽叽喳喳");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} 飞翔");
        }
    }
    
    /// <summary>
    /// 接口定义 - IVehicle
    /// </summary>
    public interface IVehicle
    {
        // C#: 接口属性
        string Brand { get; }
        int WheelCount { get; }
        
        // C#: 接口方法
        void Start();
        void Stop();
        
        // C#: 默认接口方法 (C# 8.0+)
        void Honk()
        {
            Console.WriteLine("嘟嘟！");
        }
        
        // Java对比:
        // String getBrand();
        // int getWheelCount();
        // void start();
        // void stop();
        // default void honk() { System.out.println("嘟嘟！"); }
        
        // Kotlin对比:
        // val brand: String
        // val wheelCount: Int
        // fun start()
        // fun stop()
        // fun honk() { println("嘟嘟！") }
    }
    
    /// <summary>
    /// 接口实现 - Car
    /// </summary>
    public class Car : IVehicle
    {
        public string Brand { get; }
        public int WheelCount { get; }
        
        public Car(string brand, int wheelCount)
        {
            Brand = brand;
            WheelCount = wheelCount;
        }
        
        public void Start()
        {
            Console.WriteLine($"{Brand} 汽车启动");
        }
        
        public void Stop()
        {
            Console.WriteLine($"{Brand} 汽车停止");
        }
    }
    
    /// <summary>
    /// 接口实现 - Bicycle
    /// </summary>
    public class Bicycle : IVehicle
    {
        public string Brand { get; }
        public int WheelCount { get; }
        
        public Bicycle(string brand, int wheelCount)
        {
            Brand = brand;
            WheelCount = wheelCount;
        }
        
        public void Start()
        {
            Console.WriteLine($"{Brand} 自行车开始骑行");
        }
        
        public void Stop()
        {
            Console.WriteLine($"{Brand} 自行车停止");
        }
    }
    
    /// <summary>
    /// 抽象类示例 - Shape
    /// </summary>
    public abstract class Shape
    {
        public abstract double CalculateArea();
        
        public virtual void Draw()
        {
            Console.WriteLine("绘制形状");
        }
    }
    
    /// <summary>
    /// 具体形状 - Circle
    /// </summary>
    public class Circle : Shape
    {
        private double _radius;
        
        public Circle(double radius)
        {
            _radius = radius;
        }
        
        public override double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }
        
        public override void Draw()
        {
            Console.WriteLine($"绘制半径为 {_radius} 的圆形");
        }
    }
    
    /// <summary>
    /// 具体形状 - Rectangle
    /// </summary>
    public class Rectangle : Shape
    {
        private double _width;
        private double _height;
        
        public Rectangle(double width, double height)
        {
            _width = width;
            _height = height;
        }
        
        public override double CalculateArea()
        {
            return _width * _height;
        }
        
        public override void Draw()
        {
            Console.WriteLine($"绘制 {_width}x{_height} 的矩形");
        }
    }
}
