---
tags:
- microservices
- size
- design
---

# NTP Service

The NTP service an good example of a microservices but also a good example of what we should not have in terms of size of our web service in our online back end. Instead of having a single service whose scope is to perform clock synchronization we should have a single service whose scope should be doing world synchronization. This can include: time, weather, events, etc. In essence, anything that should be the same across planes of existence.

The so called NTPservice or time service should really be the world service that synchronize worldwide or multiverse event. Time is just one of them. There is weather, news, event or faction war, natural disaster, etc.