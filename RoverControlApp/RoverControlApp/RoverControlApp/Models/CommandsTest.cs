using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RoverControlApp.Models
{
    [TestFixture]
    public class CommandsTest
    {
        [Test]
        [TestCase("D100\n")]
        [TestCase("D0\n")]
        [TestCase("T10\n")]
        [TestCase("T99\n")]
        [TestCase("T1\n")]
        public void IsValidDataTest_with_valid_data(string value)
        {
            var result = Commands.IsValidData(value);
            Assert.IsTrue(result);
        }
    }
}
