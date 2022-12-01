namespace Apt8.Utilities.InputParser.Lib;

public interface IInputSearcher
{
    bool FindNext(in ReadOnlySpan<char> input, out int startIndex, out int endIndex, int index = 0);

    int FindCount(in ReadOnlySpan<char> input, int index = 0);
}
