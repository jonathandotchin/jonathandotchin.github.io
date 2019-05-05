---
tags:
  - best practices
  - tips
---

Putting CSS files in the body can result in the browser showing a blank page until the CSS has loaded. CSS files should go into the head of the HTML document to allow the browser to start fetching them as early as possible.