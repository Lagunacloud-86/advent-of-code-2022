﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeTools
{
    public sealed class CharacterSearcher : IInputSearcher
    {
        private readonly char _searchCharacter;
        private readonly bool _includeEndAsFound;
        public CharacterSearcher(char search, bool includeEndAsFound = false)
        {
            _searchCharacter = search;
            _includeEndAsFound = includeEndAsFound;
        }

        public bool FindNext(in ReadOnlySpan<char> input, out int startIndex, out int endIndex, int index = 0)
        {
            endIndex = -1;
            startIndex = -1;

            if (index < 0) 
                return false;

            if (index >= input.Length)
                return false;

            startIndex = index;
            for(; index < input.Length; index++)
            {
                if (input[index] == _searchCharacter)
                {
                    endIndex = index;
                    return true;
                }
            }

            if (_includeEndAsFound)
            {
                endIndex = index - 1;
                return true;
            }


            return false;
        }
  
    
        public int FindCount(in ReadOnlySpan<char> input, int index = 0)
        {
            int count = 0;
            for(; index < input.Length; ++index)
            {
                if (input[index] == _searchCharacter)
                    count++;
            }


            if (_includeEndAsFound && input[index - 1] != _searchCharacter)
                count++;

            return count;
        }
    
    }
}
