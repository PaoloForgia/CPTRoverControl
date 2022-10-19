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
        public bool EmergencyStop { get; set; }

        public ControlsPage()
        {
            InitializeComponent();

            _bluetooth = Bluetooth.Instance;
            _buzzerAction = new BuzzerAction();

            EmergencyStop = false;
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

        void OnFrontLightToggle(object sender, ToggledEventArgs args)
        {
            _bluetooth.Send(Commands.LightFront(args.Value));
        }

        void OnBackLightToggle(object sender, ToggledEventArgs args)
        {
            _bluetooth.Send(Commands.LightBack(args.Value));
        }

        void OnEmergencyStopClick(object sender, EventArgs args)
        {
            EmergencyStop = !EmergencyStop;
            _bluetooth.Send(Commands.EmergencyStop(EmergencyStop));
        }

        void OnBuzzerPressed(object sender, EventArgs args)
        {
            _buzzerAction.Start();
        }

        void OnBuzzerReleased(object sender, EventArgs args)
        {
            _buzzerAction.Stop();
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
    }
}