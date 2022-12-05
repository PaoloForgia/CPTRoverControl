using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models
{
    public class Program
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string LastChangeDate { get; set; }
        public ICollection<ActionsGroup> ActionsGroups { get; set; }
    }
}
