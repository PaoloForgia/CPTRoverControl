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
        private readonly Bluetooth _bluetooth = new Bluetooth();
        private readonly Commands _commands = new Commands();


        public ControlsViewModel()
        {
            Storage.ModuleName = "PAOLO-HC-05"; // TODO: remove in the future
            Title = $"Rover Controls - {Storage.ModuleName}";

            InitializeBluetooth();
        }

        async void InitializeBluetooth()
        {
            if (!_bluetooth.Enabled)
            {
                _bluetooth.Enable();
            }

            var connect = await _bluetooth.Connect(_bluetooth.Device);

            Console.WriteLine($"Device is connected: {connect}");
        }
    }
}
