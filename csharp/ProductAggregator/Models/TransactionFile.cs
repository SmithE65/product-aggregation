namespace ProductAggregator.Models;

public class TransactionFile(string path)
{
    private readonly string _path = path;

    public IEnumerable<Transaction> ReadTransactions()
    {
        using var reader = new StreamReader(_path);
        var line = reader.ReadLine();

        if (line is not null &&
            TransactionParser.TryParse(line, out var transaction))
        {
            yield return transaction;
        }

        while ((line = reader.ReadLine()!) != null)
        {
            transaction = TransactionParser.Parse(line);
            yield return transaction;
        }
    }
}
