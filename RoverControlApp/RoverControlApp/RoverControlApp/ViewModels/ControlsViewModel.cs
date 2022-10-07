using RoverControlApp.Models;
using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverControlApp.ViewModels
{
    public class ControlsViewModel: BaseViewModel
    {
        public ICommand EnableBluetoothCommand { get; set; }

        public ControlsViewModel()
        {
            Title = $"Rover Controls - {Storage.ModuleName}";
            EnableBluetoothCommand = new Command(EnableBluetooth);
        }

        void EnableBluetooth()
        {
            Bluetooth bluetooth = new Bluetooth();
            if (!bluetooth.Enabled)
            {
                bluetooth.Enable();
            }
        }
    }
}
