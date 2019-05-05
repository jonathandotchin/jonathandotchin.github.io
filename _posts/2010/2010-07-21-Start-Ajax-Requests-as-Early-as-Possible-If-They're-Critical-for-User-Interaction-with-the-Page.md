---
tags:
  - best practices
---

Start AJAX requests as early as possible if they're critical for user interaction with the page

Since an AJAX request can take a long time, there's no need to wait for the HTML page to be ready before starting it. Instead, place the $(document).ready() call inside the AJAX completion function.