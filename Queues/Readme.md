* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Queues

* [Definition](#definition)
* [Queue Implementation with Linked List](#queue-implementation-with-linked-list)
* [Queue Implementation with Array](#queue-implementation-with-array)

## Definition

**Queue**: Abstract data type with the following operations:

* `Enqueue(Key)`: adds key to collection
* `Key Dequeue()`: removes and returns least recently-added key
* `Boolean Empty()`: are there any elements?

Queues are known as **FIFO queues**.

Queue is useful when you need to be keep track of what has happened in a particular order.

Queues can be implemented with either an **array** or a **linked list**.

Each Queue operation is `O(1)`: Enqueue, Dequeue, Empty.

One distinction between the array and the linked list implementation, is that in the array implementation, we have a maximum size that the queue can grow to. Maybe you want that in which case it's fine, but if you don't know a priori how long the queue you need is going to be an array is a bad choice. And any amount that is unused is wasted space. In a queue that's implemented with a linked list, it can get arbitrarily large as long as there's available memory. The downside is, every element you have to pay for another pointer. 

## Queue Implementation with Linked List

With a linked list, where you have a head and a tail pointer. 

Enqueue: we are going to push to the back of the linked list.

Dequeue: that's just an implementation of popping from the front.

```c#

public class LinkedListQueue<T>
{
    private readonly SinglyLinkedList<T> _singlyLinkedList;

    public LinkedListQueue()
    {
        _singlyLinkedList = new SinglyLinkedList<T>();
    }

    public void Enqueue(T key)
    {
        _singlyLinkedList.PushBack(key);
    }

    public T Dequeue()
    {
        T key = _singlyLinkedList.TopFront();
        _singlyLinkedList.PopFront();
        return key;
    }

    public bool IsEmpty()
    {
        return _singlyLinkedList.Empty();
    }

    public override string ToString()
    {
        return _singlyLinkedList.ToString();
    }
}

```

What would happen if we use List.PushFront to implement Enqueue and List.TopBack and List.PopBack to implement Dequeue?

If the linked-list is implemente as a doubly-linked-list there is no problem.
If the linked-list is implemente as a singly-linked-list, Dequeue would be `O(n)`.

## Queue Implementation with Array

We could add at the end and then pop from the front. Then enqeueing is easy, but dequeuing would be an expensive `O(n)` operation. And we want enqeueing to be `O(1)`. We can do that, in a fashion I'll show you right now which is basically keeping track of sort of the array as a circular array: so we need a write index and a reading index.

```c#

public class ArrayQueue<T>
{
    private readonly T[] _array;
    private int _readingIndex;
    private int _writingIndex;

    public ArrayQueue(int size)
    {
        _readingIndex = 0;
        _writingIndex = 0;
        _array = new T[size];
    }

    public void Enqueue(T key)
    {
        _array[_writingIndex] = key;
        _writingIndex++;
        if (_writingIndex == _array.Length)
        {
            _writingIndex = 0;
        }

        if (_writingIndex == _readingIndex)
        {
            throw new Exception("_writing index is equal to _reading index: I can't know if queue is empty");
        }
    }

    public T Dequeue()
    {
        T key = _array[_readingIndex];
        _readingIndex++;
        if (_readingIndex == _array.Length)
        {
            _readingIndex = 0;
        }
        return key;
    }

    public bool IsEmpty()
    {
        return _readingIndex == _writingIndex;
    }

    public override string ToString()
    {
        if (_writingIndex >= _readingIndex)
        {
            return String.Join(",", _array.SubArray(_readingIndex, _writingIndex - _readingIndex));
        }

        return String.Join(",", _array.SubArray(_readingIndex, _array.Length - _readingIndex)
            .Concat(_array.SubArray(0, _writingIndex)));
    }
}

```
