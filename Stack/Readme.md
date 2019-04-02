* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Stack

* [Definition](#definition)
* [Balanced Brackets example](#balanced-brackets-example)
* [Stack Implementation with Array](#stack-implementation-with-array)
* [Stack Implementation with Linked List](#stack-implementation-with-linked-list)
* [Summary](#summary)

## Definition

**Stack**: Abstract data type with the following operations:

* `Push(Key)`: adds key to collection
* `Key Top()`: returns most recently-added key
* `Key Pop()`: removes and returns most recently-added key
* `Boolean Empty()`: are there any elements?

Stack is useful when you need to be keep track of what has happened in a particular order.

Stacks can be implemented with either an **array** or a **linked list**.

Stacks are ocassionaly known as **LIFO queues**.

Each stack operation is `O(1)`: Push, Pop, Top, Empty.

## Balanced Brackets example

**Balanced**: 
* `([])[]()`
* `((([([])]))())`

**Unbalanced**:
* `([]]()`
* `][`

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Stack/Images/st1.PNG" />

```c#

private static IDictionary<char, char> _brackets = new Dictionary<char, char>()
{
    { '(', ')'},
    { '[', ']'},
    { '{', '}'},
};

static bool IsBalanced(string source)
{
    Stack<char> stack = new Stack<char>();
    foreach (char character in source)
    {
        if (IsOpenBracket(character))
        {
            stack.Push(character);
        }
        else
        {
            if (IsStackEmpty(stack))
            {
                return false;
            }

            char top = stack.Pop();
            if (_brackets.ContainsKey(top) && character != _brackets[top])
            {
                return false;
            }
        }
    }

    return IsStackEmpty(stack);
}

private static bool IsStackEmpty(Stack<char> stack)
{
    return stack.Count == 0;
}

private static bool IsOpenBracket(char character)
{
    return _brackets.ContainsKey(character);
}

```

## Stack Implementation with Array

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Stack/Images/arr1.PNG" />

```c#

public class MyStack<T>
{
    private int _index;
    private readonly T[] _array;

    public MyStack(int size)
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

```
## Stack Implementation with Linked List

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Stack/Images/sst1.PNG" />

One limitation of the array is that we have a maximum size, based on the array we initially allocated. 

The other potential problem is that we have potentially wasted space. So if we allocated a very large array, to allow a possibly large stack, we didn't actually use much of it, all the rest of it is wasted.

 If we have a linked list, there's no a priori limit as to the number of elements you can add. As long as you have available memory, you can keep adding. There's an overhead though, like in the array, we have each element size, is just big enough to store our key. Here we've got the overhead of storing a pointer as well. On the other hand there's no wasted space in terms of allocated space that isn't actually being used. 

```c#

public class LinkedListStack<T>
{
    private readonly SinglyLinkedList<T> _singlyLinkedList;

    public LinkedListStack()
    {
        _singlyLinkedList = new SinglyLinkedList<T>();
    }

    public void Push(T item)
    {
        _singlyLinkedList.PushFront(item);
    }

    public T Peek()
    {
        return _singlyLinkedList.TopFront();
    }

    public T Pop()
    {
        T top = _singlyLinkedList.TopFront();
        _singlyLinkedList.PopFront();
        return top;
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
