---
tags:
  - best practices
  - tips
  - performance
---

Similarly to external JavaScript, inline script requires the browser to stop parsing the HTML and can also prevent the parallel downloading of other resources. But unlike an external JavaScript, it may not be cached (i.e. different HTML page, same external JavaScript). Bit and pieces of JavaScript inline is also hard to maintain and could result to many repetitions. Overall the initial load of the page can be affected resulting in the dreaded ["blank-page" experience](https://developers.google.com/speed/articles/include-scripts-properly).
