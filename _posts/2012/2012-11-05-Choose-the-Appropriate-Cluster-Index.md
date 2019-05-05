---
tags:
  - best practices
  - tips
  - asp.net
  - performance
---

You get exactly one clustered index on a table. Ensure you have it in the right place. First choice is the most frequently accessed column, which may or may not be the primary key. Second choice is a column that structures the storage in a way that helps performance. This is a must for partitioning data.

Furthermore, clustered index are always sorted on disk. To avoid fragmentation, you should put it on a sequential value if possible. For example, putting on a MD5 will create lots of fragments especially if there is lots of insert in the table.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)