<div style="page-break-before: always !important;"/>

# Array

**Array**: contiguous area of memory consisting of equal-size elements indexed by countiguos integers.

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Array/Images/arr1.PNG" />

What's special about arrays? Constant-time access: `O(1)`

```
array_addr + elem_size * (i - first_index)
```

Constant-time access also for **multidimensional arrays**:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Array/Images/arr2.PNG" />

We need to skip the full rows that we are not using (`(3 - 1) * 6`), and then the situation is like for mono dimensional arrays:

```
array_addr + elem_size * ((3 - 1) * 6 + (4 - 1))
```

Time for common operations:

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/Array/Images/arr4.PNG" />
