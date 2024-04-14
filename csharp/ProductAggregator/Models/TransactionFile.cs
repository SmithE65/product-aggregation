using System.Diagnostics.CodeAnalysis;

namespace ProductAggregator.Models;

public class TransactionFile(string path)
{
    private readonly string _path = path;

    public IEnumerable<Transaction> ReadTransactions()
    {
        using var reader = new StreamReader(_path);
        string line;

        if (TryParseTransaction(reader, out var transaction))
        {
            yield return transaction;
        }

        while ((line = reader.ReadLine()!) != null)
        {
            transaction = ReadTransaction(line);
            yield return transaction;
        }
    }

    private static Transaction ReadTransaction(ReadOnlySpan<char> line)
    {
        var reader = new LineReader(line);
        var transactionId = reader.ReadField().ToString();
        var date = DateTime.Parse(reader.ReadField());
        var productId = reader.ReadField().ToString();
        var productName = reader.ReadField().ToString();
        var quantity = int.Parse(reader.ReadField());
        var pricePerUnit = double.Parse(reader.ReadField());

        return new Transaction(
            transactionId,
            date,
            new Product(productId, productName),
            quantity,
            pricePerUnit);
    }

    private static bool TryParseTransaction(StreamReader streamReader, [NotNullWhen(true)] out Transaction? transaction)
    {
        var line = streamReader.ReadLine();
        if (line is null)
        {
            transaction = null;
            return false;
        }
        var reader = new LineReader(line);
        var transactionId = reader.ReadField().ToString();
        var dateParsed = DateTime.TryParse(reader.ReadField(), out var date);
        var productId = reader.ReadField().ToString();
        var productName = reader.ReadField().ToString();
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

internal ref struct LineReader(ReadOnlySpan<char> line)
{
    private ReadOnlySpan<char> _line = line;

    public ReadOnlySpan<char> ReadField()
    {
        var index = _line.IndexOf(',');
        if (index == -1)
        {
            return _line;
        }

        var field = _line[..index];
        _line = _line[(index + 1)..];
        return field;
    }
}
