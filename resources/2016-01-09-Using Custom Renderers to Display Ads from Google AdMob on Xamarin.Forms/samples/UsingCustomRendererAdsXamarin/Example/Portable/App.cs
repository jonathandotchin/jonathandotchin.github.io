using UsingCustomRendererAdsXamarin.CustomRenderers.AdMob;

using Xamarin.Forms;

namespace UsingCustomRendererAdsXamarin
{
    public class App : Application
    {
        public App()
        {
            // create the ads
            var ads = new AdMobView();
            if (Device.OS == TargetPlatform.WinPhone)
            {
                ads.AdUnitId = "ca-app-pub-9345895155624673/5161313341";
            }

            if (Device.OS == TargetPlatform.Android)
            {
                ads.AdUnitId = "ca-app-pub-9345895155624673/8114779749";
            }

            // include the ads
            this.MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label
                            {
                                Text = "Hello Ads"
                            },
                        ads
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
