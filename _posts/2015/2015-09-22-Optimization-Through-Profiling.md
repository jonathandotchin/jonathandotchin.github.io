---
tags:
  - best practices
  - optimization
  - tips
---

# Overview

Despite our best effort in design, it is possible that the implementation of the software simply does not meet the defined performance and memory requirements. In these cases, we must profile our application.

The following document aims to document the different contexts and axis of investigations with the solutions applied that were beneficial.

# Workflow

![Workflow]({{site.url}}/resources/2015-09-22-Optimization-Through-Profiling\images/Workflow.png "Workflow"){: .align-center}
 
# Contexts and Axis of Investigation

## Garbage Collection

- In .NET, the management of memory is usually done by the garbage collector
- Each time the garbage collector runs, the application incurs a performance hit
- One key aspect is to, therefore, minimize the time the garbage collector needs to run
- Reference: https://msdn.microsoft.com/en-us/library/ee851764%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

## Computation Time

- This refer to CPU usage
- The more computation, the more the CPU is used
-  High CPU usage could block useful work from being executed

## Disk Usage

- Access time to disk is slower than RAM
- This is particularly the case of HDDs, although SSDs are still slower than RAM
- Minimizing access time and seek time is another axis

# Solutions

## Pre Allocation of Memory

### Contexts

- Garbage Collection

### Implementations

- If you know which memory will be used throughout the application, they should be declared and initialized as early as possible to minimize the shuffling of memory between generation
- For instance, if you have a buffer that contains 500 objects in which only the content of the object changes, you should initialized all 500 objects
    - This is provided that you know you will need all 500 objects eventually
    - If you don't know if you reach the 500, then it might do more harm to initialize all of them

## Memory Reuse

### Contexts

- Garbage Collection

### Implementations

- To minimize the garbage collector passing on your application too often, consider reusing the memory such that the application will not be constantly reserve new memory for the garbage collector to free
- For instance, instead of creating a new buffer and disposing of the buffer, we can create a pool of buffers
    - When requiring a buffer, we can retrieve one from the pool
    - When disposing of the buffer, we simply return it to the pool
    - When the memory is no longer needed, we free the entire pool

## LINQ Avoidance

### Contexts

- Computation Time
- Garbage Collection

### Implementations

- If you have a fix amount of elements, consider using an array directly instead of a list since you don't need dynamic allocation
- If your array of elements is sorted, consider using BinarySearch instead of LINQ search, which is linear.

## Caching

### Contexts

- Computation Time

### Implementations

- If a computational result is to be used over and over, consider caching the result to avoid having to recompute it everything.
- For instance, consider the following:
    - Input: Unique ID A » Compute Resource » Output: Resource Z
    - Make a hashtable with Unique ID as keys that map A to Z such as the next call will simply look in the hashtable

## Logging

### Contexts

- Garbage Collection
- Disk Usage

### Implementations

- Excessive logging causes slow down due to the constant access to disk and the creation of temporary object
- Constant access to disk increase disk usage and prevent the application from performing its useful task
- Creation of temporary objects forces the garbage collector to run more often.

## Multi Threading

### Contexts

- Computation Time

### Implementations

- When all the data is already in memory and this data can be partitioned, performing the computation on multiple thread to take advantage of multiple processor will decrease the overall computation time.
- Consider having two distinct value A & B to hash
    - Performing one after another will result of T(A + B) = T(A) + T(B)
    - Performing one on each processor will result in T(A +B) = M[T(A), T(B)]

## Batch Executing

### Contexts

- Disk Usage

### Implementations

- If you have data to be read or written to disk, consider doing them in chunk in order to avoid seek time.
- The OS is usually capable of doing chunk write to disk but chunk read is probably safer to be done by developer.
- Instead of reading 9K bytes at a time, perform a 5 MB read especially when you know that you need to get the 5MB. 
    - This will reduce the number of disk seek.