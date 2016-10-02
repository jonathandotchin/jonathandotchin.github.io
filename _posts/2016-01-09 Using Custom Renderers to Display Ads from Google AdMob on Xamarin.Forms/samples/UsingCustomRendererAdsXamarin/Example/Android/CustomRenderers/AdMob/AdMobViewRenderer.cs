using Android.Gms.Ads;

using UsingCustomRendererAdsXamarin.CustomRenderers.AdMob;
using UsingCustomRendererAdsXamarin.Droid.CustomRenderers.AdMob;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// registers the ViewRenderer
[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace UsingCustomRendererAdsXamarin.Droid.CustomRenderers.AdMob
{
    // creates the renderer that 'maps' the cross-platform element to the native element
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public static void Init()
        {

        }

        // renders the native element
        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);

            var adMobElement = this.Element;

            if (adMobElement != null && e.OldElement == null)
            {
                // declare the native element and assign the necessary properties like the Ad Unit Id
                AdView ad = new AdView(this.Context);
                ad.AdSize = AdSize.Banner;
                ad.AdUnitId = adMobElement.AdUnitId;
                var builder = new AdRequest.Builder();
#if DEBUG
                // Google requires the usage of test ads while debugging
                builder.AddTestDevice(AdRequest.DeviceIdEmulator);
#endif
                var adRequest = builder.Build();

                ad.LoadAd(adRequest);
                this.SetNativeControl(ad);
            }
        }
    }
}