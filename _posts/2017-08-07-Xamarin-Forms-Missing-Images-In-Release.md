---
tags:
  - xamarin
  - image
  - runtime
---

The images in my app were initially embedded in the assembly of my PCL. They would be retrieved as follow:

``` c#
return ImageSource.FromResource($"NHLScoresXamarin.PCL.Images.Teams.{abbreviation.ToLowerInvariant()}.png", Assembly);
```

In Debug mode, it was working fine but it turns out that in Release mode, the images would simply not appear. This was due to the way reflection works when using .NET Native Tool Chain for compilation.

The simplest solution is to simply move the images out of the PCL and place them in the main project. That way, the images can be retrieved as follow:

``` c#
if (Device.RuntimePlatform == Device.Android)
{
    return ImageSource.FromFile($"{abbreviation.ToLowerInvariant()}.png");
}
return ImageSource.FromFile($"Images/Teams/{abbreviation.ToLowerInvariant()}.png");
```