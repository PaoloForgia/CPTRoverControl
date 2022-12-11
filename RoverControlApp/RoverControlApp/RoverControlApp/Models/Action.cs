using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    public class Action
    {
        [PrimaryKey, AutoIncrement]

        public int ActionId { get; set; }
        public int ActionsGroupId{ get; set; }
        public string Command { get; set; }
        public ActionsGroup ActionsGroup { get; set; }
    }
}
