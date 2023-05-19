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
    public class CommandServiceTest
    {
        private readonly Mock<ILogger<CommandService>> _mockLogger;
        private readonly Mock<ICommandRepository> _mockCommandResitory;
        private readonly Mock<IClientRepository> _mockClientRepository;
        
        public CommandServiceTest() 
        {
            _mockLogger = new Mock<ILogger<CommandService>>();
            _mockCommandResitory = new Mock<ICommandRepository>();
            _mockClientRepository = new Mock<IClientRepository>();
        }

        [Test]
        public void GetCommandNotExecuted_ShouldBeSuccessful() 
        {
            _mockCommandResitory.Setup(r => r.GetCommandNotExecuted(It.IsAny<string>())).Returns(() => new List<Command>()
            {
                new Command(){ CreatedAt = DateTime.Now }
            });

            var service = new CommandService(_mockLogger.Object, _mockCommandResitory.Object, _mockClientRepository.Object);

            var result = service.GetCommandNotExecuted(It.IsAny<string>());

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void SendCommand_ShouldBeSuccessful() 
        {
            _mockClientRepository.Setup(c => c.FindByName(It.IsAny<string>())).Returns(
                new List<Client>() { 
                    new Client()
                }    
            );

            var clientsNames = new List<string>() { 
                "ClientName"
            };

            var service = new CommandService(_mockLogger.Object, _mockCommandResitory.Object, _mockClientRepository.Object);

            var result = service.SendCommand("Teste", clientsNames);

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void SendCommand_ShouldThrowClientsNameNullException()
        {
            _mockClientRepository.Setup(c => c.FindByName(It.IsAny<string>())).Returns(() => null);

            var service = new CommandService(_mockLogger.Object, _mockCommandResitory.Object, _mockClientRepository.Object);

            Assert.Throws<ClientsNameNullException>(() => service.SendCommand("Teste", null));
        }

        [Test]
        public void SendCommand_ShouldThrowListClientNullException()
        {
            _mockClientRepository.Setup(c => c.FindByName(It.IsAny<string>())).Returns(() => null);

            var service = new CommandService(_mockLogger.Object, _mockCommandResitory.Object, _mockClientRepository.Object);
            
            var clientsNames = new List<string>() {
                "ClientName"
            };

            Assert.Throws<ListClientNullException>(() => service.SendCommand("Teste", clientsNames));
        }


    }
}
