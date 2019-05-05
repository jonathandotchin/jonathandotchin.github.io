---
tags:
  - best practices
  - tips
  - performance
---

When using grid UI controls (framework based, or 3rd party owned), you should carefully consider how paging is implemented. Many controls implement paging in a simplistic fashion, where the database is required to return all available data and the control limits what is shown. This strategy is fraught with performance problems, as it means that all the data in the given set must be extracted from the database (e.g. all customers or orders). Depending on the records involved, this could cause significant problems.
