using ProductAggregator.Models;

namespace ProductAggregator.Tests;

public class TransactionParserTests
{
    [Fact]
    public void Parse_ValidLine_ReturnsTransaction()
    {
        string line = "1,1970-01-01T00:00:00.0000000Z,2,Product 1,10,5.99";
        Transaction transaction = TransactionParser.Parse(line);

        Assert.Equal("1", transaction.TransactionId);
        Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), transaction.Date);
        Assert.Equal("2", transaction.Product.Id);
        Assert.Equal("Product 1", transaction.Product.Name);
        Assert.Equal(10, transaction.Quantity);
        Assert.Equal(5.99, transaction.PricePerUnit);
    }

    [Fact]
    public void Parse_InvalidLine_ThrowsFormatException()
    {
        string line = "1,Product 1,10";
        _ = Assert.Throws<FormatException>(() => TransactionParser.Parse(line));
    }

    [Fact]
    public void TryParse_ValidLine_ReturnsTrue()
    {
        string line = "1,1970-01-01T00:00:00.0000000Z,2,Product 1,10,5.99";
        bool result = TransactionParser.TryParse(line, out var transaction);

        Assert.True(result);
        Assert.Equal("1", transaction!.TransactionId);
        Assert.Equal("2", transaction.Product.Id);
        Assert.Equal("Product 1", transaction.Product.Name);
        Assert.Equal(10, transaction.Quantity);
        Assert.Equal(5.99, transaction.PricePerUnit);
    }

    [Fact]
    public void TryParse_InvalidLine_ReturnsFalse()
    {
        string line = "1,Product 1,10";
        bool result = TransactionParser.TryParse(line, out var transaction);

        Assert.False(result);
        Assert.Null(transaction);
    }
}
