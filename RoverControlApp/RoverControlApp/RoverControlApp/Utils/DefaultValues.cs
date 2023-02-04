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
        public const bool FirstStartup = true;
        public const string ModuleName = "HC-05";
        public const int DeadZone = 10;
        public const int EngineStopValue = 128;
        public const bool GenerateDemoProgram = true;
    }
}
