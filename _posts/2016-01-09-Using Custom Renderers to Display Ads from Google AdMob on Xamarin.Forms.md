# Using Custom Renderers to Display Ads from Google AdMob on Xamarin.Forms

## Background

Xamarin.Forms support several UI elements for Pages (ContentPage, NavigationPage, TabbedPage, etc.), Layouts (StackLayout, GridLayout, ScrollView, etc.), and Controls (Button, Image, Label, etc.). Each of these elements are rendered into their native equivalents on each platform.

But what about elements without native equivalents like ads from Google AdMob? Luckily, you can use Custom Renders for that.

## Prerequisites

The following assumes that you are already familiar with Google AdMob. If you are not, you can look it up from the links below.

- [AdMob for Android](https://developers.google.com/admob/android/start)
- [AdMob for iOS](https://developers.google.com/admob/ios/start)
- [AdMob for Windows Phone](https://developers.google.com/admob/wp/quick-start)

## Getting Started

You will need to create a Xamarin.Forms project. The samples will be featuring the Portable version but you can easily adapt the code for the Shared version.

![Project Creation Template]({{site.url}}/resources/2016-01-09-Using Custom Renderers to Display Ads from Google AdMob on Xamarin.Forms/images/Project Creation Template.png "Project Creation Template")