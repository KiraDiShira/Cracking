<div style="page-break-before: always !important;"/>

# Big-O Notation

- [Asymptotic notation](#asymptotic-notation)
- [Big-O](#big-o)
- [Using Big-O](#using-big-o)

## Asymptotic notation

**Runtimes** is how long takes a program to work;

Computing runtimes is hard:
- depends on details of program
- depends on details of computer

We need something that's a little bit less precise but much easier to work with.

- **Basic idea**: there are lots of factors that have an effect on the final runtime but, most of them will only change the runtimes by a constant. If you're running on a computer that's a hundred times faster, it will take one hundreth of the time, a constant multiple. If your system architecture has multiplications that take three times as long as additions, then if your program is heavy on multiplications instead of additions, it might take three times as long, but it's only a factor of three. If your memory hierarchy is arranged in a different way, you might have to do disk lookups instead of RAM lookups. And those will be a lot slower, but only by a constant multiple. 

So if we come up with a measure of runtime complexity that ignores all of constant multiples, where running in time `n` and in running in time `100n` are sort of considered to be the same thing, then we don't have to worry about all of these little, bitty details that affect runtime. 

- **Problem**: runtimes of 1 second or 1 hour or 1 year, these only differ by constant multiples.

- **Solution**: we're not going to actually consider the runtimes of our programs on any particular input. We're going to look at what are known as **asymptotic runtimes**: `how does the runtime scale with input size`. As the input size n gets larger, does the output scale proportional to n, maybe proportional to n squared? Is it exponential in n? All these things are different. And in fact they're sort of so different that as long as n is sufficiently large, the difference between n runtime and n squared runtime is going to be worse than any constant multiple. 

Only caring about what happens in this sort of long scale behavior, we will be able to do this without seeing these constants, without having to care about these details.  

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/BigONotation/Images/BigO1.PNG" />

<div style="page-break-before: always !important;"/>

## Big-O

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/BigONotation/Images/BigO2.PNG" />

Big-O notation is an asymptotic notation.
 
`f (n) = O(g(n)) (f is Big-O of g) or f ⪯ g if there exist constants N and c so that for all n ≥ N, f (n) ≤ c · g(n).`

 At least for sufficiently large inputs, `f` is bounded above by some constant multiple of `g`. 
 
 `grows no faster than g(x)`

Advantages:
- clarifies growth rate
- cleans up notation: O(n^2) vs. 3n^2 + 5n + 2.
- we don't need to know how fast computer is, memory management, system architecture ...

Warnings:
- Using Big-O loses important information about constant multiples.
- Big-O is only asymptotic.

### Other notations:

For functions f , g : N → R+ we say that:

`f (n) = Ω(g(n)) or f ⪰ g if for some c, f (n) ≥ c · g(n) (f grows no slower than g).`

`f (n) = Θ(g(n)) or f ≍ g if f = O(g) and f = Ω(g) (f grows at the same rate as g).`

`f (n) = o(g(n)) or f ≺ g if f (n)/g(n) → 0 as n → ∞ (f grows slower than g).`

We use o-notation to denote an upper bound that is not asymptotically tight. 

The definitions of O-notation and o-notation are similar. The main difference is that in f (n) = O(g(n)), the bound 0 ≤ f (n) ≤ c · g(n) holds for some constant c > 0, but in f (n) = o(g(n)), the bound 0 ≤ f (n) < c · g(n) holds for all constants c > 0.

<div style="page-break-before: always !important;"/>

## Using Big-O

<img src="https://raw.githubusercontent.com/KiraDiShira/Cracking/master/BigONotation/Images/BigO3.PNG" />

When you see a problem where the number of elements in the problem space gets halved each time, that will likely be a `O(log N)` runtime.

When you have a recursive function that makes multiple calls, the runtime will often (but not always) look like `O(branches ^ depth)`, where branches is the number of times each recursive call branches.
