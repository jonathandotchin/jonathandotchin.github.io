---
tags:
  - best practices
  - tips
  - performance
---

# Performance

## Latency

- Latency is a measure of time delay experienced in a system.
- Performance = 1/(∑Latency)

## Cause of Latency

- User
- Network
- Disk
- Lock (thread or resources)
- Memory
- Processor

## Solution

Always aim for the biggest results with the smallest effort. 

- Avoid unnecessary work
- Better algorithms
- Data locality (caching)
- Background processing
- Bigger work unit (batching)
- Lazy initialization
- Distribute processing
- Transform the data

# Improving Performance

## High Level

- Define performance expectations
- Design and implement with those expectations in mind
    - Hardware level (i.e. physical vs. virtual machine)
    - Technological level (i.e. native vs. managed code)
    - Code level (i.e. object oriented vs. data oriented)
    - Database level (i.e. store procedure vs. object relational mapping)
    - Eliminate unnecessary work, caching, batching, multi thread, etc.
- Benchmark during development against those expectations
- Do not overdo the optimizations
    - It should be good enough based on the defined expectations
    - Knowledge should be accessible 

## Profiling

Despite our best effort in design, sometimes, it does not meet the expected performance requirements. In those cases, we must profile.

### Sampling

Take a peek at what is currently executing a specific intervals.

- Good
    - Low overhead 
    - Good against CPU bottlenecks
- Bad 
    - Need to zoom into the hotspot
    - Cannot see time wait in the kernel
    - May not highlight too many calls (depending on tools)
- Tools
    - Xpert, VTune, dotTrace, VS 2010/2012 

### Instrumentation

Inject code and/or hook dll to sniff inside the application.

- Good
    - Highlight too many calls
    - Expose kernel wait
- Bad
    - Requires more resources
    - Might require recompilation
    - Might slow down your application 
- Tools
    - Ants Profiler, VS 2010/2012

### Concurrency Viewer

Allows you to identify inter-thread dependencies (i.e. which thread is block because of another thread)

- Tools
    - Visual Studio 2010/2012 Concurrency Visualizer 

### Event-Based

Allows a profiler to tell the CPU to generate interrupts at certain events. We should be rarely at this level.

- Good
    - The only way to show if the CPU is actually doing anything internally
- Bad
    - Processor specific
    - Limited support
- Tools
    - vTune 

## Tools

### Ants Profiler

- Powerful but expensive
- Easy to use
- Remote profiling
- Performance and memory profiling available in different package

### Visual Studio Profiler 

- Available in Visual Studio 2010 Premium and above edition
- Available in Visual Studio 2012 Professional and above
- User friendly
- Not as good as Ants Profiler but good enough

### XPerf 
- Free tools from Microsoft
- Command line application
- Able to diagnose most (90%+) of performance problems
- http://msdn.microsoft.com/en-us/performance/cc709422.aspx

## Single Threaded

## Algorithm

- Bubble sort vs. quick sort vs. heap sort
- For loop vs. foreach loop vs. LINQ
- Array scan vs. binary search insert vs. hash table insert

```
Always benchmark with representative sample of data in terms of size and diversity.
```

### System calls

- File read / write
    - Call batching
    - Data buffering
    - Read / write bigger chunk 
- Network read / write 
    - Prefetch
    - Batching
    - Buffering
- Memory allocation or Object creation / destruction
    - Object pooling
        - Connection pool 
- Thread creation / destruction
- Locks
- Memory footprint
    - Big memory footprint leads to page fault (i.e. going to hard drive instead of staying in memory)

### CPU Architecture 

Cache Hit / Miss 

```
char* data = pointerToSomeData;
unsigned int sum = 0;
for (unsigned int i = 0; i < 1000000; ++i, ++data)
{
    sum += *data;
}
  
char* data = pointerToSomeData;
unsigned int sum = 0;
for (unsigned int i = 0; i < 1000000; ++i, data+=16)
{
    sum += *data;
}

```

