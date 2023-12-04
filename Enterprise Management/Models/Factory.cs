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
    
    // Свойство, которое возвращает полный список сотрудников
    private List<Employee> _employees
    {
        get
        {
            List<Employee> employees = new List<Employee>();
            employees.AddRange(_managers);
            employees.AddRange(_factoryWorkers);
            employees.AddRange(_salesPersons);
            return employees;
        }
    }

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

    public override string ToString()
    {
        // Формируем то как будет выводится инфа
        StringBuilder sb = new StringBuilder();
        sb.Append($"Предприятие : {_factoryName}\n" +
                  $"Количество сотрудников : {_employees.Count}\n" +
                  $"Количество разновидностей происзводимых товаров : {_productsList.Count}\n\n" +
                  $"Сотрудники\n");
        foreach (Employee employee in _employees)
        {
            // Полиморфный вызов
            sb.Append(employee.DisplayInfo() + "\n");
        }

        sb.Append("\nПроизводимые товары\n");
        foreach (Product product in _productsList)
        {
            sb.Append(product + "\n");
        }

        double cost = CostOfProduction;
        double sale = ProfitOnSales;
        double profit = sale - cost;
        sb.Append($"Общие затраты на производство : {cost}\n" +
                  $"Получено от продаж : {sale}\n" +
                  $"Прибыль : {profit}");
        return sb.ToString();

    }
}