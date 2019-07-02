---
tags:
  - vision
---

The final goal of this set of meetings is to establish a clear vision of our solutions in order to present a homogeneous image of our direction towards ourselves and our clients in terms of future development for the productions.

## Date

17 Nov 2015

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- Establish the scope of these meetings

## Discussion items

Scope

- Two majors aspects
  - What is the vision?
  - How to achieve that vision?

What

- Service grouping
  - Collect the technologies and services that make sense together and present them as a common facade
  - Mastering: Hide, BuildDB Agent, DigiSign, BurningCenter
  - Distribution: Tunnel, Asset Store, Symbol Store, Storage Service, Streamers, WarpGate and other clients
  - Administration: Operations (slice + global) & productions (slice)
    - Slice: Administrate their slice of the service such as adding users
    - Global: Administrate the service as a global entity such as deployment
- Naming
  - The names needs to be engaging

How

- Some questions were identified regarding our products and services
	- What are the use cases?
	- What are the boundaries and responsibilities?
	- What is the language/glossary for communication?

Who

- Who else should be involved?
	- Depending on the products and services, experts can be added to the meeting.

## Date

24 Nov 2015

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- Discuss the previous meeting
  - Any addendum?
  - Any reservation?
  - Any improvements?
- Discuss the "grand scheme of things"
- Discuss the vision for a narrow slice of the "grand scheme of things"
- Discuss the branding

## Discussion items

Previous Meeting

- We agree on what was discuss in the previous meeting
- Service grouping into logical unit makes sense

Grand Scheme of Things

- Our offering needs to move away from BuildDB in order to be more generic
- Anyone should be able to use a slice of our technological offering without an impression of heaviness
  - Present targeted API/SDK
    - Perceived as modular
	- For example, if someone wants to transfer a set of files but doesn't want to use BuildDB, it should be allowed
    - Our system can create a build in BuildDB if needed but it is transparent to the user
  - Streamlined usage
    - Each use case needs easy access
    - UPS artifacts (build db xml) should stay inside UPS
    - Configuration (how a transfer link to a build) should stay inside UPS

Narrow Slice

![Slice]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/slice.png "Slice"){: .align-center}

- Slice by use case / business case / service offer
- Individual slice can be accessed via API by anyone or via an optional gateway
  - A gateway is practically a 1 to 1 forward call to the API
  - Useful when you want to provide the service accessible to people with no to little experience in web
- The "synergized" set of service is implicit in the each service offer
  - For example,
  - User tell Transfer API to transfer build id 123
  - Transfer API takes care of it

Branding

## Date

15 Dec 2015

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- Discuss the previous meetings

- Discuss strategies on getting to the destination

## Discussion items

Previous Meetings

- We agree that the first axis remains the Unified Transfer Service / Fedex
- The question of whether we need or should have a gateway is non essential
  - A gateway can be added at little to no costs since it provide nothing more than forward calling and a common view on APIs

Opportunities

- Development needs to be aligned with opportunities
  - Symbol Store
  - The CREW needs to be able to clean and copy a cluster to another cluster
- Components of this Fedex such as the Asset Transfer Technology + API can be built independently for this need
  - This would be the first stepping stone towards the endgame
- Co-dev among the Asset Store and the BuildDB team should be favored

Strategies

- We identify two distinct flavored on how to get there with lots of similarities but distinctive details

![Versus]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/versus.png "Versus"){: .align-center}

- In step 1, we refactor in CORE and in the DB, specifics for Asset Transfer and Signiant Transfer
  - Hence, we make it generic to handle file transfer and asset transfer by breaking depedencies
  - Note that in the Transfer service, we don't have any notion of file transfer or asset transfer. There is only a notion of build transfer
    - All transfers should be grouped into package (keep it how it is and find a concensus on how to handle it)
  - Maybe we can create here something that handle concept that is not build
- In step 2, we spin off the Transfer service into its own project/solution
  - We add specifics for asset transfer and file transfer while keeping the build transfer (build are package)
  - Asset transfer and file transfer create package in the background
- In step 3, we spin off the transfer from the core
  - Assets transfers and file transfers create package to transfer
- In step 4, we spin off the transfer from the database
  - We now have an independent transfer system
  - Future development can include
    - Plugging the signiantsender into it and remove signiant transfer service

TODO

1. Epic or features based
   1. Symbol transfers
      1. Include the backend of independent asset transfers? (not mandatory)
      2. front end that combines step 1 and 2
         1. list of symbols attached to manifest
      3. Symbol without a build
         1. creates a package (should be step 3)
   2. Sound transfers

## Date

10 Feb 2016

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- How to get

## Discussion items

Previous Meeting

