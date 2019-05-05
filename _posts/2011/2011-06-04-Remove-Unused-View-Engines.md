---
tags:
  - best practices
  - tips
---

If you’re an ASP.NET MVC developer, you might not know that ASP.NET still loads the View Engines for both Razor and Web Forms by default. This can cause performance issues because MVC will normally look for Web Forms views first, before switching over to the Razor views if it can’t find them.

You can quickly eliminate this performance issue by adding the following two lines to your Global.asax, in the Application_Start():
```
ViewEngines.Engines.Clear();
ViewEngines.Engines.Add(new RazorViewEngine());
```
Goodbye Web Forms View Engine!

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)