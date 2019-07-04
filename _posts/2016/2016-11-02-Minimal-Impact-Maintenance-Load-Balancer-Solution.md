---
tags:
  - objective
  - operation
  - architectures
---

> DISCLAIMER
>
> The strategy of removing a node from the IP Table in order to perform the deployment causes connection lost. Therefore, there are no advantages in using this strategy.
>
> 

## Overview

Our production applications are currently hosted on several servers. Some of these servers are load balanced, where as others are not. In the case of Build DB and Asset Store, the services are hosted on four different machines and are load balanced via an IP Tables. The other applications such as DirectBurn, DigiSign, and SigniantSender are not yet load balanced but steps have already been undertaken to address this issue.

Regardless, recent events have proven that the current load balancing solution with IP Tables have proven to be ineffective at providing us the ability of providing minimal impact maintenance.

The following document aims at providing what is necessary from an infrastructure point of view in order to achieve minimal impact maintenance.

### Risks

Currently, doing either a platform or an application maintenance requires a downtime that negatively impact the production teams since our tools and services are inaccessible for an extended period of time.

##### Downtime During Platform Maintenance

A platform maintenance is done to improve the stability of the server without touching the application installed on it. It can be security patches, hardware adjustments, software updates, etc. Currently, if we want to reboot a server, we will not be able to do so without impacting clients. For instance, the current setup does not allow us to block new connections to the server while servicing old ones. Removing a machine from the pool while there are still connection actives literally cut off the clients and can negatively impact the productions.

##### Downtime During Application Maintenance

A application maintenance is done to improve the stability of the applications. For instance, bug fixes, adding features, improve performances, etc. For the sack of this document, we will focus only on application maintenance that are backward and forward compatible. Achieving backward/forward compatibility is out of the scope of this document and will be addressed later. While performing an application maintenance, we can experience the same impact as the platform maintenance.

### Requirements

| Name                | Description                                                  |
| :------------------ | :----------------------------------------------------------- |
| Drain Node          | The ability to gracefully remove a node from the pool of machine such that we can perform a maintenance without downtime |
| Traffic Redirection | Directing traffic either by load, round robin, etc.          |
| Failover            | To avoid a single point of failure for our load balancing solution, a multi machine solution will be considered. When one goes down, the other can take over and continue the load balancing. Either Active-Active, or Active-Passive. |
| Persistence         | When needed, information can be persisted across multiple request, hence the client will need to connect to the same server. This is a low priority. |

## Proposal

By addressing the risks outlined above, we will offer a better quality of service and be more resilient to downtimes during maintenances.

### Risk Mitigation

In order to address the two risks, we will need to implement a load balancing solution that is not only capable of routing traffic to a farm of servers but to also provide high availability. There are several solution available for this:

- Windows / IIS Servers
  - Application Request Routing
- Linux Servers
  - HAProxy
  - http://frankcontreras.com/install-haproxy-on-centos/

Since all our web applications run on IIS, we will be focusing on Application Request Routing at this time. Should the situation changes in the future, we should further consider investigate the Linux based solution.

##### Application Request Routing

In order to address the issues above, we should setup a IIS Application Request Routing Load Balancing Server. This should run on dedicated hardware with enough network and processor powered to handle the incoming and outgoing traffic for all servers in a web farm.

- Dedicated Windows server machine
  - Sufficient network powered
  - Sufficient processor powered
- IIS ARR installed and configured
  - http://www.iis.net/learn/extensions/configuring-application-request-routing-(arr)/http-load-balancing-using-application-request-routing
  - Only ARR is needed. There is no need for WFF (Web Farm Framework).
- NLB installed and configured
  - https://www.iis.net/learn/extensions/configuring-application-request-routing-arr/achieving-high-availability-and-scalability-arr-and-nlb

Application Request Routing has the built in ability to gracefully stop a server such that it will no longer accept new connection but continue to service current ones. This allow us to perform maintenance one server at a time.

##### HAProxy

