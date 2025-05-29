using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace OrderApp
{
    class Item
    {
        public string Title { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }

        public Item(string title, int quantity, double price)
        {
            Title = title;
            Quantity = quantity;
            Price = price;
        }

        public void PrintToConsole()
        {
            Console.WriteLine($"Товар: {Title}");
            Console.WriteLine($"Кiлькiсть: {Quantity}");
            Console.WriteLine($"Цiна: {Price:F2}");
            Console.WriteLine(new string('-', 30));
        }
    }

    class Order
    {
        public string Name { get; private set; }
        public string Street { get; private set; }
        public string Address { get; private set; }
        public string Country { get; private set; }

        public List<Item> Items { get; private set; }

        public Order(string name, string street, string address, string country)
        {
            Name = name;
            Street = street;
            Address = address;
            Country = country;
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void Show()
        {
            Console.WriteLine("Доставка для:");
            Console.WriteLine($"iм'я: {Name}");
            Console.WriteLine($"Вулиця: {Street}");
            Console.WriteLine($"Адреса: {Address}");
            Console.WriteLine($"Країна: {Country}");
            Console.WriteLine(new string('=', 30));

            Console.WriteLine("Список товарiв:");
            foreach (var item in Items)
            {
                item.PrintToConsole();
            }
        }
    }

    static class OrderParser
    {
        public static Order ParseFromXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\source\repos\88\Lab_10_01_OOP\Lab_10_OOP\XMLFile1.xml");

            XmlNode shipTo = doc.DocumentElement.SelectSingleNode("shipTo");

            string name = shipTo["name"].InnerText;
            string street = shipTo["street"].InnerText;
            string address = shipTo["address"].InnerText;
            string country = shipTo["country"].InnerText;

            Order order = new Order(name, street, address, country);

            XmlNodeList itemNodes = doc.DocumentElement.SelectNodes("items/item");
            foreach (XmlNode itemNode in itemNodes)
            {
                string title = itemNode["title"].InnerText;
                int quantity = int.Parse(itemNode["quantity"].InnerText);
                double price = double.Parse(itemNode["price"].InnerText, CultureInfo.InvariantCulture);

                Item item = new Item(title, quantity, price);
                order.AddItem(item);
            }

            return order;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Order order = OrderParser.ParseFromXml();
            order.Show();

        }
    }
}
