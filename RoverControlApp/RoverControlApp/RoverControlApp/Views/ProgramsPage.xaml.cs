using RoverControlApp.Models;
using RoverControlApp.Models.Demo;
using RoverControlApp.Services;
using SQLite;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
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

            if (Storage.GenerateDemoProgram)
            {
                // First time startup, generate some demo Program
                CreateDemoProgram(connection);
                Storage.GenerateDemoProgram = false;
            }

            var programs = await connection.Table<Program>().OrderByDescending(program => program.LastChangeDate).ToListAsync();

            programsListView.ItemsSource = programs;
        }

        public async void OnProgramSelect(object sender, ItemTappedEventArgs e)
        {
            var program = (Program)e.Item;

            await Navigation.PushAsync(new EditProgram(program));
        }

        private async void CreateDemoProgram(SQLiteAsyncConnection connection)
        {
            var program = Program1.GetProgramEntity();
            await connection.InsertAsync(program);

            // Action Group 1
            var actionGroup1 = Program1.GetActionGroup1Entity(program.ProgramId);
            await connection.InsertAsync(actionGroup1);

            var actions1 = Program1.GetActions1Entity(actionGroup1.ActionsGroupId);
            await connection.InsertAllAsync(actions1);

            // Action Group 2
            var actionGroup2 = Program1.GetActionGroup2Entity(program.ProgramId);
            await connection.InsertAsync(actionGroup2);

            var actions2 = Program1.GetActions2Entity(actionGroup2.ActionsGroupId);
            await connection.InsertAllAsync(actions2);
        }
    }
}