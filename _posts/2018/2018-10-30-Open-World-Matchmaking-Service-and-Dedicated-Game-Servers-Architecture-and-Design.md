---
tags:
- microservices
- owms
- design
---

## Overview

The purpose of this document is to explore the general architecture and design of the open world matchmaking service and how it is related to the dedicated game servers. It will focus on the following aspects:

- An overview of what the open world matchmaking service is trying to accomplish.
- A basic architecture and design of the components involved.
- An early investigation of the possible information exchanged, how they are filtered and analyzed with a first prototype. 

## Use Case

### Definition

Given a finite number of dedicated game servers, the open world matchmaking service will ensure that there is a consistent number of players on each dedicated game servers within the same "area" of the map. Consequently, the following behaviors will be observed on each dedicated game server:

- Ideally, there will be 10 to 12 players on each dedicated game server at all time.
- The world will be divided into zones
- The players in the same zones will be moved to the same dedicated game server.

### Scope

We will limit this investigation only to the players' positions. Therefore, we will ignore the following cases and also make several assumptions for now:

- All group information, soft or hard, is ignored.
- Historical information of any kind is not taken into account.
- Factions are ignored.
- Players' field of vision will be ignored
  - We will allow players to pop in and out during matchmaking

The above assumptions will need to be taken into account eventually but not in this prototype.

## Basic Architecture and Design

### System Diagram 

![System]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/System.png "System"){: .align-center}

- There is a direct connection between the dedicated game server and the open world matchmaking service.
- The dedicate game servers run the game, keep track of player information and send it to the open world matchmaking service.
- The open world matchmaking process the information from the dedicated game servers.
- The open world matchmaking instructs dedicated game servers to perform player session migration.
- The involved dedicated game servers perform the player session migration.
  - Therefore, there is communication between dedicated game servers (not shown here since it is out of scope of this document)

### Sequence Diagram

#### General Flow

![General]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/general.png "General"){: .align-center}

1. Tracking Dedicated Servers
   1. The dedicated game servers detect that the need to contact the open world matchmaking service.
   2. This can happen by default and can be always on for an open world server.
   3. This is analogous to registering the game server with the service.
2. Tracking Players
   1. The dedicated game servers send player information information to the open world matchmaking service.
   2. This is done on a regular basis until the dedicated game servers no longer have the desire.
   3. This can be done by a service that would live inside the dedicated game server.
3. Perform Basic Matchmaking
   1. The open world matchmaking service runs the information through the algorithm in order to find matches.
   2. New information can arrive and will be taken into account on the next run of the algorithm
4. Instruct Dedicated Servers for Session Migration
   1. When the open world matchmaking service finds a positive outcome, it instructed the dedicated game servers to perform sessions migrations.
   2. The session migration can involve multiple dedicated game servers (2 or more).
   3. The creation of a new dedicated game server is possible but beyond the scope of this prototype flow.

At the end of the workflow mentioned above, the dedicated game servers perform the session migrations for the players involved.

#### Track Dedicated Servers

![Dedicated]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/dedicated.png "Dedicated"){: .align-center}

1. The dedicated game servers detect that the need to contact the open world matchmaking service.
   1. This can happen by default and can be always on for an open world server.
2. The dedicated game servers register themselves with the open world matchmaking service.
3. The open world matchmaking service would maintain a persistent communication to each of the dedicated servers
   1. This will serve to push notification for session migration when matchmaking is successful

#### Track Players

![Track]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/track.png "Track"){: .align-center}

Having being registered with the open world matchmaking service, the dedicated game server proceed with players information.

1. The dedicated game servers register a player with the open world matchmaking service.
   1. In essence, when a new player is detect in the dedicated game server, it is registered with the open world matchmaking service
   2. Likewise, when it is no longer on that dedicated game server, it should be deregistered.
   3. It is possible for new player to arrive or leave at any time
2. On a regular basis (TBD), the dedicated game server send players' information to the open world matchmaking service
   1. The information is necessary for the matchmaking algorithm.

## Basic Matchmaking

### Input

The open world matchmaking service would receive the following information from each dedicated game server looking for a match:

- The dedicated game server identity
- The player identity.
- The position of each player.

The open world matchmaking service will be initialized with the following information

- The size of the map
  - The smallest and biggest possible XY value
- The size of the zone
- The numbers of players per dedicated server
  - i.e. 12

### Computing

#### Assumptions

- We will simply divide the entire map into zones and we will simply matchmake players accordingly.

- We will ignore the case of deadlock at this moment by assuming that the server population can easily go over the maximum of 12.

  - Given that we have server A, B and C each with 12 players in it.

  - We would need to move 6 players from A to B but B needs to move 6 to C and C needs to move 6 to A.

#### Workflow

1. Register the DS
2. Register the Players for each DS
3. Split the map into zones and maintain the zones as bucket.
4. As players' information comes in, create matchmaking request when necessary
   1. Move them in the appropriate bucket
   2. We can update the content of the bucket to represent new players' information
      - Location changed
   3. - Matchmaking no longer required
