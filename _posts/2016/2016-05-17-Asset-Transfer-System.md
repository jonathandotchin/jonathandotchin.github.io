---
tags:
  - poc
  - design
  - transfer
  - asset
---

# Use Case

- Independent asset transfer system where given a list of assets, a source asset store and a destination asset store, the necessary assets are transferred.

> In each of the following design, we can also put the agent outside the asset store. This could be interesting if we want to treat the asset store as nothing but a storage much like signiant agents do with the NAS

# Brainstorm Design I

### System-Level Architecture

![Design]({{site.url}}/resources/2016-05-17-Asset-Transfer-System\images/Design1.png "Design"){: .align-center}

### Systems

##### Client

- The client application.

##### Asset Transfer Agent

- It's basically an Asset Store Node
- A service will be running to take care of the transfer
  - The service is distributed but the resources are considered as a whole
  - Responsible for coordinating the transfer requests between the different Asset Transfer Agents.
  - For instance, if an agent should not be part of simultaneous transfers, it is the responsibility of this service to take care of it.
  - Similarly, if a queuing system were to exist, it would sit here.
  - Responsible for providing as part of the response to the client the node where the asset list should be uploaded
  - There would be a centralized but localized database that serve as

> It is important to note that a transfer request is not a transfer run.
>
> A request is simply a request. It contains all the information necessary to create a transfer run which is the actual execution of the transfer.
>
> The separation of request and run will enable better logging, tracking and queuing of transfers.

### Pros

- There will be no latency in obtaining the asset list from the NAS
- The Asset Transfer Agent Service will not need access to the NAS
- There is no need to have a centralized "manager"

### Cons

- The Asset Transfer Agent Service will need to be sitting on the Asset Store Node
- It is a client heavy designed
  - We should provide a command line to do the heavy lifting
  - Otherwise, if interacting with the API directly, the client will need to code additional work such as uploading the asset list to the right place

# Brainstorm Design II

### System-Level Architecture

![Design]({{site.url}}/resources/2016-05-17-Asset-Transfer-System\images/Design2.png "Design"){: .align-center}

### Systems

##### Client

- The client application.

##### Asset Transfer Manager

- The centralized asset transfer management service
  - A single instance could exist in MTL or PDC
- Responsible for coordinating the transfer requests between the different Asset Transfer Agents.
  - For instance, if an agent should not be part of simultaneous transfers, it is the responsibility of this service to take care of it.
  - Similarly, if a queuing system were to exist, it would sit here.
- Responsible for providing as part of the response to the client the node where the asset list should be uploaded

### Asset Transfer Agent

- It's basically an Asset Store Node
- A service will be running to take care of the transfer

> It is important to note that a transfer request is not a transfer run.
>
> A request is simply a request. It contains all the information necessary to create a transfer run which is the actual execution of the transfer.
>
> The separation of request and run will enable better logging, tracking and queuing of transfers.

### Pros

- There will be no latency in obtaining the asset list from the NAS
- The Asset Transfer Agent Service will not need access to the NAS

### Cons

- The Asset Transfer Agent Service will need to be sitting on the Asset Store Node
- It is a client heavy designed
  - We should provide a command line to do the heavy lifting
  - Otherwise, if interacting with the API directly, the client will need to code additional work such as uploading the asset list to the right place

# Brainstorm Design III

### System-Level Architecture

![Design]({{site.url}}/resources/2016-05-17-Asset-Transfer-System\images/Design3.png "Design"){: .align-center}

### Systems

##### Client

- The client application.

##### Asset Transfer Manager

- The centralized asset transfer management service
  - A single instance could exist in MTL or PDC
- Responsible for coordinating the transfer requests between the different Asset Transfer Agents.
  - For instance, if an agent should not be part of simultaneous transfers, it is the responsibility of this service to take care of it.
  - Similarly, if a queuing system were to exist, it would sit here.

##### NAS

- Hold the list of asset to transfer

##### Asset Transfer Agent

- It's basically an Asset Store Node
- A service will be running to take care of the transfer

> It is important to note that a transfer request is not a transfer run.
>
> A request is simply a request. It contains all the information necessary to create a transfer run which is the actual execution of the transfer.
>
> The separation of request and run will enable better logging, tracking and queuing of transfers.

### Pros

- There will be no latency in obtaining the asset list from the NAS

### Cons

- The Asset Transfer Agent Service will need access to the NAS
- The Asset Transfer Agent Service will need to be sitting on the Asset Store Node