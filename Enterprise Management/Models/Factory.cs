using System.Text;
using Enterprise_Management.Collection;
using Enterprise_Management.Models.Employees;
using Enterprise_Management.Models.Products;

namespace Enterprise_Management.Models;

public class Factory
{
    // ============================================================
    // Список сотрудников
    // Переделали так что каждый тип сотрудника записан в отдельном списке
    private IEmployeeCollection<Manager> _managers = new EmployeeCollection<Manager>(); 
    private IEmployeeCollection<FactoryWorker> _factoryWorkers = new EmployeeCollection<FactoryWorker>();
    private IEmployeeCollection<Salesperson> _salesPersons = new EmployeeCollection<Salesperson>();
    

    // ============================================================
    // Список товариов
    private List<Product> _productsList = new List<Product>();
    public List<Product> Products => new(_productsList); // свойство, возвращает копию списка продуктов во внешний истточник

    // Название предприятия
    private string _factoryName;
    
    // Свойство считающее затраченную сумму на производдство
    private double CostOfProduction
    {
        get
        {
            double sum = 0;
            foreach (Product product in _productsList)
            {
                sum += product.createCount * product.CostPrice;
            }
            return sum;
        }
    }
    // Свойсво считающее прибыль от продажи
    private double ProfitOnSales
    {
        get
        {
            double sum = 0;
            foreach (Product product in _productsList)
            {
                sum += product.salesCount * product.PriceForSale;
            }
            return sum;
        }
    }

    public Factory(string factoryName)
    {
        _factoryName = factoryName;
    }
    
    // ====================================================
    // Методы добавления объектов
    public void AddNewProduct(Product product)
    {
        // Новый товар
        _productsList.Add(product);
    }
    // Абстрактный метод добавления сотрудника
    public void AddNewEmployee<T>(T employee) where T : Employee
    {
        // Новый сотрудник
        // Проверяем тип сотрудника
        // В зависимости от типа добавляем в поределнный список
        switch (employee)
        {
            case Manager manager:
                _managers.Add(manager);
                break;
            case FactoryWorker factoryWorker:
                _factoryWorkers.Add(factoryWorker);
                break;
            case Salesperson salesperson:
                _salesPersons.Add(salesperson);
                break;
        }
    }
    // ====================================================
    
    // ====================================================
    // Методы работы фабрики
    public bool CreateProduct(int productId, int count)
    {
        // Происззвести продукцию
        Product? product = _productsList.Find(prod => prod.Id == productId);
        if (product == null)
        {
            return false;
        }
        product.createCount += count;
        return true;
    }

    public bool SaleProduct(int productId, int count)
    {
        // ПРодать продукцию
        Product? product = _productsList.Find(prod => prod.Id == productId);
        if (product == null)
        {
            return false;
        }
        product.salesCount += count;
        return true;
        
    }
    // ====================================================
    // Метод сортировки
    // На вход нам приходит строка, полученная из консол в классе Program
    // В зависимости от того какая строка пришла, то в делегат Func
    // записываем определенный статический метод, находящийся в классе програм
    //  Смотрим, если "1" то в делегат записываем сравнение по Имени сотрудника
    // "2" - сравнение по зарплате
    // Сортировка будет происходить именно среди коллекций определенного типа
    // Менеджеры отсортируются среди менеджеров
    // Работяги среди работяг
    // ПРодавцы среди продавцов
    public string SortEmployees(string index)
    {
        // Делегат куда записывается метод сравнения
        Func<Employee, Employee, bool> compareFunc;
        // Сообщение о том что произошло
        string message;
        switch (index)
        {
            default:
                // Елси что то не то поступило
                message = "Сортировка не произведена, посутпил неправильный параметр";
                return message;
            case "1":
                compareFunc = Employee.CompareByName;
                message = "Произведена сортировка по именам сотрудников";
                break;
            case "2":
                compareFunc = Employee.CompareBySalary;
                message = "Произведена сортировка по зарплате сотрудников";
                break;
        }
        _managers.SortEmployees(compareFunc);
        _factoryWorkers.SortEmployees(compareFunc);
        _salesPersons.SortEmployees(compareFunc);
        return message;
    }

    // ====================================================
    public override string ToString()
    {
        // Формируем то как будет выводится инфа
        StringBuilder sb = new StringBuilder();
        sb.Append($"Предприятие : {_factoryName}\n" +
                  $"Количество сотрудников : {_managers.Count + _factoryWorkers.Count + _salesPersons.Count}\n" +
                  $"Количество разновидностей происзводимых товаров : {_productsList.Count}\n\n" +
                  $"Сотрудники\n\nМенеджеры\n");
        foreach (Manager manager in _managers)
        {
            sb.Append(manager.DisplayInfo() + "\n");
        }

        sb.Append("===========\nПродавцы\n");
        foreach (Salesperson salesperson in _salesPersons)
        {
            sb.Append(salesperson.DisplayInfo() + "\n");
        }

        sb.Append("===========\nРаботники\n");
        foreach (FactoryWorker factoryWorker in _factoryWorkers)
        {
            sb.Append(factoryWorker.DisplayInfo() + "\n");
        }
        sb.Append("\nПроизводимые товары\n");
        foreach (Product product in _productsList)
        {
            sb.Append(product + "\n");
        }

        sb.Append("\n");
        double cost = CostOfProduction;
        double sale = ProfitOnSales;
        double profit = sale - cost;
        sb.Append($"Общие затраты на производство : {cost}\n" +
                  $"Получено от продаж : {sale}\n" +
                  $"Прибыль : {profit}");
        return sb.ToString();

    }
}