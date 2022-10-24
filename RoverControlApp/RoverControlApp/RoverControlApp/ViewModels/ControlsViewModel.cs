using Plugin.BluetoothClassic.Abstractions;
using RoverControlApp.Models;
using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverControlApp.ViewModels
{
    public class ControlsViewModel: BaseViewModel
    {
        private string battery;
        private string distance;
        public ControlsViewModel()
        {
            Title = "Rover Controls";
            Bluetooth.Instance.OnReceiveEvent = OnReceiveEvent;
        }
        public string Battery
        {
            get { return battery; }
            set { SetProperty(ref battery, value); }
        }

        public string Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        private void OnReceiveEvent(object sender, RecivedEventArgs args)
        {
            string received = Encoding.UTF8.GetString(args.Buffer.ToArray());
            Console.WriteLine("Received: " + received);

            Commands.ToCommandList(received)
                .FindAll(command => Commands.IsValid(command))
                .ConvertAll(command => Commands.Translate(command))
                .ForEach(data => UpdateLabels(data));
        }

        private void UpdateLabels(Data data)
        {
            if (data.IsBattery)  Battery = $"{data.Value}%";
            else if (data.IsDistance)  Distance = $"{data.Value} cm";
        }
    }
}
