using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

public class Item
{
    public string Title { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Order
{
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<Item> Items { get; set; }

    public Order()
    {
        Items = new List<Item>();
    }
}

public static class XmlOrderParser
{
    public static Order ParseOrder(string xml)
    {
        var xDoc = XDocument.Parse(xml);
        var order = new Order();

        var shipTo = xDoc.Root?.Element("shipTo");
        order.Name = shipTo?.Element("name")?.Value ?? string.Empty;
        order.Street = shipTo?.Element("street")?.Value ?? string.Empty;
        order.Address = shipTo?.Element("address")?.Value ?? string.Empty;
        order.Country = shipTo?.Element("country")?.Value ?? string.Empty;

        var items = xDoc.Root?.Element("items")?.Elements("item");
        if (items != null)
        {
            foreach (var itemElement in items)
            {
                var item = new Item
                {
                    Title = itemElement.Element("title")?.Value ?? string.Empty,
                    Quantity = int.TryParse(itemElement.Element("quantity")?.Value, out var quantity) ? quantity : 0,
                    Price = decimal.TryParse(itemElement.Element("price")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var price) ? price : 0m
                };
                order.Items.Add(item);
            }
        }

        return order;
    }
}

class Program
{
    static void Main()
    {
        string filePath = "order.xml";

        if (File.Exists(filePath))
        {
            string xmlContent = File.ReadAllText(filePath);
            Order order = XmlOrderParser.ParseOrder(xmlContent);

            Console.WriteLine($"Name: {order.Name}");
            Console.WriteLine($"Street: {order.Street}");
            Console.WriteLine($"Address: {order.Address}");
            Console.WriteLine($"Country: {order.Country}");
            Console.WriteLine("Items:");
            foreach (var item in order.Items)
            {
                Console.WriteLine($" - Title: {item.Title}, Quantity: {item.Quantity}, Price: {item.Price}");
            }
        }
        else
        {
            Console.WriteLine($"File {filePath} not found.");
        }
    }
}

