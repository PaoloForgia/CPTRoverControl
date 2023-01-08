using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    /// <summary>
    /// Database entity Action
    /// </summary>
    public class Action
    {
        [PrimaryKey, AutoIncrement]

        public int ActionId { get; set; }
        [Indexed]
        public int ActionsGroupId{ get; set; }
        public string Command { get; set; }
    }
}
