using Plugin.BluetoothClassic.Abstractions;
using RoverControlApp.Models;
using RoverControlApp.Services;
using RoverControlApp.Utils;
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
        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
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
        private string state;
        private string battery;
        private string distance;
        public ControlsViewModel()
        {
            Title = "Rover Controls";
            Bluetooth.Instance.OnReceiveEvent = OnReceiveEvent;
            Bluetooth.Instance.OnStateChangedEvent = OnStateChangedEvent;
        }

        private void OnReceiveEvent(object sender, RecivedEventArgs args)
        {
            string received = Encoding.UTF8.GetString(args.Buffer.ToArray());

            Commands.ToCommandList(received)
                .FindAll(command => Commands.IsValid(command))
                .ConvertAll(command => Commands.Translate(command))
                .ForEach(data => UpdateLabels(data));
        }

        private void OnStateChangedEvent(object sender, StateChangedEventArgs args)
        {
            State = args.ConnectionState.ToString();
            Console.WriteLine(args.ConnectionState);
        }

        private void UpdateLabels(Data data)
        {
            if (data.IsBattery)  Battery = $"{data.Value}%";
            else if (data.IsDistance)  Distance = $"{data.Value} cm";
        }
    }
}
