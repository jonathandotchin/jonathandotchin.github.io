---
tags:
  - best practices
  - tips
  - performance
---

Since the code generated from the ORM can frequently be ad hoc, ensure that the SQL Server instance has ‘Optimize for Ad Hoc’ enabled. This will store a plan stub in memory the first time a query is passed, rather than storing a full plan. This can help with memory management.
