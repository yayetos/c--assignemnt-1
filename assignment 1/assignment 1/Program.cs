using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Supplier;

namespace assignment_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}


//1 - Create IVehicle interface with two methods, one for Drive of type void
//and another for Refuel of type bool that has a parameter of type integer
//with the amount of gasoline to refuel.
//2- Then create a Car class with a constructor that receives a parameter
//with the car's starting gasoline amount and implements the Drive and
//Refuel methods of the car.
//3- The Drive method will print on the screen that the car is Driving, if the
//gasoline is greater than 0.
//4- The Refuel method will increase the gasoline of the car and return true.

public interface IVehicle
{
    void Drive();
    bool Refuel(int gasAmount);
}

public class Car : IVehicle
{
    private int _gasolineAmount;

    public Car(int startingGasoline)
    {
        _gasolineAmount = startingGasoline;
    }

    public void Drive()
    {
        if (_gasolineAmount > 0)
        {
            Console.WriteLine("The car is Driving.");
            _gasolineAmount--;
        }
        else
        {
            Console.WriteLine("The car has run out of gasoline and cannot be driven.");
        }
    }

    public bool Refuel(int gasAmount)
    {
        _gasolineAmount += gasAmount;
        return true;
    }
}



//5 - Create IDrivable interface that has 3 methods, one for Move of type void
//6-Make class Car implement IDrivable.
//a.Can class Car implement more than one Interface?
//b. Try implementing the interfaces explicitly and implicitly.

public interface IDrivable
{
    void Move();
    void Accelerate();
    void Drive();
}

public class Car : IVehicle, IDrivable
{
    private int _gasolineAmount;

    public Car(int startingGasoline)
    {
        _gasolineAmount = startingGasoline;
    }

    public void Drive()
    {
        if (_gasolineAmount > 0)
        {
            Console.WriteLine("The car is Driving.");
            _gasolineAmount--;
        }
        else
        {
            Console.WriteLine("The car has run out of gasoline and cannot be driven.");
        }
    }

    public bool Refuel(int gasAmount)
    {
        _gasolineAmount += gasAmount;
        return true;
    }

    void IDrivable.Move()
    {
        Console.WriteLine("The car is moving.");
    }

    void IDrivable.Accelerate()
    {
        Console.WriteLine("The car is accelerating.");
    }

    void IDrivable.Drive()
    {
        Drive();
    }
}


//7 - Create an interface called IStack with methods Push(), Pop().
//8- Create a class that implements this interface. Use an array to implement
//stack data structure.
//9- Create user-defined exceptions and ensure Push() and Pop() methods
//throw those exceptions when required.

// Interface IStack
public interface IStack<T>
{
    void Push(T item);
    T Pop();
}

// Custom Exceptions
public class StackOverflowException : Exception
{
    public StackOverflowException(string message) : base(message)
    {
    }
}

public class StackUnderflowException : Exception
{
    public StackUnderflowException(string message) : base(message)
    {
    }
}

// Stack implementation
public class ArrayStack<T> : IStack<T>
{
    private T[] _items;
    private int _top;
    private int _capacity;

    public ArrayStack(int capacity)
    {
        _capacity = capacity;
        _items = new T[capacity];
        _top = -1;
    }

    public void Push(T item)
    {
        if (_top == _capacity - 1)
        {
            throw new StackOverflowException("Stack is full. Cannot push more elements.");
        }

        _items[++_top] = item;
    }

    public T Pop()
    {
        if (_top == -1)
        {
            throw new StackUnderflowException("Stack is empty. Cannot pop elements.");
        }

        return _items[_top--];
    }
}

