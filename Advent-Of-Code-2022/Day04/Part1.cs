using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day04;

public sealed class Part1 : IAOCProject<int>
{

    private readonly struct Section
    {
        public int Start { get; }

        public int End { get; }

        public Section(in ReadOnlySpan<char> input)
        {
            var parser = new InputParser(in input, new CharacterSearcher('-', true));
            
            var i1 = parser.GetValue(in input, 0)[..^1];
            var i2 = parser.GetValue(in input, 1);

            Start = int.Parse(i1);
            End = int.Parse(i2);
        }
    }
    private sealed class SectionListItem
    {
        public Section Section1 { get; }
        public Section Section2 { get; }

        public SectionListItem(in ReadOnlySpan<char> input)
        {
            var spanInput = input[..^1];
            if (input[^1] != '\n')
                spanInput = input;
            var parser = new InputParser(in spanInput, new CharacterSearcher(',', true));

            var i = parser.GetValue(in spanInput, 0)[..^1];

            Section1 = new Section(in i);
            Section2 = new Section(parser.GetValue(in spanInput, 1));
        }
    }
    
    
    private string _input;

    private List<SectionListItem> SectionList = new List<SectionListItem>();
    public void Init(in string input)
    {
        ReadOnlySpan<char> spanInput = File.ReadAllText(input);
        var parser = new InputParser(in spanInput, new CharacterSearcher('\n', true));
        for (var i = 0; i < parser.NodeCount; ++i)
        {
            var listItem = new SectionListItem(parser.GetValue(in spanInput, i));
            SectionList.Add(listItem);
        }
        
        Console.WriteLine("");
    }

    public int Run()
    {
        var score = 0;
        foreach (var t in SectionList)
        {
            if (HasOverlappingWork(in t))
                score++;
        }
        
        return score;
    }

    private static bool HasOverlappingWork(in SectionListItem item)
    {
        var startOffset = item.Section1.Start - item.Section2.Start;
        var endOffset = item.Section1.End - item.Section2.End;
        if (startOffset >= 0 && endOffset <= 0) return true;
        return startOffset <= 0 && endOffset >= 0;
    }
    
    
    

}