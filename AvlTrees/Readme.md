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

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl10.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl11.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl12.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl13.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl14.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl15.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl16.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl17.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl18.png" />

