---
tags:
 - loot
 - system
 - overview
---

## Introduction

*This is a description on how to implement the premium loot system without a game server. The game server was deem necessary to act as an anti-cheat mechanism such that the player can unlock only a limited amount of premium currencies. However, the workaround described below also enables the same anti-cheat mechanism necessary without a game server.*

### Requirement

- 20 instances where players can pick up premium currencies as loot
- The quantity in each loot are configurable
- Each instance can only give premium currencies once per account
  - Multiple replay of the campaign will not give more premium currencies
  - Players can pick it multiple times but it won't be redeemable

### High Level Design

#### Configuration

- We would need to create 20 guard items and 20 associated offers. This is because we have 20 instances of pickup. If there are more, we will need to add more guard items and associated offers.
  - The guard item would have the following characteristics
    - Maximum Quantity: 1
      - This ensure that the item acts as a guard item
      - Any offers with this guard item can only be applied once
  - The associated offer would have the following characteristics
    - Items
      - The guard item
      - The currency
    - This way, when we try to apply the offer twice, since the guard item is already given, the application of the offer would fail

### Workflow

![Workflow]({{site.url}}/resources/2017-04-13-Loot-System-with-Guard-Items/Images/Workflow.png "Workflow"){: .align-center}

### Use Case

1. Player picks up premium currency as loot
   1. Player can continue playing normally
2. Game search for offers
   1. The search is based on the guard item information id and the premium currency id
   2. This should return only a single offer
3. Game applies the offer
   1. This should either return successful or not
   2. If successful, the currency is automatically added to the player's inventory
   3. If it is not successful, this means that this particular instance has already be processed previously