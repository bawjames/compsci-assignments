# James Baw Assessment Report
**I confirm that the following work is entirely my own, and that AI was not used in the making of it.**
The primary external resource I used was the official C# language reference (Microsoft Learn).
Also note that my original code was split into appropriate files, containing appropriate namespaces, following better code practices. The original files are hosted [here](github.com/bawjames/cs-assignment-01).

## Handling of Iteration
When iterating over elements of a datastructure, I used a `for` loop, or in some cases the `Select` function for terse transformation of elements during database querying.

When receiving user input, I wrote a helper function `Input` that uses a `while` loop, reprompting the user for input upon an invalid input.
Another helper function, `Menu` (which is functionally very similar to Bash's `select` statement), also uses a `while` loop for the same reason.

## Manipulation of Data Types
Some manipulation of data types is carried out in validator functions, converting `String` (input) into the desired output type.
In the `Library` class, I use methods such as `Select`, `Add`, or `ToArray` for manipulating the `List` of `Book`s in order to add books or query books in the library.
Most other forms of manipulation of data types is in the form of primitive casting (e.g, `int` into `decimal`), which is built into the C# compiler.

## Selection
The user selects which application to use via the `Menu` function, which has built-in error handling. It allows the caller to pass an array of `Action`s (functions with no arguments and void return type), and a corresponding description for each action. The descriptions are then printed along with their index in the array, and the user selects which action to execute using the number keys. Only numbers between 0-9 are accepted.

## Error Handling
Since each of the functions are extremely simple, they are only fallible if passed an invalid input, such as with an incorrect data type, or one which has not been validated. For this reason, `Validators` is a class containing methods for ensuring that an input is valid, and in some cases performing additional transformation immediately on the input (e.g, `ValidateDenary`, `ValidateHexadecimal`).
These validator functions are then passed to the `Input` function at the point of use, and `Input` will loop indefinitely until a valid input is received.
