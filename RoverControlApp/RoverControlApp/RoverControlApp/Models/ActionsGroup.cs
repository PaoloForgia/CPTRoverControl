using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    /// <summary>
    /// Database entity ActionsGroup
    /// </summary>
    public class ActionsGroup
    {
        [PrimaryKey, AutoIncrement]

        public int ActionsGroupId { get; set; }
        [Indexed]
        public int ProgramId { get; set; }
        public int Duration { get; set; }
        public int Index { get; set; }
    }
}
