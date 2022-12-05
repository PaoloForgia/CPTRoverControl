using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    public class RoverProgramsDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<RoverProgramsDatabase> Instance = new AsyncLazy<RoverProgramsDatabase>(async () =>
        {
            var instance = new RoverProgramsDatabase();
            // Create tables
            await Database.CreateTableAsync<Program>();
            await Database.CreateTableAsync<ActionsGroup>();
            await Database.CreateTableAsync<Action>();
            return instance;
        });

        public RoverProgramsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
    }
}