// Example usage
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            IStack<int> stack = new ArrayStack<int>(5);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Console.WriteLine(stack.Pop()); // Output: 5
            Console.WriteLine(stack.Pop()); // Output: 4
            Console.WriteLine(stack.Pop()); // Output: 3
            Console.WriteLine(stack.Pop()); // Output: 2
            Console.WriteLine(stack.Pop()); // Output: 1

            stack.Push(6); // Will throw StackOverflowException
        }
        catch (StackOverflowException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (StackUnderflowException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

//10 - -Design Student Class that contain Info (ID, Name)
//a. Create array of student of size 5
//b. Fill array with 5 student object
//c. Use Array.Sort() Function to sort Array
//d. Add new object in student array(item number 6)
//e.Handel Exception that appear when adding the last item

using System;

// Student Class
public class Student : IComparable<Student>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int CompareTo(Student other)
    {
        return Id.CompareTo(other.Id);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // a. Create array of student of size 5
        Student[] students = new Student[5];

        // b. Fill array with 5 student object
        students[0] = new Student(3, "John");
        students[1] = new Student(1, "Alice");
        students[2] = new Student(4, "Bob");
        students[3] = new Student(2, "Eve");
        students[4] = new Student(5, "Charlie");

        // c. Use Array.Sort() Function to sort Array
        Array.Sort(students);

        // Display the sorted array
        Console.WriteLine("Sorted Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
        }

        try
        {
            // d. Add new object in student array (item number 6)
            Array.Resize(ref students, 6);
            students[5] = new Student(6, "David");

            // Display the updated array
            Console.WriteLine("\nUpdated Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            // e. Handle Exception that appear when adding the last item
            Console.WriteLine(ex.Message);
        }
    }
}


//11 - Desgin GList Class that work as list in C# with add function (class
//accept any type and according type function detect type)

using System;
using System.Collections.Generic;

public class GList<T>
{
    private List<T> _items;

    public GList()
    {
        _items = new List<T>();
    }

    public void Add(T item)
    {
        _items.Add(item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    public T GetItem(int index)
    {
        return _items[index];
    }

    public int Count
    {
        get { return _items.Count; }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create a GList of integers
        GList<int> intList = new GList<int>();
        intList.Add(1);
        intList.Add(2);
        intList.Add(3);
        Console.WriteLine("Integer List:");
        for (int i = 0; i < intList.Count; i++)
        {
            Console.WriteLine(intList.GetItem(i));
        }

        // Create a GList of strings
        GList<string> stringList = new GList<string>();
        stringList.Add("apple");
        stringList.Add("banana");
        stringList.Add("cherry");
        Console.WriteLine("\nString List:");
        for (int i = 0; i < stringList.Count; i++)
        {
            Console.WriteLine(stringList.GetItem(i));
        }

        // Create a GList of custom objects
        GList<Person> personList = new GList<Person>();
        personList.Add(new Person { Name = "John", Age = 30 });
        personList.Add(new Person { Name = "Jane", Age = 25 });
        personList.Add(new Person { Name = "Bob", Age = 35 });
        Console.WriteLine("\nPerson List:");
        for (int i = 0; i < personList.Count; i++)
        {
            Console.WriteLine($"Name: {personList.GetItem(i).Name}, Age: {personList.GetItem(i).Age}");
        }
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

/*12 - Create a class Employee that has the following properties:
a.Id.
b.Name.
c.Salary.
d.Gender.e.Experience.
13 - Make a function Promote that return a list of promoted
Employees according to PromotionCriteria Function.
14- PromotionCriteria is a function with the following signature
15- Public Static bool PromotionCriteria(Employee emp)
16- That returns whether the emp should be promoted or not.
17- Create 2 Promotion Criteria:
a.One if the salary is under 10000.
b.One if the experience years are above 5.12- Create a class Employee that has the following properties:
a.Id.
b.Name.
c.Salary.
d.Gender.e.Experience.
13 - Make a function Promote that return a list of promoted
Employees according to PromotionCriteria Function.
14- PromotionCriteria is a function with the following signature
15- Public Static bool PromotionCriteria(Employee emp)
16- That returns whether the emp should be promoted or not.
17- Create 2 Promotion Criteria:
a.One if the salary is under 10000.
b.One if the experience years are above 5.*/

using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public Gender Gender { get; set; }
    public int Experience { get; set; }
}

public enum Gender
{
    Male,
    Female
}

public class Program
{
    public static List<Employee> Promote(List<Employee> employees)
    {
        return employees.Where(PromotionCriteria).ToList();
    }

    public static bool PromotionCriteria(Employee employee)
    {
        // Promotion Criteria 1: Salary is under 10,000
        if (employee.Salary < 10000)
        {
            return true;
        }

        // Promotion Criteria 2: Experience is above 5 years
        if (employee.Experience > 5)
        {
            return true;
        }

        return false;
    }

    public static void Main(string[] args)
    {
        // Create some sample employees
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Salary = 8000, Gender = Gender.Male, Experience = 3 },
            new Employee { Id = 2, Name = "Jane Smith", Salary = 12000, Gender = Gender.Female, Experience = 7 },
            new Employee { Id = 3, Name = "Bob Johnson", Salary = 9500, Gender = Gender.Male, Experience = 4 },
            new Employee { Id = 4, Name = "Sarah Lee", Salary = 7500, Gender = Gender.Female, Experience = 6 },
            new Employee { Id = 5, Name = "Michael Brown", Salary = 11000, Gender = Gender.Male, Experience = 2 }
        };

        // Promote eligible employees
        List<Employee> promotedEmployees = Promote(employees);

        // Display the promoted employees
        Console.WriteLine("Promoted Employees:");
        foreach (var employee in promotedEmployees)
        {
            Console.WriteLine($"Name: {employee.Name}, Salary: {employee.Salary}, Experience: {employee.Experience}");
        }
    }
}

/*19 - Try to make multicast delegate that point to a calculation
functions each one takes 2 int parameters and calculate and print the
result of sum, sub, divide, multi*/

using System;

public delegate int CalculationDelegate(int a, int b);

class Program
{
    static void Main(string[] args)
    {
        CalculationDelegate calculations = Sum;
        calculations += Subtract;
        calculations += Divide;
        calculations += Multiply;

        int result = calculations(10, 5);
        Console.WriteLine($"Result: {result}");
    }

    static int Sum(int a, int b)
    {
        int sum = a + b;
        Console.WriteLine($"Sum: {sum}");
        return sum;
    }

    static int Subtract(int a, int b)
    {
        int difference = a - b;
        Console.WriteLine($"Difference: {difference}");
        return difference;
    }

    static int Divide(int a, int b)
    {
        int quotient = a / b;
        Console.WriteLine($"Quotient: {quotient}");
        return quotient;
    }

    static int Multiply(int a, int b)
    {
        int product = a * b;
        Console.WriteLine($"Product: {product}");
        return product;
    }
}

/*20 - publisher subscriber for a company, Products and suppliers.
a. Products: ID, Name.
b. Company: ID, Name, Dictionary<Product, Quantity>.
c. Suppliers: ID, Name.
d. The company dictionary is private and you need to implement
Paying element to it
e. If the element already exists add to its quantity.
f. If the element is new jut add it.
g.You should implement Sell function that decrease the amount,
BUT if the amount is less than 5 you should notify that supplie20- publisher subscriber for a company, Products and suppliers.
a. Products: ID, Name.
b. Company: ID, Name, Dictionary<Product, Quantity>.
c. Suppliers: ID, Name.
d. The company dictionary is private and you need to implement
Paying element to it
e. If the element already exists add to its quantity.
f. If the element is new jut add it.
g.You should implement Sell function that decrease the amount,
BUT if the amount is less than 5 you should notify that supplie*/

using System;
using System.Collections.Generic;

// Product class
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Company class
public class Company
{
    private readonly Dictionary<Product, int> _inventory;
    private readonly List<Supplier> _suppliers;

    public Company(int id, string name)
    {
        Id = id;
        Name = name;
        _inventory = new Dictionary<Product, int>();
        _suppliers = new List<Supplier>();
    }

    public int Id { get; }
    public string Name { get; }

    public void AddSupplier(Supplier supplier)
    {
        _suppliers.Add(supplier);
        supplier.SupplyNotifier += OnSupplyNotification;
    }

    public void Pay(Product product, int quantity)
    {
        if (_inventory.TryGetValue(product, out var currentQuantity))
        {
            _inventory[product] = currentQuantity + quantity;
        }
        else
        {
            _inventory[product] = quantity;
        }
    }

    public bool Sell(Product product, int quantity)
    {
        if (_inventory.TryGetValue(product, out var currentQuantity))
        {
            if (currentQuantity >= quantity)
            {
                _inventory[product] = currentQuantity - quantity;
                return true;
            }
            else if (currentQuantity < 5)
            {
                NotifySuppliers(product);
                return false;
            }
        }
        return false;
    }

    private void NotifySuppliers(Product product)
    {
        foreach (var supplier in _suppliers)
        {
            supplier.SupplyNotifier?.Invoke(product);
        }
    }
}

// Supplier class
public class Supplier
{
    public delegate void SupplyNotification(Product product);
    public event SupplyNotification SupplyNotifier;

    public int Id { get; set; }
    public string Name { get; set; }

    public void NotifyLowStock(Product product)
    {
        SupplyNotifier?.Invoke(product);
    }
}

// Example usage
public class Program
{
    static void Main(string[] args)
    {
        var company = new Company(1, "ABC Company");
        var supplier1 = new Supplier { Id = 1, Name = "Supplier 1" };
        var supplier2 = new Supplier { Id = 2, Name = "Supplier 2" };

        company.AddSupplier(supplier1);
        company.AddSupplier(supplier2);

        var product1 = new Product { Id = 1, Name = "Product 1" };
        var product2 = new Product { Id = 2, Name = "Product 2" };

        company.Pay(product1, 10);
        company.Pay(product2, 20);

        Console.WriteLine("Selling 7 units of Product 1...");
        if (company.Sell(product1, 7))
        {
            Console.WriteLine("Sell successful");
        }
        else
        {
            Console.WriteLine("Sell failed due to low stock");
        }

        Console.WriteLine("Selling 15 units of Product 2...");
        if (company.Sell(product2, 15))
        {
            Console.WriteLine("Sell successful");
        }
        else
        {
            Console.WriteLine("Sell failed due to low stock");
        }
    }
}