using NUnit.Framework;
using RoverControlApp.Models;
using RoverControlApp.Services;

namespace Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Buzzer_IsOn()
        {
            var result = Commands.Buzzer(true);

            Assert.That(result, Is.EqualTo("B1\n"));
        }

        [Test]
        public void Buzzer_IsOff()
        {
            var result = Commands.Buzzer(false);

            Assert.That(result, Is.EqualTo("B0\n"));
        }

        [Test]
        public void LightFront_IsOn()
        {
            var result = Commands.LightFront(true);

            Assert.That(result, Is.EqualTo("F1\n"));
        }

        [Test]
        public void LightFront_IsOff()
        {
            var result = Commands.LightFront(false);

            Assert.That(result, Is.EqualTo("F0\n"));
        }

        [Test]
        public void LightBack_IsOn()
        {
            var result = Commands.LightBack(true);

            Assert.That(result, Is.EqualTo("P1\n"));
        }

        [Test]
        public void LightBack_IsOff()
        {
            var result = Commands.LightBack(false);

            Assert.That(result, Is.EqualTo("P0\n"));
        }

        [Test]
        public void EmergencyStop_IsOn()
        {
            var result = Commands.EmergencyStop(true);

            Assert.That(result, Is.EqualTo("S1\n"));
        }

        [Test]
        public void EmergencyStop_IsOff()
        {
            var result = Commands.EmergencyStop(false);

            Assert.That(result, Is.EqualTo("S0\n"));
        }

        [TestCase(0)]
        [TestCase(32)]
        [TestCase(128)]
        [TestCase(196)]
        [TestCase(255)]
        public void EngineLeft_ValidValues(int value)
        {
            var result = Commands.EngineLeft(value);

            Assert.That(result, Is.EqualTo($"L{value}\n"));
        }

        [TestCase(-1)]
        [TestCase(-128)]
        [TestCase(256)]
        public void EngineLeft_InvalidValues(int value)
        {
            var result = Commands.EngineLeft(value);

            Assert.That(result, Is.Null);
        }

        [TestCase(0)]
        [TestCase(32)]
        [TestCase(128)]
        [TestCase(196)]
        [TestCase(255)]
        public void EngineRight_ValidValues(int value)
        {
            var result = Commands.EngineRight(value);

            Assert.That(result, Is.EqualTo($"R{value}\n"));
        }

        [TestCase(-1)]
        [TestCase(-128)]
        [TestCase(256)]
        public void EngineRight_InvalidValues(int value)
        {
            var result = Commands.EngineRight(value);

            Assert.That(result, Is.Null);
        }

        [TestCase("L0\n")]
        [TestCase("L128\n")]
        [TestCase("L255\n")]
        [TestCase("R0\n")]
        [TestCase("R128\n")]
        [TestCase("R255\n")]
        [TestCase("B0\n")]
        [TestCase("B1\n")]
        [TestCase("F0\n")]
        [TestCase("F1\n")]
        [TestCase("P0\n")]
        [TestCase("P1\n")]
        [TestCase("S0\n")]
        [TestCase("S1\n")]
        [TestCase("T0\n")]
        [TestCase("T50\n")]
        [TestCase("T100\n")]
        [TestCase("D0\n")]
        [TestCase("D50\n")]
        [TestCase("D100\n")]
        public void IsValidData_IsTrue(string data)
        {
            Assert.That(Commands.IsValid(data), Is.True);
        }


        [TestCase("L0")]
        [TestCase("L-1\n")]
        [TestCase("L256\n")]
        [TestCase("L\n")]
        [TestCase("R-1\n")]
        [TestCase("R256\n")]
        [TestCase("R\n")]
        [TestCase("B-1\n")]
        [TestCase("B2\n")]
        [TestCase("B\n")]
        [TestCase("F-1\n")]
        [TestCase("F2\n")]
        [TestCase("F\n")]
        [TestCase("P-1\n")]
        [TestCase("P2\n")]
        [TestCase("P\n")]
        [TestCase("S-1\n")]
        [TestCase("S2\n")]
        [TestCase("S\n")]
        [TestCase("T-1\n")]
        [TestCase("T101\n")]
        [TestCase("T\n")]
        [TestCase("D-1\n")]
        [TestCase("D101\n")]
        [TestCase("D\n")]
        public void IsValidData_IsFalse(string data)
        {
            Assert.That(Commands.IsValid(data), Is.False);
        }

        [TestCase("T0\n", 0)]
        [TestCase("T50\n", 50)]
        [TestCase("T100\n", 100)]
        public void Translate_IsBattery(string command, int value)
        {
            var data = Commands.Translate(command);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.IsBattery, Is.True);
            Assert.That(data.Value, Is.EqualTo(value));
        }

        [TestCase("D0\n", 0)]
        [TestCase("D50\n", 50)]
        [TestCase("D100\n", 100)]
        public void Translate_IsDistance(string command, int value)
        {
            var data = Commands.Translate(command);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.IsDistance, Is.True);
            Assert.That(data.Value, Is.EqualTo(value));
        }

        [TestCase("T\n")]
        [TestCase("T-1\n")]
        [TestCase("T101\n")]
        [TestCase("D\n")]
        [TestCase("D-1\n")]
        [TestCase("D101\n")]
        [TestCase("A0\n")]
        public void Translate_IsInvalidData(string command)
        {
            var data = Commands.Translate(command);

            Assert.That(data, Is.Null);
        }
    }
}