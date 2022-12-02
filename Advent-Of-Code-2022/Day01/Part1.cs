using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day01;

public sealed class Part1 : IAOCProject<int>
{
    private readonly struct Elf
    {
        public List<int> FoodCalories { get; }

        public Elf(in ReadOnlySpan<char> input, in InputParser parser, in int index)
        {
            FoodCalories = new List<int>();
            
            var roValue = parser.GetValue(in input, index);
            var calories = new InputParser(roValue, new CharacterSearcher('\n', true));
            
            for (var i = 0; i < calories.NodeCount; ++i)
                if (int.TryParse(calories.GetValue(roValue, i), out var value))
                    FoodCalories.Add(value);
        }
    }

    private readonly List<Elf> _elfs = new ();
    
    public void Init(in string input)
    {
        ReadOnlySpan<char> fileData = File.ReadAllText(input);
        var parser = new InputParser(
            fileData, 
            new StringSearcher("\r\n\r\n", true));
        for (var i = 0; i < parser.NodeCount; i++)
            _elfs.Add(new Elf(in fileData, parser, in i));
    }

    public int Run()
    {
        var value = _elfs.Max(x => x.FoodCalories.Sum());
        return value;
    }
}