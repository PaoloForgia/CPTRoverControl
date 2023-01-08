using RoverControlApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RoverControlApp.Services
{
    /// <summary>
    /// Manage the local storage of the application.
    /// </summary>
    public class Storage
    {
        private static readonly string MODULE_NAME_PROPERTY = "module_name";
        private static readonly string DEAD_ZONE_PROPERTY = "dead_zone";

        public static string ModuleName
        {
            get => Preferences.Get(MODULE_NAME_PROPERTY, DefaultValues.MODULE_NAME);
            set => Preferences.Set(MODULE_NAME_PROPERTY, value);
        }

        public static int DeadZone
        {
            get => Preferences.Get(DEAD_ZONE_PROPERTY, 10);
            set => Preferences.Set(DEAD_ZONE_PROPERTY, value);
        }
    }
}
