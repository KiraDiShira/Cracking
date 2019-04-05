* [Index](https://github.com/KiraDiShira/Cracking#cracking)

# Dynamic Arrays and Amortized Analysis

* [Dynamic Arrays](#dynamic-arrays)
    - [Implementation](#implementation)
    - [Runtimes](#runtimes)
* Amortized Analysis
    - [Aggregate Method](#aggregate-method)
    - [Banker's Method](#bankers-method)
    - [Physicist's method](http://www.cs.cornell.edu/courses/cs3110/2013sp/lectures/lec21-amortized/lec21.html)
* [Summary](#summary)


## Dynamic Arrays

Unlike static arrays, dynamic arrays can be resized. Solution: dynamic arrays (also known as resizable arrays are an abstract data type with the following operations (at a minimum):

* `Get(i)`: returns element at location `i`. Must must be constant time
* `Set(i, val)`: Sets element `i` to `val`. Must be constant time
* `PushBack(val)`: Adds `val` to the end
* `Remove(i)`: Removes element at location `i`
* `Size()`: the number of elements

### Implementation

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa1.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa2.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa3.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa4.PNG" />

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa5.PNG" />

```c#

public class DynamicArray<T>
{
    private T[] _array;
    
    private int _capacity;

    public DynamicArray(int size)
    {
        Size = 0;
        _array = new T[size];
        _capacity = size;
    }

    public int Size { get; set; }

    public T Get(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new Exception("index out of range");
            }

            return _array[index];
        }

    public void Set(int index, T value)
    {
        if (index < 0 || index >= Size)
        {
            throw new Exception("index out of range");
        }
        _array[index] = value;
    }

    public void PushBack(T value)
    {
        if (Size == _capacity)
        {
            T[] newArray = new T[_capacity * 2];
            _array.CopyTo(newArray, 0);
            _array = newArray;
            _capacity = 2 * _capacity;
        }

        _array[Size] = value;
        Size++;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new Exception("index out of range");
        }

        for (int j = index; j < Size - 1; j++)
        {
            _array[j] = _array[j + 1];
        }

        Size--;
    }
}
```
### Runtimes

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa6.PNG" />

## Aggregate Method

Sometimes, we're looking at an individual worst case and that may be too severe. In particular we may want to know the total worst case for a sequence of operations and it may be some of those operations are cheap, while only certain of them are expensive. So if we look at the worst case operation for any one and multiply that by the total, it may be overstating the total cost. 

As an example, for a dynamic array, we only resize every so often. If we do our running time analysis for a sequence of `n` insert operations we would have `n * O(n) = O(n^2)`. Most of the time, we're doing a constant time operation, just adding an element. It's only when we fully reach the capacity, that we have to resize. So the question is, what's the total cost if you have to insert a bunch of items? 

So here's the definition of **amortized cost**. You have a sequence of `n` operations, the amortized cost is the cost of those `n` operations divided by `n`. 

```
Amortized Cost = Cost(n operations) / n

```

 For instance, consider the following sequence of insertions, starting with an array of length 1:
 
 <img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa7.PNG" />
 
 The table is doubled in the second, third, and fifth steps. As each insertion takes `O(n)` time in the worst case, a simple analysis would yield a bound of `O(n^2)` time for n insertions. But it is not this bad. 
 
 Let c_i be the cost of the i-th insertion:
 
  <img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa8.PNG" />
 
 Let's consider the size of the table si and the cost ci for the first few insertions in a sequence:

 <img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa9.PNG" />
 
 Alteratively we can see that `c_i=1+d_i` where `d_i` is the cost of doubling the table size. 
 
 <img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa10.PNG" />
 
 Then summing over the entire sequence, all the 1's sum to `O(n)`, and all the di also sum to `O(n)`. That is,
 
  <img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa11.PNG" />
  
So we've got `n` plus something no more than `2n`, that's clearly `O(n)` divided by `n`, and that's just `O(1)`. So what we've determined then is that we have a amortized cost for each insertion of order 1. 

Our worst case cost is still order `n`, so if we want to know how long it's going to take in the worst case for any  particular insertion is `O(n)`, but the amortized cost is `O(1)`. 

## Banker's Method

Intuitively, one can think of maintaining a bank account. Low-cost operations are charged a little bit more than their true cost, and the surplus is deposited into the bank account for later use. High-cost operations can then be charged less than their true cost, and the deficit is paid for by the savings in the bank account. In that way we spread the cost of high-cost operations over the entire sequence. The charges to each operation must be set large enough that the balance in the bank account always remains positive, but small enough that no one operation is charged significantly more than its actual cost.

We emphasize that the extra time charged to an operation does not mean that the operation really takes that much time. It is just a method of accounting that makes the analysis easier.

In dynamic array example we charge 3 for each insertion: 
* one token is the raw cost for insertion.
* one token on the newly-inserted element.
* one token capacity/2 elements prior

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa12.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa13.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa14.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa15.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa16.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa17.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa18.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa19.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa20.PNG" />
<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa21.PNG" />

What we have got is an amortized cost of `O(1)`, in particular the cost is 3

## Summary

Can we use a constant amount (+10 for instance), intead of a constant factor (* 2) for growing our dynamic array?

So this shows that if we use a constant amount to grow the dynamic array each time that we end up with an amortized cost for push back of O(n) rather than O(1). So it's extremely important to use a constant factor. 

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/DynamicArraysandAmortizedAnalysis/Images/daaa22.PNG" />

So this shows that if we use a constant amount to grow the dynamic array each time that we end up with an amortized cost for push back of O(n) rather than O(1). So it's extremely important to use a constant factor. 
