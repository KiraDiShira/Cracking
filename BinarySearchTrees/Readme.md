* [Index](https://github.com/KiraDiShira/Cracking/blob/master/README.md#cracking)

# Binary Search Trees

* [Introduction](#introduction)
* [Search Trees](#search-trees)
* [Basic Operations](#basic-operations)
* [Balance](#balance)

## Introduction

- Find all words that start with some given string
- Find all emails received in a given period.
- Find the person in your class whose height is closest to yours

All of these are example of **local search problems**:

A **Local Search Datastructure** stores a number of elements each with a key coming from an ordered set. It supports operations:

- **RangeSearch(x, y)**: Returns all elements with keys between x and y.
- **NearestNeighbors(z)**: Returns the element with keys on either side of z
- **Insert(x)**: Adds a element with key x
- **Delete(x)**: Removes the element with key x

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst1.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst2.PNG" />

How we can implement this data structure?

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst3.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst4.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst5.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst6.PNG" />

## Search Trees

Sorted arrays can search but not update.

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst7.PNG" />

The Search Tree Structure:

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst8.PNG" />

X’s key is larger than the key of any descendent of its left child, and smaller than the key of any descendant of its right child.

## Basic Operations

### Find

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst10.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst9.PNG" />

```c#

public class SearchTree<T>
{
    public T Key { get; set; }
    public SearchTree<T> Left { get; set; }
    public SearchTree<T> Right { get; set; }
    public SearchTree<T> Parent { get; set; }
}

```
if I want to return a null value when the key value is not found:

```c#

public class SearchTreeOperations<T> where T : IComparable<T>
{
    public SearchTree<T> Find(T key, SearchTree<T> root)
    {
        if (root == null)
        {
            return null; //key value not found
        }

        if (key.Equals(root.Key))
        {
            return root;
        }

        if (key.CompareTo(root.Key) < 0)
        {
            return Find(key, root.Left);
        }

        if (key.CompareTo(root.Key) > 0)
        {
            return Find(key, root.Right);
        }

        throw new Exception("Illegal Find execution");
    }
}
```
If I want to return the closest value in the set when the key is not found:

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst11.PNG" />

```c#

public class SearchTreeOperations<T> where T : IComparable<T>
{
    public SearchTree<T> Find(T key, SearchTree<T> root)
    {      
        if (key.Equals(root.Key))
        {
            return root;
        }

        if (key.CompareTo(root.Key) < 0)
        {
            if (root.Left == null)
            {
                return root;
            }

            return Find(key, root.Left);
        }

        if (key.CompareTo(root.Key) > 0)
        {
            if (root.Right == null)
            {
                return root;
            }

            return Find(key, root.Right);
        }

        throw new Exception("Illegal Find execution");
    }
}

```

### Next - node with the next largest key

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst12.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst13.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst14.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst15.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst16.PNG" />

```c#

public SearchTree<T> Next(SearchTree<T> node)
{
    if (node.Right != null)
    {
        return LeftDescendant(node.Right);
    }

    return RightAncestor(node);
}

private SearchTree<T> RightAncestor(SearchTree<T> node)
{
    //if node is the largest element in the tree
    if (node.Parent == null)
    {
        return null;
    }

    if (node.Key.CompareTo(node.Parent.Key) < 0)
    {
        return node.Parent;
    }

   return RightAncestor(node.Parent);
}

private SearchTree<T> LeftDescendant(SearchTree<T> node)
{
    if (node.Left == null)
    {
        return node;
    }

    return LeftDescendant(node.Left);
}

```
### Range Search - A list of nodes with key between x and y

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst17.PNG" />

```c#

public IList<SearchTree<T>> RangeSearch(T x, T y, SearchTree<T> root)
{
    IList<SearchTree<T>> range = new List<SearchTree<T>>();

    SearchTree<T> node = Find(x, root);

    while (node.Key.CompareTo(y) <= 0)
    {
        if (node.Key.CompareTo(x) >= 0)
        {
            range.Add(node);
        }

        node = Next(node);
    }

    return range;
}

```

### Insert - Adds node with key k to the tree

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst18.PNG" />

```c#
public void Insert(T key, SearchTree<T> root)
{
    var parent = Find(key, root);

    var nodeToInsert = new SearchTree<T>()
    {
        Key = key,
        Parent = parent
    };

    if (key.CompareTo(parent.Key) < 0)
    {
        parent.Left = nodeToInsert;
    }
    else
    {
        parent.Right = nodeToInsert;
    }
}
```

### Delete

## Balance

How long do Binary Search Tree operations take?

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst19.PNG" />

We Want left and right subtrees to have approximately the same size. Suppose perfectly balanced:
- Each subtree half the size of its parent.
- After log2(n) levels, subtree of size 1.
- Operations run in O(log(n)) time.

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst20.PNG" />

Solution: rebalancing. Rearrange tree to maintain balance.

Problem: How do we rearrange tree while maintaining order? With **rotations**

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst21.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/BinarySearchTrees/Images/bst22.PNG" />

How to keep a tree balanced. AVL trees.
