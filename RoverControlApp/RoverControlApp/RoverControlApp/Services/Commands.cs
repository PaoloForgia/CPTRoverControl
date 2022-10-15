using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RoverControlApp.Models;

namespace RoverControlApp.Services
{
    public class Commands
    {
        public static string EngineLeft(int value)
        {
            if (!IsSpeedValid(value)) return null;

            return ToCommand("L", value);
        }

        public static string EngineRight(int value)
        {
            if (!IsSpeedValid(value)) return null;

            return ToCommand("R", value);
        }

        public static string Buzzer(bool value)
        {
            return ToCommand("B", ToNumber(value));
        }

        public static string LightFront(bool value)
        {
            return ToCommand("F", ToNumber(value));
        }

        public static string LightBack(bool value)
        {
            return ToCommand("P", ToNumber(value));
        }

        public static string EmergencyStop(bool value)
        {
            return ToCommand("S", ToNumber(value));
        }

        public static Data Translate(string value)
        {
            if (!IsValidData(value)) return null;

            var type = value[0];
            if (type.Equals('T')) return new Data() { Name = "Batteria", Value = GetValue(value) };
            else if (type.Equals('D')) return new Data() { Name = "Distanza", Value = GetValue(value) };
            else return null;
        }

        public static bool IsMultipleValue(string value) => value.Split('\n').Length > 1;

        private static int? GetValue(string value)
        {
            var numericValue = value.Substring(1, value.Length - 2); // Last two letters are \n
            return int.Parse(numericValue);
        }

        public static string ToCommand(string command, int value) => $"{command}{value}\n";

        public static bool IsSpeedValid(int value) => value >= 0 && value <= 255;

        public static bool IsValidData(string value)
        {
            if (value == null) return false;

            // Uppercase letter followed by 1 to 3 numbers and ends with \n
            if (!Regex.IsMatch(value, "[A-Z][0-9]{1,3}\\n")) return false;

            var command = value[0];
            var number = Int32.Parse(value.Substring(1, value.Length - 2));

            return number >= 0 && number <= GetMax(command);
        }

        private static int GetMax(char command)
        {
            switch (command)
            {
                case 'L': // Left engine
                case 'R': // Right engine
                    return 255;
                case 'B': // Buzzer
                case 'F': // Front light
                case 'P': // Back light
                case 'S': // Emergency stop
                    return 1;
                case 'T': // Battery percentage
                case 'D': // Distance in cm from obstacle
                    return 100;
                default:
                    return -1;
            }
        }

        private static int ToNumber(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
