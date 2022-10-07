using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlsPage : ContentPage
    {

        public ControlsPage()
        {
            InitializeComponent();

            leftSlider.Value = 128;
            rightSlider.Value = 128;
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

        async void OnBellClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Bell");
            var bluetooth = new Bluetooth();
            Console.WriteLine(bluetooth.Device.Name);

            var connect = await bluetooth.Connect(bluetooth.Device);

            if (connect)
            {
                var commands = new Commands();

                var buzzer = commands.Buzzer(1);
                bluetooth.Send(buzzer);
            }
        }
    }
}