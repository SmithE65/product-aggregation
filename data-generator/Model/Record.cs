namespace DataGenerator.Model;

internal record class Record(Guid TransactionId, DateTime Date, Guid ProductId, string ProductName, int Quantity, double PricePerUnit);
