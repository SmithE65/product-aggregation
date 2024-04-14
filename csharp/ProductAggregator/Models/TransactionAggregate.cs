namespace ProductAggregator.Models;

public class TransactionAggregate(Product product, int quantity, double totalPrice)
{
    public Product Product { get; set; } = product;
    public int Quantity { get; set; } = quantity;
    public double TotalPrice { get; set; } = totalPrice;
}

public class TransactionAggregateFile(string path)
{
    public void WriteAggregates(IEnumerable<TransactionAggregate> aggregates)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("Product ID,Product Name,Total Quantity Sold,Total Revenue");

        foreach (TransactionAggregate aggregate in aggregates)
        {
            writer.WriteLine($"{aggregate.Product.Id},{aggregate.Product.Name},{aggregate.Quantity},{aggregate.TotalPrice}");
        }
    }
}