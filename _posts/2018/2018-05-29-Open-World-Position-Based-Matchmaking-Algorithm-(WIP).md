---
tags:
- prototype
- owmm
- design
---

> This is a very early draft without any structure or the like. I did it mostly for storage purposes.

## Overview

The following document provides a general analysis of the requirements for a matchmaking algorithm based on the player’s open world position. It will touch upon the items listed below:

- Define a first prototype
- Identity the use cases we want to be covered and their limitation
- Examine the technological implications of the first prototype
- List further area of investigations

## Purpose

TODO: Summary of what this document is about. We want to describe what is the matchmaking service, what it is doing in our game, what the use cases and/or problems we are trying to handle with it. We also want to identify any axis of investigation whether it is technological or algorithmic.

TODO

1. Connection to the DS
2. Receiving the data
3. Crunching the data
4. Telling people where to go

## Possible Use Cases

TODO: Summary of the different use cases that the matchmaking service address.

### Goal 1: New Players to New Game Servers

### Goal 2: New Players to Existent Game Servers

### Goal 3: Existent Players to New Game Servers

### Goal 4: Existent Players to Existent Game Servers

### Goal 5: Keeping Existent Hard Groups

### Goal 6: Keeping Existent Soft Groups

### Goal 7: TODO

## High Level Architecture

TODO: Draw diagram of multiple dedicated game servers and one matchmaking service.

### Workflow

1. Each dedicated game server has a direct connection to the matchmaking service.
2. Dedicated game servers send players' open world position and related information to the matchmaking service.
3. Matchmaking service decides whether or not to migrate a player or a group of players to another dedicated game servers

## TODO: Focus on most difficult use case. For example, we have thousands of Game Servers with thousands of players sending information to matchmaking service.

## Requirements

TODO: Based on the use cases defined above, we want to define the necessary requirements that the matchmaking service needs to have.

### REQ 1: Handle Massive Quantity of Connections to and from Game Servers

### REQ 2: Handle Massive Quantity of Data from Game Servers

### REQ 3: Quickly Process Data from Game Servers

### REQ 4: Multi Instance Safe

TODO: This means that there could be multiple instance of the Matchmaking Service but they would not give conflicting instructions to Game Servers.

### Area of Investigations

##### Quantity of Dedicated Connection Between Dedicated Game Server and Matchmaking Service

- TODO
- Web Sockets? TCP? UDP? RUDP?
- DGS and MS should be in same datacenter

##### Quantity of Information Between Matchmaking Service and Dedicated Game Server

- TODO
- Serialization / Deserialization
- Protobuf? Binary Custom?

##### Processing and Decision Time on Dedicated Game Server

- TODO
- Filtering / Bucketing Strategy on DGS
  - Dark Zone?
- Filtering / Bucketing Strategy on MS
  - Position
  - Ping

##### Redundancy of Matchmaking Service

- Transient Data
- Single Instance? Multiple Instance with Shared Data?
- TODO

## Algorithm Investigations

### Limiting Game Servers’ Output

TODO: One key way to reduce the workload in the Matchmaking Service is for the Game Servers to send only necessary information.

##### Blackout Zones

TODO: These are areas where transition cannot occurred. Hence, there is no point in sending information to the Matchmaking Service.

##### Blackout Activities

TODO: If players are engaged in activities that would prevent transition then there is no point in sending information to the Matchmaking Service.

##### Transition Zones

TODO: If we identity zones where the occurrences of transitions happen then the Game Servers can send information only at that time.

##### Transition Zones Rank

TODO: If the chances of transitions are different from zones to zones, then the Game Servers can send information at different rates.

### Matchmaking Services

##### Triage by Players’ Position

TODO: There is no point in matching players that are too far from each other’s position.

TODO: We will not need to transition players to the same Game Servers

##### Balancing Game Servers Populations

### Group Info

Can be handle by game server. It can assign a group ID and group strength.

Workers can handle different zone

We can define receiving server and giving servers. To avoid things that I put people just for those people to leave. We will need to rebalance those server.

For example, every time we want to do a decision, we bucket the server into 2.

It can be made stateless

All group information is stored on game server anyway

Historical data can be sent

When the matchmaking service receive it

It creates a map with plenty of dots

And per zone it can define

If only the position of those that are problems are sent then we can build a map with all the dots and decide. There is no skill. Only position. Later we can incorporate group and it would be a bigger dot.

## Processes

Different service for different zone for processing because they have not chance of being together.