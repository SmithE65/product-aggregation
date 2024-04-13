namespace ProductAggregator.Models;

internal record class Transaction(
    string TransactionId,
    DateTime Date,
    Product Product,
    int Quantity,
    double PricePerUnit);
