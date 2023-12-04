using System.Collections;
using Enterprise_Management.Models.Employees;

namespace Enterprise_Management.Collection;

public class EmployeeCollection<T> : IEmployeeCollection<T> where T : Employee
{
    private T[] _persons;
    private int _i = 0;
    // Колиечество элементов текущее
    public int Count => _i;
    
    // Конструктор
    public EmployeeCollection()
    {
        _persons = new T[10];
    }
    // Метод добавления нового элеменат
    public void Add(T item)
    {
        if (_i == Count)
        {
            Resize(_i * 2);
        }
        _persons[_i] = item;
        _i++;
    }
    // Метод для изменения размера массива
    private void Resize(int newSize)
    {
        T[] copy = _persons;
        _persons = new T[newSize];
        copy.CopyTo(_persons,0);
    }
    // =============================================
    // Это методы которые обязует реализовать интерфес IEnumerable
    // Они нужны для итерации по нашему объекту
    public IEnumerator<T> GetEnumerator()
    {
        // Метод возращает объект для итерации
        return (IEnumerator<T>)_persons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    

    
}