---
tags:
  - xamarin
  - android
  - build
  - error
---

Today, I ran into a small issue when working on my Xamarin Forms Android application. In essence, my application would run fine in debug mode but it would complain about a missing 'libmonodroid.so' in release mode. After digging through the internet, it turns out it was a configuration issue and/or difference in the build options. To access these options, you will need to go to the "Advanced Android Options" (hidden under the "Advanced" button in "Android Options") and validate the selected values of "Supported Architecture" so they would match the device being used.

[couldn't find "libmonodroid.so"](https://forums.xamarin.com/discussion/55557/couldnt-find-libmonodroid-so)