---
tags:
  - best practices
  - tips
  - web
---

Start with <!DOCTYPE html>.  The modern web has no place for Quirks Mode, which was designed so that mid-1990s web pages would be usable in turn-of-the-century "modern" browsers like IE6 and Firefox 2. Most web pages today end up in Quirks Mode accidentally because of an invalid doctype or extraneous text before the doctype. This can cause strange layout issues that are hard to debug.