using Enterprise_Management.Models;
using Enterprise_Management.Models.Employees;
using Enterprise_Management.Models.Products;

namespace Enterprise_Management;

static class Program
{
    private static Factory _factory = new Factory("Производство");
    public static void Main(string[] args)
    {
        InitializeData();
        bool exitFlag = false;
        while (!exitFlag)
        {
            // Выводим инфу, и смотрим что выберут
            Console.Clear();
            Console.WriteLine("Главное меню, выберите пункт чтобы продолжить работу(Введите число)\n" +
                              "1 - Просмотр информации о фабрике\n" +
                              "2 - Создать новый товар\n" +
                              "3 - Произвести товар\n" +
                              "4 - Продать товар" +
                              "5 - Нанять новго сотрудника\n" +
                              "6 - Выход\n");
            string index = Console.ReadLine() ?? "";
            string message;
            switch (index)
            {
                default:
                    // Если ничего из перечисленного то просто выводим сообщение
                    message = "Ничего не выбрано";
                    break;
                case "1":
                    // пункт меню - просмотр инфы
                    message = _factory.ToString();
                    break;
                case "2":
                    // Пункт меню - создание новго товара
                    message = ProductCreate();
                    break;
                case "3":
                    // Пункт меню - произвести товар
                    message = ProduceProduct();
                    break;
                case "4":
                    message = SaleProduct();
                    break;
                case "5":
                    message = NewEmployee();
                    break;
                
            }
            Console.WriteLine(message);
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
            Console.ReadKey();
        }
    }

    // Пункт меню - произвести товар
    private static string ProduceProduct()
    {
        foreach (Product product in _factory.Products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("Введите id товара, который желаете произвести");
        bool parseResult1 = int.TryParse(Console.ReadLine() ?? "", out int prodId);
        Console.WriteLine("Введите количество товара чтобы произвести");
        bool parseResult2 = int.TryParse(Console.ReadLine() ?? "", out int count);
        if (parseResult1 == false || parseResult2 == false)
        {
            return "Числа введены не корретно";
        }
        bool produceResult = _factory.CreateProduct(prodId, count);
        return produceResult ? $"Успешно произведено {count} едениц товара {prodId}" : "Не существует товара с таким id";
    }
    // Пункт меню - продать товар
    private static string SaleProduct()
    {
        foreach (Product product in _factory.Products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("Введите id товара, который продаёте");
        bool parseResult1 = int.TryParse(Console.ReadLine() ?? "", out int prodId);
        Console.WriteLine("Введите количество проданного товара");
        bool parseResult2 = int.TryParse(Console.ReadLine() ?? "", out int count);
        if (parseResult1 == false || parseResult2 == false)
        {
            return "Числа введены не корретно";
        }
        bool produceResult = _factory.CreateProduct(prodId, count);
        return produceResult ? $"Успешно продано {count} едениц товара {prodId}" : "Не существует товара с таким id";
    }
    // Пункт меню - создать новый товар
    private static string ProductCreate()
    {
        Console.WriteLine("Введите название продукта");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Введите цену товара(для продажи)");
        bool parseResult1 = double.TryParse(Console.ReadLine() ?? "", out double price);
        Console.WriteLine("Введите себистоимость товара");
        bool parseResult2 = double.TryParse(Console.ReadLine() ?? "", out double costPrice);
        if (parseResult1 == false || parseResult2 == false || name == "")
        {
            return "Неверно введены параметры";
        }

        Product product = new Product(name, price, costPrice);
        _factory.AddNewProduct(product);
        return $"Товар {name}, успешно создан";
    }
    // Пункт меню - новый сотрудник
    private static string NewEmployee()
    {
        Console.WriteLine("Введите имя нового сотрудника");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Введите зарплату новго сотрудника");
        bool parseResult1 = double.TryParse(Console.ReadLine() ?? "", out double salary);
        if (name == "" || parseResult1 == false)
        {
            return "Не корректный ввод базовых параметров сотрудника";
        }
        Console.WriteLine("Ввыберите должность сотрудника\n" +
                          "1 - Работник предприятия\n" +
                          "2 - Продавец-консультант\n" +
                          "3 - Менеджер\n");
        string doljnost;
        Employee employee;
        switch (Console.ReadLine() ?? "")
        {
            default:
                return "Не корретно выбрана должность";
            case "1":
                doljnost = "Работниик предприятия";
                Console.WriteLine("Введите специализацию работника");
                string specialization = Console.ReadLine() ?? "";
                employee = new FactoryWorker(name, salary, specialization);
                break;
            case "2":
                doljnost = "Продавец-консультант";
                Console.WriteLine("Введите зону работы продавца");
                string workZone = Console.ReadLine() ?? "";
                employee = new Salesperson(name, salary, workZone);
                break;
            case "3":
                doljnost = "Менеджер";
                employee = new Manager(name, salary);
                break;
        }
        _factory.AddNewEmployee(employee);
        return $"Сотрудник {name}, добавлен на должность {doljnost}";
    }

    private static void InitializeData()
    {
        _factory.AddNewEmployee(new Manager("Иван", 30000));
        _factory.AddNewEmployee(new Manager("Вася", 40000));
        
        _factory.AddNewEmployee(new Salesperson("Ирина", 25000, "Электроника"));
        _factory.AddNewEmployee(new Salesperson("Владислав", 27000,"Посуда"));
        
        _factory.AddNewEmployee(new FactoryWorker("Петр", 19500, "Происзводство электроники"));
        _factory.AddNewEmployee(new FactoryWorker("Петр", 18000, "Происзводство посуды"));
        
        _factory.AddNewProduct(new Product("телевизор-крутой",10000, 5000));
        _factory.AddNewProduct(new Product("Телефон-модный",7000, 4000));
        _factory.AddNewProduct(new Product("Магнитофон",20000, 1000));
        
        _factory.AddNewProduct(new Product("Тарелка красивая",300, 170));
        _factory.AddNewProduct(new Product("Чашка фарфоровая",200, 130));

        _factory.CreateProduct(1, 20);
        _factory.CreateProduct(2, 20);
        _factory.CreateProduct(3, 20);
        _factory.SaleProduct(1, 15);
        _factory.SaleProduct(2, 18);
        _factory.SaleProduct(3, 10);
        
        
        _factory.CreateProduct(4, 10);
        _factory.CreateProduct(5, 20);
        _factory.SaleProduct(4, 15);
        _factory.SaleProduct(5, 0);
      

    }
}