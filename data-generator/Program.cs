
using System.Diagnostics;

Random random = new();
int productIdCount = 1_000_000;
Product[] products = GenerateProductMetaData(random, productIdCount);

int recordCount = 10_000_000;

DateTime startDate = new(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc);
DateTime endDate = new(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Utc);
DateTime[] transactionDates = GenerateOrderedDates(random, recordCount, startDate, endDate);

string filename = "test.csv";
File.WriteAllLines(filename, CreateRecords(recordCount));

IEnumerable<string> CreateRecords(int count)
{
    yield return "Transaction ID, Date, Product ID, Product Name, Quantity, Price per Unit";

    for (int i = 0; i < count; i++)
    {
        Guid transactionId = Guid.NewGuid();
        DateTime date = transactionDates[i];
        int productIndex = random.Next(productIdCount);
        Product product = products[productIndex];
        Guid productId = product.Id;
        string productName = $"Product {productIndex}";
        int quantity = (int)(random.NextDouble() * product.QuantityBias);
        double priceVariation = (random.NextDouble() * 0.3) + 0.85;
        double pricePerUnit = product.PricePerUnit * priceVariation;

        yield return $"{transactionId},{date:O},{productId},{productName},{quantity},{pricePerUnit:F2}";
    }
}

Console.WriteLine($"Wrote \"{filename}\" with {recordCount} rows.");

static Product[] GenerateProductMetaData(Random random, int productIdCount)
{
    Console.WriteLine("Generating products...");
    var start = Stopwatch.GetTimestamp();

    var products = Enumerable.Range(0, productIdCount)
        .Select(_ => new Product(
            Guid.NewGuid(),
            random.NextDouble() * 100,
            random.Next(1, 1000)
        ))
        .ToArray();

    Console.WriteLine($"Created {productIdCount} products in {Stopwatch.GetElapsedTime(start)}ms.");

    return products;
}

static DateTime[] GenerateOrderedDates(Random random, int recordCount, DateTime startDate, DateTime endDate)
{
    Console.WriteLine($"Generating ordered collection of {recordCount} transaction dates...");
    var start = Stopwatch.GetTimestamp();
    long range = endDate.Ticks - startDate.Ticks;

    DateTime[] transactionDates = Enumerable.Range(0, recordCount)
        .Select(_ =>
        {
            long ticks = (long)(range * random.NextDouble());
            return startDate.AddTicks(ticks);
        })
        .ToArray();

    Array.Sort(transactionDates);

    Console.WriteLine($"Created {recordCount} transaction dates in {Stopwatch.GetElapsedTime(start)}ms.");

    return transactionDates;
}

internal record Product(Guid Id, double PricePerUnit, int QuantityBias);
