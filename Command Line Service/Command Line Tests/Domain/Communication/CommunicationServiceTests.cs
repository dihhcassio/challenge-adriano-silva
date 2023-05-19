using Command_Line_Domain.CommandLine.Services;
using Command_Line_Domain.Communication.Acls.CommunicationApi;
using Command_Line_Domain.Communication.Dtos;
using Command_Line_Domain.Communication.Exceptions;
using Command_Line_Domain.Communication.Services;
using Command_Line_Domain.WindowsInformations.Models;
using Command_Line_Domain.WindowsInformations.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Command_Line_Tests.Domain.Communication
{
    public class CommunicationServiceTests
    {
        Mock<ILogger<CommunicationService>> _mockLogger;
        Mock<ICommunicationApiAcl> _mockCommunicationApiAcl;
        Mock<ICommandLineService> _mockCommandLineService;
        Mock<IWindowsInformationService> _mockWindowsInformationService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<CommunicationService>>();
            _mockCommunicationApiAcl = new Mock<ICommunicationApiAcl>();
            _mockCommandLineService = new Mock<ICommandLineService>();
            _mockWindowsInformationService = new Mock<IWindowsInformationService>();
        }

        [Test]
        public void ExecuteCommands_ShouldBeSuccessful()
        {
            _mockWindowsInformationService.Setup(s => s.LoadWindowsInformations()).Returns(new WindowsInformation());

            _mockCommunicationApiAcl.Setup(api => api.GetCommands(It.IsAny<string>())).Returns(async () => {
                return await Task.FromResult(new List<CommandDto>()
                    {
                        new CommandDto() {Id = 1, Content = "dir"},
                        new CommandDto() {Id = 2, Content = "cd"}
                    });
            });

            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.DoesNotThrowAsync( async () => await service.ExecuteCommands());
        }

        [Test]
        public void ExecuteCommands_ThrowCommandListNullException()
        {
            _mockWindowsInformationService.Setup(s => s.LoadWindowsInformations()).Returns(new WindowsInformation());

            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.ThrowsAsync<CommandListNullException>(async () => await service.ExecuteCommands());
        }

        [Test]
        public void ExecuteCommands_ThrowWindowsInformationsNullException()
        {
            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.ThrowsAsync<WindowsInformationsNullException>(async () => await service.ExecuteCommands());
        }

        [Test]
        public void RegisterCommandLineService_ThrowWindowsInformationsNullException()
        {
            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                  _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.ThrowsAsync<WindowsInformationsNullException>(async () => await service.RegisterCommandLineService());
        }

        [Test]
        public void RegisterCommandLineService_ShouldBeSuccessful()
        {
            _mockWindowsInformationService.Setup(s => s.LoadWindowsInformations()).Returns(new WindowsInformation());

            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                  _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.DoesNotThrowAsync(async () => await service.RegisterCommandLineService());
        }

        [Test]
        public void NotifyOnline_ShouldBeSuccessful()
        {
            _mockWindowsInformationService.Setup(s => s.LoadWindowsInformations()).Returns(new WindowsInformation());

            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                  _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.DoesNotThrowAsync(async () => await service.NotifyOnline());
        }

        [Test]
        public void NotifyOnline_ThrowWindowsInformationsNullException()
        {
            _mockWindowsInformationService.Setup(s => s.LoadWindowsInformations()).Returns(() => null);

            var service = new CommunicationService(_mockLogger.Object, _mockCommunicationApiAcl.Object,
                  _mockCommandLineService.Object, _mockWindowsInformationService.Object);

            Assert.ThrowsAsync<WindowsInformationsNullException>(async () => await service.NotifyOnline());
        }
    }
}
