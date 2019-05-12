---
tags:
  - source control
  - tips
  - ops
---

In this post, we will look at different branching strategies.

# Code Promotion

In essence, a specific development version is branched off into a Test branch, at which all the integration and system testing is performed. When you complete testing, the software development assets are branched into the Production branch and ultimately deployed.

![Promotion]({{site.url}}/resources/2013-04-17-Branching-Strategies/Images/Promotion.png "Promotion"){: .align-center}

## Development
In essence, development of a story is done on the development branch. Peer review should be done on this branch. Consequently, it must be done as quickly as possible once the development is done. Ideally, it should be done even before the check in. Once the story is ready to test, it is promoted to the test branch. Development can continue, for another story, while tests are done.

## Test
The test branch is basically a release candidate branch. Tests are done on this branch. Any corrections are done on this branch and it must be merged back into the development branch as soon as it is stable. Once it passes all the tests, the code can now be promoted to the Production branch.

## Production
The production branch represents what is currently being deployed or deployed. Changes to fix bug on the production version must be done on this branch and merge back into the main branch as soon as it is stable.

# Branch By Label

The golden rule of branching is don't branch unless it is necessary. Considering that continuous integration encourages lots of integrations, it might be preferable to avoid branching altogether. In the branch by label strategy, we would simply tag with a specific label a specific revision as a release.

## Advantages

No branches to maintain. 
Integration is done only on the main branch
There is only one pipeline in a CI system

## Drawbacks

It could be a bit more troublesome to do hotfixes but if we only release the latest version, it should be fine

# Branch By Release

Branch by release is the preferred branching strategy in a continuous integration context when there is a need to maintain multiple releases at the same time. Otherwise, you would only have the trunk and there would be no branching per say. The main difference is that there is no "Code Promotion Branching Strategy" inside each release and the branching is not in a staircase (i.e. Release 3 is based on Release 2 which is based on Release 1). Instead, all releases are based on the trunk and all development is based on the trunk with the exception of bugfix.

- We code and commit on the trunk
- When the trunk contains all we want, we make a release and we branch for that release
- Only critical bug fix should be done in branches and merged back into the trunk
- We keep the release branch as long as it is in production and remove it when no longer needed

![Release]({{site.url}}/resources/2013-04-17-Branching-Strategies/Images/Release.png "Release"){: .align-center}

## Advantages

- There is a stable branch where we can do hot fixes.

## Drawbacks

- There can be a lot branches to maintain.
- It might requires us to maintain multiple pipeline in a CI system.