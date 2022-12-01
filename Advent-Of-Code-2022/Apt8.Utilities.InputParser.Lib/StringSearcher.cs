namespace Apt8.Utilities.InputParser.Lib;

public sealed class StringSearcher : IInputSearcher
{
    private readonly string _search;

    private readonly bool _includeEndAsFound;
    public StringSearcher(string search, bool includeEndAsFound = false)
    {
        _search = search;
        _includeEndAsFound = includeEndAsFound; 
    }

    public bool FindNext(in ReadOnlySpan<char> input, out int startIndex, out int endIndex, int index = 0)
    {
        endIndex = -1;
        startIndex = -1;

        if (index < 0)
            return false;

        if (index >= input.Length)
            return false;

        startIndex = index;

        for (; index < input.Length - _search.Length; index++)
        {
            var slice = input.Slice(index, _search.Length);
            if (slice.SequenceEqual(_search))
            {
                endIndex = index + _search.Length;
                return true;
            }
        }

        if (_includeEndAsFound)
        {
            endIndex = index + _search.Length - 1;
            return true;
        }


        return false;
    }


    public int FindCount(in ReadOnlySpan<char> input, int index = 0)
    {
        int count = 0;
        for (; index < input.Length - _search.Length; ++index)
        {
            var slice = input.Slice(index, _search.Length);
            if (slice.SequenceEqual(_search))
                count++;
        }

        if (_includeEndAsFound && !input[_search.Length..].SequenceEqual(_search))
            count++;

        return count;
    }
}
