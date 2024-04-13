


var random = new Random();
var productIdCount = 1_000_000;
var products = Enumerable.Range(0, productIdCount)
    .Select(_ => new { Id = Guid.NewGuid(), PricePerUnit = random.NextDouble() * 100 })
    .ToArray();

var filename = "test.csv";
File.WriteAllLines(filename, CreateRecords(10_000_000));

IEnumerable<string> CreateRecords(int count)
{
    yield return "Transaction ID, Date, Product ID, Product Name, Quantity, Price per Unit";

   for (var i = 0; i < count; i++)
    {
        var transactionId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var productIndex = random.Next(productIdCount);
        var product = products[productIndex];
        var productId = product.Id;
        var productName = $"Product {productIndex}";
        var quantity = random.Next(1, 10);
        var pricePerUnit = product.PricePerUnit;

        yield return $"{transactionId},{date},{productId},{productName},{quantity},{pricePerUnit}";
    }
}


