namespace ProductAggregator.Models;

public ref struct LineReader(ReadOnlySpan<char> line)
{
    private ReadOnlySpan<char> _line = line;

    public ReadOnlySpan<char> ReadField()
    {
        if (_line.IsEmpty)
        {
            return [];
        }

        if (IsQuotedField())
        {
            return ParseQuotedField();
        }
        else
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

    private ReadOnlySpan<char> ParseQuotedField()
    {
        var fieldStart = _line.IndexOf('"') + 1;
        var fieldEnd = fieldStart;
        fieldEnd = SeekClosingQuote(fieldEnd);

        if (fieldStart == fieldEnd)
        {
            throw new InvalidOperationException("Unterminated quoted field.");
        }

        var field = _line[fieldStart..fieldEnd];
        fieldEnd = SeekEndOfField(fieldEnd);
        _line = _line[(fieldEnd + 1)..];
        return field;
    }

    private readonly int SeekEndOfField(int fieldEnd)
    {
        fieldEnd++;
        while (fieldEnd < _line.Length)
        {
            if (_line[fieldEnd] == ',')
            {
                break;
            }
            else if (!char.IsWhiteSpace(_line[fieldEnd]))
            {
                throw new Exception("Unexpected character in quoted field.");
            }
            fieldEnd++;
        }

        return fieldEnd;
    }

    private readonly int SeekClosingQuote(int fieldEnd)
    {
        while (fieldEnd < _line.Length)
        {
            if (_line[fieldEnd] == '"')
            {
                if (_line[fieldEnd + 1] == '"')
                {
                    fieldEnd++;
                }
                else if (_line[fieldEnd + 1] == ',' || char.IsWhiteSpace(_line[fieldEnd + 1]))
                {
                    break;
                }
                else if (!char.IsWhiteSpace(_line[fieldEnd + 1]))
                {
                    throw new Exception("Unexpected character terminating quoted field.");
                }
            }
            fieldEnd++;
        }

        return fieldEnd;
    }

    private readonly bool IsQuotedField()
    {
        for (int i = 0; i < _line.Length; i++)
        {
            if (_line[i] == '"')
            {
                return true;
            }
            else if (!char.IsWhiteSpace(_line[i]) || _line[i] == ',')
            {
                return false;
            }
        }
        return _line[0] == '"';
    }
}
