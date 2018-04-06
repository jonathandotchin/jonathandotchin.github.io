---
tags:
  - xamarin
  - forms
  - android
  - uwp
---

In this short post, we will examine how to open the native App Store on Android and Windows towards a specific app. This is useful when you want to advertise other apps in the current one.

# Android

``` c#
Device.OpenUri(new Uri("market://details?id=aaa.bbb.ccc"));
```

# Windows

``` c#
Device.OpenUri(new Uri("ms-windows-store://pdp/?productid=9nblggh0dgbf"));
```

# Windows Phone

``` c#
Device.OpenUri(new Uri("https://www.microsoft.com/store/apps/9nblggh0dgbf"));
```