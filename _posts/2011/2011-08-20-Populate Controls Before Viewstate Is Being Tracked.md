---
tags:
  - best practices
  - tips
---

If you do need ViewState, understand the page lifecycle and bind your data appropriately. A control loads its ViewState after Page_Init and before Page_Load, i.e. server controls don’t start tracking changes to their ViewState until the end of the initialization stage. Any changes to ViewState mean a bigger ViewState, because you have the before value and the after value. So, if you’re changing or setting a control’s value, set it before ViewState is being tracked, if possible.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)