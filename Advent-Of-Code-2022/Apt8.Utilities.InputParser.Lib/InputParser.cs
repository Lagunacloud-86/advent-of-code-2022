namespace Apt8.Utilities.InputParser.Lib;

public sealed class InputParser
{
    private readonly string _input;
    private readonly IInputSearcher[] _searchers;

    public int NodeCount { get; }

    private Element[] Elements { get; }

    private readonly struct Element
    {
        //private readonly InputParser _document;

        public ushort Index { get; }

        public byte Length { get; }

        //public byte Depth { get; }

        public int ParentIndex { get; }


        public Element(/*InputParser document, */in ushort index, in byte length/*, in byte depth*/, int parentIndex = -1)
        {
            //_document = document;
            Index = index;
            Length = length;
            //Depth = depth;
            ParentIndex = parentIndex;
        }

    }




    public InputParser(string input, params IInputSearcher[] searchers)
    {
        _input = input;
        _searchers = searchers;

        NodeCount = FindNodeCount(input, 0, 0);
        Elements = new Element[NodeCount];

        var nodeIndex = 0;
        FindNodes(input, 0, -1, ref nodeIndex);
    }


    public ReadOnlySpan<char> GetValue(int elementIndex)
    {
        ReadOnlySpan<char> slice = _input;

        int ei = elementIndex;
        int count = 1;
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


    private int FindNodeCount(ReadOnlySpan<char> input, int inputSearcherIndex, int value)
    {
        if (inputSearcherIndex >= _searchers.Length)
            return value;
        int index = 0;
        while (_searchers[inputSearcherIndex].FindNext(in input, out var start, out var end, index))
        {
            var slice = input[start..end];
            index = end + 1;
            value++;
            value += FindNodeCount(slice, inputSearcherIndex + 1, 0);
        }
        return value;
    }

    private void FindNodes(ReadOnlySpan<char> input, int inputSearcherIndex, int parentIndex, ref int nodeIndex)
    {
        if (inputSearcherIndex >= _searchers.Length)
            return;

        var index = 0;
        while (_searchers[inputSearcherIndex].FindNext(in input, out var start, out var end, index))
        {
            var length = end - start;

            Elements[nodeIndex] = new Element(/*this, */(ushort)start, (byte)length, /*(byte)inputSearcherIndex, */parentIndex);

            var slice = input[start..end];
            index = end + 1;

            nodeIndex++;
            FindNodes(slice, inputSearcherIndex + 1, nodeIndex - 1, ref nodeIndex);
        }
        
    }

}
