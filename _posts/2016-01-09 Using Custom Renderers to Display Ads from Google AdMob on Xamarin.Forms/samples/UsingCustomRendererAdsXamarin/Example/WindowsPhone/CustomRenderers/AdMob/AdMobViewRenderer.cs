using GoogleAds;

using UsingCustomRendererAdsXamarin.CustomRenderers.AdMob;
using UsingCustomRendererAdsXamarin.WinPhone.CustomRenderers.AdMob;

using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace UsingCustomRendererAdsXamarin.WinPhone.CustomRenderers.AdMob
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> elementChangedEventArgs)
        {
            base.OnElementChanged(elementChangedEventArgs);

            var adMobElement = this.Element;

            if ((adMobElement != null) && (elementChangedEventArgs.OldElement == null))
            {
                var bannerAd = new AdView
                {
                    Format = AdFormats.Banner,
                    AdUnitID = adMobElement.AdUnitId,
                };

                var adRequest = new AdRequest();
#if DEBUG
                // use test ads in debug mode
                adRequest.ForceTesting = true;
#endif
                bannerAd.LoadAd(adRequest);
                this.Children.Add(bannerAd);
            }
        }

    }
}
