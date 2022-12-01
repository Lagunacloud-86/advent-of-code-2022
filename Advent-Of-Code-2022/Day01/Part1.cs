using AOCProject;
using Apt8.Utilities.InputParser.Lib;

namespace Day01;

public sealed class Part1 : IAOCProject<int>
{
    private readonly struct Vector
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Vector(in InputParser parser, in int index)
        {
            var xSpan = parser.GetValue(index + 1);
            var ySpan = parser.GetValue(index + 2);
            var zSpan = parser.GetValue(index + 3);

            X = int.Parse(xSpan);
            Y = int.Parse(ySpan);
            Z = int.Parse(zSpan);
        }
    }

    private readonly List<Vector> _vectors = new ();
    
    public void Init(in string input)
    {
        var fileData = File.ReadAllText(input);
        var parser = new InputParser(
            fileData, 
            new CharacterSearcher('\n', true), new CharacterSearcher(',', true));
        for (var i = 0; i < parser.NodeCount; i += 4)
            _vectors.Add(new Vector(parser, in i));
    }

    public int Run()
    {
        var value = _vectors.Sum(x => x.X + x.Y + x.Z);
        return value;
    }
}