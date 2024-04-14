using ProductAggregator.Models;

namespace ProductAggregator.Tests;

public class DequotedStringTests
{
    [Fact]
    public void DequotedString_RemovesEscapedQuotes()
    {
        var span = "1,2,3\"\"".AsSpan();
        var dequoted = span.ToDequotedString();

        Assert.Equal("1,2,3\"", dequoted);
    }
}
