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
    public class CommandReturnServiceTests
    {
        private readonly Mock<ILogger<CommandReturnService>> _mockLogger;
        private readonly Mock<ICommandReturnRepository> _mockCommandReturnRepository;
        private readonly Mock<ICommandRepository> _mockCommandRepository;


        public CommandReturnServiceTests() 
        {
            _mockLogger = new Mock<ILogger<CommandReturnService>>();
            _mockCommandReturnRepository = new Mock<ICommandReturnRepository>();
            _mockCommandRepository = new Mock<ICommandRepository>();
        }

        [Test]
        public void InsertCommandReturn_ShouldBeSuccessful() 
        {
            _mockCommandRepository.Setup(r => r.FindOne(It.IsAny<int>())).Returns((int commandId) => new Command());

            var service = new CommandReturnService(_mockLogger.Object, _mockCommandReturnRepository.Object, _mockCommandRepository.Object);

            Assert.DoesNotThrow(() => service.InsertCommandReturn(It.IsAny<int>(), It.IsAny<string>()));
        }

        [Test]
        public void InsertCommandReturn_ShouldThrowCommandNotFoundException()
        {
            _mockCommandRepository.Setup(r => r.FindOne(It.IsAny<int>())).Returns((int commandId) => null);

            var service = new CommandReturnService(_mockLogger.Object, _mockCommandReturnRepository.Object, _mockCommandRepository.Object);

            Assert.Throws<CommandNotFoundException>(() => service.InsertCommandReturn(It.IsAny<int>(), It.IsAny<string>()));
        }



    }
}
