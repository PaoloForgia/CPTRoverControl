using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Utils
{
    /// <summary>
    /// Utility file that contains the default value used in the application.
    /// Will be moved into the DB in the future.
    /// </summary>
    public class DefaultValues
    {
        public static readonly bool FIRST_STARTUP_PROPERTY = true;
        public static readonly string MODULE_NAME = "HC-05";
        public static readonly int ENGINE_STOP_VALUE = 128;
    }
}
