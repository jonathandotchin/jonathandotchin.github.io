---
tags:
  - poc
  - bittorrent
---

In this post, we will examine our endeavor into using BitTorrent to distribute build.

## Deciding Factors

### Performance

-   The performance of the library as a whole in terms of its main purpose: downloading and uploading files

### Maintainability

-   How easy is it to maintain the link between our applications and the library
-   How easy is it to actually fix the library if necessary

### Development Speed

-   How fast can we integrate the library in our code

### Support

-   Support by the creators or by the community

### Feature Set

-   Implementation of BitTorrent features such as web seeding, multiple announcers, fast resume, initial seeding, etc.

## Candidates

### Summary Matrix

|              Library               |                         Performance                          |                       Maintainability                        |                      Development Speed                       |                           Support                            | Feature                                                      | Summary                                                      |
| :--------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | ------------------------------------------------------------ | ------------------------------------------------------------ |
|            MonoTorrent             | **AVERAGE**<br />Speed is slower than uTorrent<br />Does not support uTP | **LOW**<br />Code written in C# / .NET<br />Code is not well designed<br />Requires .NET Framework 3.0+ |    **GOOD**<br />Can be used directly in our application     |          **LOW**<br />Not under active development           | **LOW**<br />Supported feature set is buggy<br />Multiple trackers, web seeding, disk caching, etc are buggy are none functional | **LOW**<br />It looked good at first, but it failed to pass our tests. |
|             libtorrent             | **GOOD**<br />Speed is slightly slower than uTorrent<br />Support uTP but mixed mode algorithm between uTP and TCP is not up to point | **LOW**<br />Code written in C++ and Boost<br />Limited knowledge of C++<br />Enhancing and patching the library can be difficult<br />Requires VC++ Redistributable 2012 |    **LOW**<br />Need to rewrite our app in C++ and Boost     | **GOOD**<br />Under active development<br />Used by many BitTorrent Client | **GOOD**<br />Supported feature set are working<br />No library for trackers | **AVERAGE**<br />In order to fully take advantage of it, we would need to rewrite several applications with C++ and Boost |
|      PInvoke over libtorrent       | **GOOD**<br />Speed is comparable to libtorrent<br />Support uTP but mixed mode algorithm between uTP and TCP is not up to point | **LOW**<br />Code written in C++ and Boost<br />Limited knowledge of C++<br /><br />CStyle wrapper written in C++<br />PInvoke wrapper written in C++ and C#<br />Enhancing and patching the library can be difficult<br />Requires VC++ Redistributable 2012 | **AVERAGE**<br />CStyle wrapper needs to be created<br />PInvoke wrapper needs to be created |              **GOOD**<br />In house development              | **LOW**<br />Each feature will need to be forwarded by us<br />No library for trackers | **AVERAGE**<br />PInvoke library needs to be written from scratch<br />We will be fully in control of the mapping<br />We will need to trust the underlying library |
| CLI over libtorrent (Ragnar based) | **GOOD**<br />Speed is comparable to libtorrent<br />Support uTP but mixed mode algorithm between uTP and TCP is not up to point | **LOW**<br />Code written in C++ and Boost<br />Limited knowledge of C++<br />CLI wrapper written in C++/CLI<br />Enhancing and patching the library can be difficult<br />Requires VC++ Redistributable 2012 | **GOOD**<br />CLI wrapper needs to be created based on Ragnar |              **GOOD**<br />In house development              | **GOOD**<br />Essential features in libtorrent seems supported by Ragnar<br />More features will need to be forwarded by us<br />No library for trackers | **GOOD**<br />A good and functional base already written in C++/CLI<br />Open source so we can modify it at will<br /> |

>**libtorrent versus libTorrent**
>
>lib**t**orrent is not to be confused with lib**T**orrent
>
>-   lib**t**orrent is an open source implementation of the BitTorrent protocol written in C++ and Boost.This library is platform independent and is used at the core of several BitTorrent clients such as qBittorent, LimeWire, Linkage, etc.
>-   lib**T**orrent is also an open source implementation of the BitTorrent protocol written in C++ for Unix.
>
>We are interested in  **libtorrent**.

### Detailed Matrix

