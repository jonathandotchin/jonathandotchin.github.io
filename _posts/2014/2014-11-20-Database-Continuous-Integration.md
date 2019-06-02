---
tags:
  - best practices
  - ci
  - tips
  - wip
---

# Context

## Version

### Database

- Support only a single version of the database at a time and it is always the current latest greatest version
- There could be a transition period where old and new entities can exist in the same database version

### Clients

- Multiple version of clients can co-exist. A client is a product (web, web services, application) that uses the database
    - The client is responsible for forward and backward compatibility with the database
    - Database can help with forward and backward compatibility by providing a transition period

## Rollback

### Immediate

    - Immediate rollback is necessary when the deployment of the database fails and we accept the limited amount of data lost

### Non Immediate

    - Non immediate rollback is necessary to handle the case when the deployment of the database succeed but there is a need to rollback without data lost

# TO DO

## Version

### Database Version

- Table version created to keep track of the database version
    - Possible information could include the version name, the upgrade date time and the linked product version
- Profiler can be used to identify used and unused entities
- During a transition period, entities to be removed can be prefixed with an _ to mark it for eventual removal

## Rollback

### Scripts

- All scripts are source controlled and can be automated
- Database creation script
    - A single script to create the schema, the objects and to seed the database with data
    - After running this script, a fully functional database should be available
    - Possible name: PRODUCT_NAME_CREATE_RELEASE_NUMBER
- Upgrade scripts
    - A script to upgrade a database from a supported version (base) to the latest version
    - A single script to create the schema, the objects and to seed the database with data
    - After running this script, a fully functional database should be available
    - Name by release and link to a product release
        - Needs to be ordered such that starting from a base script, we can run a series of upgrade scripts to create a fully functional database
        - Possible name: PRODUCT_NAME_UPDATE_TO_RELEASE_NUMBER
    - Idea: A upgrade script can contain calls to the create and previous upgrade scripts such that it will automatically handle the necessary calls
- Downgrade scripts
    - A script to download a database from the current version to a previous and supported version
    - Essentially, the same as an upgrade script but in reverse
    - New data (data added) preservation is considered
    - Name by release
        - Possible name: PRODUCT_NAME_ROLLBACK_TO_RELEASE_NUMBER
