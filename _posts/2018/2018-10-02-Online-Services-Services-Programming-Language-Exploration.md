---
tags:
- online
- services
- languages
---

# Purpose

The purpose of this document is to explore the different programming languages that can be used in the development of online services. This exercise will be divided into three parts:

- First, we will identity the areas or criteria that should be considered when choosing a programming languages,
  - This will include a brief description of the criteria when necessary and a level of requirement
- Second, we will identify the programming languages and evaluate each language against those criteria,
- Lastly, we will analyzes the findings and decide on the future course of action.

Ultimately, we want to decide on a default choice for the programming language, that is, the language that will be used unless compelling reasons forces us to do otherwise.

# Terminology

## General

- **Online Services** - Represents the web services that are to be implemented by the Far Cry Online Team in order to support the dedicated server.
- **Dedicated Servers** - Represents the servers running game logic (i.e. PVE, PVP).

## Level of Requirement

- **Must Have** - A must have requirement is critical for the success of any online services.
- **Situational** - A situational requirement is critical for the success of some online services.
- **Nice to Have** - A nice to have requirement is a facilitator for the success of any or some online services.

## Criteria Fulfillment Scale (Points)

- **Bad (1)** - The language barely fulfill this criteria in any satisfactory manner.
- **Good (2)** - The language fulfill the criteria in a satisfactory manner.
- **Excellent (3)** - The language is a prime candidate to fulfill the criteria.

# Criteria

## English Syntax

The language syntax is in English.

**MUST HAVE** - I don't think anyone is interested in programming in another language than English.

## Vertical Performance

Represents the language ability to scale vertically and squeeze as much as possible in terms of resources such as CPU, Memory, IOs from a single server.

**SITUATIONAL** - If we have a stateful service where only a single instance can exist then we would need to scale vertically.

## Horizontal Performance

Represents the ability and the ease to scale horizontally.

**MUST HAVE**

## Portability

Represents the ability to run the code in both Windows and Linux.

**MUST HAVE** - We develop in Windows but we host on Linux.

## Learnability

Represents the ability or ease to learn the language or new features in the language.

**MUST HAVE** - We are developing online services, which is something new to the vast majority of Far Cry. We will most likely tap into new aspect of any language.

## Current Knowledge

Represents the current knowledge of the team with regards the language for the online team and a subset for the gameplay team.

**SITUATIONAL** - If we are in the prototype phase, we might have time to learn but if we are crunching, we won't have time anymore.

## Talent Pool

Represents the ability to gain new workers to help us in the development of the services.

**SITUATIONAL** - When shit hits the the fan, we will need to bring new workers and we won't have time to train them.

## Libraries / SDK / Package Managements

Represents the availability of libraries that would aid us in our development. For instance, communication libraries inter service, or database connection, etc.

**SITUATIONAL** - Depending on the feature we wish to support, we would need the additional packages to aid in development. Do we have official support, unofficial packages or build our own.

## Ubisoft Inner Source / Sharing

Represents the ability to share our work with other productions

**NICE TO HAVE** - They encourage us to do it but not at any costs. It helps to have people to lean on.

## Productivity Tools

Represents the availability of tools that would enable us to write code better including but not limited to : code analysis, styling, automated test, profiling, debugging tools, etc.

**MUST HAVE** - 

## Rapid Development

Represents the properties that the language provides in order to allow us to rapidly development a solution and push it out.

**NICE TO HAVE** - 

## Code and Data Sharing from Game to Online Service

Taking code and data that was built for the game and run on Online Service

**SITUATIONAL** - Depending on the online service, we might need to use existent code from the game.

## Rapid Iteration

How fast we can iterate on the code for complete the work : changes, compilation, tests, deployment, etc.

**SITUATIONAL** - Depends on which service, for instance, those that needs to pull in dunia.