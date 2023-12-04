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
        if (_i == _persons.Length)
        {
            Resize(_i * 2);
        }
        _persons[_i] = item;
        _i++;
    }
    // =========================================================
    // Метод для сортировки 
    // На вход поступает метод - для сравнения
    // Тут реализована пузырьковая сортировка - потому что на ней можно наглядно показать вызов делегата
    // на строке 44 - мы делаем вызов делегата, который сравнивает два постпуивших на вход Employee
    // Если первый больше второго, то меняем местами
    public void SortEmployees(Func<T, T, bool> compareFunc)
    {
        var len = _i;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                // Помещаем 2 параметра в функцию сравнения
                // Если первый больше второго, то меняем их местами
                T? p1 = _persons[j];
                T? p2 = _persons[j + 1];
                bool firstBiggerThanSecond = compareFunc(p1, p2);
                if(firstBiggerThanSecond)
                {
                    (_persons[j], _persons[j + 1]) = (_persons[j + 1], _persons[j]);
                }
            }
        }
    }
    // =========================================================

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
        List<T> a = new List<T>();
        foreach (T person in _persons)
        {
            if (person != null)
            {
                a.Add(person);
            }
        }
       
        return a.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    

    
}