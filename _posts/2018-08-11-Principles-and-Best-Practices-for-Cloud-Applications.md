---
tags:
- microservices
- design
- architecture
---

In this blog post, we will highlight the principles and best practices when developing cloud applications. The nature of cloud applications, being a software program often run in a remote data center operated by a third party and access via a perpetual internet connection, is fundamentally different from the traditional desktop application that sits on a single computer. This difference is also reflected on how we develop cloud applications and how the application behaves when encountering problems. Problems are no longer limited to a single user on a single desktop. As your application scale in the cloud, there is a risk that your problems will scale as well, hence, they will be magnified. Regardless of whether the plan is to deploy the application in the cloud or not, by being mindful of the following principles and best practices, we can ensure the application readiness.

We will focus in the following area:
- Scalability
- Resilience
- Security
- Design
- Operation

# Scalability

A principle of Service Oriented Architecture (SOA) states that cloud applications are designed as stateless and discoverable (via a service registry) services with no affinity to the infrastructure (i.e. no sticky sessions). Hence, as the demand increase, we can deploy more instance of these services and as the load decrease we can scale down the additional instances to save cost.

- Design for horizontal scalability (i.e. scaling out / in vs. up / down).
    - Ensure applications and services are stateless.
    - Avoid client affinity and server-side session state.
    - Partition and decompose workloads into discrete units.
    - Partition around data, network, and compute limits.
    - Minimize coordination and shared state.
    - Distribute background tasks across multiple workers.
    - Consider document/graph DB or de-normalizing data model.
    - Data duplication is correct as long as it is consistent. Duplicate data ownership is bad.
    - Favor dividing data based on usage requirements and type of data.
    - Avoid locking database resources.
    - Prefer optimistic concurrency and eventual consistency.
    - Use low latency external storage when state is needed.
- Reduce resources utilization where appropriate
    - Understand customer SLA for performance.
    - Measure and profile performance with load benchmarks.
    - Cache items that don’t change much.
    - Use CDN for caching static data.
    - Reduce chatty interactions between components.
    - Queue I/O and CPU intensive requests as background tasks.
    - Consider compression and binary format for DTO transfer.
    - Optimize SQL indexes and queries.
    - Minimize time that connections and resources are in use.
    - Minimize number of connections required.
    - Use appropriate data store for different use cases.
    - Extensive reads favor object storage service.
    - Dynamic relationships between data objects favor No-SQL.
    - Large datasets with extensive computation favor Hadoop MapReduce.
- Leverage automation
    - Build golden component images using Docker.
    - Leverage PAAS auto-scaling features with golden images.
- Helpful patterns include: Content Delivery Network, Caching, Decomposition, Elasticity, Encapsulation, Eventual Consistency, Materialized View, Partitioning, Pipes and Filters, Queue Worker and Stateless

# Resilience

In Service Oriented Architecture, each service and each instance of that service has its own lifecycle. Intermittent failure, download and ever-changing environment are the norm and there is a need to recover from them.
- Plan for failure
    - Understand customer SLA for availability.
    - Analyze system to identify failures, impact, and recovery.
    - Use redundant components to minimize single point of failure.
    - Monitor health of dependencies and endpoints.
    - Checkpoint long-running transactions.
    - Design for failure and self-healing.
    - Understand replication methods for data sources.
    - Automate persistent data backup.
    - Document failover/failback processes and test them.
    - Throttle excessively active clients. Block bad actors (DDoS).
    - Perform fault injection testing to verify system resiliency.
    - Data management should be always distributed. You have to assume servers fail and fail often. Relational databases are no longer the norm in the cloud.
- Handle failure
    - Handle transient failures with limited retries and back-off.
    - Handle persistent failures with circuit breaker that falls back to reasonable action while dependency is unavailable.
    - Use load balancing to distribute requests.
    - Use multiple availability zones.
    - Service Discovery is critical. For example, the application talks to a database, but you talk to a logical resource that gives you connections. When you deploy the application, you map this to a real database IP/URL and there is no hard coding.
    - Rely on a location pattern or service that abstracts out the physical IPs.
- Helpful patterns include: Circuit Breaker, Health checks, Load Balancing, Redundancy, Replication, Retry and Telemetry

