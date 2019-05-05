---
tags:
  - best practices
  - tips
  - web
---

Always set the CacheControlMaxAge attribute in web.config to a high number (a year is good). You don’t want people pulling down the same static content they did last week. It’ll also save on the bandwidth you’re paying for.