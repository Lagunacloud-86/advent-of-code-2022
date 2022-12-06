
using System.Text;
using AOCProject;

namespace Day05;

public sealed class Part1 : IAOCProject<string>
{

    private readonly struct Instruction
    {
        public int FromId { get; }

        public int ToId { get; }
        
        public int HowMany { get; }

        public Instruction(in int fromId, in int toId, in int howMany)
        {
            FromId = fromId;
            ToId = toId;
            HowMany = howMany;
        }
    }

    private readonly Dictionary<int, Stack<char>> _cargo = new ();
    private readonly List<Instruction> _instructions = new ();

    private const string MoveStr = "move";
    
    public void Init(in string input)
    {
        ReadOnlySpan<char> spanInput = File.ReadAllText(input);
        
        var instructionSection = 0;
        for (; instructionSection < spanInput.Length - MoveStr.Length; ++instructionSection)
        {
            var slicedSection = spanInput.Slice(instructionSection, MoveStr.Length);
            if (slicedSection.SequenceEqual(MoveStr))
                break;
        }
        
        var cargoSection = spanInput[..instructionSection];
        var instructionsSection = spanInput[instructionSection..];

        ParseCargo(in cargoSection);
        ParseInstructions(in instructionsSection);

    }

    private void ParseCargo(in ReadOnlySpan<char> input)
    {
        // int startIndex = 0, i = 0;
        int i, indexer = 0, count = 0;
        for (i = 0; i < input.Length; ++i)
            if (input[i] == '\n')
                count++;

        Span<int> indexes = stackalloc int[count];
        for (i = 0; i < input.Length; ++i)
            if (input[i] == '\n')
            {
                indexes[indexer] = i;
                indexer++;
            }

        var cargoCount = (indexes[count - 2] - (indexes[count - 3] - 1)) / 4;
        for (i = 0; i < cargoCount; ++i)
            _cargo.Add(i + 1, new Stack<char>());
        ReadOnlySpan<char> row;
        for (var s = count - 4; s >= 0; --s)
        {
            row = input.Slice(indexes[s] + 1, indexes[s + 1] - indexes[s]);
            for (i = 0; i < cargoCount; i++)
            {
                var entry = row.Slice(i * 4, 3);
                if (entry[0] == '[')
                    _cargo[i + 1].Push(entry[1]);
            }
        }
        row = input.Slice(0, indexes[0] + 1);
        for (i = 0; i < cargoCount; i++)
        {
            var entry = row.Slice(i * 4, 3);
            if (entry[0] == '[')
                _cargo[i + 1].Push(entry[1]);
        }



    }

    private void ParseInstructions(in ReadOnlySpan<char> input)
    {
        // int startIndex = 0, i = 0;
        int i, indexer = 0, count = 0;
        for (i = 0; i < input.Length; ++i)
            if (input[i] == '\n')
                count++;

        Span<int> indexes = stackalloc int[count];
        for (i = 0; i < input.Length; ++i)
            if (input[i] == '\n')
            {
                indexes[indexer] = i;
                indexer++;
            }

        
        indexer = 0;
        for (i = 0; i <= indexes.Length; ++i)
        {
            count = i == indexes.Length ? input.Length - indexer : indexes[i] + 1 - indexer;
            var section = input.Slice(indexer, count);
            if (i != indexes.Length)
                indexer = indexes[i] + 1;
            _instructions.Add(ParseInstructionSection(in section));
        }





    }

    private static Instruction ParseInstructionSection(in ReadOnlySpan<char> input)
    {
        var index = 0;
        Span<int> indexes = stackalloc int[5];
        for (var i = 0; i < input.Length; ++i)
        {
            if (input[i] != ' ') continue;
            indexes[index] = i;
            index++;
        }

        var howManySpan = input[indexes[0]..indexes[1]];
        var fromSpan = input[indexes[2]..indexes[3]];
        var toSpan = input[indexes[4]..];

        var howMany = int.Parse(howManySpan);
        var from = int.Parse(fromSpan);
        var to = int.Parse(toSpan);

        return new Instruction(in from, in to, in howMany);
    }
    
    

    public string Run()
    {
        var builder = new StringBuilder(_cargo.Count);

        foreach(var instruction in _instructions)
            HandleInstruction(in instruction);
        
        for (int i = 0; i < _cargo.Count; ++i)
            builder.Append(_cargo[i + 1].Pop());

        return builder.ToString();
    }

    private void HandleInstruction(in Instruction instruction)
    {
        for (var moveCount = 0; moveCount < instruction.HowMany; moveCount++)
        {
            var c = _cargo[instruction.FromId].Pop();
            _cargo[instruction.ToId].Push(c);
        }
    }
}