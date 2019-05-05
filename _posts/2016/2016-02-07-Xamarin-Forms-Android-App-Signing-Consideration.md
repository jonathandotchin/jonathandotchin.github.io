---
tags:
  - xamarin
  - android
  - publish
---

In this post, we will examine a minor but important difference in the app signing process between Android and Windows apps.

# Background

Before an app can be published to the store and then be installed by the end user, the app needs to be signed by a digital certificate. This is true for both Android and Windows apps. 

# Windows

On Windows, when we package an app with Visual Studio to be published on the Microsoft Store, the app is automatically signed with a trusted certificate for us. Hence, Microsoft manage app certification.

# Android

On Android, however, this is slightly different. The app signing key is managed by the developer. It is, therefore, the responsibility to the developer to ensure that the key is secured. If the developer loses access to the key, then it will be impossible to release new version of the original app.