HAProxy was only tested minimally on a virtual machine. Consequently, the rest of the report will focus on Application Request Routing.

### Test Environment

##### 2 Controller with ARR Installed

- Physical machine
- Intel Xeon CPU E5-1650 @ 3.20.GHz
  - 6 Cores / 12 Threads
- 32 GB RAM
- 1 Gbps Network

##### 4 Web Server with IIS Installed

- Virtual machine
- 1 CPU @ 2.00 GHz
- 2 GB RAM
- 10 Gbps Network (Shared)

##### Test Client

- Apache Benchmark 
  - 8000 requests with 4 connections
  - 16 000 requests with 8 connections
  - 32 000 requests with 16 connections
  - 64 000 requests with 32 connections
  - 128 000 requests with 64 connections
  - 256 000 requests with 128 connections
  - 512 000 requests with 256 connections
- Static HTML page

### Results

| Feature             | ARR and NLB | HAProxy  |
| :------------------ | :---------- | :------- |
| Drain Node          | **GOOD**    | **GOOD** |
| Traffic Redirection | **GOOD**    | **GOOD** |
| Persistence         | **GOOD**    | **GOOD** |
| Failover            | **GOOD**    | **GOOD** |

## Recommendations

### Build DB

For Build DB, it would be necessary to have dedicated hardware.Ideally, this hardware should be clustered.

### Other Productions Applications

For other production applications such as Direct Burn, Signiant Sender, DigiSign, etc. they can all share the same hardware. Ideally, this hardware should be clustered.

### Minimum Recommended Hardware

1 Dell M620

1 x Intel Xeon 6-Core Processor

8 GB RAM

10 Gbps fiber channel bounded

##### CPU

The process of "resolving" URL and directing is very CPU bound. At 512 000 requests with 256 connections for 40 000 requests per seconds, both the controller and the web servers' CPU were capped at 100%. Consequently, we would need a physical server in order to handle the CPU intensive load.

##### RAM

Throughout the tests, the memory usage of the ARR process did not exceed 500 megabytes. The memory of the machine did not exceed 4 GB. The safe ballpark amount would be 8 GB with a possibility to extend**.**

##### Network

All traffic goes through the machine with ARR installed. Consequently, the traffic can be tremendous. The traffic through the current VIP should be a good indicator. Furthermore, the database cluster is on a 2 x 2.5Gbps fiber channel. Consequently, we would never pass that threshold.

##### Disk

There was no significant disk activity. Obviously, if we use the machine as a caching system, the disk would be heavy used it is not in our plan to do so. Consequently, we need little or no local storage.

### Ideal Hardware

Ideally, we should double the hardware mentioned above in order to create a cluster of load balancer. This will add more stability to our setup. The current setup (i.e. with IP Tables) has a single point of failure. Having a cluster of load balancer will address that.

2 Dell M620

1 x Intel Xeon 6-Core Processor

8 GB RAM

10 Gbps fiber channel bounded

The reason why we are doubling the network requirement is that there will be a need to establish a private network between the two cluster so they can communicate between them.

### Final Hardware Tally

4 Dell M620

1 x Intel Xeon 6-Core Processor

8 GB RAM

10 Gbps fiber channel bounded

## Addendum

- If there are any services to be run on Linux, consider testing a Linux solution further.

## References

### ARR

http://www.iis.net/learn/extensions/planning-for-arr

[http://www.iis.net/learn/extensions/configuring-application-request-routing-%28arr%29/http-load-balancing-using-application-request-routing](http://www.iis.net/learn/extensions/configuring-application-request-routing-(arr)/http-load-balancing-using-application-request-routing)

https://www.iis.net/learn/extensions/configuring-application-request-routing-arr/achieving-high-availability-and-scalability-arr-and-nlb

### HAProxy

http://frankcontreras.com/install-haproxy-on-centos/

http://www.networkinghowtos.com/howto/viewing-haproxy-statistics/

http://raghupathy.wordpress.com/2008/05/01/high-availabilitycluster-with-load-balancing-by-using-heartbeat-and-haproxy/

