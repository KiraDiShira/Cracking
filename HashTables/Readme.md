* [Index](https://github.com/KiraDiShira/Cracking/blob/master/README.md#cracking)

# Hash Tables

* [Introduction, Direct Addressing and Chaining](#Introduction,-Direct-Addressing-and-Chaining)

## Introduction, Direct Addressing and Chaining

For any set of objects *S* and any integer *m > 0*, a function *h : S -> {0, 1, …, m – 1 }* is called a **hash function**.`

*m* is called the **cardinality** of hash function *h*.

When h(o1) == h(o2) and o1 != o2, there is a *collision*.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h1.png" />

We want to implement a map, using hash function. So what we'll do is called **chaining**. We will create an array of size *m*, where *m* is the cardinality of the hash function, and in this case, let *m* be 8. This will be an array of lists of pairs. And each pair will consist of an object *O*, and a value V, corresponding to this object.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h2.png" />
