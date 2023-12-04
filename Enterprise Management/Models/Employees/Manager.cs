namespace Enterprise_Management.Models.Employees;

// Класс - менеджер предприятия
public class Manager : Employee
{
    
    public Manager(string name, double salary) : base(name, salary)
    {
        
    }

    public override string DisplayInfo()
    {
        return $"- {base.ToString()} , Должность : Менеджер";
    }
}