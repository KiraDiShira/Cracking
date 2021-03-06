* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Priority Queues

* [Definition](#definition)
* [Naive implementation](#naive-implementation)
* [Priority queue: heaps](#priority-queue-heaps)
* [Basic operations](#basic-operations)
* [Complete binary tree](#complete-binary-tree)
* [Code](#code)
* [Heap sort](#heap-sort)
* [Summary](#summary)
* [Building a heap](#building-a-heap)
* [Partial sorting](#partial-sorting)
  
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

A binary heap is one of the most common ways of implementing a priority queue.

So just by definition a **max binary heap** is a binary tree (each node has zero, one, or two children) where the value of each the node is at least the value of all its children. 

Example:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/bh1.PNG" />

Not an example:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/bh2.PNG" />

## Basic operations

### Get Max

The maximum value is stored in the root of the tree.

To implement GetMax, we just return the value at the root of our tree: `O(1)`
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/getmax.PNG" />

### Insert

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/insert1.PNG" />

- attach a new node to any leaf

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/insert2.PNG" />

- this may violate the heap property, as in this case.
- to fix this, we let the new node **sift up**: swap the problematic node with its parent until the property is satisfied.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/insert3.PNG" />

While sift up the heap property is violated on at most one edge of our binary tree. 

This in particular implies that the number of swaps required is at most the height of this tree. Which in turn means that the running time of insertion procedure, as well as the running time of the sifting up procedure, is `O(tree height)`.

### Extract Max

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/extrMax1.PNG" />

- replace the root with any leaf

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/extrMax2.PNG" />

- this may violate the heap property, as in this case (on two edges).
- to fix it, we let the problematic node **sift down**: we swap the problematic node with larger child until the heap property is satisfied.
- we swap with the larger child which automatically fixes one of the two bad edges

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/extrMax3.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/extrMax4.PNG" />

the total running time of the extract max as well as the sift down procedures is `O(tree height)`.

### Change priority

Change the priority and let the changed element sift up or down depending on whether its priority decreased or increased.

Running time: `O(tree height)`.

### Remove

Change the priority of the element to `+Inf`, let it sift up, and then extract maximum.

Running time: `O(tree height)`.

## Complete binary tree

Most of operations are `O(tree height)` so our goal is to keep the tree shallow.

A binary tree is **complete** if all its levels are filled except possibly the last one which is filled from left to right.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt1.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt2.PNG" />

**First advantage, low height**:

A complete binary tree with n nodes has height at most `O(log n)`.

Proof:

- Complete the last level to get a full binary tree on `n' >= n` nodes and the same number of levels `l`

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt3.PNG" />

- Note that `n' <= 2n`
- Then n' = 2^l - 1 and hence: `l = log_2 (n' + 1) <= log_2 (2n + 1) = O(log(n))`

**Second advantage, store as array**:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt4.PNG" />

What do we pay for these advantages?

We need to keep the tree complete.

Which binary heap operations modify the shape of the tree?

Only `Insert` and `ExtractMax` (`Remove` changes the shape by calling `ExtractMax`).

To keep the tree complete while insert and element, insert it as a leaf in the leftmost vacant position in the last level and let it sift up.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt5.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt6.PNG" />

To extract the maximum value, replace the root by the last leaf and let it  sift down

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt7.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/cbt8.PNG" />

## Code

```c#

using System;
using System.Linq;

namespace BinaryHeapImplementation
{
    public class Heap
    {
        private readonly int[] _array;

        public int Size { get; set; }
        public int MaxSize => _array.Length;

        public int this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public Heap(int[] array, int size)
        {
            _array = array;
            Size = size;
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
                Swap(index, largest);
                SiftDown(largest);
            }
        }

        private void SiftUp(int index)
        {
            while (index > 0 && _array[Parent(index)] < _array[index])
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        public void Insert(int key)
        {
            if (Size == MaxSize)
            {
                throw new Exception("error");
            }

            _array[Size] = key;
            SiftUp(Size);
            Size++;
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

        private void Swap(int index1, int index2)
        {
            int index1value = _array[index1];
            _array[index1] = _array[index2];
            _array[index2] = index1value;
        }

        public override string ToString()
        {
            return $"{nameof(_array)}: {String.Join(",", _array.Take(Size))}";
        }
    }
}

```

## Heap sort

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs1.PNG" />

The resulting algorithm is comparison-based and has running time `O(n * log(n))`, hence asymptotically optimal.

Not in place: uses additional space to store the priority queue.

We can implemet an in place Heap Sort algorithm. For this, we will first turn a given array into a heap by permuting its elements.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs2.PNG" />

Nel ciclo for si parte dal floor di n/2 perchè non si stanno considerando le foglie dell'heap per cui sicuramente la proprietà di un max binary heap è soddisfatta (le foglie non hanno figli, e quindi non possono avere figli il cui valore sia maggiore del proprio)

The running time is `O(n log(n))`.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs3.PNG" />


## Summary

Heap Sort:

- It is in place. 
- running time is `O(n log(n))`. It is asymptotically optimal. So, this makes it a good alternative to the quick sort algorithm. 

So, in practice quicksort is usually faster, it is still faster. However, the heap sort algorithm has worst case running time n log n. While the quick sort algorithm has average case running time n log n. For this reason, a popular approach and practice is the following. It is called **IntraSort algorithm**. You first run quick sort algorithm. If it turns out the be slow, I mean, if the recursion deep, exceeds c log n for some constant, c, then you stop the current call to quick sort algorithm and switch to heap sort algorithm, which is guaranteed to have running time n log n. So, in this case, in this implementation, your algorithm usually, in most cases it works like quick sort algorithm. And even in these unfortunate cases where it works in larger, where quick sort has running time larger than n log n, you stop it in the right point of time and switch to HeapSort. So, this gives an algorithm which in many cases behaves like quick sort algorithm, and it has worst case running time `O(n log(n))`.

## Building a heap

The running time of BuildHeap is `O(n log(n))` since we call SiftDown `O(n)` nodes.

Note:
- if a node is already close to the leaves, the sifting it down is fast.
- we have many suche nodes.

Was our estimate of the running time of BuildHeap too pessimistic?

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs4.PNG" />

l'ultimo <= è dovuto al fatto che la sommatoria procede sino all'infinito e non si farma a log n. Il 2 invece:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs5.PNG" />

Our new estimate for the running time of the BuildHeap procedure does not actually improve the running time of the HeapSort algorithm. Because the HeapSort algorithm first builds a heap, and now we know that it can be done in linear time, but then we need to extract max n minus 1 times.

So we still have n log n time, and actually we cannot do better than n log n asymptotically. We already know this, because it is a comparison-based algorithm. 

However, this helps to solve a different problem faster than naively:

## Partial sorting

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs6.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/PriorityQueues/Images/hs7.PNG" />
