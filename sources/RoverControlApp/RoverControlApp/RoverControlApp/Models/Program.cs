using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    /// <summary>
    /// Database entity Program
    /// </summary>
    public class Program
    {
        [PrimaryKey, AutoIncrement]
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public DateTime LastChangeDate { get; set; }
    }
}
