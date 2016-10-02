using Xamarin.Forms;

namespace UsingCustomRendererAdsXamarin.CustomRenderers.AdMob
{
    public class AdMobView : View
    {
        // enables databinding for the AdMob ad unit id 
        public static readonly BindableProperty AdUnitIdProperty =
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