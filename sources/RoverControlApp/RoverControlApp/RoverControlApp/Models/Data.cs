using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    /// <summary>
    /// Class representing one of the possible incoming Bluetooth values.
    /// </summary>
    public class Data
    {
        public int? Value { get; set; }
        public bool IsBattery { get; set; } = false;
        public bool IsDistance { get; set; } = false;
    }
}
