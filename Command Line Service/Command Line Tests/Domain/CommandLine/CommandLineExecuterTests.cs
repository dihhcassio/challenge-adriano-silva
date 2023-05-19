using Command_Line_Domain.CommandLine.CommandExecuter;
using Command_Line_Domain.CommandLine.Execptions;
using Command_Line_Domain.CommandLine.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Command_Line_Tests.Domain.CommandLine
{
    public  class CommandLineExecuterTests
    {
        Mock<ICommandExecuter> _mockCommandExecuter;
        Mock<ILogger<CommandLineService>> _mockLogger;

        [SetUp]
        public void SetUp()
        {
            _mockCommandExecuter = new Mock<ICommandExecuter>();
            _mockLogger = new Mock<ILogger<CommandLineService>>();
        }


        [Test]
        public void RunCommand_ShouldBeSuccessful()
        {
            var service = new CommandLineService(_mockLogger.Object, _mockCommandExecuter.Object);

            Assert.DoesNotThrow(() => service.RunCommand("dir"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RunCommand_ShouldThrowEmptyCommandException(string command) 
        {
            var service = new CommandLineService(_mockLogger.Object, _mockCommandExecuter.Object);

            Assert.Throws<EmptyCommandException>(() => service.RunCommand(command));
        }
    }
}
