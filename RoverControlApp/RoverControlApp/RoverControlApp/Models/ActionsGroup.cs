using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    public class ActionsGroup
    {
        public int ActionsGroupId { get; set; }
        public int ProgramId { get; set; }
        public int Duration { get; set; }
        public int Index { get; set; }
        public Program program;
        public ICollection<Action> Actions { get; set; }
    }
}
