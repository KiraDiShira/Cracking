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

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h8.png" />



What is a *hash table*? A hash table is any implementation of a set or a map which is using hashing, hash functions. It can even not use chaining. There are different ways to use hash functions to store a set or a map in memory. But chaining is one of the most frequently used methods to implement a hash table.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h9.png" />

So we want both *m* and *c* being relatively small . How can we do that? Well, we can do that based on a clever selection of a hash function. 
