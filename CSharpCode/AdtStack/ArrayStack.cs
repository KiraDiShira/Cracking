using System;
using System.Linq;

namespace AdtStack
{
    public class ArrayStack<T>
    {
        private int _index;
        private readonly T[] _array;

        public ArrayStack(int size)
        {
            _array = new T[size];
            _index = 0;
        }

        public void Push(T item)
        {
            if (_index >= _array.Length)
            {
                throw new IndexOutOfRangeException("pushing index error");
            }

            _array[_index] = item;
            _index++;
        }

        public T Peek()
        {
            return _array[_index - 1];
        }

        public T Pop()
        {
            if (_index - 1 < 0)
            {
                throw new IndexOutOfRangeException("popping index error");
            }

            T item = _array[_index - 1];
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