- The first one is supposed to be faster because there is a prefetching. We are talking about the L1, L2 cache and the system of cache miss.
- The key is often to place the data for it to be performing for the CPU instead of having it pretty for the developer. We want Data Oriented Design so that it is CPU friendly. If we can layout the data in big continuous blocks, then we maximize cache utilization.
- Sequential writes help the CPU a lot. Also easier to multi thread with DOD because the data is local to the thread.
- DOD should be applied with care. It will increase performance but it should be encapsulated inside OOP for better maintenance.

### Load - Hit - Store

```
void slow()
{
    for (int i = 0; i < 100; ++i)
    {
        m_iData++;
    }
}

```

- The variable is global so the compiler needs to load it every time in case someone else wrote into it also. This shouldn't be a problem in PC and NextGen but in OldGen it was. It was a design policy in the OldGen. To avoid Load-Hit-Store, we use a local variable.

### Branch - Misprediction

- Pipeline of execution should be filled because the CPU should be able to predict what to executing. So the CPU assumes the next, if it is wrong, it adjusts. But if it is right, it just flies.
- Custom ToLower in 4 character at a time. Then we can eliminate branching by using a masking algorithm. To avoid the decision of the CPU. This is called vectorization. (http://en.wikipedia.org/wiki/Vectorization_(parallel_computing))
- Sometimes the compiler is capable of doing it for us.

```
// compiler can do it for us since there is no dependency on external parameters
int a[128];
int b[128];
  
// initialize b  
  
for (i = 0; i<128; i++)  
    a[i] = b[i] + 5;
  
// compiler cannot do it for us since we got pointers and memory on the heap
int *a = malloc(128*sizeof(int));
int *b = malloc(128*sizeof(int));
  
// initialize b  
for (i = 0; i<128; i++, a++, b++)  
    *a = *b + 5;
  
// ... 
// ...
// ...
free(b);
free(a);
  
// vectorization can be accomplished with the following
for (i = 0; i < 1024; i++)   
    C[i] = A[i]*B[i];
  
for (i = 0; i < 1024; i+=4)     
    C[i:i+3] = A[i:i+3]*B[i:i+3];
```

### Data Dependency

- We wait for data to be handled because it can be used for the next operation. What this does is that the pipeline got holes also. One way is to make sure that the previous is completed way before the second loop.
- We could batch execute the first loop overlap with the second loop in chunk of 4. The bet is that the first data will already be in memory.

### Bus Transfer & Write Combined-Memory

- Processor write in block because it is expensive to write. It flush a chunk of data instead of 1 by 1. If data is sequential, it works well because it is all sequential.

## Multi Threaded

### Load Balancing

- What's wrong with the following?

```
// Start workers.            
for (int i = 0; i < Threads; i++)            
{                
    new Thread(delegate()                
    {                    
        for (int x = 0; x < Calls; x++)                    
        {                        
            var webClient = new WebClient { Proxy = null };                        
            var response = webClient.DownloadString(Restah160);                    
        }
                     
        // If we're the last thread, signal                    
        if (Interlocked.Decrement(ref toProcess) == 0)                    
        {                        
            resetEvent.Set();                    
        }                
    }).Start();            
}
             
// Wait for workers.            
resetEvent.WaitOne();
```

- Make sure the work is evenly distributed across all thread and all cores.

### Shared Data

- Sharing data between thread is bad since it creates locks and contention
- Focus on read only data since they don't require synchronization
- Distribute the data before running the threads (data layout)

### Lockless Programming
- Active area of research

### Compare and Swap

- It compares the contents of a memory location to a given value and, only if they are the same, modifies the contents of that memory location to a given new value
- Useful for initialization when multiple try to initialize the same data. Useful for API because people will be able to multi thread the API as they want.
- They call it lockless implementation (instead of using a lock to init).

### Lock Free Data Structure

Can be implemented with CAS

- Queue
- Stack
- Bitset 

Relativistic Programming

- No lock between readers
- Different reader threads may see events in different orders, hence, relativistic programming

### Linked List

- Writer can write in the list even if readers are reading it. But deleting is a bit hard. We need to delete when we know for sure no one is using it. This is why we need a node dispensers that handle this specific task.

False Sharing

- This happens when the data is on the same line of cache. The way to fix it is to "align" the data on different line of cache to make sure it doesn't go into l2 cache.