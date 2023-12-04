using Enterprise_Management.Models.Employees;

namespace Enterprise_Management.Collection;

public interface IEmployeeCollection<T> : IEnumerable<T> where T : Employee
{
    public int Count { get; }
    public void Add(T item);
}