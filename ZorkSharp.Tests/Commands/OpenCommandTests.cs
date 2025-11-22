using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ZorkSharp.Commands;
using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

namespace ZorkSharp.Tests.Commands
{
    public class OpenCommandTests
    {
        private readonly Mock<IWorld> _world;
        private readonly Mock<IGameState> _gameState;
        private readonly GameObject _mailbox;
        private readonly OpenCommand _command;
        private readonly ParsedCommand _parsed;

        public OpenCommandTests()
        {
            _world = new Mock<IWorld>();
            _gameState = new Mock<IGameState>();
            _mailbox = new GameObject
            {
                Id = "MAILBOX",
                Name = "small mailbox",
                Flags = ObjectFlags.Openable | ObjectFlags.Container
            };

            _world.Setup(w => w.FindObjectInRoom(It.IsAny<string>(), "mailbox"))
                .Returns(_mailbox);

            _command = new OpenCommand();
            _parsed = new ParsedCommand { DirectObject = "mailbox" };
        }

        [Fact]
        public void Open_ValidOpenableObject_OpensSuccessfully()
        {
            // Arrange

            // Act
            var result = _command.Execute(_parsed, _world.Object, _gameState.Object);

            // Assert
            Assert.Equal(CommandStatus.Success, result.Status);
            Assert.True(_mailbox.HasFlag(ObjectFlags.IsOpen));
        }

        [Fact]
        public void Open_LockedObject_Fails()
        {
            // Arrange
            _mailbox.SetFlag(ObjectFlags.Locked);

            // Act
            var result = _command.Execute(_parsed, _world.Object, _gameState.Object);

            // Assert
            Assert.Equal(CommandStatus.Failed, result.Status);
            Assert.False(_mailbox.HasFlag(ObjectFlags.IsOpen));
            Assert.Contains($"{_mailbox.Name} is locked", result.Message);
        }

        [Fact]
        public void Open_AlreadyOpen_Fails()
        {
            // Arrange
            _mailbox.SetFlag(ObjectFlags.IsOpen);

            // Act
            var result = _command.Execute(_parsed, _world.Object, _gameState.Object);

            // Assert
            Assert.Equal(CommandStatus.Failed, result.Status);
            Assert.True(_mailbox.HasFlag(ObjectFlags.IsOpen));
            Assert.Contains($"{_mailbox.Name} is already open", result.Message);
        }

        [Fact]
        public void Open_NotOpenable_Fails()
        {
            // Arrange
            _mailbox.ClearFlag(ObjectFlags.Openable);

            // Act
            var result = _command.Execute(_parsed, _world.Object, _gameState.Object);

            // Assert
            Assert.Equal(CommandStatus.Failed, result.Status);
            Assert.False(_mailbox.HasFlag(ObjectFlags.Openable));
            Assert.Contains($"can't open the {_mailbox.Name}", result.Message);
        }
    }
}
