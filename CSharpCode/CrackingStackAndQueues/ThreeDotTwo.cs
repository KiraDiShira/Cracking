using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrackingStackAndQueues
{
    public class MinStack
    {
        private int _index;
        private int _minIndex = -1;
        private int _min2Index = -1;
        private readonly int[] _array;

        public MinStack(int size)
        {
            _array = new int[size];
            _index = 0;
        }

        public void Push(int item)
        {
            if (_index >= _array.Length)
            {
                throw new IndexOutOfRangeException("pushing index error");
            }

            if(_index == 0)
            {
                _minIndex = 0;
            }
            else
            {
                if(item < _array[_minIndex])
                {
                    _minIndex = _index;
                }
            }

            _array[_index] = item;
            _index++;
        }

        public int Min()
        {
            return _array[_minIndex];
        }

        public int Peek()
        {
            return _array[_index - 1];
        }

        public int Pop()
        {
            if (_index - 1 < 0)
            {
                throw new IndexOutOfRangeException("popping index error");
            }

            int item = _array[_index - 1];
            _index--;
            return item;
        }

        public bool IsEmpty()
        {
            if (_index == 0)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return String.Join(",", _array.Take(_index));
        }
    }
}
