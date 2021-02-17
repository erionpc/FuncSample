# SQLQueryHelper

A .NET Core library which illustrates an approach of generating a portion of a WHERE clause in a SQL query on the basis of varying filtering criteria which need to be combined through logical OR.

## Context

Imagine you have an application which selects a list of items from a database table running a query like this:

```SQL
select * from items where active = 1
```

Imagine you have a filter in your application which allows you to select multiple filtering criteria for your items which need to be combined using `OR`. For this example we're only going to consider boolean filters (e.g. isOnSale, hasSpecialOffer, etc). You won't know how many of these filters have been set by the user and the application has to translate the filter into a series of `OR` clauses for a `WHERE` in a SQL query. If you were to write down all the possible combinations for just 3 possible filtering criteria, you would end up with 7 `If` blocks in your code. What if there were 6 filtering criteria, or more? This solution takes this problem away by calculating the combinations automatically and it uses `Func`s to reduce the amount of code for handling this.

## Example
```SQL
select * from items where active = 1 AND (hasSpecialOffer = 1 OR isOnSale = 1)
```

## The code
The filtering criteria is passed as a `Dictionary<string, bool>` to the `AppendOrBlockToQuery` method in [FuncSample.Lib.SqlQueryHelper](FuncSample/FuncSample.Lib/SqlQueryHelper.cs). The key in the dictionary is the name of the column in the database table, the value is the corresponding filter value. If true, it means that the filtering criteria was selected. [FuncSample.Lib.Extensions](FuncSample/FuncSample.Lib/Extensions.cs) contains a generic extension method for IEnumerable<T> which calculates the combinations for a collection of items. This extension method is used in [FuncSample.Lib.SqlQueryHelper](FuncSample/FuncSample.Lib/SqlQueryHelper.cs) to calculate the combinations of a `IEnumerable<Tuple<Func<bool>, string>>`. 

## The tests
[FuncSample.Lib.Tests.SqlQueryHelperTests](FuncSample/FuncSample.Lib.Tests/SqlQueryHelperTests.cs) contains xUnit unit tests which test [FuncSample.Lib.SqlQueryHelper](FuncSample/FuncSample.Lib/SqlQueryHelper.cs), showing what output should be expected for different scenarios.
