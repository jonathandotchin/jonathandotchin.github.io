---
tags:
  - poc
  - udp
  - multicast
  - distribution
---

# Important Stats

### Package Deployment

| Console  | Size  | Download to PC Client | Launchable on Console | Fully Transfer on Console | Total Time |
| :------- | :---- | :-------------------- | :-------------------- | :------------------------ | :--------- |
| Xbox One | 40 GB | 20 minutes            | 2 minutes             | 18 minutes                | 40 minutes |
|          |       |                       |                       |                           |            |

# General Architecture

![Architecture]({{site.url}}/resources/2015-11-10-Multicasting-to-Distribute-Builds\images/Architecture.png "Architecture"){: .align-center}

1. When the BuildDB Agent is done building a package, it is placed on the BuildDB Streamer
2. All the PC Client are in the same VLAN
3. PC Client 2 requests the package in question
4. The BuildDB Streamer sends to PC Client 2 the necessary information such as the multicast group in order to receive the package
5. The BuildDB Streamer starts multicasting the package
6. When other PC Client wishes to obtain the same package, they join the multicast group
7. Since PC Client 2 and 3 joined the multicast when it was already started, they will ask the BuildDD Streamer to resend the stream in order to collect the missing parts.

# Multicast Technology Walkthrough

|                            Event                             |   Status    |                           Comments                           |
| :----------------------------------------------------------: | :---------: | :----------------------------------------------------------: |
| Multicast attempt with StarBurst Multicasthttp://windowsitpro.com/windows/starburst-multicast[http://www.starburstcom.com](http://www.starburstcom.com/) | **FAILED**  | There is no link to download the software trial or paid There is no communication address on their website Their website seems abandoned as it seems to be half finished |
| Multicast attempt with UFTP[http://sourceforge.net/projects/uftp-multicast/](http://sourceforge.net/projects/uftp-multicast/?source=navbar) | **FAILED**  | The application works as is It doesn't implement solely multicast but more like a reliable multicast where the system can detect lost and out-of-order messages and take corrective action The speed limit is not due to the tool but a new standard implemented by Ubisoft to prevent flooding. The speed limit is at 5Mbps in Montreal but none in Bucharest Speed of 5 Mbps Few NAKs received from clients Files are sent successfullySpeed of 50 Mbps Tremendous amount of NAKs was received from the clients The file is still being transferred, but it never completed due to a timeoutThe reliable multicast protocol implemented is fairly straightforward and should be the basis of our implementationBasically, the file is separated into multiple sections and the sections are sent in multiple blocksWhen the client detects missing blocks, it will send back to the server a list of NAKs for missing blocksThe servers continues transferring the un-transmitted blocks but will go back and transmit the NAKs blocksThis is repeated until all the file is transmittedThe problem is that the system can loop forever or until a timeoutWhen this happens, perhaps it will be better to transfer in a more traditional approachFor instance, if everyone is missing different blocks, we shouldn't multicast. On the other hand, if everyone is missing the same blocks, we should multicast |
|                    Barebone UDP Multicast                    | **SUCCEED** | Multicast is supported natively in the .NET frameworkhttp://www.jarloo.com/c-udp-multicasting-tutorial/https://msdn.microsoft.com/en-us/library/hh556229(v=vs.110).aspxhttp://codeidol.com/csharp/csharp-network/IP-Multicasting/Sample-Multicast-Application/ |
|                     RDM Multicast (PGM)                      | **SUCCEED** | Documentation is a bit low but there are a few examples on the NET especially on the C++ sideReliable multicast is also supported via Microsoft implementation of PGM (Pragmatic General Multicast) and Rdm sockets This would require the windows message queuing component Practical example -  http://www.codeproject.com/Articles/62277/Reliable-Multicast-with-PGM-and-WCFResearch paper - http://research.microsoft.com/pubs/68888/pgm_ieee_network.docOther supported framework include RabbitMQ, ZeroMQ |
|                       Other Framework                        | **WARNING** |   UDT - http://udt.sourceforge.net/Incomplete .NET wrapper   |
|                                                              |             |                                                              |

# Solution Summary

### Comparison

|        Requirements        |                             TCP                              |                             UDP                              |                     PGM(**SHOWSTOPPER**)                     |
| :------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: |
|         Definition         | A traditional client / server solution where the client download necessary files using TCP either via a share or a file streamer | A multicasting solution where the server multicast the build to interested clients (who are listening to a multicast group) using UDPhttp://www.jarloo.com/c-udp-multicasting-tutorial/http://www.codeproject.com/Articles/1705/IP-Multicasting-in-C | A multicasting solution where the server multicast the build to interested clients (who are listening to a multicast group) using PGMhttps://msdn.microsoft.com/en-us/library/windows/desktop/ms740125(v=vs.85).aspxhttp://www.codeproject.com/Articles/62277/Reliable-Multicast-with-PGM-and-WCF |
|          Reliable          |                           **YES**                            | **NO**There is no guarantee on the delivery of the data. Basically, the server will shoot in the wire and hope for the base | **SHOWSTOPPER**The sender buffers data for retransmission in the event that it needs to be re-transferred when a NAK is sent from the client to the server. However, the buffer is limited so you can still get unrecoverable loss. The following is yet to be determined: Notification of unrecoverable loss is presented as a socket closed? (http://stackoverflow.com/questions/22806471/pgm-order-of-packets-and-reliability). In essence, the data may not be gap free.The unrecoverable loss are notify by a socket reset that causes a socket exception so there is a disconnect. When that the exception happens and it doesn't work anymore. It seems to be connection oriented. We don't need that.  So we have to work with udp because it is connection less.The socket closed causes the reception of the transmission to end completely. Basically, a 1% unrecoverable loss can trigger our backup transmission system prematurely. |
|          Ordered           |                           **YES**                            | **NO**Packet may arrived out of ordered and there is no corrective measure that is automatically taken |                           **YES**                            |
|       Error-Checked        |                           **YES**                            | **YES**In IPv4, the checksum is optional. But it can be activated (generated) by the transmitter to ensure error checked by the recipient or it is offload to the network hardware |                           **YES**                            |
|    Duplicate Protection    |                           **YES**                            | **NO**A client can receive duplicate packet. It is up to the client to handle it correctly. |                           **YES**                            |
|     Congestion Control     |                           **YES**                            | **NO**The server will basically fire and forget as much as he can. Congestion control is not implemented so we will need to implement our own using custom library since we will be in the microsecond seconds versus millisecondshttp://mounla.blogspot.de/2010/03/network-traffic-limitation-for-c.htmlhttp://www.codeproject.com/Articles/98346/Microsecond-and-Millisecond-NET-Timer | **YES**By using predefined socket options such as RM_HIGH_SPEED_INTRANET_OPT and RM_RATE_WINDOW_SIZE to specify the sender's transmission rate.https://msdn.microsoft.com/en-us/windows/apps/ms738591(v=vs.80) |
|         Late Join          | **YES**Each client gets its own stream so late joining is handled | **NO**Since we are multicasting, there is simply a single instance of the data leaving the server and with any client can join the multicast group at any time, it is possible that some clients simply miss data. | **NO**Since we are multicasting, there is simply a single instance of the data leaving the server and with any client can join the multicast group at any time, it is possible that some clients simply miss data. PGM does provide some guards against late joiners via socket options like RM_LATEJOIN but you can go back at best 75% of the sender's buffer. |
|        Multi Files         | **YES**Each file is sent in its own stream so it is not a problem since the receiver is pulling the files needed. | **NO**We are multicasting so the receiver is just getting data and has no knowledge if it is a new file or not. | **NO**We are multicasting so the receiver is just getting data and has no knowledge if it is a new file or not. |
|           Stats            |            **NO**Needs to be calculated ourselves            |            **NO**Needs to be calculated ourselves            | **YES**Available through RM_SENDER_STATISTICS and RM_RECEIVER_STATISTICS. Necessary if we want to tune the socket options like the transmission rate, windows size, etc. |
| Irrelevant Data Protection | **YES**You know exactly what you are getting since it is one to one | **NO**It is possible that someone else is multicasting data on the same ip address and port and you get irrelevant data | **NO**It is possible that someone else is multicasting data on the same ip address and port and you get irrelevant data |
|  Opt In / Flood Deterrent  | **YES**Since each stream is one to one, there is not flooding | **YES**The .NET UDP wrapper client seems to automatically handle the IGMPv2. However, it doesn't handle IGMPv3. Trying to set anything for IGMPv3 causes a crash.Using RAW UDP socket by setting "AddSourceMembership" still shows IGMPv2 in Wireshark.Connecting to the multicast group but filtering out the source shows that no data is written on disk, however, wireshark shows a load of UDP packet.If we don't connect to the multicast group, we don't see any UDP at the receiver but it doesn't mean that it doesn't get any.This is because IGMP snooping is enable and it should take care of the filteringSocket options are availablehttps://msdn.microsoft.com/en-us/library/windows/desktop/ms738558(v=vs.85).aspx | **YES**If we don't connect to the multicast group, we don't see any UDP at the receiver but it doesn't mean that it doesn't get any. This is because IGMP snooping is enable and it should takes care of the filtering. We don't need to set anything because the socket of RDM handle it.Socket options cannot be applied for some reasonhttps://msdn.microsoft.com/en-us/library/windows/desktop/ms738558(v=vs.85).aspx |

> IGMP
>
> We misunderstood IGMP v2 versus v3. With IGMP snooping enable, the filtering of multicast is done. Basically, the only way the traffic is forwarded is when you join a multicast group. If you don't, there is no path defined. The difference between v1 and v2 is that in v2, you are allowed to leave the group. Hence, not receive packet anymore when you don't want to. In v3, you can join a group but filter out specific sender.

### Possible Features

|       Feature        |                        Implementation                        | Target Technology |    Target Requirement    |
| :------------------: | :----------------------------------------------------------: | :---------------: | :----------------------: |
|    Gap Detection     |   Maintain a data map for the file to clearly identify gap   |    UDP and PGM    |         Reliable         |
| Duplicate Detection  | Maintain a data map for the file to clearly identify already received data |        UDP        |         Ordered          |
|   Speed Limitation   |  Calculate speed with CPU spin wait or microseconds timers   |        UDP        |    Congestion Control    |
|    Chunk Download    | Allow the server to serve data via chunkAllow the client to require chunk directly |    UDP and PGM    |        Late Join         |
|    Loop Multicast    | Server can loop a multicast if the sum of gap of the clients represents a distributed map of data (i'll explain it later) |    UDP and PGM    |   Reliable, Late Join    |
| "Protocol" Extension | Add to the message sent to the client "additional header" for metadata such as the file the data belongs too, the location it should belong tooFor instance, before receiving data, the client get a package map that represent information on the package in terms of index and size.The metadata can be used to identify to which files it belongs and on which location using a series of numbersWe also need to add a field to clearly identify that it is for BuildDB |    UDP and PGM    | ReliableOrderedLate Join |
|       Metrics        |         Calculate and record metrics for fine tuning         |    UDP and PGM    |          Stats           |
| Intelligent Fallback | Maintain a progress of the server and the client (in consideration of late joiner) where each client can decide to fallback to another technology |    UDP and PGM    |         Reliable         |

 
### Action Items

- Multicast VLAN / Routing / PIM Dense Mode
  - Basically, one server will be shooting packet to multiple VLAN in Bucharest
  - Ethernet Medium got 2 mode - unicast and multicast. 
  - Any hardware specification that needs to be set? what happen if each VLAN is on different subnet
  - We can configured the stack to be for specific VLAN
  - We might not be able to do Layer 3 at the stack so each stack should be for a VLAN otherwise we will need to put all VLAN clients on the same stack
  - Ideally, one stack per VLAN otherwise you will need to send the data per stack
  - One multicast per VLAN so if the same people on multiple VLAN wants the same build, we have to have multiple stream of the same package
  - But we could put more VLAN on the same stack if we are moving forward with multicast
  - We could have more multicast machines too if we need more streams
  - Multicaster should be in a VLAN with himself and then the switch should handle at forwarded to the right VLAN
    - So multiple VLAN should not be a problem or subnet should not be a problem
    - https://technet.microsoft.com/en-us/library/cc759719(v=ws.10).aspx
  - Is it necessary and active? http://en.wikipedia.org/wiki/Protocol_Independent_Multicast
    - pimv2 on CS and igmpv2 on AS
- Triple Check and Confirmed IGMP snooping
  - It is confirmed with Léa. No traffic sent because of IGMP snooping.
  - Check if the uplink is or the multiple stack or access switch is being hammered with the admin on tuesday
- Identify, monitor and block irrelevant traffic
  - For instance, would it be possible to block anyone except a specific machine from multicasting to a specific group
    - This can't be done easily but maybe the rendez-vous point can do it. Need to investigate.
- Reserve 40 multicast group for BuildDB
  - We need it because we only have a single server multicasting
  - Maybe at the server level that needs to be sets
- Bandwidth Throttle or White List. In Montreal, we got 5 Mbps, we need to go over that in Bucharest
  - In Montreal, there is a broadcast and multicast storm protection that disconnect the source PC
  - In Bucharest, there is only a broadcast storm protection
- Test Prod like setup
  - 2 host in different vlan
  - 2 host in different subset
  - 1 sender is vlan by itself