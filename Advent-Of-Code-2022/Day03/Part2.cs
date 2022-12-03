using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day03;

public sealed class Part2 : IAOCProject<int>
{
    private readonly struct Rucksack
    {
        public string Contents { get; }
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
        for (var i = 0; i < _rucksacks.Count; i += 3)
        {
            var common = FindGroupBadge(_rucksacks[i + 0], _rucksacks[i + 1], _rucksacks[i + 2]);
            var priority = GetCharPriority(common);
            score += priority;
            //Console.WriteLine($"Common: '{common}', Priority: {priority}");
        }
        
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

    
    private static char FindGroupBadge(in Rucksack rucksack1, in Rucksack rucksack2, in Rucksack rucksack3)
    {
        var status = new Dictionary<char, byte>(52);
        CountGroupBadge(status, rucksack1.Contents, 0b001);
        CountGroupBadge(status, rucksack2.Contents, 0b010);
        CountGroupBadge(status, rucksack3.Contents, 0b100);
        
        return status.First(x => x.Value == 0b111).Key;
    }

    private static void CountGroupBadge(Dictionary<char, byte> status, in ReadOnlySpan<char> content, in byte bit)
    {
        for (var i = 0; i < content.Length; ++i)
        {
            if (!status.ContainsKey(content[i]))
                status.Add(content[i], 0);

            status[content[i]] |= bit;
        }

    }
}