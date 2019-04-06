* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Priority Queues

* [Definition](#definition)
* [Naive implementation](#naive-implementation)
* [Priority queue: heaps](#priority-queue-heaps)
* [Summary](#summary)
  
## Definition

**Priority queue** is an abstract data type supporting the following main operations:

* `Insert(p)` adds a new element with priority p
* `ExtractMax()` extracts an element with maximum priority

**Additional operations**:

* `Remove(it)` removes an element pointed by an iterator `it`
* `GetMax()` returns an element with maximum priority (without changing the set of elements)
* `ChangePriority(it, p)` changes the priority of an element pointed by `it` to `p`

**Algorithms that Use Priority Queues**

* `Dijkstra’s algorithm`: finding a shortest path in a graph
* `Prim’s algorithm`: constructing a minimum spanning tree of a graph
* `Huffman’s algorithm`: constructing an optimum prefix-free encoding of a string
* `Heap sort`: sorting a given sequence

## Naive implementation

**Unsorted array/list**

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/unsorted.PNG" />

- Insert(e)
  - add e to the end 
  - running time O(1)
- Extract Max()
  - scan the array/list 
  - running time O(n)

**Sorted array**
 
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/sortedArray.PNG" />
 
- Extract Max()
  - extract the last element: O(1)
- Insert(e)
  - find a position for e (O(log(n) by using binary search)), shift all elements to the right by 1 (O(n)), insert e (O(1))
  - running time O(n)

**Sorted list**

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/sortedlist.PNG" />

- Extract Max()
  - extract the last element: O(1)
- Insert(e)
  - find a position for e (O(n)), insert e (O(1))
  - running time: O(n)

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/pq1.PNG" />

## Priority queue: heaps


```c#

using System;
using System.Linq;

namespace HeapCoursera
{
    public class Heap
    {
        private readonly int[] _array;

        public int Size { get; set; }
        public int Length => _array.Length;

        public int this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public Heap(int[] array)
        {
            _array = array;
            Size = array.Length;
        }

        private static int Right(int index)
        {
            return 2 * index + 2;
        }

        private static int Left(int index)
        {
            return 2 * index + 1;
        }

        private static int Parent(int index)
        {
            return (index - 1) / 2;
        }

        //MaxHeapify
        private void SiftDown(int index)
        {
            int largest = index;
            int left = Left(index);
            if (left < Size && _array[left] > _array[index])
            {
                largest = left;
            }
            int right = Right(index);
            if (right < Size && _array[right] > _array[largest])
            {
                largest = right;
            }
            if (largest != index)
            {
                Exchange(index, largest);
                SiftDown(largest);
            }
        }

        private void SiftUp(int index)
        {
            while (index > 0 && _array[Parent(index)] < _array[index])
            {
                Exchange(index, Parent(index));
                index = Parent(index);
            }
        }

        public void Insert(int key)
        {
            if (Size == Length)
            {
                throw new Exception("error");
            }

            Size++;
            _array[Size] = key;
            SiftUp(Size);
        }

        public int ExtractMax()
        {
            if (Size <= 0)
            {
                throw new Exception("heap underflow");
            }

            int max = _array[0];

            _array[0] = _array[Size - 1];
            Size--;
            SiftDown(0);

            return max;
        }

        public int Max()
        {
            return _array[0];
        }

        public void Remove(int index)
        {
            _array[index] = int.MaxValue;
            SiftUp(index);
            ExtractMax();
        }

        public void ChangePriority(int index, int priority)
        {
            var oldPriority = _array[index];
            _array[index] = priority;

            if (priority > oldPriority)
            {
                SiftUp(index);
            }
            else
            {
                SiftDown(index);
            }
        }

        private  void Exchange(int index, int largest)
        {
            int heapIndex = _array[index];
            _array[index] = _array[largest];
            _array[largest] = heapIndex;
        }

        public override string ToString()
        {
            return $"{nameof(_array)}: {String.Join(",", _array.Take(Size - 1))}";
        }
    }
}

```
## Summary

So, once again, let me state some properties of the resulting algorithm which is called Heap Sort. It is in place. it doesn't need any additional memory. Everything is happening inside the given array A. So this is in advantage of this algorithm. Another advantage is that its running time is n log n. It is as simple, as it is optimal. So, this makes it a good alternative to the quick sort algorithm. So, in practice presort is usually faster, it is still faster. However, the heap sort algorithm has worst case running time n log n. While the quick sort algorithm has average case running time n log n. For this reason, a popular approach and practice is the following. It is called **IntraSort algorithm**. You first run quick sort algorithm. If it turns out the be slow, I mean, if the recursion deep, exceeds c log n for some constant, c, then you stop the current call to quick sort algorithm and switch to heap sort algorithm, which is guaranteed to have running time n log n. So, in this case, in this implementation, your algorithm usually, in most cases it works like quick sort algorithm. And even in these unfortunate cases where it works in larger, where quick sort has running time larger than n log n, you stop it in the right point of time and switch to HeapSort. So, this gives an algorithm which in many cases behaves like quick sort algorithm, and it has worst case running time. 
