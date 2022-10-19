using RoverControlApp.Models;
using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverControlApp.ViewModels
{
    public class ControlsViewModel: BaseViewModel
    {
        public ControlsViewModel()
        {
            Title = "Rover Controls";
        }
    }
}
