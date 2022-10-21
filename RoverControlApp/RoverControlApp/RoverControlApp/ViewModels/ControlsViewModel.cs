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

       public new event PropertyChangedEventHandler PropertyChanged;

        string _battery;
        string _distance;
        public ControlsViewModel()
        {
            Title = "Rover Controls";
        }

        public string Battery
        {
            get
            {
                return _battery;
            }
            set
            {
                if (_battery != value)
                {
                    _battery = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Battery"));
                }
            }
        }

        public string Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                if (_distance != value)
                {
                    _distance = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Distance"));
                }
            }
        }

        public void OnReceiveEvent(object sender, RecivedEventArgs args)
        {
            string received = Encoding.UTF8.GetString(args.Buffer.ToArray());
            Console.WriteLine("Received: " + received);

            if (Commands.IsMultipleValue(received))
            {
                var commands = Commands.ToCommandArray(received);
                commands.ToList()
                    .FindAll(command => Commands.IsValid(command))
                    .ConvertAll(command => Commands.Translate(command))
                    .ForEach(data => UpdateLabels(data));
            }
            else
            {
                var data = Commands.Translate(received);
                UpdateLabels(data);
            }
        }

        private void UpdateLabels(Data data)
        {
            if (data.IsBattery)
            {
                Battery = $"{data.Value}%";
            }
            else if (data.IsDistance)
            {
                Distance = $"{data.Value} cm";
            }
        }
    }
}
