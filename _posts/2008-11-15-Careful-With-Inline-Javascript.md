---
tags:
  - best practices
  - tips
  - performance
---

An example would be 

``` javascript
<button onclick="validate()">Validate</button>
```

This practice breaks the clean separation that should exist between markup, presentation, and behavior. Also, if scripts load at the bottom of the file, it is possible for a user to interact with the page and trigger an event that [attempts to call a script](http://www.quirksmode.org/js/events_early.html) that isn't loaded yet – causing an error.

Use JQuery or addEventListener