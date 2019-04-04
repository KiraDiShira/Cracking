* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Tree

* [Definition](#definition)
* [Depth-first](#depth-first)
* [Breath-first](#breath-first)

## Definition

A **Tree** is:

* empty, or
* a node with:
  * a key, and
  * a list of child trees.
  * (optional) a parent
  
For binary tree, node contains:
* key
* left
* right
* (optional) parent

```c#
public class Tree<T>
{
    public T Key { get; set; }
    public Tree<T> Left { get; set; }
    public Tree<T> Right { get; set; }
}
```


**Height**: maximum depth of subtree node and farthest leaf

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree1.PNG" />

**Size**: number of nodes

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree2.PNG" />

```c#
public class TreeApi<T>
{
    public int Height(Tree<T> tree)
    {
        if (tree == null)
        {
            return 0;
        }

        return 1 + Math.Max(Height(tree.Left), Height(tree.Right));
    }

    public int Size(Tree<T> tree)
    {
        if (tree == null)
        {
            return 0;
        }

        return 1 + Size(tree.Left) + Size(tree.Right);
    }
```

### Binary Search Tree

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree2_b.PNG" />

The binary search tree it's binary: it has at most two children at each node.

And we have the property that at the root node, the value of that root node is greater than or equal to all of the nodes in the left child, and it's less than the nodes in the right child. 

The binary search tree allows you to search quickly. For instance, if we wanted to search in this tree for Tony, we could start at Les. Notice that we are greater than Les, so therefore, we're going to go right. We're greater than Sam so we'll go right. We're less than Violet so we'll go left and then we find Tony. And we do that in just four comparisons. It's a lot like a binary search in a sorted array.

## Tree Traversal

Often we want to visit the nodes of a tree in a particular order. For example, print the nodes of the tree.

**Depth-first**: We completely traverse one sub-tree before exploring a sibling sub-tree.

**Breadth-first**: We traverse all nodes at one level before progressing to the next level.

### Depth-first

In order traversal has a meaning only for binary tree, because every tree has maximum two child and between them I can insert the print procedure.

Depth-first search now is implemented in a recursive way, but it can also be implemented with an iterative alghoritm and a stack.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree3.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree4.PNG" />

```c#
public void InOrderRecursiveTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }

    InOrderRecursiveTraversal(tree.Left);
    Console.WriteLine(tree.Key);
    InOrderRecursiveTraversal(tree.Right);
}

public void InOrderIterativeTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }
    
    var stack = new Stack<Tree<T>>();
    stack.Push(tree);
    Tree<T> actualTree = tree;

    while (stack.Count != 0)
    {
        if (actualTree.Left != null)
        {
            stack.Push(actualTree.Left);
            actualTree = actualTree.Left;
        }
        else
        {
            actualTree = stack.Pop();
            Console.WriteLine(actualTree.Key);

            if (actualTree.Right != null)
            {
                stack.Push(actualTree.Right);
                actualTree = actualTree.Right;
            }
        }   
    }
}

```

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree5.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree6.PNG" />

```c#
public void PreOrderRecursiveTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }

    Console.WriteLine(tree.Key);
    PreOrderRecursiveTraversal(tree.Left);
    PreOrderRecursiveTraversal(tree.Right);
}

public void PreOrderIterativeTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }

    var stack = new Stack<Tree<T>>();
    stack.Push(tree);

    while (stack.Count != 0)
    {
        Tree<T> actualTree = stack.Pop();
        Console.WriteLine(actualTree.Key);
        if (actualTree.Right != null)
        {
            stack.Push(actualTree.Right);
        }
        if (actualTree.Left != null)
        {
            stack.Push(actualTree.Left);
        }
    }
}

```

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree7.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree8.PNG" />

```c#
public void PostOrderRecursiveTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }
    
    PostOrderRecursiveTraversal(tree.Left);
    PostOrderRecursiveTraversal(tree.Right);
    Console.WriteLine(tree.Key);
}

public void PostOrderIterativeTraversal(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }

    var reverseStack = new Stack<Tree<T>>();
    reverseStack.Push(tree);
    var postOrderStack = new Stack<T>();

    while (reverseStack.Count != 0)
    {
        Tree<T> actualTree = reverseStack.Pop();
        postOrderStack.Push(actualTree.Key);
        if (actualTree.Left != null)
        {
            reverseStack.Push(actualTree.Left);
        }
        if (actualTree.Right != null)
        {
            reverseStack.Push(actualTree.Right);
        }
       
    }

    while (postOrderStack.Count != 0)
    {
        Console.WriteLine(postOrderStack.Pop());
    }
}
```
There is also a PostOrder iterative single stack implementation:

https://www.geeksforgeeks.org/iterative-postorder-traversal-using-stack/

### Breath-first

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree9.PNG" />
<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Tree/Images/tree10.PNG" />

```c#
public void BreathFirst(Tree<T> tree)
{
    if (tree == null)
    {
        return;
    }

    Queue<Tree<T>> queue = new Queue<Tree<T>>();
    queue.Enqueue(tree);

    while (queue.Count != 0)
    {
        Tree<T> actualTree = queue.Dequeue();
        Console.WriteLine(actualTree.Key);
        if (actualTree.Left != null)
        {
            queue.Enqueue(actualTree.Left);
        }
        if (actualTree.Right != null)
        {
            queue.Enqueue(actualTree.Right);
        }
    }
}
```
