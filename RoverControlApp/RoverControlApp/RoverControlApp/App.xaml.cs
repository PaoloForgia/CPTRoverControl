using RoverControlApp.Services;
using RoverControlApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Send emergency stop and disconnect when the application is not in use
            var bluetooth = Bluetooth.Instance;
            if (bluetooth.Connected)
            {
                bluetooth.Send(Commands.EmergencyStop(true));
                bluetooth.Disconnect();
            }
        }

        protected override void OnResume()
        {
        }
    }
}
