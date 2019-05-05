---
tags:
- microservices
- design
- architecture
---

Microservices is a huge buzzword nowadays. Everyone wants to use microservices, everyone thinks they are using microservices. But microservices is much more than a buzzword. It is architectural style that requires a different approach to developing applications.

# Introduction to Microservices

## What is this Architectural Style?
In a microservices style architecture, the application is composed of small and independent services; each of them called confusingly a microservice. Each microservice will have the following characteristics:
- It implements a single business capability.
- It is small enough for a small team of developers to maintain it.
- It runs in a separate process and communicate via well-defined APIs.
- It does not shared data stores or data schemas with each others.
- It has its own separate code bases but common utility libraries are allowed.
- It can be deployed and updated separately from other microservices.
## What are the Benefits?
If used correctly, the benefits are numerous:
- Better release management
    - A single service can be updated without affecting the entire application as opposed to a monolith where a release can be held back by a bug found in another part of the code base.
- Smaller code
    - A smaller codebase leads to smaller teams and to a better understanding of the code base.
- High cohesion and low coupling.
    - By not sharing code between microservices, we minimize dependencies.
- Proper technologies
    - Each service, being independent, can be built with the technologies that best fit them.
- Better resilience
    - An outage of an individual services won't disrupt the entire application provided proper fault tolerance (i.e. circuit breaker pattern)
- Better scalability
    - If a microservice is more resource hungry than others, this microservice can be scaled out independently.
- Data isolation.
    - Since each microservice is responsible for its own data and communicate via well defined APIs and interfaces, data changes do not leak to other services.

One thing to keep in mind is that several of the benefits can be achieved without the use of a microservices style architecture. For example, we do not need to have a network separation in order to achieve high cohesion and low coupling. The same goes with smaller code, better resilience and data isolation. This can be achieve with a monolith application with proper domain-driven design. Small team can be responsible for components of the monolith and communicate via well defined interfaces. Benefits such as better release management, proper technologies and better scalability are much more difficult to achieve in a monolith as its size increase. Remember, each microservice is basically a micro monolith. Therefore, make sure you are using microservices for the right reason.
## What are the Challenges?
As anyone can imagine, there are several challenges to overcome before one can reap the benefits mentioned above.
- Proper service boundaries
    - One of the hardest thing to get right is to properly decide what goes in each microservices. Once a service is built and deployed, it becomes next to impossible to refactor since client may already depend on it.
- Data management
    - Each microservice manage its own data but this can have an adverse effect in the sense of data integrity and redundancy.
- Network
    - The presence of multiple services can result in an increase of communications and latency.
- Complexity
    - Each microservice may be simple but the overall architecture is now composed of a great amount of moving parts.
- Communication with the backend
    - The backend is now composed of multiple services. A question is raise as in how should client communication with them? Individually? Route through an API Gateway?
- Monitoring
    - Monitoring microservices individually would present an incomplete picture of the system. The telemetry from multiple services must be correlated.
- Continuous integration and delivery
    - Since each microservices can be deployed individually and there are a greater number of them, procedural manual deployment will no longer be viable. The key is automation and this is where continuous integration and delivery comes into play.

We will look at each of these challenges individually and how we can overcome them.

# References

[Introduction to Microservices](https://docs.microsoft.com/en-us/azure/architecture/microservices/)