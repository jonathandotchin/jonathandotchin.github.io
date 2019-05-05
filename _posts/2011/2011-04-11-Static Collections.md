---
tags:
  - best practices
  - tips
---

If a collection is static, make sure it only contains the objects you need. If the collection is iterated over often, then the performance can be slow if you don’t remove unnecessary objects. Objects in a collection will be held in memory, even if they have been disposed of, which can also lead to a memory leak.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)