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

Note that AVL trees with a minimum number of nodes are the worst case examples of AVL tree: every node’s subtrees differ in height by one. You can see examples of such trees below:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl6.png" />

If we consider worst case example:

```
i = ℎ/2
```

```
𝑁(ℎ) > 2^(ℎ/2) ⇔ log 𝑁(ℎ) > log 2^(ℎ/2) ⇔ ℎ < 2 log 𝑁(ℎ)
```

Thus, these worst-case AVL trees have height **ℎ = O(log 𝑛)**.

## Avl Tree implementation

Updates to the tree can destroy AVL tree property:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/AvlTrees/Images/avl7.png" />

We need a new insertion algorithm that involves rebalancing the tree to maintain the AVL property.

