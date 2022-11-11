using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static RoverControlApp.Utils.Engine;

namespace RoverControlApp.Utils
{
    public class DynamicAttributes
    {
        private static readonly string EmergencyStopOffLabel = "ENABLE STOP";
        private static readonly string EmergencyStopOnLabel = "DISABLE STOP";
        private static readonly Color EmergencyStopOffColor = Color.FromHex("#000000");
        private static readonly Color EmergencyStopOnColor = Color.FromHex("#FF694F");

        private static readonly Color EngineStill = Color.FromHex("#2196F3");
        private static readonly Color EngineForward = Color.FromHex("#94f221");
        private static readonly Color EngineBackward= Color.FromHex("#F22167");

        public static Color EmergencyStopColor(bool active) => active ? EmergencyStopOnColor : EmergencyStopOffColor;
        public static string EmergencyStopLabel(bool active) => active ? EmergencyStopOnLabel : EmergencyStopOffLabel;

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
