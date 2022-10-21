using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    public class Data
    {
        public int? Value { get; set; }
        public bool IsBattery { get; set; } = false;
        public bool IsDistance { get; set; } = false;
    }
}
