using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RoverControlApp.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Context = Android.Content.Context;

[assembly: ExportRenderer(typeof(Entry), typeof(InputCursorRenderer))]
namespace RoverControlApp.Droid.Renderer
{
    public class InputCursorRenderer : EntryRenderer
    {
        public InputCursorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                EditText nativeEditText = (global::Android.Widget.EditText)Control;
                nativeEditText.SetTextCursorDrawable(Resource.Drawable.input_cursor);
            }
        }
    }
}