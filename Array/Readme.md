* [Index](https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/README.md#project-title)

# Array

**Array**: contiguous area of memory consisting of equal-size elements indexed by countiguos integers.

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/Array/Images/arr1.PNG" />

What's special about arrays? Constant-time access: `O(1)`

```
array_addr + elem_size * (i - first_index)
```

Constant-time access also for **multidimensional arrays**:

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/Array/Images/arr2.PNG" />

We need to skip the full rows that we are not using (`(3 - 1) * 6`), and then the situation is like for mono dimensional arrays:

```
array_addr + elem_size * ((3 - 1) * 6 + (4 - 1))
```
For multimensional arrays we made a supposition: all the elements of the first row, followed by all of the elements of the second row, and so on. That's called **row-major ordering** or **row-major indexing**. And what we do is basically, we lay out, (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6). And then right after that in memory (2, 1), (2, 2), (2, 3), (2, 4), (2, 5), (2, 6). So the column index is changing most rapidly as we're looking at successive elements. And that's an indication of it's row-major indexing. 

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/Array/Images/arr3.PNG" />

Time for common operations:

<img src="https://github.com/KiraDiShira/AlgorithmsAndDataStructures/blob/master/RepoFiles/Array/Images/arr4.PNG" />
