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
        private const string firstStartupProperty = "first_startup";
        private const string moduleNameProperty = "module_name";
        private const string deadZoneProperty = "dead_zone";
        private const string generateDemoProgramProperty = "generate_demo_program";

        public static bool FirstStartup
        {
            get => Preferences.Get(firstStartupProperty, DefaultValues.FirstStartup);
            set => Preferences.Set(firstStartupProperty, value);
        }

        public static string ModuleName
        {
            get => Preferences.Get(moduleNameProperty, DefaultValues.ModuleName);
            set => Preferences.Set(moduleNameProperty, value);
        }

        public static int DeadZone
        {
            get => Preferences.Get(deadZoneProperty, DefaultValues.DeadZone);
            set => Preferences.Set(deadZoneProperty, value);
        }

        public static bool GenerateDemoProgram
        {
            get => Preferences.Get(generateDemoProgramProperty, DefaultValues.GenerateDemoProgram);
            set => Preferences.Set(generateDemoProgramProperty, value);
        }
    }
}
