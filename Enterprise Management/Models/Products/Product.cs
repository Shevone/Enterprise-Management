namespace Enterprise_Management.Models.Products;

// Класс - продукта
public class Product
{
    // ==============================
    // Id
    private static int _id = 1;
    public int Id { get; }
    // ==============================
    
    private string _name; // Поле в котором хранится имя 
    
    // ==============================
    private double _priceForSaleForSale; // Поле в котором хранится цена
    private double _costPrice;
    public double PriceForSale
    {
        // Свойство, через которое происходи взаимодействие с ценой
        get => _priceForSaleForSale;
        // В инициализаторе проверям чтоб цена не была 0 и меньше
        init
        {
            if (value <= 0 || value < CostPrice)
            {
                _priceForSaleForSale = CostPrice;
            }

            _priceForSaleForSale = value;
        }
    } 
    public double CostPrice
    {
        // Свойство, через которое происходи взаимодействие с себистоимость.
        get => _costPrice;
        // В инициализаторе проверям чтоб цена не была 0 и меньше
        init
        {
            if (value <= 0)
            {
                _costPrice = 1;
            }

            _costPrice = value;
        }
    } 
    // ===============================
    // Количество продано
    public int salesCount;
    // Количество произведено
    public int createCount;
    // ===============================

    

    public Product(string name, double price, double costPrice)
    {
        Id = _id;
        _id++;
        _name = name;
        CostPrice = costPrice;
        PriceForSale = price;
    }

    // Метод для отображения информации о продукте
    public override string ToString()
    {
        return $" Id : {Id}, Товар {_name}, Цена {PriceForSale}, Cебестоимость {CostPrice}, Количество проданных {salesCount}шт., Всего произведено {createCount}";
    }
}