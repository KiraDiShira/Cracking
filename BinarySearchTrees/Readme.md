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

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst1.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst2.png" />

How we can implement this data structure?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst3.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst4.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst5.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst6.png" />

Sorted arrays can search but not update.

## Search Trees

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst7.png" />

The Search Tree Structure:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst8.png" />

X’s key is larger than the key of any descendent of its left child, and smaller than the key of any descendant of its right child.

## Basic Operations

### Find

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst9.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst10.png" />

```c#

    public class TreeNode
    {
        public int Key { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode Parent { get; set; }        
    }

```
if I want to return a null value when the key value is not found:

```c#

    public class BinarySearchTreeOperations
    {
        public TreeNode Find(int key, TreeNode root)
        {
            if (key == root.Key)
            {
                return root;
            }

            if (key < root.Key)
            {
                return Find(key, root.Left);
            }

            if (key > root.Key)
            {                
                return Find(key, root.Right);
            }

            throw new Exception("Illegal Find execution");
        }
```
If I want to return the closest value in the set when the key is not found:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst11.png" />

```c#

    public class BinarySearchTreeOperations
    {
        public TreeNode Find(int key, TreeNode root)
        {
            if (key == root.Key)
            {
                return root;
            }

            if (key < root.Key)
            {
                if (root.Left == null)
                {
                    return root;
                }

                return Find(key, root.Left);
            }

            if (key > root.Key)
            {
                if (root.Right == null)
                {
                    return root;
                }

                return Find(key, root.Right);
            }

            throw new Exception("Illegal Find execution");
        }

```

### Next - node with the next largest key

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst12.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst13.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst14.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst15.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst16.png" />

```c#

        public TreeNode Next(TreeNode node)
        {
            if (node.Right != null)
            {
                return LeftDescendant(node.Right);
            }

            return RightAncestor(node);
        }

        private TreeNode RightAncestor(TreeNode node)
        {
            //if node is the largest element in the tree
            if (node.Parent == null)
            {
                return null;
            }

            if (node.Key < node.Parent.Key)
            {
                return node.Parent;
            }

            return RightAncestor(node.Parent);
        }

        private TreeNode LeftDescendant(TreeNode node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return LeftDescendant(node.Left);
        }

```
### Range Search - A list of nodes with key between x and y

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst17.png" />

```c#

        public IList<TreeNode> RangeSearch(int x, int y, TreeNode root)
        {
            IList<TreeNode> range = new List<TreeNode>();

            TreeNode node = Find(x, root);

            while (node.Key <= y)
            {
                if (node.Key >= x)
                {
                    range.Add(node);
                }

                node = Next(node);
            }

            return range;
        }

```

### Insert - Adds node with key k to the tree

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst18.png" />

```c#
        public void Insert(int key, TreeNode root)
        {
            var parent = Find(key, root);

            var nodeToInsert = new TreeNode()
            {
                Key = key,
                Parent = parent
            };

            if (key < parent.Key)
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

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst19.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst20.png" />

La croce rossa è il nodo da cancellare, X invece è il Next

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst21.png" />

```c#
        public void Delete(TreeNode nodeToDelete)
        {         
            if (nodeToDelete.Right == null)
            {
                if (nodeToDelete.Parent.Key > nodeToDelete.Key)
                {
                    nodeToDelete.Parent.Left = nodeToDelete.Left;
                }
                else
                {
                    nodeToDelete.Parent.Right = nodeToDelete.Left;
                }
            }
            else
            {
                TreeNode next = Next(nodeToDelete);
                nodeToDelete.Key = next.Key;
                next.Key = next.Right.Key;
                next.Left = next.Right.Left;
                next.Parent = next.Right.Parent;
                next.Right = next.Right.Right;
            }
        }
  ```

Nota che questa implementazione non copre il caso in cul il *node to delete* sia la root.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst22.png" />

## Balance

How long do Binary Search Tree operations take?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst23.png" />

We Want left and right subtrees to have approximately the same size. Suppose perfectly balanced:
- Each subtree half the size of its parent.
- After log2(n) levels, subtree of size 1.
- Operations run in O(log(n)) time.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst24.png" />

Solution: rebalancing. Rearrange tree to maintain balance.

Problem: How do we rearrange tree while maintaining order? With **rotations**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst25.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/BinarySearchTrees/Images/bst26.png" />

```c#
public void RotateRight(TreeNode x)
{
    TreeNode xParent = x.Parent;
    TreeNode y = x.Left;
    TreeNode b = y.Right;
    y.Parent = x.Parent;

    if (xParent.Key > y.Key)
    {
        xParent.Left = y;
    }
    else
    {
        x.Parent.Right = y;
    }
    x.Parent = y;
    y.Right = x;
    b.Parent = x;
    x.Left = b;
}

```

How to keep a tree balanced. AVL trees.
