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
        private bool emergencyStop;

        public bool DisableComponent => Bluetooth.Instance.Connected;

        public bool EmergencyStop 
        { 
            get { return emergencyStop; } 
            set {
                emergencyStop = value; 
                EmergencyStopButton.BackgroundColor = DynamicColors.EmergencyStopColor(emergencyStop); 
            } 
        }

        public ControlsPage()
        {
            InitializeComponent();
            BindingContext = new ControlsViewModel();

            buzzerAction = new BuzzerAction();
            leftEngineAction = new LeftEngineAction();
            rightEngineAction = new RightEngineAction();
        }

        protected override void OnAppearing()
        {
            Connect();
            SetDefaultControls();
        }

        protected override void OnDisappearing()
        {
            // To prevent losing control
            SetDefaultControls();

            if (leftEngineAction.IsActive) leftEngineAction.Stop();
            if (rightEngineAction.IsActive) rightEngineAction.Stop();
            Bluetooth.Instance.Disconnect();
        }

        void Connect()
        {
            var bluetooth = Bluetooth.Instance;
            if (!bluetooth.Enabled) bluetooth.Enable();

            // Refreshing since the name could have changed
            bluetooth.RefreshDevice();

            var device = bluetooth.Device;
            if (device != null) bluetooth.Connect(device);
            else Popup.DisplayAlert(
                "Dispositivo non trovato.",
                $"'{Storage.ModuleName}' non trovato tra i dispositivi collegati.\n\nControlla le impostazioni Bluetooth del telefono.");
        }

        void SetDefaultControls()
        {
            // Default state: everything off
            // UI
            EmergencyStop = true;
            frontLightSwitch.IsToggled = false;
            backLightSwitch.IsToggled = false;
            leftSlider.Value = DefaultValues.ENGINE_STOP_VALUE;
            rightSlider.Value = DefaultValues.ENGINE_STOP_VALUE;
            // Send commands
            Bluetooth.Instance.Send(Commands.EmergencyStop(true));
            Bluetooth.Instance.Send(Commands.LightFront(false));
            Bluetooth.Instance.Send(Commands.LightBack(false));
            Bluetooth.Instance.Send(Commands.EngineLeft(DefaultValues.ENGINE_STOP_VALUE));
            Bluetooth.Instance.Send(Commands.EngineRight(DefaultValues.ENGINE_STOP_VALUE));
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