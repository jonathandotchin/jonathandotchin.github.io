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

Your solution should be similar to

![Project Creation Result]({{site.url}}/resources/2016-01-09-Using Custom Renderers to Display Ads from Google AdMob on Xamarin.Forms/images/Project Creation Result.png "Project Creation Result")

## Portable

In the Portable project, you will need to create a class that inherits from Xamarin.Forms.View.

![AdMobView Creation]({{site.url}}/resources/2016-01-09-Using Custom Renderers to Display Ads from Google AdMob on Xamarin.Forms/images/AdMobView Creation.png "AdMobView Creation")

``` c#
using Xamarin.Forms;

namespace UsingCustomRendererAdsXamarin.CustomRenderers.AdMob
{
    public class AdMobView : View
    {
        // enables data binding for the AdMob ad unit id
        public static read-only BindableProperty AdUnitIdProperty = 
            BindableProperty.Create<AdMobView, string>(p => p.AdUnitId, "");

        // holds the AdMob ad unit id
        public string AdUnitId
        {
            get
            {
                return (string)this.GetValue(AdUnitIdProperty);
            }
            set
            {
                this.SetValue(AdUnitIdProperty, value);
            }
        }
    }
}
```

## Android

## Windows Phone