- During the previous, we try to establish a road map on how the development should take place.
- It has proven to be a bit more difficult to establish such steps.
- Perhaps, we should focus on what we have now, on our endgame and aligned the development with business opportunities.

Opportunities

- Standalone Assets Transfers
	- Symbol Transfers
	- Sound Transfers
	- Clusters to Clusters Transfers

Current Architecture

- The following diagram represents what we have now.

![Current]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/current.png "Current"){: .align-center}

Architecture Change

- The following diagram represents our architecture as far as the transfer service is concerned by considering the business opportunities
- The left represents what would BuildDB looks like when we slice off the standalone transfer service, which is on the right
- The standalone transfer service can
  - transfers a collection of files
  - transfers a collection of assets
  - transfers a package that is composed of files and/or assets
  - queues transfers
- The BuildDB Core will be responsible BuildDB logic
  - Identify the right studio (not the right agents or node, this is the other guy)
  - Notification for agents and cachers
  - Inject BuildDB information into the transfer like the path change

![Future]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/future.png "Future"){: .align-center}

- There should be no transfer logic in BuildDB since it should only use it as a black box
- You could have an endpoint that allows the transfer of a build but it will be forwarded to Transfer Service

Epics

- Merge BuildDB Transfer (relevant part) and Signiant Transfer Service into Transfer Service
  - The only call to Transfer Service should be transfer this list of files and/or assets, let me know when it's done
- Asset Store API includes transfer mechanism

Open Questions

- REST / Resource Based API versus SOAP / Action Based API

## Date

16 Mar 2016

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- Compare and contrast the FAT and SLIM approach

## Discussion items

SLIM

- SLIM is devoided of all business logics
- It receives instructions and simply executes the instructions
- If a transfer of a certain entity needs to place things in a certain order, the SLIM will need to receive such instructions

FAT

- FAT contains all business logics related to transfers of entities
- If a transfer of a certain entity needs to place things in a certain order, the FAT will orchestrate all the necessary

SLIM versus FAT

- SLIM is the technological aspect of the transfer
- FAT is the business aspect of the transfer

SLIM and FAT

- SLIM and FAT are not mutually exclusive
- Often, FAT is composed of multiple SLIM subsystem
- What is different is whether users can access the SLIM system by itself or not

Business Vision of SLIM and FAT

- SLIM is a subsystem of FAT
- All clients' access is through the FAT
- If clients don't need the FAT, they will still access the FAT but the calls can be forwarded internally to SLIM
  - It will be simply another path in FAT

![slimfat]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/slimfat.png "slimfat"){: .align-center}

## Date

21 Mar 2016

## Attendees

- Jonathan Chin
- Laurent Chouinard
- Alexandre Fournier
- Simon Poliquin

## Goals

- Define the business use cases for the service

## Discussion items

Business Use Case Definition

![business]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/business.png "business"){: .align-center}

## Date

22 Mar 2016

## Attendees

- Jonathan Chin
- Alexandre Fournier
- Simon Poliquin

## Goals

- Link the Business Cases to Features

## Discussion items

Linking to Features

![features]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/features.png "features"){: .align-center}

Features

**P1: On Demand Package Transfers**

- API
- Application

**P3: Dropbox / Live Replication**

- Setup via Application

**P3: Peer to Peer Package Transfers (ala Media Exchange to Internal and External)**

- API
- Application

High Level Architecture

**Database**

- The system will have its own database
- It will store the information related to the transfers
- It will contains soft keys to other database when needed

**Technology Services**

- The technology services are slim / dumb services that receive and execute instructions for transfers
  - It is devoided of any business logic that is not related to the actual transfers
  - Basically, it is like a CRUD
- There could be 1 services per storage technology. For instance,
  - Transferring files located on a SAN or Windows Share will be done via Signiant
  - Transferring files located on an Asset Store will be done via Asset Replicate (or the new Independent Asset Transfer System)

**Business Services**

- This box contains the business logics related to transfers and the orchestration of such transfers
  - Basically, using the provided CRUD, it does magic
- The business logics would be common to all types of transfers although certain particularities can be different
  - For example, security would encompass any type of transfers whether it is signiant transfer or asset replicate
  - Another example would be queuing.

**Clients**

- Clients can initiate transfer through multiple entry points: API, Application (Mainframe, SigniantSender, etc.)
- When using the API, clients are responsible for creating a package for the transfer
- When using the applications, the latter will handle the creation of the package for the transfer
  - A package is defined as a package in the ecosystem of UPS.
  - For instance, the package in the current ecosystem of UPS would be a Build because of BuildDB
- Technology Services are hidden behind the Business Services
  - If we need to update the technology services, it will be protected by the business services

![fedex]({{site.url}}/resources/2016-03-22-UPS-Vision-Meeting-Notes/images/fedex.png "fedex"){: .align-center}