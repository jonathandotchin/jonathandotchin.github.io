---
tags:
  - contests
---

This is a re-post of an article that I wrote for the App Innovation Contest run by Code Project.

# On The Spot 

## Tags 

C# Windows .NET WPF Sensors 

## One Line Description 

With the ability to customize your search, no matter where you are and what you want, there is a spot for you. 

## Disclaimer 

This article is an entry in our AppInnovation Contest. Articles in this sub-section are not required to be full articles so care should be taken when voting.  

## Introduction 

On The Spot is an application that provides information of interest based on your current location.  
 
Need some money? Low on gas? Hungry? On The Spot can help you. It will locates point of interest such as "Banks", "Gas Station" and "Restaurants" and provides detail information such address (with maps and itineraries), phone numbers and distance and bearing relative to your current position such that you will have no problem getting what you want.  
 
What new today? What's the weather? How’s traffic? On The Spot can help you. Local news, weather and traffic information are at your fingertip. 
 
With the ability to customize your search, no matter where you are and what you want, there is a spot for you. 

## Application Overview 

### Location Part I 

Many of the data services used in this application require the location in latitude and longitude. For this we can use the class “GeoCoordinateWatcher”, which I wrapped in the “Location” helper class. 

``` c#
#region References

    using System;
    using System.Device.Location;

    #endregion

    /// <summary>
    /// Defines the Location type.
    /// </summary>
    public static class Location
    {
        /// <summary>
        /// Represent the default sync timeout
        /// </summary>
        private static readonly TimeSpan DefaultSyncTimeout = new TimeSpan(0, 0, 15, 0);

        /// <summary>
        /// Gets the last location sync date time.
        /// </summary>
        /// <value>The last location sync date time.</value>
        public static DateTime LastLocationSyncDateTime { get; private set; }

        /// <summary>
        /// Gets the last location sync latitude.
        /// </summary>
        /// <value>The last location sync latitude.</value>
        public static double LastLocationSyncLatitude { get; private set; }

        /// <summary>
        /// Gets the last location sync longitude.
        /// </summary>
        /// <value>The last location sync longitude.</value>
        public static double LastLocationSyncLongitude { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [last location sync successful].
        /// </summary>
        /// <value>
        /// <c>true</c> if [last location sync successful]; otherwise, <c>false</c>.
        /// </value>
        public static bool LastLocationSyncSuccessful { get; private set; }

        /// <summary>
        /// Gets the last location sync status.
        /// </summary>
        /// <value>The last location sync status.</value>
        public static string LastLocationSyncStatus { get; private set; }

        /// <summary>
        /// Gets the last location sync horizontal accuracy.
        /// </summary>
        public static double LastLocationSyncHorizontalAccuracy { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [sync required].
        /// </summary>
        /// <value><c>true</c> if [sync required]; otherwise, <c>false</c>.</value>
        public static bool SyncRequired
        {
            get
            {
                // check a sync should be done
                if (DateTime.Now.Subtract(DefaultSyncTimeout) < LastLocationSyncDateTime && LastLocationSyncSuccessful)
                {
                    // no need to sync
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Creates the geo coordinate watcher.
        /// </summary>
        /// <returns>the geo coordinate watcher</returns>
        public static GeoCoordinateWatcher CreateGeoCoordinateWatcher()
        {
           var geoCoordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High) { MovementThreshold = 20 };
            geoCoordinateWatcher.StatusChanged += GeoCoordinateWatcherStatusChanged;
            geoCoordinateWatcher.PositionChanged += GeoCoordinateWatcherPositionChanged;
            return geoCoordinateWatcher;
        }

        /// <summary>
        /// Handles the coordinate watcher position changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void GeoCoordinateWatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            // set the location
            LastLocationSyncDateTime = DateTime.Now;
            LastLocationSyncLatitude = e.Position.Location.Latitude;
            LastLocationSyncLongitude = e.Position.Location.Longitude;
            LastLocationSyncHorizontalAccuracy = e.Position.Location.HorizontalAccuracy;
            LastLocationSyncSuccessful = true;
            LastLocationSyncStatus = string.Empty;

            // stop the watcher
            ((GeoCoordinateWatcher)sender).Stop();
        }

        /// <summary>
        /// Handles the StatusChanged event of the GeoCoordinateWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionStatusChangedEventArgs"/> instance containing the event data.</param>
        public static void GeoCoordinateWatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Ready:
                    {
                        // set the location
                        LastLocationSyncDateTime = DateTime.Now;
                        LastLocationSyncLatitude = ((GeoCoordinateWatcher)sender).Position.Location.Latitude;
                        LastLocationSyncLongitude = ((GeoCoordinateWatcher)sender).Position.Location.Longitude;
                        LastLocationSyncHorizontalAccuracy = ((GeoCoordinateWatcher)sender).Position.Location.HorizontalAccuracy;
                        LastLocationSyncSuccessful = true;
                        LastLocationSyncStatus = string.Empty;

                        // stop the watcher
                        ((GeoCoordinateWatcher)sender).Stop();
                        break;
                    }

                case GeoPositionStatus.Initializing:
                    {
                        // do nothing because still working
                        LastLocationSyncSuccessful = false;
                        LastLocationSyncDateTime = DateTime.Now;
                        LastLocationSyncStatus = "Acquiring your current location. This could take up to one minute.";

                        break;
                    }

                case GeoPositionStatus.Disabled:
                    {
                        // say msg disable
                        LastLocationSyncSuccessful = false;
                        LastLocationSyncDateTime = DateTime.Now;
                        LastLocationSyncStatus =
                            "The Location Service is currently disable on your device. Since this application depends on it for proper and accurate information, it must be enabled to function properly.";

                        // stop the watcher
                        ((GeoCoordinateWatcher)sender).Stop();
                        break;
                    }

                case GeoPositionStatus.NoData:
                    {
                        // say msg
                        LastLocationSyncSuccessful = false;
                        LastLocationSyncDateTime = DateTime.Now;
                        LastLocationSyncStatus =
                            "The Location Service of your device did not return your location at this time. Since this application depends on it for proper and accurate information, please make sure that it is up and running and try again later.";

                        // stop the watcher
                        ((GeoCoordinateWatcher)sender).Stop();
                        break;
                    }
            }
        }
    }
```

