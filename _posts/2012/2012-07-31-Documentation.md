---
tags:
  - best practices
---

# Summary

The following describes what the minimum in terms of documentation is when we are in the development, test and production stage.

## Development

It is quite possible to have multiple diagrams for certain products.

### Architecture Diagram

The goal of an architecture diagram is to show how the application is deployed and how it is interacting with others applications or external resources.

At minimum, it should show:
- The different servers involved (i.e. web, database, file, etc)
- Which components is hosted on those servers
- The identity running those components
- The main flow of the application

### Components / Design Diagram

Components diagrams represent how the different design components interact within an application. It is up to the developer's judgment on how detailed it should be. 
For example, the watermarking application can benefit from documentation that would detail:
- Each assembly
- The pipeline design
- The watermarking algorithm

### Deployment Procedure

This is self-explanatory. This document represents the exact steps on how to deploy the application.

## Test

It is quite possible to have multiple test plans for certain products.

### Test Plan

The test plan tells you exactly which tests must be performed in order to be certified as production ready. The goal of the test plan is that anyone can take the plan and test the application and perform the job as good as anyone else. The test plan can also include a smoke test section, which detailed the minimum amount of tests required to validate that the application is still functional.

## Production

Ideally, before the application is put into production, we should have documentation that clearly provides an overview of the application.

### Product Overview

It should include but is not limited to:
- Goal of the application
- Key features and benefits
- What it does
- How it does it
- In what way can it help users

### User Guide

In essence, information for the users on how to use the application. It could be a separate document or inline.

### Administrator Guide

Documentation for the administrators on how to administer and maintain the application. It could be a separate document or inline.