using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace RoverControlApp.Models
{
    internal class Constants
    {
        private const string DatabaseFilename = "CPTRover.db3";

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
