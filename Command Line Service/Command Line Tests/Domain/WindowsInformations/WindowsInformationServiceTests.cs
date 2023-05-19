using Command_Line_Domain.WindowsInformations.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Tests.Domain.WindowsInformations
{
    public class WindowsInformationServiceTests
    {
        Mock<ILogger<WindowsInformationService>> _mockLogger;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<WindowsInformationService>>();
        }

        [Test]
        public void LoadWindowsInformations_ShouldBeSuccessful() 
        {
            var service = new WindowsInformationService(_mockLogger.Object);

            var windowsInformations = service.LoadWindowsInformations();

            Assert.NotNull(windowsInformations.AntivirusList);
            Assert.NotNull(windowsInformations.DotNetVersion);
            Assert.NotNull(windowsInformations.HardDrives);
            Assert.NotNull(windowsInformations.HostName);
            Assert.NotNull(windowsInformations.IpAddress);
            Assert.NotNull(windowsInformations.IsFirewallActive);
            Assert.NotNull(windowsInformations.OsVersion);
        }
    }
}
