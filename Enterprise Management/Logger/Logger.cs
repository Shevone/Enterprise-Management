namespace Enterprise_Management.Logger;

public class Logger
{
    public delegate void EventHandler(string message); // делегат(шаблон того, какие методы мы сможем записать в наш Event)

    public event EventHandler? LogEvent; // event, куда записаны методы логирования
    
    public void AddHandler(EventHandler handler)
    {
        // Записываем все передаенные метожы логгирования в переменную
        LogEvent += handler;
    }
    public void LogMessage(string message)
    {
        LogEvent?.Invoke(message);
    }
}