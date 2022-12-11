using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace RoverControlApp.Models
{
    public class Database
    {
        public SQLiteAsyncConnection Connection { get; private set; }

        private Database()
        {
            Connection = new SQLiteAsyncConnection(Constants.DatabasePath);
        }

        public static async Task<Database> Instance()
        {
            var instance = new Database();
            var connection = instance.Connection;

            // Create tables
            await connection.CreateTableAsync<Program>();
            await connection.CreateTableAsync<ActionsGroup>();
            await connection.CreateTableAsync<Action>();

            return instance;
        }
    }
}
