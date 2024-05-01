using ProductAggregator.Models;

namespace ProductAggregator.Extensions;

public static class TransactionAggregateExtensions
{
    public static void Upsert(this Dictionary<string, TransactionAggregate> aggregates, Transaction transaction)
    {
        double transactionTotal = transaction.Quantity * transaction.PricePerUnit;

        if (aggregates.TryGetValue(transaction.Product.Id, out var aggregate))
        {
            aggregate.Quantity += transaction.Quantity;
            aggregate.TotalPrice += transactionTotal;
        }
        else
        {
            aggregate = new TransactionAggregate(
                transaction.Product,
                transaction.Quantity,
                transactionTotal);
            aggregates[transaction.Product.Id] = aggregate;
        }
    }
}
