---
tags:
  - best practices
  - tips
  - web
---

The userAgent string is a poor indicator of whether a particular feature (or bug) is present. To compound the problem, much of the code that interprets userAgent does so incorrectly. For example, one browser-sniffing library expected the major version to be only a single digit, so it reported Firefox 15 as Firefox 1 and IE 10 as IE 1! It is more reliable to [detect the feature](http://msdn.microsoft.com/en-us/magazine/hh475813.aspx) or problem directly, and use that as the decision criteria for code branches.  We recommend [Modernizr](http://modernizr.com/) as the easiest way to implement feature detection.