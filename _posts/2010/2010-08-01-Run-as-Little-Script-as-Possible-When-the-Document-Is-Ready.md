---
tags:
  - best practices
  - tips
  - performance
---

Techniques such as jQuery's $(document).ready() make it easy to run script as soon as the HTML on the page is loaded, which is usually the earliest possible moment it can be safely run. However, running a lot of complex script at this point can make the page appear sluggish and prevent the user from interacting with it immediately. Often the initialization for things like a tooltip or dialog can be delayed until the item actually needs to be displayed, with no noticeable stutter.