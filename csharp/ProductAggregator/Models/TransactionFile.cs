namespace ProductAggregator.Models;

internal class TransactionFile(string path)
{
    private readonly string _path = path;

    public IEnumerable<Transaction> ReadTransactions()
    {
        using var reader = new StreamReader(_path);
        string line;
        while ((line = reader.ReadLine()!) != null)
        {
            var transaction = ReadTransaction(line);
            yield return transaction;
        }
    }

    private static Transaction ReadTransaction(string line)
    {
        var parts = line.Split(',');
        return new Transaction(
            parts[0],
            DateTime.Parse(parts[1]),
            new Product(parts[2], parts[3]),
            int.Parse(parts[4]),
            double.Parse(parts[5]));
    }
}