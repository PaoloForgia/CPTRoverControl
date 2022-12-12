using RoverControlApp.Models;
using RoverControlApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProgram : ContentPage
    {
        public Program Program { get; private set; }
        private SQLiteAsyncConnection Connection;
        public EditProgram(Program program)
        {
            InitializeComponent();

            Program = program;
            Title = Program.Name;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var instance = await Database.Instance();
            Connection = instance.Connection;

            var actionsGroups = await Connection.Table<ActionsGroup>().OrderBy(group => group.Index).ToListAsync();
        }
    }
}