---
tags:
  - ClickOnce
---

In this post, we will look at creating mandatory updates for ClickOnce Application.

# Checking for Updates After Application Startup

To enable this update strategy, click After the application starts in the Choose when the application should check for updates section of the Application Updates dialog box. Then specify an update interval in the section Specify how frequently the application should check for updates.

# Checking for Updates Before Application Startup

To enable this update strategy, click Before the application starts in the Choose when the application should check for updates section of the Application Updates dialog box.

# Making Updates Required

To mark an update as required, click Specify a minimum required version for this application in the Application Updates dialog box, and then specify the publish version (Major, Minor, Build, Revision), which specifies the lowest version number of the application that can be installed.

# References

http://msdn.microsoft.com/en-us/library/vstudio/s22azw1e.aspx