---
tags:
  - xamarin
  - android
  - build
  - error
---

In this post, we will examine a build error that be initially be baffling. In essence, after adding a library to your Xamarin Forms Android project, we end up with a "java.exe exit code 1" error. Removing the library in question and everything works fine.

# Background

This is due to the 65K reference limit of Android application.

> Android application (APK) files contain executable bytecode files in the form of Dalvik Executable (DEX) files, which contain the compiled code used to run your app. The Dalvik Executable specification limits the total number of methods that can be referenced within a single DEX file to 65,536, including Android framework methods, library methods, and methods in your own code. 

# Solution

In many cases, the application doesn't need all the 65K references. They just happen to be there and take valuable space. The simplest solution would be to enable [Proguard](https://developer.android.com/studio/build/shrink-code.html). In Xamarin Forms, it is as simple as checking the box "Enable Proguard" in "Android Options" of the Android app. However, there is a couple of issues to be on the lookout for.

## Spaces in Paths

If the following error is encountered when running Proguard

```
Proguard error '...' (Access is Denied)
```

It probably means that one of more of the Android Settings (Java Development Kit Location, Android SDK Location, Android NDK Location) contain location with spaces in their path

To remove the spaces, we can reinstall the tool at the proper location or create symbolic links.

```
mklink /J "D:\Xamarin\jdk1.8.0_112" "C:\Program Files (x86)\Java\jdk1.8.0_112"
mklink /J "D:\Xamarin\android-sdk" "C:\Program Files (x86)\Android\android-sdk"
mklink /J "D:\Xamarin\android-ndk-r10e" "C:\ProgramData\Microsoft\AndroidNDK\android-ndk-r10e"
```

Afterward, we just need to update Visual Studio Options

![Visual Studio Options]({{site.url}}/resources/2017-07-11-Xamarin-Forms-Exit-Code-1-Error-After-Adding-Libraries/Images/Visual-Studio-Options.png "Visual Studio Options"){: .align-center}

## Overzealous Proguard

It happens that Proguard removes more than it should. This typically happens when it is unable to properly scan the usage of the application. To fix this issue, we can include a proguard configuration file at the root of the Android app.

1. Add a new text file called proguard.cfg at the root of the Android app
2. Make sure the build action is set to ```ProGuardConfiguration```
3. Include in this file the library you want to keep. In my case, it was

```
-keep public class com.google.android.gms.* { public *; }
```