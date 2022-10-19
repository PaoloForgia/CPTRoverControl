using RoverControlApp.Models;
using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlsPage : ContentPage
    {
        private readonly Bluetooth _bluetooth;
        private readonly BuzzerAction _buzzerAction;

        public ControlsPage()
        {
            InitializeComponent();

            _bluetooth = Bluetooth.Instance;
            _buzzerAction = new BuzzerAction();

            leftSlider.Value = 128;
            rightSlider.Value = 128;
        }

        protected async override void OnAppearing()
        {
            if (!_bluetooth.Enabled)
            {
                _bluetooth.Enable();
            }

            _bluetooth.RefreshDevice();

            var device = _bluetooth.Device;
            if (device != null)
            {
                var connect = await _bluetooth.Connect(device);

                Console.WriteLine($"Device is connected: {connect}");
            } else
            {
                Console.WriteLine("Device not found");
            }
        }

        void OnLeftSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            // Console.WriteLine("Left Slider " + value);
        }

        void OnRightSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            // Console.WriteLine("Right Slider " + value);
        }

        void TurnOnBuzzer(object sender, EventArgs args)
        {
            _buzzerAction.Start();
        }

        void TurnOffBuzzer(object sender, EventArgs args)
        {
            _buzzerAction.Stop();
        }
    }
}