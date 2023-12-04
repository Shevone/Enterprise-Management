namespace Enterprise_Management.Models.Employees;

// Класс - обычный работяга предприятия
public class FactoryWorker : Employee
{
    private string _workSpecialization; // Рабочая специализация
    
    public FactoryWorker(string name, double salary, string workSpecialization) : base(name, salary)
    {
        _workSpecialization = workSpecialization;
    }

    public override string DisplayInfo()
    {
        return $"- {base.ToString()} , Должность : Работник фабрики, Спецаилизация : {_workSpecialization}";
    }
}