# Security

There was a time when an application would sit without our own wall and we would feel secure enough. It is like when the entire building belongs to you. Securing the access point would be sufficient. In a cloud environment however, the application can be hosted in multi-tenant environment across multiple networks that are managed by third parties. Therefore, it is imperative to encrypt all communication. Furthermore, we must follow 3 guidelines to secure both our application and our data: secure and encrypt data in transit, secure and encrypt data at rest and control all API/access.
- Pre-emptive measures
    - Apply defense in depth; secure all resources — not just edges.
    - Secure weakest link.
    - Trust reluctantly and verify.
    - Fail securely.
    - Pay attention to data privacy and residency requirements.
    - Protect data at rest (storage encryption) and in transit (SSL).
    - Mitigate DDoS using cloud platform’s network layer.
    - Enforce ACL’s at network, application, and data layers.
    - Conduct vulnerability analysis and penetration tests.
    - Manage keys carefully and secure with hardware tokens.
    - Use SSO, multi-factor authentication, and federated identity.
    - Use anti-virus and anti-malware for network and host nodes.
    - Simplify BCDR through PaaS centric, automated backup and recovery.
    - Integrate diagnostics of network, application, and data layers to have monitor system and correlate enterprise intrusions.
    - Prefer connectivity from cloud to on-prem resources using dedicated, private WAN links vs. VPN tunnels over public links.
- Helpful patterns include: Compartmentalize, Defense in Depth, Federated Identity, Gatekeeper, Least Privilege, Traceability

# Design

The infrastructure against which our application is running should always be considered when designing our application. However, for cloud applications, this is taken a step further. Since we are charged for CPU, memory, network, etc., we need to be mindful about their usage and not be wasteful.
- Design for Infrastructure
    - Design with the organization goals and end-user in mind. Web services are about functionalities and use cases not department structure.
    - Design for evolution and change.
    - Prefer loosely-coupled components whose communication is asynchronous that can evolve, heal, and scale smarter.
    - Separate infrastructure logic from domain logic.
    - Prefer RESTful Web API’s for external communication.
    - Prefer asynchronous messaging for internal communication.
    - The pipe between the service and the client can be limited. You must find way to minimize the amount of data and also the numbers call. Batches can help.
    - Use functional programming when it is meaningful. Functional programming means defining functions dynamically that act on data without state and can be evenly distributed to any number of cloud servers as needed.
- Design for Latency
    - Design application with latency in mind. A scalable system is more reliable under load and maintains a consistent performance. However, latency is added because of the remote execution.
    - Network congestions, request contentions, I/O saturation in storage systems, software and hardware failures etc... These failures do occur time to time, therefore applications must be designed to handle failures gracefully, and automatically recover from failures. A popular design pattern that is used to address latency and failure is request/response queue, where requests and responses are stored in queues so that they are not lost. Also, the applications do not block the user experience when the response is slow or when failure occurs. Rather, they indicate that the request is in progress and let the user continue to use other functions of the application.
- Helpful patterns include: CQRS, DDD, Dependency Inversion, High Cohesion, Interface Segregation, Loose Coupling, Messaging, Open/Closed, REST, Single Responsibility

# Operation

Cloud application is composed of many moving parts. Proper operation is therefore critical to ensure that the application works correctly. Contrary to a monolith application where a single unit needs to be managed, cloud applications involves many little units distributed among many servers.
- Incorporate DevOps
    - Design for IT Ops (Deploy, Monitor, Investigate, Secure)
    - Document system release process and use change control.
    - Automate system build and deployment processes.
    - Implement logging and alerting in all system components.
    - Instrument components to monitor availability, performance, and health.
    - Standardize log formats and metrics.
    - Inventory, inspect, and audit cloud assets.
    - Use distributed tracing (asynchronous, Correlation ID).
    - Version and control configuration like other system artifacts.
    - Use Agile project methodology for iterative development and release.
- Helpful patterns include: Agile, Automation, DevOps, Source Control, Telemetry

# References
- http://jayendrapatil.com/aws-architecting-for-the-cloud-best-practices-whitepaper/
- https://hackernoon.com/principles-and-practices-of-cloud-applications-4a8ef32cab36