|      Feature      |      Detail       |                         MonoTorrent                          |                          libtorrent                          |                   PInvoke over libtorrent                    |                     CLI over libtorrent                      |
| :---------------: | :---------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: |
|      Feature      |      Detail       |                         MonoTorrent                          |                          libtorrent                          |                   PInvoke over libtorrent                    |                     CLI over libtorrent                      |
|    Performance    |                   |                                                              |                                                              |                                                              |                                                              |
|                   |   Disk Caching    |    It caches block by block but writes out block by block    | Per their documentation, it is piece basedWhen disk caching is insufficient, it sorts read requests based on the location of data on the physical disksCopying is minimized by using page aligned buffers to receive the data, decrypt it if necessary (in place) and move the buffer reference to the disk cacheWhen everything is ready, write to diskshttp://libtorrent.org/features.html#highlighted-features | Extra overhead comes from marshalling and unmarshalling of calls and callbacks | Extra overhead comes from marshalling and unmarshalling of calls and callbacksEmpirical data indicates that it is slower than PInvoke but MSDN suggests otherwiseNevertheless, the performance enhancing features of the libtorrent are still present |
|  Maintainability  |                   |                                                              |                                                              |                                                              |                                                              |
|                   |     Language      |            Written in the familiar C# / .NET 3.0             |                    Written C++ and Boost                     |         C Style wrapper needs to be created in C/C++         |          CLI Wrapper needs to be created in C++/CLI          |
|                   |      Design       | Static main loop issueStack overflow occurrence due to poorly implemented producer / consumer with tasksBlock size of 16 KB were defined at multiple placesUnit tests present | Limited experience suggest that it is well maintained and well designedBlock size was defined as 16 KB at once place only and the remaining code depends on thatTBC: Changing the block size from 16 KB to 32 KB could be possibleUnit tests present |            Static based forward callDesign by us             |            Object based forward callDesign by us             |
| Development Speed |                   |                                                              |                                                              |                                                              |                                                              |
|                   |      Cachers      |           Blends well with our cachers technology            |                     Need to be rewritten                     |                    Implement the wrappers                    |                    Implement the wrappers                    |
|                   |    Web Seeders    |      Cannot be used because web seeding is not working       | Can use IIS or Apache as web serversClient can take advantages of it | Can use IIS or Apache as web serversClient can take advantages of it | Can use IIS or Apache as web serversClient can take advantages of it |
|                   |      Clients      |           Blends well with our cachers technology            |                     Need to be rewritten                     |                    Implement the wrappers                    |                    Implement the wrappers                    |
|      Support      |                   |                                                              |                                                              |                                                              |                                                              |
|                   |     Creators      |                         They are MIA                         |                            Active                            |                              Us                              |                     UsOne guy on GitHub                      |
|                   |     Community     |                    There is no community                     | ActiveMany products depends on the library and provides feedback and fixes |                             None                             |                             None                             |
|    Feature Set    |                   |                                                              |                                                              |                                                              |                                                              |
|                   | Multiple Trackers | Multiple trackersAnnouncement to multiple tiers is not implemented |           Not enable by default but it can be set            |                          Forwarded                           |                          Forwarded                           |
|                   |        uTP        |                       Not implemented                        | Enable by defaultSpeed is limited at 10 MB/s but it can be removed |                          Forwarded                           |                          Forwarded                           |
|                   |    Web Seeding    |                         Not working                          |                         Working fine                         |                          Forwarded                           |                          Forwarded                           |
|                   |     Trackers      |           Presence of libraries to create trackers           |                   No library for trackers                    |                          Forwarded                           |                          Forwarded                           |

> **Disk Caching**
>
> libtorrent disk caching can use a lot of RAM if we are seeding a significant amount of data. We should limit the disk caching or move the initial seeding to a dedicated content server.

## Configuration

### uTP

In order to get consistent speed, we need to enable the uTP (aka Micro Transport Protocol or the uTorrent protocol). This is an UDP-based variant aimed at mitigating poor latency and congestion control issues found in conventional TCP. However, in the case of libtorrent, the mixed mode algorithm that aims to strike a balance between uTP and TCP is not as greater as the one present in uTorrent. It is, therefore, preferable to disable uTP for the moment to achieve better speed.

> **uTP versus TCP**
>
> It is possible that in some cases uTP does not perform as well as TCP. In cases like these, you must make sure that TCP takes precedent.

### Web Seeding

