---
tags:
  - web service
  - asp.net
  - performance
---

In this short post, we will examine a small error in our client library implementation that lead to unexpected bursts of calls to our backend service.

In essence, we had a simple requirement that, every 20 minutes, we need to send to our backend service a tracking profile. This tracking profile contains a significant information, which is why we can only send it only every 20 minutes. However, instead of using relative origin (the time the app booted) for the start of the 20 minutes, we used an absolute origin (i.e. every 20 minutes based midnight on the UTC clock). The end result was at every interval 20 minutes (hh:00, hh:20, hh:40, etc.), every clients in the world would send the tracking profile. Luckily, we noticed these bursts of the same event at regular intervals during our tests and we have updated our monitoring system to be alerted of this kind of things in the future.