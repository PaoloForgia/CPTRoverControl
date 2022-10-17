using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RoverControlApp.Services
{
    public class Storage
    {
        private static readonly string MODULE_NAME_PROPERTY = "module_name";
        private static readonly string DEAD_ZONE_PROPERTY = "dead_zone";

        public static string ModuleName
        {
            get => Preferences.Get(MODULE_NAME_PROPERTY, Bluetooth.MODULE_NAME_DEFAULT);
            set => Preferences.Set(MODULE_NAME_PROPERTY, value);
        }

        public static int DeadZone
        {
            get => Preferences.Get(DEAD_ZONE_PROPERTY, 10);
            set => Preferences.Set(DEAD_ZONE_PROPERTY, value);
        }
    }
}
