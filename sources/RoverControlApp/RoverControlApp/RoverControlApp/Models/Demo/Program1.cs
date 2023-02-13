using System;
using System.Collections.Generic;
using System.Text;

namespace RoverControlApp.Models.Demo
{
    /// <summary>
    /// Demo program database entities
    /// </summary>
    public class Program1
    {
       
        public static Program GetProgramEntity() => new Program() { Name = "Program 1", LastChangeDate = DateTime.Now };

        public static ActionsGroup GetActionGroup1Entity(int programId) => new ActionsGroup() { ProgramId = programId, Index = 1, Duration = 1000 };

        public static ActionsGroup GetActionGroup2Entity(int programId) => new ActionsGroup() { ProgramId = programId, Index = 2, Duration = 500 };

        public static List<Action> GetActions1Entity(int actionGroupId) => new List<Action>
            {
                new Action() { ActionsGroupId = actionGroupId, Command = "S0\n" },
                new Action() { ActionsGroupId = actionGroupId, Command = "L255\n" },
                new Action() { ActionsGroupId = actionGroupId, Command = "R255\n" }
            };

        public static List<Action> GetActions2Entity(int actionGroupId) => new List<Action>
            {
                new Action() { ActionsGroupId = actionGroupId, Command = "B1\n" },
                new Action() { ActionsGroupId = actionGroupId, Command = "F1\n" },
                new Action() { ActionsGroupId = actionGroupId, Command = "P1\n" }
            };
    }
}
