---
tags:
  - poc
  - signiant
  - design
---

# Synopsis

Currently, Signiant Sender supports three major transfer mode: Folder to Folder, File by File and Media Exchange. In Folder to Folder and File by File, there is an issue related to collisions among common files of multiple concurrent transfers.

In essence, the behavior is illustrated below:

![Overview]({{site.url}}/resources/2015-11-05-Collision-Issues-Signiant-Sender\images/Overview.png "Overview"){: .align-center}

When collisions occurs during concurrent transfers, both transfer A and B are writing in the same file. The following problems are occurring:

- One or more transfers result in a failure
  - Essentially, when one transfer is done and is trying to rename the file, it fails because it is still in use by the other transfer
- Transfers are overriding each others leading to more data being transferred than it should be
  - For instance, when transferring a 5G file, both transfers ended up sending 20G.

# Potential Solutions

There are multiple solutions possible, each with their pros and cons

1. Signiant's workflow modification
2. Queue creation based on root folder
3. Queue creation based on common files

## Solution #1

### Overview

We can modify the signiant workflow to workaround the problem.

- Create temporary files with different name
- Rename the temporary files when the transfers completes
- Ignore the renaming when another transfers already done the job

### Advantages

- Zero changes in our external workflow

### Disadvantages

- Changes needs to be done inside Signiant
- Duplicate data will still be sent

##### Conclusions

Working inside Signiant and the duplicate of data makes this solution unfeasible.

## Solution #2

### Overview

We can modify the signiant transfer service to queue transfers sent to the same root folder

- In folder to folder, if the same folder is used at destination, the transfer is queue
- In file by file, if the same root folder is used at destination, the transfer is queue
- The transfer is dequeue when the previous transfer is done

### Advantages

- Production has no changes to do except from removing their own workaround
- Duplicate data will not be sent since it will be skipped by Signiant

### Disadvantages

- Queuing is extremely aggressive
- Queuing is not 100% safe proof
  - collision between folder to folder and file by file can occur
  - this can be avoided with an even more aggressive scheme
- Transfers without common files will still be queue

### Conclusions

This could be a temporary solution since it will be an already be an improvement over the current situation. The transfers no longer fails and duplicate data are no longer sent.

However, the queuing is extremely aggressive and not optimized will result in possibly longer transfer time.

## Solution #3

### Overview

We can modify the signiant transfer service to queue transfers with common files sent to the same destination

- In folder to folder, we will need to "explode" the content of the transfers in a format that is similar to file by file
  - This will facilitate the collision detection such that we can apply the scheme for file by file only
- In file by file, we need to resolve the target path for each files
- The transfer is queue when there are files in common at destination
- The transfer is dequeue when the previous transfer is done

> We need to validate that we can have access to the information of which files in in the transfers
>
> In Folder to Folder:
>
> - Need to be able to list the content of the folder
>
> In File by File:
>
> - Need to be able to parse the inside of the transfer manifest file
>
> Currently, we don't have direct access. Only the agents account and srv_bdb_transfer has too much power.
>
### Advantages

- Production has no changes to do except from removing their own workaround
- Duplicate data will not be sent since it will be skipped by Signiant

### Disadvantages

- Much more time to implement correctly
- We will need to modify the access scheme of signiant since we don't have access to the content of the transfers

### Conclusions

Apart from the time to implement correctly, this is probably the recommended solution. However, we will probably need to modify the access scheme in order to know the content of the transfers.

# High Level Design

## Sequence of Queuing Mechanism

The following diagrams assumes that we know the content of the transfers.

![Design]({{site.url}}/resources/2015-11-05-Collision-Issues-Signiant-Sender\images/Design.png "Design"){: .align-center}

## Requirements

### Transfer Request Remains Idempotent

- The content of the transfer does not change. Only the action executed can change.
  - This is critical to make sure that a transfer is idempotent, that is the same output can occurred when retried.
- For instance, all transfers should attempt to transfer all files requested.
  - It would simply skip if it is already sent.
  - It would simply send if it is not sent.

### New Security Scheme

- In order to properly detect the content of the transfer, we will probably need to implement a new security scheme
- Via HTTPS, we can ask for username/password in order to parse the content of the transfer and use it to determine if we have collisions
  - This is a breaking change
- Otherwise, the signiant transfer service would need to receive the complete list of files to transfer, which means that the security scheme will be implemented in the client and the construction of the list also in the client
  - If the service were to made public, this would be the ideal solution

### Queue Tracking Algorithm

- Each transfer would be part of a queue regardless if he is by himself or not.
- This will facilitate the queue system since if a new transfer needs to be queue, the queue would already exists. It would simply need to join it.
- When the transfer finishes, and it needs to start the next transfer in queue, the queue would simply be empty.
- The "queue" would simply be a grouping of transfers that are first come first serve.
- This queue mechanism is a bit primitive
  - Transfer A in active with file1,file2,file3
  - Transfer B comes with file1,file20, file30
    - Transfer B joins transfer A's group and wait
  - Transfer C comes with file10, file20, file30
    - Transfer C is assigned another queue and run
    - Then when Transfer B is free because A is done, it should be updated to join Transfer C group because C is in progress.
  - Theoretically, this could lead to starvation but practically, I highly doubt it.
    - If it does, we can implement some kind of priority queue

### Collision Detection Algorithm

Collision detection are run only for transfer in progress

Folder to Folder

- Need to explode the content of the transfer and obtain the same information as in file by file
- We will need to resolve the destination path

File by File

- We will need to resolve the destination path

Detection

- If a transfer B contains files in common per resolved destination path with transfer A, transfer B will join the queue/group of transfer A
- When dequeue, it perform the check again

### Dequeue Algorithm

The dequeue algorithm would need to be on demand and also be on poll as a safety

- On complete, we would start the next transfer in the queue/group
  - Collision detection would be needed again
- From time to time, we check if there are transfer queue/group with no active transfer

## Caveats

### Breaking Changes

- The new security scheme will most likely result in a breaking change
  - A new endpoint would probably be best