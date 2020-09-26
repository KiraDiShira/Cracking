* [Index](https://github.com/KiraDiShira/Cracking/blob/master/README.md#cracking)

# Hash Tables

- [Introduction, Direct Addressing and Chaining](#introduction-direct-addressing-and-chaining)
- [Hash functions](#hash-functions)
- [Searching Patterns](#searching-patterns)

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

### Hashing integers code

```c#

public class NumbersHashTable
{
    public List<PhoneContact>[] _array;
    public decimal _numberOfKeys;
    public decimal _numberOfRehash; // per fini di debug
    private NumbersHashFunction _hashFunction;
    private decimal _maxLoadFactor = 3m;

    public NumbersHashTable(long size = 1, long maxDomainSize = 10000019)
    {
        _array = new List<PhoneContact>[size];
        for(int i = 0; i< _array.Length; i++)
        {
            _array[i] = new List<PhoneContact>();
        }
        _hashFunction = new NumbersHashFunction(size, maxDomainSize);
    }

    public decimal LoadFactor
    {
        get
        {
            return _numberOfKeys / _array.LongLength;
        }
    }

    public bool HasKey(long key)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        foreach (PhoneContact phoneContact in phoneContacts)
        {
            if(key == phoneContact.Number)
            {
                return true;
            }             
        }
        return false;
    }

    public PhoneContact Get(long key)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        foreach (PhoneContact phoneContact in phoneContacts)
        {
            if (key == phoneContact.Number)
            {
                return phoneContact;
            }
        }

        throw new Exception($"The given key '{key}' was not present in the dictionary.");
    }

    public void Set(long key, PhoneContact newContact)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        for (int i = 0; i < phoneContacts.Count; i++)
        {
            PhoneContact phoneContact = phoneContacts[i];
            if (key == phoneContact.Number)
            {
                phoneContact = newContact;
                return;
            }
        }

        phoneContacts.Add(newContact);
        _numberOfKeys++;

        if (LoadFactor > _maxLoadFactor)
        {
            _numberOfRehash++;
            Rehash();
        }
    }    

    private void Rehash()
    {
        long newSize = _array.LongLength * 2;
        var tNew = new NumbersHashTable(size: newSize);

        foreach (List<PhoneContact> contacts in _array)
        {
            foreach (PhoneContact contact in contacts)
            {
                tNew.Set(contact.Number, contact);
            }
        }

        _hashFunction = tNew._hashFunction;
        _array = tNew._array;
    }
}

public class NumbersHashFunction
{
    public long PrimeNumber { get; private set; }
    public long A { get; private set; }
    public long B { get; private set; }
    public long HashTableSize { get; private set; }

    public NumbersHashFunction(long size, long maxDomainSize)
    {
        PrimeNumber = GetPrimeNumberGreaterThen(maxDomainSize);
        Random rand = new Random();
        A = LongRandom(1, PrimeNumber - 1, rand);
        B = LongRandom(0, PrimeNumber - 1, rand);
        HashTableSize = size;
    }

    public long Hash(long key)
    {
        return ((A * key + B) % PrimeNumber) % HashTableSize;
    }

    private static long GetPrimeNumberGreaterThen(long maxDomainSize)
    {
        //todo calculate prime number greater then max domain size
        return maxDomainSize;
    }

    private static long LongRandom(long min, long max, Random rand)
    {
        max += 1;
        byte[] buf = new byte[8];
        rand.NextBytes(buf);
        long longRand = BitConverter.ToInt64(buf, 0);
        return (Math.Abs(longRand % (max - min)) + min);
    }
}

static void Main(string[] args)
{
    var hashTable = new NumbersHashTable();

    Random rnd = new Random();
    for (int i = 0; i < 1700000; i++)
    {
        var longRandom = LongRandom(1, 10000000, rnd);
        hashTable.Set(longRandom, new PhoneContact(longRandom.ToString(), longRandom));
    }

    Console.WriteLine($"number of rehash: {hashTable._numberOfRehash}");
    Console.WriteLine($"number of keys: {hashTable._numberOfKeys}");
    Console.WriteLine($"m or size: {hashTable._array.Length}");
    Console.WriteLine($"Load factor: {hashTable.LoadFactor}");

    IDictionary<int, int> sizeCount = new Dictionary<int, int>();
    foreach (List<PhoneContact> bucket in hashTable._array)
    {
        var bucketCount = bucket.Count;
        if (sizeCount.ContainsKey(bucketCount))
        {
            sizeCount[bucketCount]++;
        }
        else
        {
            sizeCount.Add(bucketCount, 1);
        }
    }

    decimal totSum = 0m;
    foreach (var item in sizeCount.OrderBy(x => x.Key))
    {
        Console.WriteLine($"bucket Length: {item.Key} Count: {item.Value}");
        totSum +=  item.Key * item.Value;
    }

    Console.WriteLine($"averagee length: {totSum / sizeCount.Where(x => x.Key != 0).Select(x => x.Value).Sum()} expected lenth: {1 + hashTable.LoadFactor}");
    

    Console.WriteLine();
    Console.Read();
}


private static long LongRandom(long min, long max, Random rand)
{
    max += 1;
    byte[] buf = new byte[8];
    rand.NextBytes(buf);
    long longRand = BitConverter.ToInt64(buf, 0);
    return (Math.Abs(longRand % (max - min)) + min);
}


```

### Hashing Strings

L'idea è quella di trasformare con una funzione di hash una stringa in un numero. Trasformazione a bassa probabilità di collisione. E usare tale numero come input per la funzione di hash numerica. In tal modo posso costruire una hash table che abbia come chiavi delle stringhe.

**Definition** Denote by **|S|** the length of string S.

Examples:
- |“a”| = 1
- |“ab”| = 2
- |“abcde”| = 5

**Preparation** 
- Convert each character S[i] to integer code (ASCII code, Unicode, etc.)
- Choose big prime number p

We introduce a new family of hash functions called **polynomial hash functions**:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h23.png" />

How many hash functions are there in this family? Well of course, there are exactly p- 1 different hash functions, because to choose to define a hash function from this family you would just need to choose the value of x. And x changes from 1 to p- 1, and it's an integer number of course. 

So how can we implement a hash function from this family? 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h24.png" />

```c#
private long PolyHash(string s)
{
    long hash = 0;
    for (int i = s.Length - 1; i >= 0; --i)
        hash = (hash * multiplier + s[i]) % _p;
    return hash;
}
```

**Java implementation**

The method hashCode of the built-in Java class String is very similar to our PolyHash, it just uses x = 31 and for technical reasons avoids the (mod p) operator. 

You now know how a function that is used trillions of times a day in many thousands of programs is implemented!

**Efficency of polynomial family?**

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h25.png" />

### Cardinality fix

Now we know of polynomial hash family or hashing strings. But there's a problem with that family. All the hash functions in that family have a cardinality of P, where P is a very big prime number. And what we want is the cardinality of hash functions to be the same as the size of our hash table. So, once a small cardinality. So, we won't be able to use this polynomial hashing family directly in our hash tables. We want to somehow fix the cardinality of the functions in the polynomial family. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h26.png" />

Note that it is very important that we first select both random function from the polynomial family and the random function from the universal family of our integers. And we fix them, and we use the same pair of functions for the whole algorithm. And then, the whole function from string to integer number from between zero and minus one is a deterministic hash function. 

And it can be shown that the family of functions define this way is a very good family. It is not a universal family, but it is a very good family with low probability of collisions.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h27.png" />

So, that is not an universal family because for a universal family there shouldn't be any sum on L over p the probability of collision should be at most 1 over M. But we can be very, very close to universal family because we can control P. We can make P very big. And then L over p will be very small. And so, the probability of collision will be at most will 1 over m plus some very small number.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h28.png" />

So that way, we proved that combination of polynomial hashing with universal hashing for integers, is a really good family of hash functions. Now what if we take this new family of hash functions and apply it to build a hash table? 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h29.png" />

### Hashing strings code

```c#

public class StringsHashFunction
{
    private readonly NumbersHashFunction _numbersHashFunction;

    public long PrimeNumber { get; private set; }
    public long X { get; private set; }

    public StringsHashFunction(NumbersHashFunction numbersHashFunction, LongRandomCalculator longRandomCalculator)
    {
        PrimeNumber = 1610612741;
        Random rand = new Random();
        X = longRandomCalculator.LongRandom(1, PrimeNumber - 1, rand);
        _numbersHashFunction = numbersHashFunction;
    }

    public long Hash(string key)
    {
        long stringHash = 0;
        for (int i = key.Length - 1; i >= 0; --i)
            stringHash = (stringHash * X + key[i]) % PrimeNumber;

        long numberHash = _numbersHashFunction.Hash(stringHash);

        return numberHash;
    }
}

public class StringsHashTable
{
    public List<PhoneContact>[] _array;
    public decimal _numberOfKeys;
    public decimal _numberOfRehash; // per fini di debug
    private StringsHashFunction _hashFunction;
    private decimal _maxLoadFactor = 3m;

    public StringsHashTable(long size = 1, long maxDomainSize = 10000019)
    {
        _array = new List<PhoneContact>[size];
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i] = new List<PhoneContact>();
        }
        _hashFunction = new StringsHashFunction(new NumbersHashFunction(new LongRandomCalculator(), size, maxDomainSize), new LongRandomCalculator());
    }

    public decimal LoadFactor
    {
        get
        {
            return _numberOfKeys / _array.LongLength;
        }
    }

    public bool HasKey(string key)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        foreach (PhoneContact phoneContact in phoneContacts)
        {
            if (key == phoneContact.Name)
            {
                return true;
            }
        }
        return false;
    }

    public PhoneContact Get(string key)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        foreach (PhoneContact phoneContact in phoneContacts)
        {
            if (key == phoneContact.Name)
            {
                return phoneContact;
            }
        }

        throw new Exception($"The given key '{key}' was not present in the dictionary.");
    }

    public void Set(string key, PhoneContact newContact)
    {
        List<PhoneContact> phoneContacts = _array[_hashFunction.Hash(key)];

        for (int i = 0; i < phoneContacts.Count; i++)
        {
            PhoneContact phoneContact = phoneContacts[i];
            if (key == phoneContact.Name)
            {
                phoneContact = newContact;
                return;
            }
        }

        phoneContacts.Add(newContact);
        _numberOfKeys++;

        if (LoadFactor > _maxLoadFactor)
        {
            _numberOfRehash++;
            Rehash();
        }
    }

    private void Rehash()
    {
        long newSize = _array.LongLength * 2;
        var tNew = new StringsHashTable(size: newSize);

        foreach (List<PhoneContact> contacts in _array)
        {
            foreach (PhoneContact contact in contacts)
            {
                tNew.Set(contact.Name, contact);
            }
        }

        _hashFunction = tNew._hashFunction;
        _array = tNew._array;
    }
}

```

## Searching Patterns

Given a text T (book, website, facebook profile) and a pattern P (word, phrase,sentence), find all occurrences of P in T.
Examples: Your name on a website, Twitter messages about your company.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h30.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h31.png" />

**Naive Algorithm**: For each position i from 0 to |T| − |P|, check character-by-character whether T[i..i + |P| − 1] = P or not. If yes, append i to the result.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h32.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h33.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h34.png" />

### Rabin-Karp's Algorithm

Need to compare P with all substrings S of T of length |P|. Idea: use hashing to quickly compare P with substrings of T.

- If h(P) != h(S), then definitely P != S
- If h(P) = h(S), call AreEqual(P, S)
- Use polynomial hash family 𝒫p with prime p
- If P != S, the probability Pr[h(P) = h(S)] is at most |P|/p for polynomial hashing

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h35.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h36.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h37.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h38.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h39.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h40.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h41.png" />

(Non è usato il modulo per semplicità)

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/IMG_20200926_130215.jpg" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h42.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h43.png" />

Here's the function to pre compute all the hash values of our polynomial hash function on the substrings of the text t with the length equal to the length of the pattern, and with prime number, P and selected integer x. We initialize our answer, big H, as an array of length, length of text minus length of pattern plus one. Which is the number of substrings of the text with length equal to the length of the pattern. Also initialize S by the last substring of the text with a length equal to the length of the pattern. And you compute the hash value for this last substring directly by calling our implementation of polynomial hash with the substring prime number P and integer x. Then we also need to precompute the value of x to the power of length of the pattern and store it in the variable y. To do that we need initialize it with 1 and then multiply it length of P times by x and take this module of p. And then the main for loop, the second for loop goes from right to left and computes the hash values for all the substrings of the text, but for the last one for which we already know the answer. So to compute H[i] given H[i + 1], we multiply it by x. Then we add T[i] and we subtract y, which is x to the power of length of P, by T[i + length of the pattern]. And we take the expression module of p. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h44.png" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/HashTables/Images/h45.png" />

```c#
public class RabinKarpAlgorithm
{
    private int prime = 1610612741;
    private int multiplier = 31;

    private int PolyHash(string pattern)
    {
        long hash = 0;
        for (int i = pattern.Length - 1; i >= 0; --i)
            hash = (hash * multiplier + pattern[i]) % prime;
        return (int)hash;
    }

    private int[] PrecomputeHashes(string text, int patternLength)
    {
        int[] precomputedHashes = new int[text.Length - patternLength + 1];
        string S = text.Substring(text.Length - patternLength, patternLength );
        precomputedHashes[text.Length - patternLength] = PolyHash(S);
        int y = 1;
        for (int i = 1; i <= patternLength; i++)
        {
            y = (y * multiplier) % prime;
        }

        for (int i = text.Length - patternLength - 1; i >= 0; i--)
        {
            precomputedHashes[i] = (multiplier * precomputedHashes[i + 1] + text[i] -
                                    y * text[i + patternLength]) % prime;
        }

        return precomputedHashes;
    }

    public IList<int> RabinKarp(string text, string pattern)
    {
        IList<int> results = new List<int>();
        int patternHash = PolyHash(pattern);
        int[] precomputedHashes = PrecomputeHashes(text, pattern.Length);

        for (int i = 0; i <= text.Length - pattern.Length; i++)
        {
            if (patternHash != precomputedHashes[i])
            {
                continue;
            }
            if (text.Substring(i, pattern.Length) == pattern)
            {
                results.Add(i);
            }
        }

        return results;
    }
}
```

