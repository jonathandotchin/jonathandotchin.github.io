---
tags:
  - best practices
  - tips
  - performance
---

As with external script references, an inline script requires the browser to stop parsing HTML and can also prevent parallel downloading of other resources on the page. This can seriously slow the initial load of the page and give the user a dreaded "blank-page" experience.  Script sprinkled around the markup is also more difficult to maintain.