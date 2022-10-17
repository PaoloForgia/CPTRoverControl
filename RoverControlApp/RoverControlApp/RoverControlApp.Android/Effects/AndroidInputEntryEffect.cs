using Android.Content.Res;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RoverControlApp.Droid.Renderer;


[assembly: ResolutionGroupName("InputEntryGroup")]
[assembly: ExportEffect(typeof(AndroidInputEntryEffect), "InputEntryEffect")]
namespace RoverControlApp.Droid.Renderer
{
    public class AndroidInputEntryEffect : PlatformEffect
    {
        public AndroidInputEntryEffect()
        {
        }

        protected override void OnAttached()
        {
            if (Control != null)
            {
                Android.Graphics.Color entryLineColor = Android.Graphics.Color.Rgb(33, 150, 243); // Primary color
                Control.BackgroundTintList = ColorStateList.ValueOf(entryLineColor);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
    }
}