This post documents the different technologies investigated for cross-platform mobile development.

# Background

This post documents the findings related to each technology investigated for cross-platform mobile development. As more features are investigated, this post will be updated in consequence.

# Comparison Matrix

|Features|Xamarin|Apache Cordova|
|---|---|---|
|IDE|Visual Studio Community Edition <br/> [Xamarin Platform](https://www.xamarin.com/platform)|Visual Studio Community Edition <br/> [Tools for Apache Cordova](https://www.visualstudio.com/vs/cordova/)|
|Language|XAML and C#|HTML and JavaScript|
|Pricing|~~1 year trial and 25$ per month~~ <br/> ~~1000$ per year for Visual Studio Integration~~ <br/> Xamarin now provides a free version for students, oss development and small non-enterprise teams.|Free|
|Shared UI|[Xamarin.Forms](https://www.xamarin.com/forms)|HTML Elements <br/> Third party libraries like [Onsen](https://onsen.io/) and [Ionic](http://ionicframework.com/)|
|Shared Business Logic|C# <br/> [Shared Project](https://developer.xamarin.com/guides/cross-platform/application_fundamentals/shared_projects/) <br/> [Portable Class Libraries](https://developer.xamarin.com/guides/cross-platform/application_fundamentals/pcl/introduction_to_portable_class_libraries/)|JavaScript|
|Libraries|[Xamarin Components Store](https://components.xamarin.com/) <br/> More might be found on NuGet but you need to make sure it works on the platform of your choice.|Since it is classic JavaScript, packages found on [Bower](https://bower.io/) are working so far.|
|Emulation|Visual Studio Emulators|Visual Studio Emulators|
|Community|[Dedicated Forums](http://forums.xamarin.com/)|[StackOverflow](http://stackoverflow.com/questions/tagged/cordova)|
|Native Features|Access via [Dependency Service](https://developer.xamarin.com/guides/xamarin-forms/dependency-service/)|Access via [Plugins](https://cordova.apache.org/docs/en/latest/guide/hybrid/plugins/)<br/>[Apache Cordova Plugins](https://cordova.apache.org/plugins/)|
|UI Styling|[Styling](https://developer.xamarin.com/guides/xamarin-forms/user-interface/styles/) in Xamarin.Forms is similiar to XAML Styling <br/> Xamarin.Forms renders elements using native element. If you want to change the look and feel, you will need [Custom Renderer](https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/)|Classic CSS Style Sheets|
|Updates|Notice via the system tray|Notice via Visual Studio's notification|

# Decision Tree

.NET developer should be favouring Xamarin over Apache Cordova whereas JavaScript developer should be doing the opposite. The only time I used Apache Cordova over Xamarin was when a third party provider started to include HTML construct in the response of its web service. Needless to say that it's not a good practice.