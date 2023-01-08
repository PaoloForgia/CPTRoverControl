using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Utils
{
    /// <summary>
    /// Utility file to display to perform checks related to the engine status of the rover.
    /// </summary>
    public class Engine
    {

        public enum EngineDirection { Forward, Still, Backward };

        public static EngineDirection IsMoving(int value)
        {
            var minValue = DefaultValues.ENGINE_STOP_VALUE - Storage.DeadZone;
            var maxValue = DefaultValues.ENGINE_STOP_VALUE + Storage.DeadZone;

            if (value < minValue) return EngineDirection.Backward;
            else if (value > maxValue) return EngineDirection.Forward;
            else return EngineDirection.Still;
        }
    }
}