5. Once a bucket is filled, we issues the session migration order for the dedicated servers involved.
   1. The destination dedicated servers should have enough room for the new arrivals
6. The dedicated servers involved would be lock from further processing until the migration is done.

### Output

The following information would be communicated by the open world matchmaking service to each dedicated game server involved in players' session migrations.

- The player identity 
- The future home (the dedicated game server identity) of the player

The involved dedicated game servers would, thereafter, communicate among themselves to perform the players' session migration.

## First Prototype

### Scope

- The scope of the first prototype will be limited to the basic matchmaking algorithm explained above.
- Communication protocol and serialization technology investigation is out of the scope of this prototype
  - We will simply use gRPC.

### Technology

#### Open World Matchmaking Service

Built in .NET Core

#### Communication between Dedicated Game Server and Open World Matchmaking Service

We will leverage gRPC immediately for communication between the dedicated game server and the open world matchmaking service.

## Sequence

### Diagram

![Sequence]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/sequence.png "Sequence"){: .align-center}

### Legend

- Open World Matchmaking Web Service
  - Represents an instance of the Open World Matchmaking Web Service being deployed on the cloud. You can think of this as the unit of deployment.
- Open World Matchmaking Service
  - Represents the component that contains the communication layer.
- Open World Matchmaking gRPC API
  - Is responsible from interpreting gRPC calls
- Open World Matchmaking Strategy
  - Represents the component that contains the open world matchmaking algorithm
- Session Coordinator
  - Is responsible for coordinate gaming sessions
  - Currently, only through speed dating
  - Later, it could also be through server re-balancing
- Open World A
  - One of the open world involved in the matchmaking
  - Is responsible for maintaining the lifetime of a matchmaking request
- Open World B
  - One of the open world involved in the matchmaking
  - Is responsible for maintaining the lifetime of a matchmaking request
- Roamer A1
  - A player roaming on Open World A
- Roamer A2
  - A player roaming on Open World A
- Roamer B1
  - A player roaming on Open World B
- Roamer B2
  - A player roaming on Open World B
- Matchmaker
  - Is responsible for maintaining and processing the open world map of matchmaking
- Zone
  - Represents a bucket where the matchmaking algorithm is applied

### Workflow

1. A DS register an open world map in the OWMM service through the open world matchmaking gRPC Api.
2. The call is interpreted and forwarded to the session coordinator.
3. The session coordinator creates an internal representation of the world.
4. A DS register an open world map in the OWMM service through the open world matchmaking gRPC Api.
5. The call is interpreted and forwarded to the session coordinator.
6. The session coordinator creates an internal representation of the world.
7. A DS register a player (roamer) in the OWMM service through the open world matchmaking gRPC Api.
8. The call is interpreted and forwarded to the session coordinator.
9. The session coordinator creates an internal representation of the roamer.
10. The session coordinator adds the roamer to the world.
11. A DS register a player (roamer) in the OWMM service through the open world matchmaking gRPC Api.
12. The call is interpreted and forwarded to the session coordinator.
13. The session coordinator creates an internal representation of the roamer.
14. The session coordinator adds the roamer to the world.
15. A DS register a player (roamer) in the OWMM service through the open world matchmaking gRPC Api.
16. The call is interpreted and forwarded to the session coordinator.
17. The session coordinator creates an internal representation of the roamer.
18. The session coordinator adds the roamer to the world.
19. A DS register a player (roamer) in the OWMM service through the open world matchmaking gRPC Api
20. The call is interpreted and forwarded to the session coordinator.
21. The session coordinator creates an internal representation of the roamer.
22. The session coordinator adds the roamer to the world.
23. Detect the need to matchmake a roamer.
24. Queue for matchmaking.
25. Add the matchmaking request to the proper zone (i.e. bucket).
26. Detect the need to matchmake a roamer.
27. Queue for matchmaking.
28. Add the matchmaking request to the proper zone (i.e. bucket).
29. Detect the need to matchmake a roamer.
30. Queue for matchmaking.
31. Add the matchmaking request to the proper zone (i.e. bucket).
32. Detect the need to matchmake a roamer.
33. Queue for matchmaking.
34. Add the matchmaking request to the proper zone (i.e. bucket).
35. A DS update roamers information through the open world matchmaking gRPC API.
36. The call is interpreted and forwarded to the session coordinator.
37. The session coordinator update the roamers through the open world.
38. The open world update roamer A.
39. The open world update roamer B.
40. The open world update the matchmaking request.
41. The matchmaker process the zone for potential session migration.
42. A match is found the specified zone.
43. The matchmaker raise a notification to the session coordinator.
44. The session migration request is forwarded to the open world matchmaking gRPC api.
45. The open world matchmaking gRPC api interpret and forward the information to the needed DS.