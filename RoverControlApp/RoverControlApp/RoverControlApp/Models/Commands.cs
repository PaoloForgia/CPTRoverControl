using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RoverControlApp.Models
{
    public class Commands
    {
        public string EngineLeft(int value)
        {
            if (!IsSpeedValid(value)) return null;

            return ToCommand("L", value);
        }

        public string EngineRight(int value)
        {
            if (!IsSpeedValid(value)) return null;

            return ToCommand("R", value);
        }

        public string Buzzer(int value)
        {
            if (!IsBoolValid(value)) return null;

            return ToCommand("B", value);

        }

        public string LightFront(int value)
        {
            if (!IsBoolValid(value)) return null;

            return ToCommand("F", value);

        }

        public string LightBack(int value)
        {
            if (!IsBoolValid(value)) return null;

            return ToCommand("P", value);

        }

        public string EmergencyStop(int value)
        {
            if (!IsBoolValid(value)) return null;

            return ToCommand("S", value);

        }

        public Data Translate(string value)
        {
            if (value == null || !IsValidData(value)) return null;

            var type = value[0];
            if (type.Equals("T")) return new Data() { Name = "Batteria", Value = GetValue(value) };
            else if (type.Equals("D")) return new Data() { Name = "Distanza", Value = GetValue(value) };
            else return null;
        }

        public bool IsMultipleValue(string value) => value.Split('\n').Length > 1;

        private int? GetValue(string value)
        {
            var numericValue = value.Substring(1, 3);
            return int.Parse(numericValue);
        }

        public static string ToCommand(string command, int value) => $"{command}{value}\n";

        public static bool IsSpeedValid(int value) => value < 0 || value > 255;

        public static bool IsBoolValid(int value) => value == 0 || value == 1;

        // Letter followed than 1 to 3 numbers
        public static bool IsValidData(string value) => Regex.IsMatch(value, @"[A-Z]+[0-9]{1,3}\n");
    }
}