### Location Part II

Some data services, such as weather, do not accept latitude and longitude as input. They require city, state and country. To obtain this information, we need to use the Geocode service from the Bing Maps SOAP Services.

After adding a service reference to http://dev.virtualearth.net/webservices/v1/geocodeservice/GeocodeService.svc/mex, we can do the following:

``` c#
private async void ReverseGeocode(double latitude, double longitude)
        {
            // use bing to make a revers lookup
            try
            {
                // set the credentials using a valid bing maps key
                var reverseGeocodeRequest = new ReverseGeocodeRequest
                {
                    Credentials = new Credentials { ApplicationId  DevKey }
                };

                // set the point to use to find a matching address
                var point = new GeocodeLocation
                {
                    Latitude = latitude,
                    Longitude = longitude
                };

                reverseGeocodeRequest.Location = point;

                // make the reverse geocode request
                var geocodeServiceClient = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
                var geocodeResponse = await geocodeServiceClient.ReverseGeocodeAsync(reverseGeocodeRequest);

                if (geocodeResponse != null)
                {
                    var address = geocodeResponse.Results[0].Address;
                    this.city = address.Locality;
                    this.state = String.IsNullOrEmpty(address.AdminDistrict) ? address.District : address.AdminDistrict;
                    this.zip = address.PostalCode;
                    this.country = address.CountryRegion;
                }
            }
            catch
            {
                // handle error
            }
        }
```

Armed with these information, it is now possible to query services from Bing, Yahoo and Google and obtain results for point of interest, news, weather and traffic.

### Mock up

![Mock Up]({{site.url}}/resources/2012-10-23-On-The-Spot-App-Innovation-Contest-Entry/images/MockUp.png "Mock Up"){: .align-center}

## Device Overview

### Touch UI

- Modern UI usable with Touch and stylus in addition to the more traditional inputs such as keyboard and mouse.

### GPS

- Location aware data will be provided with the help of the GPS as well as the speed of travel and the estimated time of arrival.

### Compass

- Provides bearing information to aid with the navigations.

### NFC

- Share point of interests with other parties.

## Point of Interest

Although the application is still under development, I already had three interesting experiences that I believe are worthy of sharing.

### TDD and Mocking

Since the beginning, I wanted to approach this project using Test-Driven-Development. While exploring Visual Studio 2012, I notice its excellent support for unit test frameworks whether it is Microsoft’s homegrown version or community driven version such as xUnit.net.

In order to properly isolate myself from my source of data, I started “stubbing” all my data. I soon realized how tedious it was. It was at that moment that I discovered a mocking framework called “Moq”, which made my development much easier.
http://code.google.com/p/moq/

### TFS

For personal application development, I use a local source control. Initially, I was using a Perforce Server that I installed locally on my desktop. I picked Perforce simply because it was something that I was familiar with due to my day to day usage at work. Since the beginning, I realize that it wasn’t really convenient since my code, although versioned, was not secured and I had to do manual backup.

While poking around the web, I discovered that Microsoft have a free preview of a hosted Team Foundation Service. With it tight integrations, source control, back up, and unit tests became a breeze.
https://tfspreview.com/en-us/

### Continuous Integration

While exploring the different features offered by the Team Foundation Service, I noticed the ability to setup continuous integration. In essence, every time I checked in my code, I setup TFS to automatically run a build process that also executes my unit tests. This basically allows me to catch bugs early by building and testing more often. In a one man team, the advantages are not as extended as in a collaborative project but it was still interesting to know.

## History

- October 18, 2012 – Initial Version
