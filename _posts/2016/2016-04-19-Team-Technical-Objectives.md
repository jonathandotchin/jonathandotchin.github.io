---
tags:
  - objectives
  - team
---

## Overview

The technical objectives should meet 3 general criteria:

### Quality and Performance

It needs to improve the quality and performance of our tools and services

It could be tools such as Build DB, Asset Store, Watermarking, etc.

- It could be services such as Perforce, JIRA, Confluence, etc.
- It should have, directly or indirectly, a positive impact on the production of games

### Unobtrusive

To be done in conjunction with the day to day activities

- We will not create a specific project or a specific story for these objectives
- They should be additional tasks or improvements to our current processes

### SMARTER

Must be SMARTER (specific, measurable, attainable, relevant, time-bound, evaluate, reevaluate)

- In an agile context, we want to be able to evaluate and reevaluate the objectives and make changes if necessary
- Although it sounds counter to "SMART", the idea is to start large and slowly zone in, as they are more about exploration first

# Objective #1: Deployment Improvements

## Current Situation

Ever since our tools and services are being used worldwide, deployment of newer versions are becoming increasingly difficult, stressful and error prone.

Some of the contributing factors are:

- Small to non-existent deployment windows
- Deployment slows down production teams
  - Potential downtime
  - Lack of backward compatibility
- Constant mismatch between our DEV, TST and PRD environments

## What We Want To Accomplish And How

We want to be able to deploy tools and services that are critical to the production teams while maintaining backward compatibility and the confidence that current tools will not be negatively impacted.

In essence, the deployment needs to be:

- Reliable
- Repeatable
- Backward compatible
- Zero downtime

### Reliable

A reliable deployment is achieved when there is confidence that the code under deployment is tested and validated. The easiest way to achieve this is via test automation. This will in turn increase the confidence in our deployment.

- Unit test
  - Use dependency injection, mocking, avoid the database or any external system
- Integration and functional tests
  - Mock external resources only
  - Tests can be focused around features (more functional)
- Acceptance tests
  - No more mocking of external resources
  - The environment should be controlled
  - May or may not be functional (i.e. could be performance related)

### Repeatable

In order to minimize errors during the deployment, we need to ensure that each deployment is done the same way and on any environment. The deployment of the latest version can be done at any desired time. Continuous Integration is the first step to achieve this.

- Build automation & continuous feedback
  - CruiseControl.NET or other CI tool
- Releasable application
  - Branch for release
  - Incremental changes
  - Hide new functionalities
- Database deployment
  - Tools: Dbdeploy, RoundhouseE, RedGate

### Backward Compatible

In order to give production teams the necessary time to upgrade and adapt to the  new versions of our tools and services, we need to maintain backward compatibility.

- Versioning and best practices of tools and services
- Database
  - Decoupled application deployment from the database migration
  - Application should work with the new and old database
- Clear definition of tools impacted
- Usage statistics

### Zero Downtime

Downtime is the worst enemy of any production teams since it prevents them from working. Downtime must be minimized but there is no silver bullet for it.

- Revised application specific deployment
  - Each application is different so the deployment must takes those particularities into account
- Control the VIP
  - Round robin deployment in a web farm if possible
- Automated if possible
  - Scripting removes human factor

  ## Status

### Legends

| Status     | Meaning                                                      |
| :--------- | :----------------------------------------------------------- |
| **GREEN**  | May not be in place for all our projects in productions but it is an intrinsic part of our development process |
| **YELLOW** | Work in progress, not yet part of our development process    |
| **RED**    | No action has been taken thus far                            |

### Summary

| Quality             | Details                         | Status     | Comments                                                     |
| :------------------ | :------------------------------ | :--------- | :----------------------------------------------------------- |
| Reliable            |                                 |            |                                                              |
|                     | Unit Tests                      | **GREEN**  | Part of "Done When"                                          |
|                     | Functional Tests                | **GREEN**  | Part of "Done When"                                          |
|                     | Acceptance Tests                | **YELLOW** | Manual smoke tests are done<br />Blacksmith is being built   |
| Repeatable          |                                 |            |                                                              |
|                     | Build automation                | **GREEN**  | Part of "Done When"                                          |
|                     | Releasable application          | **YELLOW** | Strategy agreed upon for release on major projects: Asset Store, BuildDB, Magneto<br />Branch by abstraction still to be considered |
|                     | Database deployment             | **GREEN**  | Part of "Done When"                                          |
| Backward Compatible |                                 |            |                                                              |
|                     | Versioning                      | **GREEN**  | Part of the development process<br />Versioning of services and tools are taken into account<br />Verified during peer reviews and tests |
|                     | Database                        | **GREEN**  | Part of the development process<br />Changes in database are backward compatible<br />Verified during peer reviews and tests |
|                     | Ripple Map                      | **YELLOW** | Cross system architecture are being documented<br />Incomplete<br />Not yet verifiable |
|                     | Usage Statistics                | **GREEN**  | Part of "Done When"                                          |
| Zero Downtime       |                                 |            |                                                              |
|                     | Deployment Plan per Application | **GREEN**  | Specific deployment plans are written and respected          |
|                     | Controlled Load Balancing       | **YELLOW** | Currently not possible with current load balancing strategy<br />New load balancer are being purchased |
|                     | Automation                      | **GREEN**  | Pipelines are being built                                    |

