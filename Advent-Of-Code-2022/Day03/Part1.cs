using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day03;

public sealed class Part1 : IAOCProject<int>
{
    public readonly struct Rucksack
    {
        private string Contents { get; }
        public int Length { get; }

        public ReadOnlySpan<char> GetCompartment1()
        {
            ReadOnlySpan<char> c = Contents;
            return c[..Length];
        }
        public ReadOnlySpan<char> GetCompartment2()
        {
            ReadOnlySpan<char> c = Contents;
            return c[Length..];
        }



        public Rucksack(in ReadOnlySpan<char> section)
        {
            Contents = section.ToString().Trim();
            Length = Contents.Length / 2;
            

        }
    }

    private List<Rucksack> _rucksacks = new List<Rucksack>();

    private string _input;
    
    public void Init(in string input)
    {
        _input = File.ReadAllText(input);
        ReadOnlySpan<char> fileContents = _input;
        var parser = new InputParser(in fileContents, new CharacterSearcher('\n', true));
        for (var i = 0; i < parser.NodeCount; ++i)
            _rucksacks.Add(new Rucksack(parser.GetValue(in fileContents, i)));

    }

    public int Run()
    {
        var score = 0;
        foreach (var rs in _rucksacks)
        {
            var common = FindCommonItemInRuckSack(in rs);
            var priority = GetCharPriority(common);
            score += priority;
            //Console.WriteLine($"Common: '{common}', Priority: {priority}");
        }

        //Console.WriteLine($"a: {(int)'a'}, z: {(int)'z'}, A: {(int)'A'}, Z: {(int)'Z'}");
        
        
        return score;
    }

    private static int GetCharPriority(in char input)
    {
        const string lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(var i = 0; i <lookup.Length; ++i)
            if (lookup[i] == input)
                return i + 1;
        return 0;
    }

    private static char FindCommonItemInRuckSack(in Rucksack rucksack)
    {
        var content1 = rucksack.GetCompartment1();
        var content2 = rucksack.GetCompartment2();

        for (var c1 = 0; c1 < content1.Length; ++c1)
        {
            for (var c2 = 0; c2 < content2.Length; ++c2)
            {
                if (content1[c1] == content2[c2])
                    return content1[c1];
            }
        }

        return (char)0;
    }

}