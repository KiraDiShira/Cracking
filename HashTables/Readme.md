* [Index](https://github.com/KiraDiShira/Cracking/blob/master/README.md#cracking)

# Hash Tables

* [Introduction, Direct Addressing and Chaining](#introduction-direct-addressing-and-chaining)
- [Hash functions](#hash-functions)

## Introduction, Direct Addressing and Chaining

For any set of objects *S* and any integer *m > 0*, a function *h : S -> {0, 1, …, m – 1 }* is called a **hash function**.`

*m* is called the **cardinality** of hash function *h*.

When h(o1) == h(o2) and o1 != o2, there is a *collision*.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h1.png" />

We want to implement a map, using hash function. So what we'll do is called **chaining**. We will create an array of size *m*, where *m* is the cardinality of the hash function, and in this case, let *m* be 8. This will be an array of lists of pairs. And each pair will consist of an object *O*, and a value V, corresponding to this object.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h2.png" />

How to implement this in code?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h3.png" />

```c#
public bool HasKey(int number)
{
    long arrayIndex = Hashing(number);
    IList<Contact> collisions = _table[arrayIndex];
    foreach (var collision in collisions)
    {
        if (collision.Number == number)
        {
            return true;
        }
    }
    return false;
}
```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h4.png" />

```c#
public string Get(int number)
{
    long arrayIndex = Hashing(number);
    IList<Contact> collisions = _table[arrayIndex];
    foreach (Contact collision in collisions)
    {
        if (collision.Number == number)
        {
            return collision.Name;
        }
    }
    return "not found";
}

```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h5.png" />

```c#
public void Set(int number, string name)
{
    long arrayIndex = Hashing(number);
    IList<Contact> contacts = _table[arrayIndex];
    foreach (Contact contact in contacts)
    {
        if (contact.Number == number)
        {
            contact.Name = name;
            return;
        }
    }
    _table[arrayIndex]
        .Add(new Contact(name, number));    
}
```
Now let's look at asymptotics of chaining schema:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h6.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h7.png" />

 We've introduced the notion of map, and now we'll introduce a very similar and natural notion of a **set**. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h8.png" />

What is a *hash table*? A hash table is any implementation of a set or a map which is using hashing, hash functions. It can even not use chaining. There are different ways to use hash functions to store a set or a map in memory. But chaining is one of the most frequently used methods to implement a hash table.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h9.png" />

So we want both *m* and *c* being relatively small . How can we do that? Well, we can do that based on a clever selection of a hash function. 

## Hash functions

You want to design a data structure to store your contacts: names of people along with their phone numbers. The data structure should
be able to do the following quickly:

- Add and delete contacts,
- Lookup the phone number by name,
- Determine who is calling given their phone number.

We need two Maps: (phone number → name) and (name → phone number)

Implement these Maps as hash tables. First, we will focus on the Map from phone numbers to names.

**A good schema is chaining.**

Il numero di telefono lo trasformiamo con una hash function e otteniamo un numero (da 2232323 a 1) e cosi via...

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h10.png" />

Parameters:

- **n**: phone numbers stored
- **m**: cardinality of the hash function
- **c**: length of the longest chain
- **O(n + m)** memory is used
- **𝛼 = n/m**: is called **load factor**
- Operations run in time O(c + 1)
- You want small m and c!

Good example:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h11.png" />

Bad example:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h12.png" />

Good hash functions are:

- Deterministic
- Fast to compute
- Distributes keys well into different cells
- Few collisions

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h13.png" />

So for any deterministic hash function, there is a bad input on which it will have a lot of collisions. How we can solve this problem?

We need the concept of **universal family** of hash functions:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h14.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h15.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h16.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h17.png" />

You want alpha to be below 1 because otherwise you store too much keys in the same hash table and then everything could becomes slow. But also you don't want alpha to be too small because that way you will waste a lot of memory.

How to choose the size of our hash table?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h18.png" />

What if number of keys n is unknown in advance?

If we start with very big hash table we will waste a lot of memory. So we can copy the idea of dynamic arrays! Resize the hash table when 𝛼 becomes too large and choose new hash function and rehash all the objects.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h19.png" />

```c#
private void Rehash()
{
    double loadFactor = (double)_numberOfKeys / _tableSize;
    if (loadFactor > 0.9)
    {
        _numberOfKeys = 0;
        _tableSize *= 2;
        SetAandB();
        IList<Contact>[] newTable = new IList<Contact>[_tableSize];
        foreach (IList<Contact> contacts in _table)
        {
            foreach (Contact contact in contacts)
            {
                long arrayIndex = Hashing(contact.Number);
                newTable[arrayIndex]
                    .Add(new Contact(contact.Name, contact.Number));
            }
        }
        _table = newTable;
    }
}

private void SetAandB()
{
    _b = (long) _random.NextDouble() * (_p - 1);
    _a = 1 + (long) _random.NextDouble() * (_p - 2);
}
```

So here is the code which tries to keep loadfFactor below 0.9. And 0.9 is just a number I selected, you could put 1 here or 0.8, that doesn't really matter.

So to achieve that, you need to call this procedure rehash after each operation which inserts something in your hash table. And it could work slowly when this happens because the rehash procedure needs to copy all the keys from your current hash table to the new big hash table, and that works in linear time. But similarly to dynamic arrays, the amortized running time will still be constant on average because their hash will happen only rarely. So you reach a certain level of load factor and you increase the size of our table twice. And then it will take twice longer to again reach too high value of load factor. And then you'll again increase your hash table twice. So the more keys you put in, the longer it takes until the next rehash. So their hashes will be really rare, and that's why it won't influence your running time with operations, significantly. 

`Similarly to dynamic arrays, single rehashing takes O(n) time, but amortized running time of each operation with hash table is still O(1) on average, because rehashing will be rare.`

### Hashing integers

You will start with a universal family for the most important object which is integer number. Because any object on your computer is represented as a series of bits or bytes, and so you can think of it as a sequence of integer numbers. And so first, we need to learn to hash integers efficiently. 

Example with a phone number:

- Take phone numbers up to length 7, for example 148-25-67
- Convert phone numbers to integers from 0 to 10^7 − 1 = 9 999 999: 148-25-67 → 1 482 567
- Choose prime number bigger than 10^7, e.g. p = 10 000 019
- Choose hash table size, e.g. m = 1 000

So now that we selected p and m, we are ready to define universal family for integers between 0 and 10^7 - 1. So the Lemma says that the following family of hash functions is a universal family. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h20.png" />

```
p = prime number
a,b = those parameters are different for different hash functions in these family
```

```c#
private long Hashing(long number)
{
    return ((_a * number + _b) % _p) % _tableSize;
}
```
And the size of this hash family, what do you think it is? 

Well, it is equal to `p (p - 1)`, why is that? Because there are p minus 1 variance for a, and independently from that, there are p variance for b.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h21.png" />

In the general case:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h22.png" />

### da inserirre https://www.cs.cornell.edu/courses/cs312/2008sp/lectures/lec20.html

Resizable hash tables and amortized analysis
