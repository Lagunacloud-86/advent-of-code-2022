using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeTools
{
    public sealed class InputParser
    {
        private readonly string _input;
        private readonly IInputSearcher[] _searchers;

        public int NodeCount { get; init; }

        public Element[] Elements { get; init; }

        public readonly struct Element
        {
            private readonly InputParser _document;

            public ushort Index { get; }

            public byte Length { get; }

            public byte Depth { get; }

            public int ParentIndex { get; }


            public Element(InputParser document, in ushort index, in byte length, in byte depth, int parentIndex = -1)
            {
                _document = document;
                Index = index;
                Length = length;
                Depth = depth;
                ParentIndex = parentIndex;
            }

        }




        public InputParser(string input, params IInputSearcher[] searchers)
        {
            _input = input;
            _searchers = searchers;

            NodeCount = FindNodeCount(input, 0, 0);
            Elements = new Element[NodeCount];

            int nodeIndex = 0;
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
            for (int i = count - 1; i >= 0; i--)
            {
                stack[i] = ei;
                ei = Elements[ei].ParentIndex;
            }

            for(int i = 0; i < count; i++)
                slice = slice.Slice(Elements[stack[i]].Index, Elements[stack[i]].Length);
            return slice;

        }


        private int FindNodeCount(ReadOnlySpan<char> input, int inputSearcherIndex, int value)
        {
            if (inputSearcherIndex >= _searchers.Length)
                return value;
            int index = 0;
            while (_searchers[inputSearcherIndex].FindNext(in input, out int start, out int end, index))
            {
                var slice = input[start..(end + 1)];
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

            int index = 0;
            while (_searchers[inputSearcherIndex].FindNext(in input, out int start, out int end, index))
            {
                int length = (end + 1) - start;

                Elements[nodeIndex] = new Element(this, (ushort)start, (byte)length, (byte)inputSearcherIndex, parentIndex);

                var slice = input[start..(end + 1)];
                index = end + 1;

                nodeIndex++;
                FindNodes(slice, inputSearcherIndex + 1, nodeIndex - 1, ref nodeIndex);
            }
            
        }

    }
}
