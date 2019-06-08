---
tags:
- workflow
- owmm
- design
---

# Overview

The purpose of this document is to explore the general architecture and design used to connect a player to an open world game server. It will focus on the following aspects:

    - The player connects to an existent dedicated game server.
    - The player connects to a new dedicated game server.

The scope of this document is limited to the conception workshop prototype and the interaction of the major components of our backend.

# Use Case

## Definition

When a player would like to connect to the open world, the following will happen

    - The player will be connected to a compatible (see below) open world game server
    - If no compatible open world game server is available, the player will be connected to a new open world game server

## Scope

For the sake of the prototype, we will limit the definition of compatible open world game server as follow

    - If there is room of the player in the server, then the connection will made

# Basic Architecture and Design

## Joining Existent Servers

![Existent]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/Existent.png "Existent"){: .align-center}

    1. The "Game Client" request from the "Open World Matchmaking Service" a "Game Server" to enter the "Open World" (PVE)
    2. The "Open World Matchmaking Service" finds a compatible "Game Server"
    3. The compatible "Game Server" connection information is returned to the "Game Client"
    4. The "Game Client" uses the connection information to connect to the "Dedicated Game Server"
    5. The "Dedicated Game Server" register the player with the "Open World Matchmaking Service"

## Joining New Servers

![New]({{site.url}}/resources/2018-10-23-Architecture-and-Design-of-the-Initial-Connection-to-the-Open-World/images/New.png "New"){: .align-center}

    1. The "Game Client" request from the "Open World Matchmaking Service" a "Game Server" to enter the "Open World" (PVE)
    2. The "Open World Matchmaking Service" is unable to finds compatible "Game Server"
    3. The "Open World Matchmaking Service" request the "Dedicated Game Server Manager Service" for a new "Open World Dedicated Game Server"
    4. The "Dedicated Game Server Manager Service" spawns a new "Open World Dedicated Game Server"
    5.The "Dedicated Game Server Manager Service" returns the connection information to the "Open World Matchmaking Service"
    6. The compatible "Game Server" connection information is returned to the "Game Client"
    7. The "Dedicated Game Server" is registered with the "Open World Matchmaking Service"
    8. The "Game Client" uses the connection information to connect to the "Dedicated Game Server"
    9. The "Dedicated Game Server" register the player with the "Open World Matchmaking Service"