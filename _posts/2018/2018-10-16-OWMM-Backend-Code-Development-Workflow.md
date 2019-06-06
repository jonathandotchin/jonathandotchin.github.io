---
tags:
- workflow
- owmm
- perforce
---

The following provides a description of the workflow use during the development of the OWMM service. We are assuming that the development is divided into epics, epics into tasks and tasks are considered code deliverables. In essence, we are taking advantage of the relative small footprint of the OWMM backend code that makes it easy to perform some form of feature branching.

# Steps

## Create a Task Branch

When starting a new task, we create a new branch based on the main branch. We would call this a task branch.

## Working on Sub Tasks

On this task branch, we are free to perform swarm review and commit at heart depending on how the work is divided. For example, one might want to divide the task at hand into multiple sub tasks such that as we work on each sub tasks, each of them should be committed into perforce individually. Hence, for each sub tasks, we should request a code review, follow the workflow and commit when ready. This will have the benefits of smaller reviews and checkins allowing a better visibility on the tasks.

## Merge Task Branch into the Main Branch

When the task is ready to be completed, the task branch is merged back into main. Request a swarm code review, follow the code review workflow and commit when ready. A single merge will not "contaminate" the main branch with the multiple checkins when working on sub tasks. Once the merge is done, then the task is completed.

# Addendum

Once the modification of the backend is completed, we can then update the code in the game client / dedicated game server if necessary.