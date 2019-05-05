---
tags:
  - best practices
  - tips
---

Yes, if you are in a loop and adding to a string, then a StringBuilder could be most appropriate. However, the overhead of spinning up a StringBuilder instance makes the following pretty dumb:
```
var sb = new StringBuilder();
sb.Append(“Frankly, this is “);
sb.Append(notMoreEfficient);
sb.Append(“. Even if you are in a loop.”);
var whyNotJustConcat = sb.ToString();
```
Instead, use String.Join, which is typically more performant than spinning up a StringBuilder instance for a limited number of strings. It’s my go-to concat option:
```
string key = String.Join(“ “, new String[] { “This”, “is”, “a”, “much”, “better”, solution, “.”});
The first variable of " " can just be set to "" when you don’t want a delimiter.
```
For loops that do a lot of, er, looping, sure, use a StringBuilder. Just don’t assume it’s the de facto solution in all, or even the majority of cases. My rule of thumb is to add strings together when I’ve got one to five of them (likewise with String.Format if it helps with legibility). For most other cases, I tend towards String.Join. Only when dealing with a loop that isn’t limited to about 10 iterations, especially one that really lets rip, do I spin up a StringBuilder.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)