
using System.Text;
using AOCProject;

namespace Day06;

public sealed class Part1 : IAOCProject<int>
{

    private string _input;
    public void Init(in string input)
    {
        _input = File.ReadAllText(input);
    }


    public int Run() => FindMarker(_input);

    private static int FindMarker(in ReadOnlySpan<char> input)
    {
        for (var i = 0; i < input.Length - 4; ++i)
        {
            var section = input.Slice(i, 4);
            if (AllDifferentCharacters(in section))
                return i + 4;
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