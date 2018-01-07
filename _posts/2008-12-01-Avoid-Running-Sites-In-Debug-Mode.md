---
tags:
  - best practices
  - tips
  - asp.net
---

This may sound simple but it is so easy to forget. Do not publish your ASP.NET application in debug mode. We can do as much optimizations in our code as we want by using StringBuilders, Arrays, Switch, but if we deploy our site in debug mode, the impact would dwarf any optimizations made.