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
}