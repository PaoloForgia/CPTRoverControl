using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static RoverControlApp.Utils.Engine;

namespace RoverControlApp.Utils
{
    public class DynamicColors
    {
        private static readonly Color EmergencyStopOff = Color.FromHex("#FF694F");
        private static readonly Color EmergencyStopOn = Color.FromHex("#871D0A");

        private static readonly Color EngineStill = Color.FromHex("#2196F3");
        private static readonly Color EngineForward = Color.FromHex("#94f221");
        private static readonly Color EngineBackward= Color.FromHex("#F22167");

        public static Color EmergencyStopColor(bool active) => active ? EmergencyStopOn : EmergencyStopOff;
        public static Color EngineColor(EngineDirection engineDirection)
        {
            switch(engineDirection)
            {
                case EngineDirection.Forward: return EngineForward;
                case EngineDirection.Backward: return EngineBackward;
                case EngineDirection.Still: return EngineStill;
                default: return EngineStill;
            }
        }
    }
}
