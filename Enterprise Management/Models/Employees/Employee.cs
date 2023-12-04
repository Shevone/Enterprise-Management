namespace Enterprise_Management.Models.Employees;

// Абстрактный класс - сотрудник предприятия 
public abstract class Employee
{
    // Поле имя, где хранится
    private string _name;

    // Поле - зп сотрудника
    private double _salary;
    // Свойство
    private double Salary
    {
        get => _salary;
        init
        {
            if (value <= 0)
            {
                _salary = 1;
            }
            _salary = value;
        }
    }
    // Protected(только наследникик) rjycnehrnjh
    protected Employee(string name, double salary)
    {
        _name = name;
        Salary = salary;
    }

    // Абстрактный метод для выполнения работы
    // Будет исопльзоватн для полиморфного вызова
    public abstract string DisplayInfo();
    public override string ToString()
    {
        return $"Имя : {_name}, Зарплата : {Salary}";
    }
    // =======================================================================================================================
    // Статические методы которые возвращают true если первый переданный объект "больше" второго
    // метод CompareTo возвращает:
    // -1 - если первый больше второ
    // 0 - если одинаковые
    // 1 - если первый больше второго
    // Поэтому если результат сравнения больше 0, то вернется true, а иначе false
    public static bool CompareByName(Employee employee1, Employee employee2)
    {
        int compareRes = string.Compare(employee1._name, employee2._name, StringComparison.Ordinal);
        return compareRes > 0;
    }
    public static bool CompareBySalary(Employee employee1, Employee employee2)
    {
        int compareRes = employee1.Salary.CompareTo(employee2.Salary);
        return compareRes > 0;
    }
    // =======================================================================================================================
}