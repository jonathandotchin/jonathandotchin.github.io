---
tags:
  - best practices
  - tips
  - security
---

This isn’t exactly a performance tip but rather a security tip for when people think that they could improve performance by cutting out server-side validation. These days, client-side validation can be bypassed with ease, so you can’t trust what comes from the browser. So if you think you can save some processing cycles and bypass these steps, don’t, as you’re actually opening massive security holes.