Web seeding a technology that unables a web server (i.e. a content server) to serve data as a seeder

Web seeding using url-list (aka get right algorithm), requires 2 particular setup

1. On the server, make sure the web server is capable of serving all mime-type.
   1. .* for application/octet-stream
2. Placement of url "/" is critical at the end. If there is an "/" at the end of the url, it will assume a directory where the filename need to be appended. Therefore, the url must represent the root of the torrent.

> **Security**
>
> Web Seeding can represent a security risk since files on the web server could be accessible to anyone. We should investigate the possibility to put authentication or filter by ip address.

### Platform x86 versus x64

libtorrent, by default, is compiled on x86 and all clients using libtorrent must be compiled on x86. It is possible to compile on x64 but this has not been tested. qBittorrent has detailed instruction on how to do it.

### Shared Mode

The share mode feature in libtorrent is intended for users who are only interested in helping out swarms, not downloading the torrents.

It works by predicting the demand for pieces, and only download pieces if there is enough demand. New pieces will only be downloaded once the share ratio has hit a certain target.

This feature is especially useful when combined with RSS, so that a client can be set up to provide additional bandwidth to an entire feed.

## Synthetic Test Results

> Speed are bandwidth. Therefore, upload / download combined.

### Normal Seed

|                         | MonoTorrent | CLI libtorrent |
| :---------------------- | :---------- | :------------- |
| **1 Seed and 1 Peer**   | ~20 MB/s    | ~30 MB/s       |
| **1 Seed and 5 Peers**  | ~40 MB/s    | ~70 MB/s       |
| **5 Seeds and 1 Peers** | ~40 MB/s    | ~70 MB/s       |
| **5 Seeds and 5 Peers** | ~40 MB/s    | ~70 MB/s       |

### Web Seed

|                         | MonoTorrent | CLI libtorrent |
| :---------------------- | :---------- | :------------- |
| **1 Seed and 1 Peer**   | N/A         | ~40 MB/s       |
| **1 Seed and 5 Peers**  | N/A         | ~70 MB/s       |
| **5 Seeds and 1 Peers** | N/A         | ~70 MB/s       |
| **5 Seeds and 5 Peers** | N/A         | ~70 MB/s       |

## Real Life Test Case Results and Observations

The following results and observations were gathered from using MonoTorrent. Although libtorrent was not used in real life tests cases, the limitation we observed were not solely based on the implementation but also on the technology.

### Caching Speed

Using Bittorrent to cache on file streamers did not work too well for several reasons. We observed average speed of 20 MB/s.

##### Limited Amount of Peers

Bittorrent thrive with the large amount of seeds but most importantly even larger amount of peers. We should consider moving towards web seeding and use traditional copies for the file cachers if they are limited to around 6.

- This is also supported by data from ebay and their experience http://www.ebaytechblog.com/2012/01/31/bittorrent-for-package-distribution-in-the-enterprise/#.VBniHk1OVaQ
- A more intelligent approach would be to create tiers in the cachers and use resource reservation to ensure that each copy is done sequentially at maximum speed

##### Choke / Unchock and Stop / Start

It has been observed that constantly stopping and starting torrent has a negative effect on the overall swarm. Best case, it can leave a phantom peer to which no one can connect to and worst case a peer might be banned for such behavior.

##### Disk Seek Time

Bittorrent is known to download in a non sequential format. When dealing with greater amount of simultaneous downloads or uploads, the disk seek time is greater increase and this negatively impact the performance of the overall swarm, especially without the presence of an adequate disk caching system.

### Client Download

The PC Client couldn't take advantage of the torrent since it couldn't download from it. It would be better to implement the download from torrent.

### Network Congestion

Communication between the trackers and the peers can generate significant traffic. The usage of web seed removes this traffic.

### Single Point of Failure

The trackers can represent a single point of failure. MonoTorrent doesn't support multiple trackers announcements but libttorrent does.

## Future Test Cases

- Use web seeding
  - Investigate a secure version
    - Basic authentication over HTTPS?
    - IP Filtering?
  - 10 Gbps network
  - SSD ideal but HDD in Raid 0 can work
- Implement a client torrent
- Examine the delivery speed with 300 client and 1 web seed
  - This will determine how many web seed we need
  - If we need more, perhaps a caching in tiers for better distribution