
using System.Text;
using AOCProject;

namespace Day06;

public sealed class Part2 : IAOCProject<int>
{
    private const int UniqueLength = 14;
    private string _input;
    public void Init(in string input)
    {
        _input = File.ReadAllText(input);
    }


    public int Run() => FindMarker(_input);

    private static int FindMarker(in ReadOnlySpan<char> input)
    {
        for (var i = 0; i < input.Length - UniqueLength; ++i)
        {
            var section = input.Slice(i, UniqueLength);
            if (AllDifferentCharacters(in section))
                return i + UniqueLength;
        }

        return -1;
    }

    private static bool AllDifferentCharacters(in ReadOnlySpan<char> input)
    {
        for (var i = 0; i < input.Length - 1; ++i)
        {
            for(var j = i + 1; j < input.Length; ++j)
                if (input[i] == input[j])
                    return false;
            
        }

        return true;
    }

}