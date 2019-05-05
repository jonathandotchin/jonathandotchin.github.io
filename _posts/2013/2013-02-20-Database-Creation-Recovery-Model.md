---
tags:
  - database
---

There are 3 recovery models we can choose from when creating a database: Simple, Full and Bulk Logged

## Simple

Basically, a transactional log where changes to the database are remembered but only for the transaction in progress.
In terms of backup and restore, we can only do full backup and full backup restore. Nothing more, nothing less.

## Full

Basically, we have a log that contains every changes from a backup to another backup of the log in question.
In terms of backup and restore, we got two possibilities:
- Full backup (like before)
- Transactional backup or Log backup where we can backup and restore from or to a specific point in time based on the log.

## Bulk Logged

Not in this discussion
 
## Simple vs Full

In test, we should select Simple since it takes less spaces
In prod, we should take Full
If the platform is handle by a third party (GNS Infrastructure), we should take full