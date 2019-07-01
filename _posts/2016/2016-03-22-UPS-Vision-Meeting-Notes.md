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

| Item  | Notes                                                        |
| :---- | :----------------------------------------------------------- |
| Scope | <ul><li>Two majors aspects<ul><li>What is the vision?</li><li>How to achieve that vision?</li></ul></li></ul> |
| What  | <ul><li>Service grouping<ul><li>Collect the technologies and services that make sense together and present them as a common facade</li><li>Mastering: Hide, BuildDB Agent, DigiSign, BurningCenter</li><li>Distribution: Tunnel, Asset Store, Symbol Store, Storage Service, Streamers, WarpGate and other clients</li><li>Administration: Operations (slice + global) &amp; productions (slice)<ul><li>Slice: Administrate their slice of the service such as adding users</li><li>Global: Administrate the service as a global entity such as deployment</li></ul></li></ul></li><li>Naming<ul><li>The names needs to be engaging</li></ul></li></ul> |
| How   | <ul><li>Some questions were identified regarding our products and services<br /><ul><li>What are the use cases?</li><li>What are the boundaries and responsibilities?</li><li>What is the language/glossary for communication?</li></ul></li></ul> |
| Who   | <ul><li>Who else should be involved?<ul><li>Depending on the products and services, experts can be added to the meeting.</li></ul></li></ul> |

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

| Item                   | Notes                                                        |
| :--------------------- | :----------------------------------------------------------- |
| Previous Meeting       | <ul><li>We agree on what was discuss in the previous meeting</li><li>Service grouping into logical unit makes sense</li></ul> |
| Grand Scheme of Things | <ul><li>Our offering needs to move away from BuildDB in order to be more generic</li><li>Anyone should be able to use a slice of our technological offering without an impression of heaviness<ul><li>Present targeted API/SDK<ul><li>Perceived as modular</li></ul></li></ul></li><li style="list-style-type: none;background-image: none;"><ul><li style="list-style-type: none;background-image: none;"><ul><li>For example, if someone wants to transfer a set of files but doesn't want to use BuildDB, it should be allowed</li><li>Our system can create a build in BuildDB if needed but it is transparent to the user</li></ul></li><li>Streamlined usage<ul><li>Each use case needs easy access</li><li>UPS artifacts (build db xml) should stay inside UPS</li><li>Configuration (how a transfer link to a build) should stay inside UPS</li></ul></li></ul></li></ul> |
| Narrow Slice           | TODO DIAGRAM<br /><ul><li>Slice by use case / business case / service offer</li><li>Individual slice can be accessed via API by anyone or via an optional gateway<ul><li>A gateway is practically a 1 to 1 forward call to the API</li><li>Useful when you want to provide the service accessible to people with no to little experience in web</li></ul></li><li>The &quot;synergized&quot; set of service is implicit in the each service offer<ul><li>For example,</li><li>User tell Transfer API to transfer build id 123</li><li>Transfer API takes care of it</li></ul></li></ul> |
| Branding               | <ul><li>The new unified indepedent transfer system will be called &quot;Fedex&quot;</li></ul> |

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

| Item              | Notes                                                        |
| :---------------- | :----------------------------------------------------------- |
| Previous Meetings | We agree that the first axis remains the Unified Transfer Service / FedexThe question of whether we need or should have a gateway is non essentialA gateway can be added at little to no costs since it provide nothing more than forward calling and a common view on APIs |
| Opportunities     | Development needs to be aligned with opportunitiesSymbol StoreThe CREW needs to be able to clean and copy a cluster to another clusterComponents of this Fedex such as the Asset Transfer Technology + API can be built independently for this needThis would be the first stepping stone towards the endgameCo-dev among the Asset Store and the BuildDB team should be favored |
| Strategies        | We identify two distinct flavored on how to get there with lots of similarities but distinctive detailsIn step 1, we refactor in CORE and in the DB, specifics for Asset Transfer and Signiant TransferHence, we make it generic to handle file transfer and asset transfer by breaking depedenciesNote that in the Transfer service, we don't have any notion of file transfer or asset transfer. There is only a notion of build transferAll transfers should be grouped into package (keep it how it is and find a concensus on how to handle it)Maybe we can create here something that handle concept that is not buildIn step 2, we spin off the Transfer service into its own project/solutionWe add specifics for asset transfer and file transfer while keeping the build transfer (build are package)Asset transfer and file transfer create package in the backgroundIn step 3, we spin off the transfer from the coreAssets transfers and file transfers create package to transferIn step 4, we spin off the transfer from the databaseWe now have an independent transfer systemFuture development can includePlugging the signiantsender into it and remove signiant transfer service  TODO:Steps:Epic or features basedSymbol transfersInclude the backend of independent asset transfers? (not mandatory)front end that combines step 1 and 2list of symbols attached to manifestSymbol without a buildcreates a package (should be step 3)Sound transfers |

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

