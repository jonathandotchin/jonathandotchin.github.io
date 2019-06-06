---
tags:
- brainstorm
- owmm
- design
---

# Context 

If we only have a single service handling the data for the entire population, we will limit ourselves in the following area:

- Space: We might not have enough storage space to handle such as a high volume of data.
- Processing: The computing resources to process all this data might be too great resulting in extended response times.
- Bandwidth: We might exceed network bandwidth.
- Geography: We might experience high latency. 

# Solution

We can divide the data into horizontal partitions or shards. For example, we can have different service for US-EAST, US-WEST, EU-WEST, EU-EAST, etc. Since people are generally more willing to play with others that close to them, this should not be an issue.

# Considerations

Nevertheless, if players of different zone wants to play together, we should allow them to form a party.

