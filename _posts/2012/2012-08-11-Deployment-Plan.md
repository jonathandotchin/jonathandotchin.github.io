---
tags:
  - best practices
---

The following is a general guideline on the different steps involved in deploying an application. Each application will have its own deployment procedure. 

# Pre

## Integration Tests

It is extremely important to not only test your changes but also the rest of the application. This is done to make sure that the new features and old features are working correctly.

## Deployment Plan

Create the deployment plan if it is not done, update it if necessary.

## Customer Notifications

Make sure that the customers are aware of the deployment and of any downtime. Otherwise, they will punch you and if they don't Tommy will.

# During
## Deployment Plan

Execute the deployment plan (duh!)

# Post

## Smoke Tests

Smoke tests are present to make sure that the application still works as intended when the deployment is done. Basically, you make sure that the main workflow of the application is working.
For instance, in build db, it should still download builds; the website should load; people can upload builds, etc.

## Customers Notifications

Make sure that the customers are notified that the application is up and running since they been waiting since your previous notification.