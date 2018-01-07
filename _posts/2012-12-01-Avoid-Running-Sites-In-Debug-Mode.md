---
tags:
  - best practices
  - tips
  - asp.net
---

This may sound simple but it is so easy to forget. Do not publish your ASP.NET application in debug mode. We can do as much optimizations in our code as we want by using StringBuilders, Arrays, Switch, but if we deploy our site in debug mode, the impact would dwarf any optimizations made.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)