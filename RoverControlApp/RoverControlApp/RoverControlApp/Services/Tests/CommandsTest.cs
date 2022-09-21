using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoverControlApp.Services.Tests
{
    [TestClass]
    public class CommandsTest
    {
        [TestMethod]
        [DataRow("D100\n")]
        /* [DataRow("D100\n")]
        [DataRow("D0\n")]
        [DataRow("T10\n")]
        [DataRow("T99\n")]
        [DataRow("T1\n")] */
        public void IsValidDataTest_with_valid_data(string value)
        {
            var result = Commands.IsValidData(value);
            Assert.IsTrue(result);
        }
    }
}
