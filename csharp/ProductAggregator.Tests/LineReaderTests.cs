using ProductAggregator.Models;

namespace ProductAggregator.Tests;

public class LineReaderTests
{
    [Fact]
    public void ReadsSimpleLine()
    {
        var line = "1,2";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var two = reader.ReadField();

        Assert.Equal("1", one.ToString());
        Assert.Equal("2", two.ToString());
    }

    [Fact]
    public void ReadField_PreservesWhitespace()
    {
        var line = " 1 , 2 ";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var two = reader.ReadField();

        Assert.Equal(" 1 ", one.ToString());
        Assert.Equal(" 2 ", two.ToString());
    }

    [Fact]
    public void ReadField_QuotedField()
    {
        var line = "\"1,2\",3";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var three = reader.ReadField();

        Assert.Equal("1,2", one.ToString());
        Assert.Equal("3", three.ToString());
    }

    [Fact]
    public void ReadField_IgnoresSpaceOutsideQuotedFields()
    {
        var line = "  \"1,2\"  , 3 ";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var three = reader.ReadField();

        Assert.Equal("1,2", one.ToString());
        Assert.Equal(" 3 ", three.ToString());
    }

    [Fact]
    public void ReadField_QuotedFieldWithEscapedQuote()
    {
        var line = "\"1,2,3\"\"\",4";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var four = reader.ReadField();

        Assert.Equal("1,2,3\"\"", one.ToString());
        Assert.Equal("4", four.ToString());
    }

    [Fact]
    public void ReadField_QuotedFieldWithEscapedQuoteAtEnd()
    {
        var line = "\"1,2,3\"\"\",";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var empty = reader.ReadField();

        Assert.Equal("1,2,3\"\"", one.ToString());
        Assert.Equal("", empty.ToString());
    }

    [Fact]
    public void ReadField_QuotedFieldWithEscapedQuoteAtStart()
    {
        var line = "\"\"\"1,2,3\",4";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var four = reader.ReadField();

        Assert.Equal("\"\"1,2,3", one.ToString());
        Assert.Equal("4", four.ToString());
    }

    [Fact]
    public void ReadField_QuotedFieldWithEscapedQuoteAtStartAndEnd()
    {
        var line = "\"\"\"1,2,3\"\"\",4";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var four = reader.ReadField();

        Assert.Equal("\"\"1,2,3\"\"", one.ToString());
        Assert.Equal("4", four.ToString());
    }

    [Fact]
    public void ReadField_QuotedFieldWithEscapedQuoteAtStartAndEndAndMiddle()
    {
        var line = "\"\"\"1,\"\"2,3\"\"\",4";
        var reader = new LineReader(line.AsSpan());

        var one = reader.ReadField();
        var four = reader.ReadField();

        Assert.Equal("\"\"1,\"\"2,3\"\"", one.ToString());
        Assert.Equal("4", four.ToString());
    }
}