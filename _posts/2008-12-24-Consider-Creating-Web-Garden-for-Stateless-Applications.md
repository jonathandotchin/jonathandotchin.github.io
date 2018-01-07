---
tags:
  - best practices
  - tips
  - asp.net
---

By default each Application Pool runs with a Single Worker Process (W3Wp.exe). We can assign multiple Worker Processes With a Single Application Pool. An Application Poll with multiple Worker process is called "Web Gardens". Many worker processes with the same Application Pool can sometimes provide better throughput performance and application response time. And each worker process should have their own Thread and Own Memory space.

In the advance settings of the "Application Pools", under "Process Model", consider the number for "maximum worker process".

http://blogs.iis.net/chrisad/archive/2006/07/14/1342059.aspx