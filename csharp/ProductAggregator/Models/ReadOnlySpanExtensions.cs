namespace ProductAggregator.Models;

public static class ReadOnlySpanExtensions
{
    public static string ToDequotedString(this ReadOnlySpan<char> span)
    {
        return span.ToString().Replace("\"\"", "\"");
    }
}
