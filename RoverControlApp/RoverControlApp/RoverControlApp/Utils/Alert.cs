using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoverControlApp.Utils
{
    public class Popup
    {

        public static void DisplayAlert(string title, string description, string buttonText = "OK")
        {
            Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert(
                    title,
                    description,
                    buttonText));
        }

        public static async Task<bool> DisplayPrompt(string title, string description, string accept = "Si", string cancel = "No")
        {
           return await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert(
                    title,
                    description,
                    accept,
                    cancel));
        }
    }
}
