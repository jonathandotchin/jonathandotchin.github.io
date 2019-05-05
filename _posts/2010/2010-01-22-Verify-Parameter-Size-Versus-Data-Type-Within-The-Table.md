---
tags:
  - best practices
  - tips
  - performance
---

Be sure your code is generating a parameter size equivalent to the data type defined within table in the database. Some ORM tools size the parameter to the size of the value passed. This can lead to serious performance problems.
