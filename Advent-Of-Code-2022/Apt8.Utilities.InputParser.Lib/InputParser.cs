namespace Apt8.Utilities.InputParser.Lib;

public sealed class InputParser
{
    private readonly IInputSearcher[] _searchers;

    public int NodeCount { get; }

    public Element[] Elements { get; }

    public readonly struct Element
    {
        //private readonly InputParser _document;

        public ushort Index { get; }

        public byte Length { get; }

        public byte Depth { get; }

        public int ParentIndex { get; }


        public Element(/*InputParser document, */in ushort index, in byte length, in byte depth, int parentIndex = -1)
        {
            //_document = document;
            Index = index;
            Length = length;
            Depth = depth;
            ParentIndex = parentIndex;
        }

    }




    public InputParser(string input, params IInputSearcher[] searchers)
    {
        _searchers = searchers;

        NodeCount = FindNodeCount(input, 0, 0);
        Elements = new Element[NodeCount];

        var nodeIndex = 0;
        FindNodes(input, 0, -1, ref nodeIndex);
    }

    public InputParser(in ReadOnlySpan<char> input, params IInputSearcher[] searchers)
    {
        _searchers = searchers;

        NodeCount = FindNodeCount(input, 0, 0);
        Elements = new Element[NodeCount];

        var nodeIndex = 0;
        FindNodes(input, 0, -1, ref nodeIndex);
    }

    public ReadOnlySpan<char> GetValue(in ReadOnlySpan<char> input, int elementIndex)
    {
        var slice = input;

        var ei = elementIndex;
        var count = 1;
        while (Elements[ei].ParentIndex != -1)
        {
            count++;
            ei = Elements[ei].ParentIndex;
        }

        ei = elementIndex;
        Span<int> stack = stackalloc int[count];
        for (var i = count - 1; i >= 0; i--)
        {
            stack[i] = ei;
            ei = Elements[ei].ParentIndex;
        }

        for(var i = 0; i < count; i++)
            slice = slice.Slice(Elements[stack[i]].Index, Elements[stack[i]].Length);
        return slice;

    }


    private int FindNodeCount(in ReadOnlySpan<char> input, int inputSearcherIndex, int value)
    {
        if (inputSearcherIndex >= _searchers.Length)
            return value;
        var index = 0;
        while (_searchers[inputSearcherIndex].FindNext(in input, out var start, out var end, index))
        {
            var slice = input.Slice(start, (end + 1) - start);
            index = end + 1;
            value++;
            value += FindNodeCount(in slice, inputSearcherIndex + 1, 0);
        }
        return value;
    }

    private void FindNodes(in ReadOnlySpan<char> input, int inputSearcherIndex, int parentIndex, ref int nodeIndex)
    {
        if (inputSearcherIndex >= _searchers.Length)
            return;

        var index = 0;
        while (_searchers[inputSearcherIndex].FindNext(in input, out var start, out var end, index))
        {
            var length = (end + 1) - start;

            Elements[nodeIndex] = new Element(/*this, */(ushort)start, (byte)length, (byte)inputSearcherIndex, parentIndex);

            var slice = input.Slice(start, length);//[start..(end + 1)];
            index = end + 1;

            nodeIndex++;
            FindNodes(in slice, inputSearcherIndex + 1, nodeIndex - 1, ref nodeIndex);
        }
        
    }

}
