using RoverControlApp.Models;
using RoverControlApp.Services;
using SQLite;
using SQLitePCL;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var instance = await Database.Instance();
            var connection = instance.Connection;

           await connection.InsertAsync(new Program()
            {
                Name = "Demo",
                LastChangeDate = DateTime.Now,
            });

            var programs = await connection.Table<Program>().ToListAsync();

            programsListView.ItemsSource = programs;
        }
    }
}