---
tags:
  - best practices
  - tips
---

Every time you update your site, IIS must recompile it during the first request, so the initial request takes significantly longer than subsequent ones. An easy solution is to tell IIS to automatically recompile your site as part of the update process. This can be done using the startMode attribute in the ApplicationHost.config file. You can even specify a custom action to run on start-up, such as pre-populating a data cache.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)