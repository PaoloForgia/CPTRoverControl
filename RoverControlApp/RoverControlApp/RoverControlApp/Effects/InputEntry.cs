using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RoverControlApp.Effects
{
    /// <summary>
    /// Enable the OS specific render of the Input component
    /// </summary>
    public class InputEntry : RoutingEffect
    {
        public InputEntry() : base("InputEntryGroup.InputEntryEffect")
        {
        }
    }
}