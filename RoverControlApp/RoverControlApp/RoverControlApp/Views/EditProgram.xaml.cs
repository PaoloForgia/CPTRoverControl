using NuGet.Common;
using RoverControlApp.Models;
using RoverControlApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RoverControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProgram : ContentPage
    {
        public Program Program { get; private set; }
        public List<ActionsGroup> ActionsGroups { get; private set; }
        public Dictionary<int, List<Models.Action>> Actions { get; private set; }
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

            ActionsGroups = await Connection.Table<ActionsGroup>()
                .Where(actionsGroup => actionsGroup.ProgramId == Program.ProgramId)
                .OrderBy(group => group.Index)
                .ToListAsync();

            var actionsGroupIds = ActionsGroups.Select(a => a.ActionsGroupId);

            var actionList = await Connection.Table<Models.Action>()
                .Where(action => actionsGroupIds.Contains(action.ActionsGroupId))
                .ToListAsync();

            Actions = actionList.GroupBy(a => a.ActionsGroupId).ToDictionary(a => a.Key, a => a.ToList());

            CreatePage();
        }

        private void CreatePage()
        {
            ActionsGroups.ForEach(actionsGroup => actionsGroupsContainer.Children.Add(ToActionsGroupFrame(actionsGroup)));
        }

        private Frame ToActionsGroupFrame(ActionsGroup actionsGroup)
        {
            StackLayout root = new StackLayout { Orientation = StackOrientation.Vertical };

            StackLayout actionGroupHeader = new StackLayout { Orientation = StackOrientation.Horizontal };
            root.Children.Add(actionGroupHeader);
            actionGroupHeader.Children.Add(new Label { Text = $"Action group {actionsGroup.ActionsGroupId}" });

            StackLayout actionGroupSettings = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.EndAndExpand };
            actionGroupHeader.Children.Add(actionGroupSettings);
            actionGroupSettings.Children.Add(new Entry { Keyboard = Keyboard.Numeric, Text = actionsGroup.Duration.ToString() });
            actionGroupSettings.Children.Add(new Label { Text = "ms" });

            // Actions
            StackLayout actionsContainer = new StackLayout { Orientation = StackOrientation.Vertical };
            root.Children.Add(actionsContainer);

            Actions[actionsGroup.ActionsGroupId].ForEach(action => actionsContainer.Children.Add(ToActionFrame(action)));

            return new Frame
            {
                Content = root
            };
        }

        private Frame ToActionFrame(Models.Action action)
        {
            return new Frame
            {
                Content = new Entry { Text = action.Command }
            };
        }
    }
}