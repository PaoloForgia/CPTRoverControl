using RoverControlApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        void TurnOnBuzzer(object sender, EventArgs args)
        {
            while (true)
            {
                if (BellButton.IsPressed)
                {
                    Console.WriteLine("Pressed");
                } else
                {
                    Console.WriteLine("Pressed");
                    break;
                }
            } 
        }
    }
}