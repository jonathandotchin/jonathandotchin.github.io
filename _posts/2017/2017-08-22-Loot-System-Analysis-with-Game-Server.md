---
tags:
 - loot
 - system
 - overview
---

## Introduction

*The following describes the high level design of the loot system for premium currency. In essence, it must rely on server to server communication. In fact, anything that the user can receive for "free" should be through server to server communication to avoid exploitation as much as possible.*

## Requirements

- The quantity in each loot are configurable
- Each instance can only give premium currencies once per account
  - Multiple replay of the campaign will not give more premium currencies
  - Players can pick it multiple times but it won't be redeemable

## High Level Design

### Workflow

![Workflow]({{site.url}}/resources/2017-08-22-Loot-System-Analysis-with-Game-Server/Images/Workflow.png "Workflow"){: .align-center}

### Use Case

1. Player picks up premium currency as loot

   1. Player can continue playing normally

2. Game sends a message to the game server to indicate that a player pick up premium currency as loot

   If the game is offline, the message will be sent the next time the game is online

   1. The message is asynchronous
   2. It includes the player identification
   3. It includes the loot identification

3. The game server sends an acknowledgement to the game

   1. If there is no acknowledgement, the game should consider sending the message again

4. The game server validate if we can apply the premium currency or not

   1. It checks if the loot is a valid premium currency loot
   2. It checks whether that particular loot has already been applied to the particular account

5. If the request is valid, we apply the premium in the services

6. The services sends an acknowledgement to the game server

   1. If there is no acknowledgement, the game server should consider sending the message again

7. The game server uses services to notify the game that premium currency has been applied

### Assumptions

1. The next time the player opens the store, the newly applied premium currency will appear
2. The player can pick up the currency and the game will attempt to validate multiple time but it will simply be invalidated by the game server
3. The game server tracks which player has used which loot
   1. Similarly, this information can be used for statistics
4. The communication between the game and the game server and from the game server to services must have some sort of message queue (or like safety)
   1. This is to ensure that a message is always successful such that the currency can be apply and applied only once
5. The validation and the application of the premium currency on the game server must be thread safe per player
6. The quantity it unlocks is defined by the backend and therefore can be changed live
   1. Retroactive change is not necessary but nice to have

## Risks

1. If a player, somehow, obtains all the loot GUID, they could hack commands to unlock all loot instance in one shot