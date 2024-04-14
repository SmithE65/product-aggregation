namespace ProductAggregator.Models;

public record class Transaction(
    string TransactionId,
    DateTime Date,
    Product Product,
    int Quantity,
    double PricePerUnit);
