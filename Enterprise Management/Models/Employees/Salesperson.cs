namespace Enterprise_Management.Models.Employees;

// Класс - продавец
public class Salesperson : Employee
{
    private string _areaOfResponsibility;// Зона ответсвенности например продавец электроники
    
    public Salesperson(string name, double salary, string areaOfResponsibility) : base(name, salary)
    {
        _areaOfResponsibility = areaOfResponsibility;
    }

    public override string DisplayInfo()
    {
        return $"- {base.ToString()} , Должность : Продавец-консультант, Зона продажи : {_areaOfResponsibility}";
    }
    
 
}