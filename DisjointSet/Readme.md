* [Index](https://github.com/KiraDiShira/Cracking/blob/master/README.md#cracking)

# Disjoint Sets

* [Definition](#definition)
* [Naive Implementations](#naive-implementations)
* [Trees](#trees)
* [Union by Rank](#union-by-rank)
* [Path Compression](#path-compression)
* [Analysis](#analysis)

## Definition

A **disjoint-set** data structure supports the following operations:

* `MakeSet(x)`: creates a singleton set {x}
* `Find(x)`: returns ID of the set containing x:
    - if x and y lie in the same set, then `Find(x) = Find(y)`
    - otherwise, `Find(x) ̸= Find(y)` 
* `Union(x, y)`: merges two sets containing x and y

## Naive Implementations

* Use the smallest element of a set as its ID
* Use array smallest[1 . . . n]: smallest[i] stores the smallest element in the set i belongs to

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds1.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds2.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds3.PNG" />

```c#

public class ArrayDisjointSet
{
    private readonly int[] _smallest;

    public ArrayDisjointSet()
    {
        _smallest = new int[100];
    }

    public void MakeSet(int index)
    {
        _smallest[index] = index;
    }

    public int Find(int index)
    {
        return _smallest[index];
    }

    public void Union(int indexI, int indexJ)
    {
        int iId = Find(indexI);
        int jId = Find(indexJ);
        if (iId == jId)
        {
            return;
        }
        int min = Math.Min(iId, jId);
        for (int i = 0; i < _smallest.Length; i++)
        {
            if (_smallest[i] == iId || _smallest[i] == jId)
            {
                _smallest[i] = min;
            }
        }
    }
}

```
Current bottleneck: Union.

What basic data structure allows for efficient merging? Linked list!

Idea: represent a set as a linked list, use the list tail as ID of the set

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds4.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds5.PNG" />

- Pros:
    * Running time of Union is O(1)
    * Well-defined ID    
- Cons:
    * Running time of Find is O(n) as we need to traverse the list to find its tail
    * Union(x, y) works in time O(1) only if we can get the tail of the list of x and the head of the list of y in constant time!
    
Can we merge in a different way?

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds6.PNG" />

## Trees

- Represent each set as a rooted tree
- ID of a set is the root of the tree
- Use array parent[1 . . . n]: parent[i] is the parent of i, or i if it is the root

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds7.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds8.PNG" />

How to merge two trees?

Hang one of the trees under the root of the other one

Which one to hang?

A shorter one, since we would like to keep the trees shallow

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds9.PNG" />

## Union by Rank

* When merging two trees we hang a shorter one under the root of a taller one
* To quickly find a height of a tree, we will keep the height of each subtree in an array rank[1 . . . n]: rank[i] is the height of the subtree whose root is `i`
* Hanging a shorter tree under a taller one is called a union by rank heuristic

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds10.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds11.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds12.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds13.PNG" />

Union(5, 2)
Union(3, 1)
Union(2, 3)
Union(2, 6)

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds14.PNG" />

**Lemma**

`The height of any tree in the forest is at most log2 n`

Follows from the following lemma.

**Lemma**

`Any tree of height k in the forest has at least 2^k nodes`

This will imply the first lemma as follows. I assume that some tree has height strictly greater than binany logarithm of n. Using the second lemma it will be possible to show then that this tree contains more than n nodes, right? Which would lead to a contradiction with the fact that we only have n objects in our data structure. 

Ad esempio prendo `k = log2 (n + 1)` che è un numero più grande di `k = log2 (n)`.

Per il secondo lemma avrò un numero di nodi pari a `2^k = 2^(log2 (n + 1)) = n + 1 > n` che è una contraddizione.

Proof on 2nd lemma:

Induction on k.

*Base*: initially, a tree has height 0 and one node: 2^0 = 1.

*Step*: a tree of height k results from merging two trees of height k − 1. By induction hypothesis, each of two trees
has at least 2^(k−1) nodes, hence the resulting tree contains at least 2^k nodes: 

```
2^(k−1) + 2^(k−1) = 2^(k−1) * (1 + 1) = 2^(k−1) * (2) = 2^(k-1+1) = 2^k
```

The union by rank heuristic guarantees that Union and Find work in time `O(log n)`.

Next part We’ll discover another heuristic that improves the running time to nearly constant!

## Path Compression

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds15.PNG" />

not only it finds the root for 6, it does so for all the nodes on this path. Let’s not lose this useful info.

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds16.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds17.PNG" />

```c#
public class TreeDisjointSet
{
    private  int[] _parents;
    private  int[] _ranks;

    public TreeDisjointSet()
    {
        _parents = new int[]
        {
            0,
            6,
            3,
            5,
            9,
            5,
            12,
            10,
            12,
            5,
            5,
            6,
            3
        };
        _ranks = new int[_parents.Length];
    }

    public void MakeSet(int index)
    {
        _parents[index] = index;
    }

    public int Find(int index)
    {
        if (index != _parents[index])
        {
            _parents[index] = Find(_parents[index]);
        }
        return _parents[index];
    }

    public void Union(int indexI, int indexJ)
    {
        int iId = Find(indexI);
        int jId = Find(indexJ);
        if (iId == jId)
        {
            return;
        }
        if (_ranks[iId] > _ranks[jId])
        {
            _parents[jId] = iId;
        }
        else
        {
            _parents[iId] = jId;
            if (_ranks[iId] == _ranks[jId])
            {
                _ranks[jId] = _ranks[jId] + 1;
            }
        }
    }

    public override string ToString()
    {
        return String.Join(",", _parents);
    }
}
```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds18.PNG" />

Esempio:

```
log* 16 = ?

log2 16 = 4, 4 <= 1? no, quindi itero
log2 4 = 2, 2 <= 1? no, quindi itero
log2 2 = 1 <= 1? si, mi fermo

quante volte ho chiamato la funzione log2 x? 3 quindi il log* 16 = 3
```

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds19.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds20.PNG" />

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ds21.PNG" />

## Analysis

Our goal is to show that if we use both path compression and union by rank heuristics then the average running time of a single operation is upper bounded by O(log* (n)).

Before going into details of the proof, let's realize some properties:

`Proprietà: Height <= Rank`

In questo esempio, il rank è 2 e l'altezza passa da 2 a 1:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ana0.PNG" />

`Proprietà: ogni albero con root di rank k ha almeno 2^k nodi`

`Proprietà: There are at most n/2^k nodes of rank k`

Problema: Quanti sono i nodi rank k?

Dimostrazione: ogni albero con root di rank k ha almeno 2^k nodi, quindi:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ana1.PNG" />

- y = nodi di rank k
- epsilon = numero di nodi in più in un albero
- n = numero totale di nodi

Quanti sono i nodi di rank k?

y * (2^k + epsilon) <= n

- < n perchè al massimo il numero di nodi rank k può essere uguale al numero di nodi.
- 2^k è il numero minimo di nodi che ha un albero con root di rank k, quindi per sapere il numero totale di nodi ci aggiungo una quantita variabile a piacere

y <= n / (2^k + epsilon)

Se epsilon cresce la parte destra diminuisce, quindi il valore max della parte destra è

 n / 2^k
 
 quindi 
 
 y_maxValue = n / 2^k

`Proprietà: For any node i, rank[i] < rank[parent(i)]`

`Proprietà: Once an internal node, always an internal node. An internal node can't become root`

We now start to estimate the running time of `m` operations. Note that the union operation is two calls to Find operation and also to some constant operations.

So for this reason when estimating the total running time we will just assume that we have `m` calls to find operation.

Each Find operation traverses some path from a node to find the root of the corresponding tree. So we traverse some number of edges. So the total run in time of all the defind operations, of all the calls to the Find operation is just the total number of edges traversed. 

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/ana2.PNG" />

* Per `m` si intende il numero di chiamate alla `Find` (numero totale di operazioni).
* Per `n` si intende il numero di `makeSet`

Il primo membro dell'equazione è `O(m)` perchè il numero di chiamate alla Find sarà uguale al numero di edge che vanno verso il parent che sono proprio m nel caso peggiore: immagina un albero con solo root o con root + child di primo livello.

Per il secondo membro: il rank della root (massimo rank di un albero) è al massimo `log n` (dimostrabile per induzione),  quindi `log* rank = log* (log n) = log* (n)`. Quindi ci sono al max log* n valori differenti per log* rank. Quindi:

`O(m log* n)`

In questo esempio, `log* (n) = log* (29) = 4`, quindi ci sono al massimo 4 valori differenti di log*. 

Per il terzo membro:

<img src="https://github.com/KiraDiShira/Cracking/blob/master/DisjointSet/Images/terz.PNG" />

1) l'abbiamo dimostrato prima
2) Quando si va verso l'alto il rank aumenta sempre (proprietà precedente)
