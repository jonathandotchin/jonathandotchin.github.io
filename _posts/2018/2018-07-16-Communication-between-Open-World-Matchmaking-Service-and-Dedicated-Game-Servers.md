---
tags:
- workflow
- owmm
- design
---

## Overview

The purpose of this document is to explore the communication between the open world matchmaking service and dedicated game servers with a first prototype focusing on the following aspects.

- A simple use case from which we get a series of assumptions to simplify the investigations
- A basic architecture and design of the components involved
- An exploration of the possible technologies used to communicate between the open world matchmaking service and the dedicated game servers

## Use Case

### Definition

Given a constant number of dedicated game servers, the open world matchmaking service will receive players' information such as the position from each for the purpose of performing session migration.

We will not look into the following

- Spawn or decommission a new dedicated game server
- Direct new players to the dedicated game server
- Session migration or instruction for session migration

### Assumptions

For the sake of this first prototype, we will be making the following assumptions to simplify our investigations. These assumptions may not be valid for the final product but we want to put some boundaries to the investigation.

- A single instance of the open world matchmaking service
- 1 000 000 concurrent players
  - 1 000 instances of the dedicated game servers
  - 1 000 players on each instance of the dedicated game servers

## Basic Architecture / Design


### Workflow

1. Given a set of dedicated game server and a single open world matchmaking service
2. Each dedicated game server maintains a connection with the open world matchmaking service
3. At specific time intervals, the dedicated game server will send information on the players to the open world matchmaking service
   1. The information can include players' position, network information, groups, etc.
4. The matchmaking service proceeds to analyze and to direct session migration if necessary

## Investigation

### Communication between Dedicated Game Server and Open World Matchmaking Service

If the OWMS is in a container, how does connection on specific work? The port of the container is not the same as the host so we will need to dynamically assigned.

#### Communication Protocols

##### Websockets

TODO: Pros and Cons

##### TCP

TODO: Pros and Cons

##### (R)UDP

TODO: Pros and Cons

#### Serializations Technologies

##### Protobuf

TODO: Pros and Cons

##### Custom Binary

TODO: Pros and Cons

#### Framework

##### GRPC

https://grpc.io/

##### Microsoft Bond

https://github.com/Microsoft/bond

## First Prototype

### Goal

The goal of the first prototype is to explores the communication between the dedicated game servers and the open world matchmaking service. In this prototype, we will be simulating the information exchanged in order to examine the following

- Communication protocols
- Serialization technology
- Load test in the terms of connection and information flooding