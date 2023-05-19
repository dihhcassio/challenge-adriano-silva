using Command_Line_Api_Domain.CommandLine.Exceptions;
using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using Command_Line_Api_Domain.CommandLine.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Tests.CommandLine.Services
{
    public class ClientServiceTest
    {
        private readonly Mock<ILogger<ClientService>> _mockLogger;
        private readonly Mock<IClientRepository> _mockClientRepository;

        public ClientServiceTest() 
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockLogger = new Mock<ILogger<ClientService>>();
        }

        [Test]
        public void GetOnlines_ShouldBeSuccessful() 
        {
            _mockClientRepository.Setup(r => r.GetAll()).Returns(() => new List<Client>() 
            {
                 new Client(){ LastNotify = DateTime.Now },
                 new Client(){ LastNotify = DateTime.Now.AddMinutes(-5) }
            });

            var service = new ClientService(_mockLogger.Object, _mockClientRepository.Object);

            var result = service.GetOnlines();

            Assert.IsNotEmpty(result);

        }

        [Test]
        public void GetOnlines_ShouldThrowListClientNullException()
        {
            _mockClientRepository.Setup(r => r.GetAll()).Returns(() => null);

            var service = new ClientService(_mockLogger.Object, _mockClientRepository.Object);

            Assert.Throws<ListClientNullException>(() => service.GetOnlines());

        }

        [Test]
        public void NotifyOnline_ShouldBeSuccessful()
        {
            _mockClientRepository.Setup(r => r.FindOne(It.IsAny<string>())).Returns((string macAddres) => new Client());

            var service = new ClientService(_mockLogger.Object, _mockClientRepository.Object);

            Assert.DoesNotThrow(() => service.NotifyOnline(It.IsAny<string>()));
        }

        [Test]
        public void NotifyOnline_ShouldThrowClientNullException()
        {
            _mockClientRepository.Setup(r => r.FindOne(It.IsAny<string>())).Returns((string macAddres) => null);

            var service = new ClientService(_mockLogger.Object, _mockClientRepository.Object);

            Assert.Throws<ClientNullException>(() => service.NotifyOnline(It.IsAny<string>()));
        }

        [TestCase("123")]
        [TestCase("000")]
        public void Register_ShouldBeSuccessful(string macAddress)
        {
            _mockClientRepository.Setup(r => r.FindOne(It.IsAny<string>())).Returns((string macAddres) =>
            {
                if (macAddres == "123")
                    return new Client() { MacAddress = "123" };
                else return null;
            });

            var client = new Client()
            {
                MacAddress = macAddress
            };

            var service = new ClientService(_mockLogger.Object, _mockClientRepository.Object);

            Assert.DoesNotThrow(() => service.Register(client));
        }


    }
}
