---
tags:
- data
- virtualization
- design
---

## Overview

The following document will outline the work that will be implemented in order to reconcile the data among multiple distinct but complementary tools. It will outline:

- the current state of affairs, 
- identify the current issues and problematics, and 
- propose a solution that will alleviate them

## Current State

Currently, the data is split among different data stores. The following observations are made:

- Each datastore is compartmentalized in silos with their related applications 
- Each data store serves a different but often singular purpose
- Users often utilize one silo but it is not uncommon to use more.

![Before]({{site.url}}/resources/2019-05-17-Data-Virtualization/images/before.png "Before"){: .align-center}

### Issues and Problematics

The compartmentalization raises certain issues and problematics.

- The data are stored in silos with little to no sharing between them.
- Users of one store lack visibility of the other store.
- Data shared/copied between stores can be conflicting and desynchronized.
- Customers have a difficult time obtaining information regarding their own assets.

### Proposed Solution

In order to alleviate the lack of visibility our customers, we would need to provide a single point of entry to the information we store. However, since we cannot rebuild everything from scratch, we will need to keep the following in mind:

- The current workflow should continue to work
  - This is important when the say workflow is known and battle harden
- We need to provide visibility to our customers

### Data Virtualization

Since each of these data stores are optimized for their own purposes, creating a singular store that would work well for each and every use cases would prove to be too difficult and time-consuming. Instead, we will aggregate the data from these different sources to develop a single and logical but virtual view that can be accessed by front-end solutions such as portals and dashboards. To do so, we will leverage an approach called "Data Virtualization". 

![After]({{site.url}}/resources/2019-05-17-Data-Virtualization/images/after.png "After"){: .align-center}

- The data virtualization layer will avoid data replication as much as possible. Instead, it will utilize its own database to store
  - the additional data to complement the information available
  - the glue (i.e. keys) that will allow us to stitch the data from the different store
- The current users of each silo can still continue to work as they are doing right now
  - It is important to have little to no disruption to their work

## Technology

### Data Virtualization Layer

The following observations are important for the virtualization layer

- This is, in essence, an HTTP based web API
- Depending on the information to retrieve or inject, it would communicate with the proper datastore
- When information from multiple stores is needed, the following would occur:
  - In most case, we would want to have the information stitched together in real-time
  - If the information takes too much to time to stitch, we can consider preprocessing
  - There should be no to very little data replication
    - This is critical to avoid data duplication and conflict
- Additional metadata that would not fit in each silo can be stored in the dedicated database for the data virtualization
  - Ideally, every effort should be made to store the data in one of the existent silos where it makes the most sense.

### Data Virtualization Database

The following observations are important for the database

- Data are pulled from different sources (files, API, database, etc.) 
  - The data is often unknown and, on top of that, the source might be unknown
  - This leads to a schema that is difficult to define and predict
  - **If we wish to use a database to store complementary information, this leads us towards a schemaless solution like NoSQL**
- The complementary information persisted is done for applications
  - This is like an inventory system
  - We need to support ACID transaction like the following:
    - **Assign an item to a customer often means removing from another customer**
    - **This is an ACID transaction**
    - **Without ACID, we could end up with two customers having the same item**
- The database will also be used to for dashboards
  - **We want speed and big data**

**The initial decision would be to use MongoDB 4.0. The reason is that it should be able to handle the speed, load and big data for the dashboards and, on top of that, MongoDB 4.0 added support for ACID transactions while still providing a schemaless solution.**

## Conclusion

This document represents the first volley into the architecture of data reconciliation where two main requirements must be respected

- Current workflows must not be disrupted but improved
- Provide end to end visibility to our clients
