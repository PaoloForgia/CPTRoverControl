using Plugin.BluetoothClassic.Abstractions;
using RoverControlApp.Models;
using RoverControlApp.Services;
using RoverControlApp.Utils;
using RoverControlApp.ViewModels;
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
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlsPage : ContentPage
    {
        private readonly BuzzerAction buzzerAction;
        private readonly LeftEngineAction leftEngineAction;
        private readonly RightEngineAction rightEngineAction;

        public bool DisableComponent => Bluetooth.Instance.Connected;

        public bool EmergencyStop { get; set; }

        public ControlsPage()
        {
            InitializeComponent();
            BindingContext = new ControlsViewModel();

            buzzerAction = new BuzzerAction();
            leftEngineAction = new LeftEngineAction();
            rightEngineAction = new RightEngineAction();

            // TODO: by default EmergencyStop = true; lights = off
            // -> run commands

            EmergencyStop = false;
            leftSlider.Value = DefaultValues.ENGINE_STOP_VALUE;
            rightSlider.Value = DefaultValues.ENGINE_STOP_VALUE;
        }

        protected async override void OnAppearing()
        {
            // TODO: by default EmergencyStop = true; lights = off
            // -> run commands

            var bluetooth = Bluetooth.Instance;
            if (!bluetooth.Enabled) bluetooth.Enable();

            // Refreshing since the name could have changed
            bluetooth.RefreshDevice();

            var device = bluetooth.Device;
            if (device != null)
            {
                var connect = await bluetooth.Connect(device);

                Console.WriteLine($"Device is connected: {connect}");
            } else
            {
                Console.WriteLine("Device not found");
            }
        }

        protected override void OnDisappearing()
        {
            // TODO: OnDisappearing -> EmergencyStop = true

            if (leftEngineAction.IsActive) leftEngineAction.Stop();
            if (rightEngineAction.IsActive) rightEngineAction.Stop();
            Bluetooth.Instance.Disconnect();
        }

        void OnFrontLightToggle(object sender, ToggledEventArgs args)
        {
            Bluetooth.Instance.Send(Commands.LightFront(args.Value));
        }

        void OnBackLightToggle(object sender, ToggledEventArgs args)
        {
            Bluetooth.Instance.Send(Commands.LightBack(args.Value));
        }

        void OnEmergencyStopClick(object sender, EventArgs args)
        {
            EmergencyStop = !EmergencyStop;
            Bluetooth.Instance.Send(Commands.EmergencyStop(EmergencyStop));

            EmergencyStopButton.BackgroundColor = DynamicColors.EmergencyStopColor(EmergencyStop);
        }

        void OnBuzzerPressed(object sender, EventArgs args)
        {
            buzzerAction.Start();
        }

        void OnBuzzerReleased(object sender, EventArgs args)
        {
            buzzerAction.Stop();
        }

        void OnLeftSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (leftEngineAction == null) return;

            // Stop the previous
            if (leftEngineAction.IsActive) leftEngineAction.Stop();

            int value = (int) args.NewValue;

            leftEngineAction.Start(value);

            var color = DynamicColors.EngineColor(Engine.IsMoving(value));
            leftSlider.MinimumTrackColor = color;
            leftSlider.ThumbColor = color;
        }

        void OnRightSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (rightEngineAction == null) return;

            // Stop the previous
            if (rightEngineAction.IsActive) rightEngineAction.Stop();

            int value = (int) args.NewValue;

            rightEngineAction.Start(value);

            var color = DynamicColors.EngineColor(Engine.IsMoving(value));
            rightSlider.MinimumTrackColor = color;
            rightSlider.ThumbColor = color;
        }
    }
}