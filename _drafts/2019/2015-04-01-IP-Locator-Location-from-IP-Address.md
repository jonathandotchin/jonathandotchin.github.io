---
tags:
  - poc
  - ops
  - prospect
---

## Context

### IP Locator Defined

An internal service called locator is currently used by our application (namely the Asset Store) in order to detect in which studio an user belongs based on their IP address.

### Opportunity

##### Assassin's Creed Unity

They manually coded a studio resolution system based on ranges of IP addresses. They used this information to create a fallback system for slimpack.

##### Watch Dog 2

They have expressed an interest in having an API where it is possible to detect in which studio a specific user is located based on the IP address. He would like to use such API to detect users belonging to studio without Asset Store and disable specific features.

### Benefits

##### Reduce Code Duplication

As more and more product uses products and services that are location based, having a location service to pinpoint an user location based on IP address is critical to properly design workflow such as selecting the right asset store in the right studio. The logic to return to the right studio is often duplicated among several production.

##### Common Reference

The main benefit of providing IP Locator as a Service would be that it would create a common reference that will be usable by all productions for their tools. For instance, MTL will be the code name for studio for all production.

##### Up to date data

The data used by the locator to identify the studio will be maintained and updated as IP ranges are added or removed. Productions will not need to keep track of these changes anymore.

## Current

The current tool is internal only. It is currently used mainly in the API of the Asset Store.

The service has 2 functions:

- Automatically return the studio based on the caller's IP address
- Return the studio based on an IP address provided by the caller as a parameter

## Future

Offer the tool as an service for all productions. The initial will provide the same basic functions. Missing functions can be added at the request of the productions.

## What's Missing?

### Validate Requirements

- We need to validate with the productions their needs
  - Functions?
  - Request per seconds?
  - Latency?

### Reliability

- We need to host the service inside a farm of machine instead of a single server.

### Operationalization

- We need to provide a way to add new address range easily for ops
- We need to add logging to facilitate debugging if there is a problem.

### Service Level

- We need to define a service level
  - Time for new address range to be added?

### Logging

- Current logging is only in the database and provide only requestor ip, requested ip and the date. We should centralized with more information