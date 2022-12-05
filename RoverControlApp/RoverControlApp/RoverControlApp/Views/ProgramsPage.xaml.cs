using RoverControlApp.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    public partial class ProgramsPage : ContentPage
    {
        public ProgramsPage()
        {
            InitializeComponent();

            BindingContext = new[] { "A", "B", "C" };
        }

       
    }
}