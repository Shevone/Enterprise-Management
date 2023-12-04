namespace Enterprise_Management.Logger;

public class FileWriter
{
    private string _filePath;
    public FileWriter(string fileName)
    {
        _filePath = fileName;
        Initialize();
    }
    
    // В этом методе мы првоереим корректность переданного пути до файла
    private void Initialize()
    {
        // если длина пути до файла больше либо равна 4 то смотрим
        string a = _filePath.Length >=4 ? _filePath[^4..] : "";
        if (a != ".txt")
        {
            // Если расширение файла - не корректно, то заменим на путь по умолчаниию
            // *Директория проекта + файл log.tx
            _filePath = Directory.GetCurrentDirectory()[..^16] + "log.txt";
        }
        // Если файл не существует то создадим его
        if (!File.Exists(_filePath))
        {
            // Создаем и закрываем затем
            File.Create(_filePath).Close();
        }
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine("\n=================================================");
    }
    // Метод печати лога в файл
    public void FileLogPrint(string message)
    {
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine(DateTime.Now + " | " + message);
    }
}