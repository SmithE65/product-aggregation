using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ProductAggregator.Models;

public static class TransactionParser
{
    public static Transaction Parse(ReadOnlySpan<char> line)
    {
        var reader = new LineReader(line);
        var transactionId = reader.ReadField().ToDequotedString();
        var dateField = reader.ReadField();
        var date = DateTime.ParseExact(dateField, "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        var productId = reader.ReadField().ToDequotedString();
        var productName = reader.ReadField().ToDequotedString();
        var quantity = int.Parse(reader.ReadField());
        var pricePerUnit = double.Parse(reader.ReadField());

        return new Transaction(
            transactionId,
            date,
            new Product(productId, productName),
            quantity,
            pricePerUnit);
    }

    public static bool TryParse(ReadOnlySpan<char> line, [NotNullWhen(true)] out Transaction? transaction)
    {
        if (line.IsEmpty)
        {
            transaction = null;
            return false;
        }
        var reader = new LineReader(line);
        var transactionId = reader.ReadField().ToDequotedString();
        var dateParsed = DateTime.TryParse(reader.ReadField(), out var date);
        var productId = reader.ReadField().ToDequotedString();
        var productName = reader.ReadField().ToDequotedString();
        var quantityParsed = int.TryParse(reader.ReadField(), out var quantity);
        var pricePerUnitParsed = double.TryParse(reader.ReadField(), out var pricePerUnit);

        if (!(dateParsed && quantityParsed && pricePerUnitParsed))
        {
            transaction = null;
            return false;
        }

        transaction = new Transaction(
            transactionId,
            date,
            new Product(productId, productName),
            quantity,
            pricePerUnit);

        return true;
    }
}
