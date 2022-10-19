using RoverControlApp.Models;
using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
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
        private readonly LeftEngineAction _leftEngineAction;
        private readonly RightEngineAction _rightEngineAction;

        public bool EmergencyStop { get; set; }

        public ControlsPage()
        {
            InitializeComponent();

            _bluetooth = Bluetooth.Instance;
            _buzzerAction = new BuzzerAction();
            _leftEngineAction = new LeftEngineAction();
            _rightEngineAction = new RightEngineAction();

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

        protected override void OnDisappearing()
        {
            if (_leftEngineAction.IsActive) _leftEngineAction.Stop();
            if (_rightEngineAction.IsActive) _rightEngineAction.Stop();
            _bluetooth.Disconnect();
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
            if (_leftEngineAction == null) return;

            // Stop the previous
            if (_leftEngineAction.IsActive) _leftEngineAction.Stop();

            int value = (int) args.NewValue;

            _leftEngineAction.Start(value);
        }

        void OnRightSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (_rightEngineAction == null) return;

            // Stop the previous
            if (_rightEngineAction.IsActive) _rightEngineAction.Stop();

            int value = (int) args.NewValue;

            _rightEngineAction.Start(value);
        }
    }
}