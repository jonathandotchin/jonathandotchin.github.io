---
tags:
  - best practices
  - tips
  - performance
---

Always profile your ORM database hits with SQL Profiler during development. ORMs get away from you very quickly. Before you know it, you’ve run a query 2000 times in a loop, when you could have retrieved all your data with a single database hit.
