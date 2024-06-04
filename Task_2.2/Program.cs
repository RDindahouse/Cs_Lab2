using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

// Represents an item in the order
public class Item
{
    public string Title { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// Represents an order with shipping information and items
public class Order
{
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<Item> Items { get; set; }

    // Constructor to initialize the list of items
    public Order()
    {
        Items = new List<Item>();
    }
}

// Static class to parse XML into Order objects
public static class XmlOrderParser
{
    // Parses the XML string into an Order object
    public static Order ParseOrder(string xml)
    {
        var xDoc = XDocument.Parse(xml);
        var order = new Order();

        // Parse shipping information
        var shipTo = xDoc.Root?.Element("shipTo");
        order.Name = shipTo?.Element("name")?.Value ?? string.Empty;
        order.Street = shipTo?.Element("street")?.Value ?? string.Empty;
        order.Address = shipTo?.Element("address")?.Value ?? string.Empty;
        order.Country = shipTo?.Element("country")?.Value ?? string.Empty;

        // Parse items
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
                order.Items.Add(item); // Add parsed item to the order's item list
            }
        }

        return order; // Return the parsed order
    }
}

class Program
{
    static void Main()
    {
        string filePath = "order.xml";

        if (File.Exists(filePath))
        {
            string xmlContent = File.ReadAllText(filePath); // Read XML content from file
            Order order = XmlOrderParser.ParseOrder(xmlContent); // Parse the XML content into an Order object

            // Display parsed data
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

