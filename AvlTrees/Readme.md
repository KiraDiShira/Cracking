* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# AVL Trees

* [Introduction](#introduction)
* [Avl Tree implementation](#avl-tree-implementation)

## Introduction

AVL trees are a specific way of maintaining balance in your binary search tree. We want to maintain balance and we need a way to measure balance.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl1.png" />

- Height is 1 if the node is a leaf
- Height = 1 + max(node.Left.Height, node.Right.height) otherwise

In order to make use of this height we are going to add a new field to our nodes:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl2.png" />

And note that we are going to have to do some work to insure that this height field is actually kept up to date. 

For the balance we want size of subtrees roughly the same.

**AVL Property**: AVL trees maintain the following property: For all nodes N,

```
|N.Left.Height − N.Right.Height| ≤ 1
```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl3.png" />

We claim that this ensures balance.



We need to show that AVL property implies Height = O(log(n)).

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl4.png" />

We assumed that `𝑁(ℎ − 1) > 𝑁(ℎ − 2)`, so we can say that

```
𝑁(ℎ) > 1 + 𝑁(ℎ − 2) + 𝑁(ℎ − 2) = 1 + 2 ⋅ 𝑁(ℎ − 2) > 2 ⋅ 𝑁(ℎ − 2)
```

So we have:
```
𝑁(ℎ) > 2 ⋅ 𝑁(ℎ − 2)
```
We can try to solve this as a recurrence (note that 𝑁(0) = 1):

```
𝑁(ℎ) > 2 ⋅ 𝑁(ℎ − 2) > 2 ⋅ 2 ⋅ 𝑁(ℎ − 4) > 2 ⋅ 2 ⋅ 2 ⋅ 𝑁(ℎ − 6) > ⋯ > 2^(ℎ/2)
```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl5.png" />

Note that AVL trees with a minimum number of nodes are the worst case examples of AVL tree (cioè la configurazione AVL con minimo numero di nodi è quella di un albero AVL con massima altezza e minor numero possibile di nodi): every node’s subtrees differ in height by one. You can see examples of such trees below:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl6.png" />

If we consider worst case example:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl7.png" />

Thus, these worst-case AVL trees have height **ℎ = O(log 𝑛)**.

## Avl Tree implementation

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl8.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl9.png" />

We need a new insertion algorithm that involves rebalancing the tree to maintain the AVL property.

Note this example, for the same set of keys we have differents shape of the tree. And we want the one with the lowest height.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari1.png" />

We can achieve this if we use the *rotations*.

Let's define the *balance factor*

```
balance factor = height of the left subtree - height of the right subtree
```

If there is a unbalanced node (balance factor < -1 or balance factor > 1) rotations are performed to restore the AVL tree property.

There are four rotarions.

**LL rotation**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari2.png" />

**LR rotation**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari3.png" />

**RR rotation**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari4.png" />

**RL rotation**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari5.png" />

**LL rotation with more nodes**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari6.png" />

Note how the *Br* child moves.

The same concepts applies to *RR rotation* but in the opposite side.

**LR rotation with more nodes**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari7.png" />

Instead of apply two rotations we can use this formula: move *C* up and *A* as his right child.

What if we have more nodes?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari8.png" />

**Example**

if multiple nodes become unbalanced which one we should perform rotations?

Starting from inserted node, lookup the first unbalanced ancestor --> if we rotate this we will fix balance on the whole tree. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari9.png" />

*20* and *40* are unbalanced, and following the explained rule we need to rotate *40*. Starting from *40* the insertion path is left-right so this is a *left right rotation*.

What if the insertion path has depth more than two from the unbalanced node? which rotations should I apply? In this example I have RLL insertion.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/bari10.png" />

The rule say that we need to consider the first two steps from the unbalanced node, so we will apply a RL rotation.

### Insert and Delete code

```c#

using System; 
  
public class Node  
{  
    public int key, height;  
    public Node left, right;  
  
    public Node(int d)  
    {  
        key = d;  
        height = 1;  
    }  
}  
  
public class AVLTree  
{  
    Node root;  
  
    // A utility function to get height of the tree  
    int height(Node N)  
    {  
        if (N == null)  
            return 0;  
        return N.height;  
    }  
  
    // A utility function to  
    // get maximum of two integers  
    int max(int a, int b)  
    {  
        return (a > b) ? a : b;  
    }  
  
    // A utility function to right  
    // rotate subtree rooted with y  
    // See the diagram given above.  
    Node rightRotate(Node y)  
    {  
        Node x = y.left;  
        Node T2 = x.right;  
  
        // Perform rotation  
        x.right = y;  
        y.left = T2;  
  
        // Update heights  
        y.height = max(height(y.left), height(y.right)) + 1;  
        x.height = max(height(x.left), height(x.right)) + 1;  
  
        // Return new root  
        return x;  
    }  
  
    // A utility function to left  
    // rotate subtree rooted with x  
    // See the diagram given above.  
    Node leftRotate(Node x)  
    {  
        Node y = x.right;  
        Node T2 = y.left;  
  
        // Perform rotation  
        y.left = x;  
        x.right = T2;  
  
        // Update heights  
        x.height = max(height(x.left), height(x.right)) + 1;  
        y.height = max(height(y.left), height(y.right)) + 1;  
  
        // Return new root  
        return y;  
    }  
  
    // Get Balance factor of node N  
    int getBalance(Node N)  
    {  
        if (N == null)  
            return 0;  
        return height(N.left) - height(N.right);  
    }  
  
    Node insert(Node node, int key)  
    {  
        /* 1. Perform the normal BST rotation */
        if (node == null)  
            return (new Node(key));  
  
        if (key < node.key)  
            node.left = insert(node.left, key);  
        else if (key > node.key)  
            node.right = insert(node.right, key);  
        else // Equal keys not allowed  
            return node;  
  
        /* 2. Update height of this ancestor node */
        node.height = 1 + max(height(node.left),  
                            height(node.right));  
  
        /* 3. Get the balance factor of this ancestor  
        node to check whether this node became  
        Wunbalanced */
        int balance = getBalance(node);  
  
        // If this node becomes unbalanced, then  
        // there are 4 cases Left Left Case  
        if (balance > 1 && key < node.left.key)  
            return rightRotate(node);  
  
        // Right Right Case  
        if (balance < -1 && key > node.right.key)  
            return leftRotate(node);  
  
        // Left Right Case  
        if (balance > 1 && key > node.left.key)  
        {  
            node.left = leftRotate(node.left);  
            return rightRotate(node);  
        }  
  
        // Right Left Case  
        if (balance < -1 && key < node.right.key)  
        {  
            node.right = rightRotate(node.right);  
            return leftRotate(node);  
        }  
  
        /* return the (unchanged) node pointer */
        return node;  
    }  
  
    /* Given a non-empty binary search tree, return the  
    node with minimum key value found in that tree.  
    Note that the entire tree does not need to be  
    searched. */
    Node minValueNode(Node node)  
    {  
        Node current = node;  
  
        /* loop down to find the leftmost leaf */
        while (current.left != null)  
        current = current.left;  
  
        return current;  
    }  
  
    Node deleteNode(Node root, int key)  
    {  
        // STEP 1: PERFORM STANDARD BST DELETE  
        if (root == null)  
            return root;  
  
        // If the key to be deleted is smaller than  
        // the root's key, then it lies in left subtree  
        if (key < root.key)  
            root.left = deleteNode(root.left, key);  
  
        // If the key to be deleted is greater than the  
        // root's key, then it lies in right subtree  
        else if (key > root.key)  
            root.right = deleteNode(root.right, key);  
  
        // if key is same as root's key, then this is the node  
        // to be deleted  
        else
        {  
  
            // node with only one child or no child  
            if ((root.left == null) || (root.right == null))  
            {  
                Node temp = null;  
                if (temp == root.left)  
                    temp = root.right;  
                else
                    temp = root.left;  
  
                // No child case  
                if (temp == null)  
                {  
                    temp = root;  
                    root = null;  
                }  
                else // One child case  
                    root = temp; // Copy the contents of  
                                // the non-empty child  
            }  
            else
            {  
  
                // node with two children: Get the inorder  
                // successor (smallest in the right subtree)  
                Node temp = minValueNode(root.right);  
  
                // Copy the inorder successor's data to this node  
                root.key = temp.key;  
  
                // Delete the inorder successor  
                root.right = deleteNode(root.right, temp.key);  
            }  
        }  
  
        // If the tree had only one node then return  
        if (root == null)  
            return root;  
  
        // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE  
        root.height = max(height(root.left),  
                    height(root.right)) + 1;  
  
        // STEP 3: GET THE BALANCE FACTOR 
        // OF THIS NODE (to check whether  
        // this node became unbalanced)  
        int balance = getBalance(root);  
  
        // If this node becomes unbalanced,  
        // then there are 4 cases  
        // Left Left Case  
        if (balance > 1 && getBalance(root.left) >= 0)  
            return rightRotate(root);  
  
        // Left Right Case  
        if (balance > 1 && getBalance(root.left) < 0)  
        {  
            root.left = leftRotate(root.left);  
            return rightRotate(root);  
        }  
  
        // Right Right Case  
        if (balance < -1 && getBalance(root.right) <= 0)  
            return leftRotate(root);  
  
        // Right Left Case  
        if (balance < -1 && getBalance(root.right) > 0)  
        {  
            root.right = rightRotate(root.right);  
            return leftRotate(root);  
        }  
  
        return root;  
    }  
  
    // A utility function to print preorder traversal of  
    // the tree. The function also prints height of every  
    // node  
    void preOrder(Node node)  
    {  
        if (node != null)  
        {  
            Console.Write(node.key + " ");  
            preOrder(node.left);  
            preOrder(node.right);  
        }  
    }  
  
    // Driver code 
    public static void Main()  
    {  
        AVLTree tree = new AVLTree();  
  
        /* Constructing tree given in the above figure */
        tree.root = tree.insert(tree.root, 9);  
        tree.root = tree.insert(tree.root, 5);  
        tree.root = tree.insert(tree.root, 10);  
        tree.root = tree.insert(tree.root, 0);  
        tree.root = tree.insert(tree.root, 6);  
        tree.root = tree.insert(tree.root, 11);  
        tree.root = tree.insert(tree.root, -1);  
        tree.root = tree.insert(tree.root, 1);  
        tree.root = tree.insert(tree.root, 2);  
  
        /* The constructed AVL Tree would be  
        9  
        / \  
        1 10  
        / \ \  
        0 5 11  
        / / \  
        -1 2 6  
        */
        Console.WriteLine("Preorder traversal of "+  
                            "constructed tree is : ");  
        tree.preOrder(tree.root);  
  
        tree.root = tree.deleteNode(tree.root, 10);  
  
        /* The AVL Tree after deletion of 10  
        1  
        / \  
        0 9  
        /     / \  
        -1 5 11  
        / \  
        2 6  
        */
        Console.WriteLine("");  
        Console.WriteLine("Preorder traversal after "+  
                        "deletion of 10 :");  
        tree.preOrder(tree.root);  
    }  
}

```

AVL trees can implement all of the basic operations in O(log(n)) time per operation.
