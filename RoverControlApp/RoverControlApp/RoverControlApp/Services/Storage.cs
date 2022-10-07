using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RoverControlApp.Services
{
    public class Storage
    {
        public static string ModuleName
        {
            get => Preferences.Get(Bluetooth.MODULE_NAME_PROPERTY, Bluetooth.MODULE_NAME_DEFAULT);
            set => Preferences.Set(Bluetooth.MODULE_NAME_PROPERTY, value);
        }
    }
}
