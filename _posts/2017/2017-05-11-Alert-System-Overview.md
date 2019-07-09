---
tags:
 - alert
 - system
 - overview
---

## Synopsis

The alert system represents the technology used to inform the players on specific aspect of the game via the live tile system found in the game. The following documentations describes the various analysis and investigation done to fulfill specific features related to this system. 

> The player needs to be online to receive any forms of alerts. A previous alert can be displayed or continue to be displayed when the player is offline but new information can only be received when the player is online. Offline behavior will rely on a fallback mechanism.

 

## Alerts

### Summary

This feature resolves around the need to notify players on specific events such as a maintenance, a live events, a promotion, etc. The technology needs to:

- be in real time
- broadcast to all players to all or specific platforms
- send specific event
- attach additional payload information
- provide a fallback mechanism for players that were offline

### Real Time Approach

In this approach, we would a notification system

- We are interested at the "Application broadcast" use case where:
  - "A message triggered by an admin to all users of an application, it can be an announcement or trigger an action on the client."
- We would need implementation on the client (the game) in order to process the notification
  - We would need a double notification implementation
  - We need a long live service that would run as long as the game is running
  - This service would act as a notification hub in order to limit the exposure of notification throughout the game.
  - The service in question would listen for notification events
  - The service would in turn raise a second notification (in game) that the news service or others would be listening too
- We would need implementation on the server in order to send the notification
  - The server to send message to games
  - The server would need to send the following information
    - The message (English only for custom information)
    - The expiry time
    - The string for localization
      - Each string needs to be baked into the game
- We would need to implement a fallback mechanism such that if the player misses the notification because it was offline, it would display the alert when it comes online

### Fall Back Mechanism

The following describe the strategy that will be used to alert users of important events when say users were not able to receive the real time notification for various reasons. 

##### Configuration System

Configurations are retrieved on login. It would be an appropriate fallback system since we can essential use the same JSON format as the notifications for the alert.

##### News System

The News System allows admin to specify tag for each news item. Consequently, we can solve the problem related to the fallback alert as follow:

- News items that are alerts can be tagged with 'Alert'
- The game, when it retrieves the news items, can interpret the news items tagged 'Alert' and it additional information and handle it in consequence in game.
  - Hence, if the player was offline and miss the notification for the alert, it would get the 'news' when it goes online and the alert would be displayed.

However, if we use the Configuration System as the main fallback, we can utilize the News System to provide more details.