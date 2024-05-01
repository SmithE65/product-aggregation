using ProductAggregator.Extensions;
using ProductAggregator.Models;

Console.Write("Path: ");
string? inputPath = Console.ReadLine();

if (inputPath is null || !File.Exists(inputPath))
{
    Console.Write("Invalid path.");
    return;
}

Dictionary<string, TransactionAggregate> aggregates = [];
int transactionCount = 0;
long totalUnitsSold = 0;
double totalRevenue = 0;

TransactionFile transactionFile = new(inputPath);
foreach (Transaction transaction in transactionFile.ReadTransactions())
{
    aggregates.Upsert(transaction);
    transactionCount++;
    totalUnitsSold += transaction.Quantity;
    totalRevenue += transaction.Quantity * transaction.PricePerUnit;
}

Console.WriteLine($"Read {transactionCount} transactions for {aggregates.Count} products.");
Console.WriteLine($"Total units sold: {totalUnitsSold}");
Console.WriteLine($"Total revenue: {totalRevenue}");

string? outputPath = Path.ChangeExtension(inputPath, ".aggregates.csv");
TransactionAggregateFile aggregateFile = new(outputPath);
aggregateFile.WriteAggregates(aggregates.Values);
Console.WriteLine($"Wrote aggregate data to '{outputPath}'");
