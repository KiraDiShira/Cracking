<div style="page-break-before: always !important;"/>

# Singly-Linked Lists

* [Definition](#definition)
* [List Api](#list-api)
* [Times for Some Operations](#times-for-some-operations)
* [Other operations](#other-operations)
* [Summary](#summary)

## Definition

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll1b.PNG" />

It's possible to have also a **Tail**: a pointer to the last node.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/Tail.PNG" />

```c#

public class Node
{
    public object Key { get; set; }
    public Node Next { get; set; }
}

```

```c#

public class SinglyLinkedList
{
    public Node Head { get; set; }
    public Node Tail { get; set; }
    
    public void PushFront(object key)
    {
           ...

```

<div style="page-break-before: always !important;"/>

## List API

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll2b.PNG" />


## Times for Some Operations

### PushFront

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll3.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll4.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll5.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice1.PNG" />

```c#

public void PushFront(object key)
{
    var node = new Node()
    {
        Key = key,
        Next = Head
    };

    Head = node;

    //Se la coda non punta ad alcuna parte vuol dire che prima la lista era vuota e 
    //il nuovo nodo è il primo nodo, quindi faccio puntare la tail all'unico elemento inserito
    if (Tail == null)
    {
        Tail = Head;
    }
}

```

<div style="page-break-before: always !important;"/>

### PopFront

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll6.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll7.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll8.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice2.PNG" />

```c#

public void PopFront()
{
    if (Head == null)
    {
        throw new Exception("ERROR: empty list");
    }

    Head = Head.Next;
    if (Head == null) //significa che la lista è vuota, quindi setto a null anche la tail
    {
        Tail = null;
    }
}

```

### Times for PushBack (no tail)

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll9.PNG" />

### Times for PopBack (no tail)

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll10.PNG" />

### Times for PushBack (with tail)

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll11.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll12.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll13.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll14.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice3.PNG" />

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
    }
    else
    {
        Tail.Next = node;
        Tail = node;
    }
}

```

### Times for PopBack (with tail)

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll15.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll16.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll17.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/sll18.PNG" />

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice4.PNG" />

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
    }
    else
    {
        Node currentNode = Head;

        //mi trova il penultimo elemento
        while (currentNode.Next.Next != null)
        {
            currentNode = currentNode.Next;
        }

        currentNode.Next = null;
        Tail = currentNode;
    }
}

```
### Add After

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice5.PNG" />

```c#

public void AddAfter(Node node, object key)
{
    var newNode = new Node()
    {
        Key = key,
        Next = node.Next
    };

    node.Next = newNode;

    if (Tail == node)
    {
        Tail = newNode;
    }
}

```

## Other operations

```c#

public object TopFront()
{
    if (Head == null)
    {
        throw new Exception("ERROR: empty list");
    }

    return Head.Key;
}

public object TopBack()
{
    if (Head == null)
    {
        throw new Exception("ERROR: empty list");
    }

    return Tail.Key;
}

public bool Find(object key)
{
    if (Head == null)
    {
        return false;
    }

    Node currentNode = Head;
    if (currentNode.Key.Equals(key))
    {
        return true;
    }

    //mi trova il ultimo elemento
    while (currentNode.Next != null)
    {
        currentNode = currentNode.Next;

        if (currentNode.Key.Equals(key))
        {
            return true;
        }
    }

    return false;
}

public void Erase(object key)
{
    if (Head == null)
    {
        throw new Exception("cannot delete");
    }

    if (Head.Key.Equals(key))
    {
        Head = Head.Next;
        if (Head == null)
        {
            Tail = null;
        }
        return;
    }

    Node currentNode = Head;
    Node previousNode = null;

    while (currentNode != null && !currentNode.Key.Equals(key))
    {
        previousNode = currentNode;
        currentNode = currentNode.Next;
    }

    if (currentNode == null) throw new Exception("cannot delete");

    //delete cur node
    previousNode.Next = currentNode.Next;

}

public bool Empty()
{
    if (Tail == null)
    {
        return true;
    }

    return false;
}

public void AddBefore(Node node, object key)
{
    if (Head == null)
            {
               return;
            }

    if (Head.Key.Equals(key))
    {
        PushFront(key);
        return;
    }

    Node currentNode = Head;
    Node previousNode = null;

    while (currentNode != null && !currentNode.Key.Equals(key))
    {
        previousNode = currentNode;
        currentNode = currentNode.Next;
    }

    if (currentNode == null)
    {
        return;
    }

    var newNode2 = new Node()
    {
        Key = key,
        Next = currentNode
    };

    previousNode.Next = newNode2;
}

public override string ToString()
{
    if (Head == null)
    {
        return "NULL";
    }

    string toString = Head.Key + "->";

    Node curr = Head;
    while (curr.Next != null)
    {
        curr = curr.Next;
        toString += curr.Key;
        toString += "->";
    }

    return toString;
}


```

## Summary

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/SinglyLinkedList/Images/codice6.PNG" />

If I need a reference to a node different from the `Head` or the `Tail` the running time is `O(n)`.

**PopBack** is `O(n)` because I need a reference to last minus one node.

**Find** and **Erase** are linear searching operation so they are `O(n)`

**AddBefore** is `O(n)` because I need a reference to the node previuous to my target node.
