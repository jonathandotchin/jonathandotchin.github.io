---
tags:
  - best practices
  - tips
  - performance
---

CSS parser hacks are unreliable because browsers can be updated causing these hacks to fail. Instead try adding version-specific classes to the html or body tag that can be used in stylesheet rules. Conditional comments can also be used to include a browser-specific CSS file only in the versions that need it.