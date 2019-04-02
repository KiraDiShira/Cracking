* [Index](https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/README.md#project-title)

# Doubly-Linked Lists

* [Definition](#definition)
* [Times for Some Operations](#times-for-some-operations)
* [Summary](#summary)

## Definition

There is a way to make `popping the back` and `adding before` cheap: we need a reference to the previous node. 

```c#

public class Node
{
    public object Key { get; set; }
    public Node Next { get; set; }
    public Node Prev { get; set; }
}

```

```c#

public class DoublyLinkedList
{
    public Node Head { get; set; }
    public Node Tail { get; set; }

    ...

```

**Doubly-Linked Lists**:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll1.PNG" />

## Times for some operations

### PopBack

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll2.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll3.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll4.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll5.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll6.PNG" />

```c#

public void PopBack()
{
    if (Head == null)
    {
        throw new Exception("ERROR: empty list");
    }

    if (Head == Tail)
    {
        Head = null;
        Tail = null;
        return;
    }

    Tail = Tail.Prev;
    Tail.Next = null;
}

```

### PushBack

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll7.PNG" />

```c#

public void PushBack(object key)
{
    var node = new Node()
    {
        Key = key,
        Next = null
    };

    if (Tail == null)
    {
        Head = node;
        Tail = node;
        node.Prev = null;
    }
    else
    {
        node.Prev = Tail;
        Tail.Next = node;
        Tail = node;
    }
}

```

### AddAfter

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll8.PNG" />

```c#

public void AddAfter(Node node, object key)
{
    var newNode = new Node()
    {
        Key = key,
        Next = node.Next,
        Prev = node
    };

    node.Next = newNode;

    //devo aggiornare il nodo precedente a quello successivo a newnode
    if (newNode.Next != null)
    {
        newNode.Next.Prev = newNode;
    }

    if (Tail == node)
    {
        Tail = newNode;
    }
}

```

### AddBefore

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll9.PNG" />

```c#

public void AddBefore(Node node, object key)
{
    if (Head == null)
    {
        return;
    }

    var newNode = new Node()
    {
        Key = key,
        Next = node,
        Prev = node.Prev
    };

    node.Prev = newNode;

    if (newNode.Prev != null)
    {
        newNode.Prev.Next = newNode;
    }

    if (Head == node)
    {
        Head = newNode;
    }
}

```

### Summary

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/DoublyLinkedList/Images/dll10.PNG" />
