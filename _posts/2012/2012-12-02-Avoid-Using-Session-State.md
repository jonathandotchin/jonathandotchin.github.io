---
tags:
  - best practices
  - tips
  - asp.net
  - performance
---

Where possible, you should try and avoid using session state, especially in process session state. While using one web server, performance is usually not a problem. This changes as soon as you need to scale to multiple servers, as different, and usually slower, techniques need to be used. If you are using in process session state with multiple servers, you will be forced to enable sticky session, thus, it could result in a uneven distribution of load among your servers.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)