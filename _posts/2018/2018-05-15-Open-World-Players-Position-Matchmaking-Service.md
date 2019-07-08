---
tags:
- prototype
- owmm
- design
---

## Purpose

The purpose was to brainstorm on the specific of the online service used to matchmake players based on their open world position. It focuses on identifying any area that warrant further investigations that will be outlined in Axis of Investigations where different developers will take the lead on.

## Area of Investigation

| Topic                       | Description                                                  | Interrogation                                                |
| :-------------------------- | :----------------------------------------------------------- | :----------------------------------------------------------- |
| Responsibilities            | Define the responsibilities of the service                   | Better name than "Matchmaking Service" or "Open World Players' Position Matchmaking Service"<br />Criteria to move players between dedicated game servers<br />Population rebalancing<br />Consolidate players population for scaling down dedicated game server<br />Authoritative <br />Is the decision of the service final?<br />Can the dedicated game server overrule the decision? |
| Architecture and Design     | Define the architecture and the design of service<br />Define the topology of the components | Multiple process for the "service"<br />1 process that execute the request for players' migration<br />1 process for filtering the data for analysis<br />1 process for processing the data for matchmaking<br />etc.<br />Master Agent Workers Architecture<br />Will We Allow Players to Change Matchmaking Service? <br />Topology <br />Service and dedicated game server kept on same data center Versioning<br />Match service with game version<br />Version of service itself |
| Communication Technologies  | Look into the different technologies for proper and efficient communications<br />Possibility of enormous amount of information between dedicated game server and this service<br />Possibility of large quantity of connections between dedicated game server and this service | Protocols<br />TCP vs. Websockets vs. RUDP vs. Others<br />Different protocol for different use cases<br />Serialization / Deserialization<br />Protobuf vs. custom binary vs. others |
| Data Management             | Look into how the data from the dedicated game server is managed / saved in the service | Historical data<br />Grouping info of the past 5-10 minutes<br />Storage<br />Same process vs. redis |
| Implementation Technologies | Default language choice C# / .NET Core<br />Possibility of enormous "processing" power requirement | Performance<br />Native compilation                          |
| Implementation Strategies   | Look into the different strategies / algorithms for efficient processing of data and decision making | Filter / throttle by dedicated game server<br />No point of sending information when players are in specific situation (i.e. outpost)<br />Service can ask dedicated game server to reduce load<br />Service's Data Scalability<br />Partition<br />Filter<br />Share<br />Bucket |
| Fault Tolerance             | Look into how to make the service robust                     | Crash recovery<br />Automated<br />Self healing<br />Kubernetes / cluster management |