# Objective #2: Cross Cutting Concerns and Microservices

## Current Situation

Following several years of developments, many of our products had repeatedly implemented solutions to address specific concerns and solve specific problems. For example, there are multiple solutions to manage users authorization, configuration in an application as well as many solutions to handle package transfers and referential data (production, project, studio, etc.)

This objective is about cutting out duplication and reuse software where it makes sense.

## The Endgame

These so called solutions can take a technological (cross cutting concerns) and/or business forms (microservices) and they can exists within and/or outside a specific applications.

### Cross Cutting Concerns

Cross cutting concerns are those that are not directly related to business value. Our definition of "cross cutting concerns" will not be limited to a single applications but could be expanded to multiple system as well. For instance, authentication and authorization exists in various system in order the identify an user and to define what actions this user can perform. Consequently, an user management system can provide such service centrally. The following is an non-exhaustive list of possible concerns in this category that are actively in used in most of our applications.

| Concerns                                         | Description                                                  | Examples of Solutions                                      |
| :----------------------------------------------- | :----------------------------------------------------------- | :--------------------------------------------------------- |
| User Management / Authentication & Authorization | Authentication is about identifying the user<br />Authorization is about allowing specific actions | Active directory / Security Groups<br />ASP.NET Membership |
| Configuration Management                         | Settings specific to the working of the application<br />Settings specific to some users of the application | A glorified key-value pair<br />Zookeeper<br />Consul      |
| Exception Management and Reporting               | The handling and reporting exception                         | Bloomberg                                                  |
| Logging and Auditing                             | Keeping track of system actions<br />Keeping track of user actions | Logstash<br />Elastic Search                               |
| Instrumentation                                  | Insights on the usage of our application                     | TG Scout                                                   |
| Workflow / Orchestration                         | Executing sequence of tasks                                  | Airflow<br />BizTalk<br />Workflow Foundation              |
| Event / Messaging / Notification                 | Publish events to be consumed by others                      | RabbitMQ<br />ZeroMQ                                       |
| Scheduling                                       | Triggering specific actions based on specific events         | Primitive Windows Scheduler                                |
| Service Registry / Discovery                     | Discover the running service                                 | Eureka<br />Zookeeper<br />Consul<br />Etcd                |

### Microservices

Microservices are more business oriented in the sense that they can provide a direct gain to our clients or serve as a building block to it. They are a web service that can do one thing and does that one thing well. For the scope of this document, sometimes, the line between cross cutting concerns and a microservices can be extremely blurred since solving a cross cutting concerns can take the form of a microservices. For example, transferring a package between Site A and Site B should be provided by a single microservices. It can have multiple endpoints/clients but they all funnel into a single services. This has a direct gain to the clients. One example that is more of a building block would be a service that collect manifests of package.

| Business Use Case            | Example of Solutions                                         |
| :--------------------------- | :----------------------------------------------------------- |
| Package Transfer             | Fedex<br />    Combination of BuildDB transfer, Signiant Transfer and Asset Store Transfer |
| Package Distribution         | WarpGate, File Streamers                                     |
| Digital Mastering of Package | Renamed BuildDB Agents                                       |
| Digital Watermarking         | Hide                                                         |
| Digital Signature            | DigiSign                                                     |

## Road to Success

The following represents an non exhaustive list of criteria that will contribute the successful implementation of a cross cutting solutions / microservices that can be re used across different projects and different teams.

| Criterion                  | Description                                                  |
| :------------------------- | :----------------------------------------------------------- |
| Multi Platforms            | Examples Clients on multiple platforms<br />Centralized servers |
| Standardized Communication | Request Reply<br />Event Based                               |
| Reusable                   | Clear contracts<br />Versioning<br />Changes are easy to consume by new consumer |
| Compartmentalization       | It does one thing and does that thing well<br />Clear boundaries |
| Collaboration              | Joint venture among many teams<br />Technology standardization<br />API contracts |
| Connect                    | API Gateway / Proxy<br />Present a business use case         |