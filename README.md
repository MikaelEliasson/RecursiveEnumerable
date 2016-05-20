# RecursiveEnumerable
A demostration of how an recursive list could simplify and reduce bugs in many code bases where iterating trees is common.

The code is free to use and modify but no support is provided.

## Examples

### Iterating up in the tree

```
var b1 = new Block(1);
var b11 = b1.Add(11);
var b12 = b1.Add(12);
var b21 = b11.Add(111);

var enumerable = new RecursiveList<Block>(b21, b => b.Parent).Select(b => b.Id).ToList();

enumerable.ShouldBe(new List<int>() { 111, 11, 1 });
```

### Iterating down in the tree

```
var b1 = new Block(1);
var b11 = b1.Add(11);
var b12 = b1.Add(12);
var b21 = b11.Add(111);

var enumerable = new RecursiveList<Block>(b1, b => b.Children).Select(b => b.Id).ToList();

enumerable.ShouldBe(new List<int>() { 1, 11, 12, 111 });
```
