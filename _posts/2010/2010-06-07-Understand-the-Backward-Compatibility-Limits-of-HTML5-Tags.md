---
tags:
  - best practices
  - tips
---

New HTML5 tags like <section>, <header>, and <footer> improve the semantics of markup, but require a special "shiv" script to run in Internet Explorer 6, 7, and 8 or they won't be recognized. Pages that need to work with these legacy browsers even when scripts are disabled cannot use the new HTML5 tags. Using plain <div> elements and classes is often a safer course of action for those cases.