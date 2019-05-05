---
tags:
  - xamarin
  - renderer
---

This post examines one nagging issue with respect to how Xamarin Forms render native controls.

# Background

The idea behind Xamarin Froms is simple and effective. Basically, one would authored the UI in C# or XAML and, at runtime, each controls are rendered in their native elements on each platforms. The idea would that they would function the same but look native. However, there are some subtle differences that are just annoying.

# Page Padding

## Issue

On Windows, there is an extra padding around the content of a page. However, this padding is not existent on Android. The consequence is that on Android, the control are basically glued to the side of the screen.

## Workaround

To fix this issue, we can simply add extra padding but only on Android.

``` XAML
<Grid.Padding>
<OnPlatform x:TypeArguments="Thickness">
<On Platform="Android">8</On>
</OnPlatform>
</Grid.Padding>
```

# List View Items

## Issue

On Android, list view items do not expand in height to show its full content. On Windows, this is done by default.

## Workaround

To fix this issue, we set the attribute ```HasUnevenRows``` of the ```ListView``` to ```True```.