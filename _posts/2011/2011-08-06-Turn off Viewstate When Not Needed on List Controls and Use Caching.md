---
tags:
  - best practices
  - tips
---

The same goes for most implementations of Repeaters, ListViews, and so on. These are usually the biggest culprits and they can be ugly. The advantage of ViewState with these is avoiding having to populate values again in a postback. If you’re convinced that it’s worth passing ViewState back and forth again and again to save your app the extra database hit…well…you’re probably wrong. Save the database hit (if you need to) with some caching and disable that dang ViewState on that Repeater!

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)