using RoverControlApp.Models;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverControlApp.ViewModels
{
    public class EditProgramViewModel : BaseViewModel
    {
        public Program Program { get; private set; }

        public EditProgramViewModel()
        {
        }
    }
}