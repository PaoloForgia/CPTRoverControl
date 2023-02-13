using RoverControlApp.Services;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            DeviceNameInput.Text = Storage.ModuleName;
            DeadZoneInput.Text = Storage.DeadZone.ToString();
        }

        void OnDeviceNameChange(object sender, TextChangedEventArgs args)
        {
            string value = args.NewTextValue;

            Storage.ModuleName = value;
        }

        void OnDeadZoneChange(object sender, TextChangedEventArgs args)
        {
            string value = args.NewTextValue;

            Storage.DeadZone = value == "" ? 0 : Int32.Parse(value);
        }
    }
}