| Item                 | Notes                                                        |
| :------------------- | :----------------------------------------------------------- |
| Previous Meeting     | During the previous, we try to establish a road map on how the development should take place.It has proven to be a bit more difficult to establish such steps.Perhaps, we should focus on what we have now, on our endgame and aligned the development with business opportunities. |
| Opportunities        | Standalone Assets TransfersSymbol TransfersSound TransfersClusters to Clusters Transfers |
| Current Architecture | The following diagram represents what we have now.           |
| Architecture Change  | The following diagram represents our architecture as far as the transfer service is concerned by considering the business opportunitiesThe left represents what would BuildDB looks like when we slice off the standalone transfer service, which is on the rightThe standalone transfer service cantransfers a collection of filestransfers a collection of assetstransfers a package that is composed of files and/or assetsqueues transfersThe BuildDB Core will be responsible BuildDB logic Identify the right studio (not the right agents or node, this is the other guy)Notification for agents and cachersInject BuildDB information into the transfer like the path changeThere should be no transfer logic in BuildDB since it should only use it as a black boxYou could have an endpoint that allows the transfer of a build but it will be forwarded to Transfer Service |
| Epics                | Merge BuildDB Transfer (relevant part) and Signiant Transfer Service into Transfer ServiceThe only call to Transfer Service should be transfer this list of files and/or assets, let me know when it's doneAsset Store API includes transfer mechanism |
| Open Questions       | REST / Resource Based API versus SOAP / Action Based API     |

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

| Item                            | Notes                                                        |
| :------------------------------ | :----------------------------------------------------------- |
| SLIM                            | SLIM is devoided of all business logicsIt receives instructions and simply executes the instructionsIf a transfer of a certain entity needs to place things in a certain order, the SLIM will need to receive such instructions |
| FAT                             | FAT contains all business logics related to transfers of entitiesIf a transfer of a certain entity needs to place things in a certain order, the FAT will orchestrate all the necessary |
| SLIM versus FAT                 | SLIM is the technological aspect of the transferFAT is the business aspect of the transfer |
| SLIM and FAT                    | SLIM and FAT are not mutually exclusiveOften, FAT is composed of multiple SLIM subsystemWhat is different is whether users can access the SLIM system by itself or not |
| Business Vision of SLIM and FAT | SLIM is a subsystem of FATAll clients' access is through the FATIf clients don't need the FAT, they will still access the FAT but the calls can be forwarded internally to SLIMIt will be simply another path in FAT ![img](https://mdc-web-tomcat17.ubisoft.org/confluence/download/attachments/348057470/SLIM%20and%20FAT.jpg?version=1&modificationDate=1458224571000&api=v2) |

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

| Item                         | Notes                                                        |
| :--------------------------- | :----------------------------------------------------------- |
| Business Use Case Definition | ![img](https://mdc-web-tomcat17.ubisoft.org/confluence/download/attachments/349122634/WP_20160321_16_41_06_Pro_processed.jpg?version=1&modificationDate=1458660377000&api=v2) |

## Date

22 Mar 2016

## Attendees

- Jonathan Chin
- Alexandre Fournier
- Simon Poliquin

## Goals

- Link the Business Cases to Features

## Discussion items

| Item                    | Notes                                                        |
| :---------------------- | :----------------------------------------------------------- |
| Linking to Features     | ![img](https://mdc-web-tomcat17.ubisoft.org/confluence/download/attachments/349127211/IMG_2203.JPG?version=1&modificationDate=1458746484000&api=v2) |
| Features                | P1: On Demand Package TransfersAPIApplicationP3: Dropbox / Live ReplicationSetup via ApplicationP3: Peer to Peer Package Transfers (ala Media Exchange to Internal and External)APIApplication |
| High Level Architecture | DatabaseThe system will have its own databaseIt will store the information related to the transfersIt will contains soft keys to other database when neededTechnology ServicesThe technology services are slim / dumb services that receive and execute instructions for transfersIt is devoided of any business logic that is not related to the actual transfersBasically, it is like a CRUDThere could be 1 services per storage technology. For instance, Transferring files located on a SAN or Windows Share will be done via SigniantTransferring files located on an Asset Store will be done via Asset Replicate (or the new Independent Asset Transfer System)Business ServicesThis box contains the business logics related to transfers and the orchestration of such transfersBasically, using the provided CRUD, it does magicThe business logics would be common to all types of transfers although certain particularities can be differentFor example, security would encompass any type of transfers whether it is signiant transfer or asset replicateAnother example would be queuing.ClientsClients can initiate transfer through multiple entry points: API, Application (Mainframe, SigniantSender, etc.)When using the API, clients are responsible for creating a package for the transferWhen using the applications, the latter will handle the creation of the package for the transferA package is defined as a package in the ecosystem of UPS.For instance, the package in the current ecosystem of UPS would be a Build because of BuildDBTechnology Services are hidden behind the Business ServicesIf we need to update the technology services, it will be protected by the business services ![img](https://mdc-web-tomcat17.ubisoft.org/confluence/download/attachments/349127211/Office%20Lens_20161102_152319_processed.jpg?version=1&modificationDate=1478115182000&api=v2) |
