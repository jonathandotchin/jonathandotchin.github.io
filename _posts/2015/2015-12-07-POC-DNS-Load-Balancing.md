---
tags:
  - poc
  - ops
  - deployment
---

The purpose of this proof of concept is to investigate and determine a strategy to load balance web server geographically via DNS entries. In essence, given a dns entry "upstestgeo", the users will hit the server closest to their location.

# Scope

We are limiting ourselves at the web server level. Synchronization of data if necessary is another story.

# Setup

With the help of a Linux Administrator, we setup the DNS map for the follow servers.

BUC: 10.18.1.68
MTL: 10.129.9.20
DNS: http://upstestgeo

# Results
The command "ping upstestgeo" was run on the following computer in the following location.

Montreal
```
Pinging [10.129.9.20] with 32 bytes of data:
Reply from 10.129.9.20: bytes=32 time<1ms TTL=124
Reply from 10.129.9.20: bytes=32 time<1ms TTL=124
Reply from 10.129.9.20: bytes=32 time=3ms TTL=124
Reply from 10.129.9.20: bytes=32 time<1ms TTL=124
```

Bucharest
```
Pinging [10.18.1.68] with 32 bytes of data:
Reply from 10.18.1.68: bytes=32 time<1ms TTL=128
Reply from 10.18.1.68: bytes=32 time<1ms TTL=128
Reply from 10.18.1.68: bytes=32 time<1ms TTL=128
Reply from 10.18.1.68: bytes=32 time<1ms TTL=128
```

Shanghai
```
Pinging [10.129.9.20] with 32 bytes of data:
Reply from 10.129.9.20: bytes=32 time=249ms TTL=121
Reply from 10.129.9.20: bytes=32 time=249ms TTL=121
Reply from 10.129.9.20: bytes=32 time=249ms TTL=121
Reply from 10.129.9.20: bytes=32 time=249ms TTL=121
```

Kiev
```
Pinging [10.18.1.68] with 32 bytes of data:
Reply from 10.18.1.68: bytes=32 time=81ms TTL=125
Reply from 10.18.1.68: bytes=32 time=80ms TTL=125
Reply from 10.18.1.68: bytes=32 time=80ms TTL=125
Reply from 10.18.1.68: bytes=32 time=80ms TTL=125
```

# Conclusion
Global server load balancing via DNS is possible. It is possible to deploy a service in different datacenter to reduce latency for clients. As seen above, the closest server is pinged in accordance to the DNS mapping.