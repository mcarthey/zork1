using Moq;
using Xunit;
using ZorkSharp.Commands;
using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

namespace ZorkSharp.Tests;

public class CommandTests
{
    private readonly Mock<IWorld> _mockWorld;
    private readonly Mock<IGameState> _mockGameState;
    private readonly Mock<IOutputWriter> _mockOutput;

    public CommandTests()
    {
        _mockWorld = new Mock<IWorld>();
        _mockGameState = new Mock<IGameState>();
        _mockOutput = new Mock<IOutputWriter>();
    }

    [Fact]
    public void TakeCommand_ValidObject_ReturnsSuccess()
    {
        // Arrange
        var lamp = new GameObject
        {
            Id = "LAMP",
            Name = "brass lantern",
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible,
            Size = 5
        };

        var inventory = new Mock<IInventory>();
        inventory.Setup(i => i.CanCarry(It.IsAny<IGameObject>())).Returns(true);
        inventory.Setup(i => i.Add(It.IsAny<string>())).Returns(true);

        _mockGameState.Setup(gs => gs.CurrentRoomId).Returns("LIVING-ROOM");
        _mockWorld.Setup(w => w.FindObjectInRoom("LIVING-ROOM", "lamp")).Returns(lamp);
        _mockWorld.Setup(w => w.PlayerInventory).Returns(inventory.Object);
        _mockWorld.Setup(w => w.MoveObject(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var command = new TakeCommand();
        var parsedCommand = ParsedCommand.Valid("take", "lamp");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        inventory.Verify(i => i.Add("LAMP"), Times.Once);
    }

    [Fact]
    public void TakeCommand_NoObject_ReturnsFailed()
    {
        // Arrange
        var command = new TakeCommand();
        var parsedCommand = ParsedCommand.Valid("take");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Failed, result.Status);
        Assert.Contains("What do you want to take", result.Message);
    }

    [Fact]
    public void TakeCommand_ObjectNotFound_ReturnsFailed()
    {
        // Arrange
        _mockGameState.Setup(gs => gs.CurrentRoomId).Returns("LIVING-ROOM");
        _mockWorld.Setup(w => w.FindObjectInRoom("LIVING-ROOM", "unicorn")).Returns((IGameObject?)null);

        var command = new TakeCommand();
        var parsedCommand = ParsedCommand.Valid("take", "unicorn");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Failed, result.Status);
        Assert.Contains("don't see", result.Message);
    }

    [Fact]
    public void DropCommand_ValidObject_ReturnsSuccess()
    {
        // Arrange
        var lamp = new GameObject
        {
            Id = "LAMP",
            Name = "brass lantern",
            Flags = ObjectFlags.Takeable
        };

        var inventory = new Mock<IInventory>();
        inventory.Setup(i => i.Remove(It.IsAny<string>())).Returns(true);

        _mockGameState.Setup(gs => gs.CurrentRoomId).Returns("LIVING-ROOM");
        _mockWorld.Setup(w => w.FindObjectInInventory("lamp")).Returns(lamp);
        _mockWorld.Setup(w => w.PlayerInventory).Returns(inventory.Object);
        _mockWorld.Setup(w => w.MoveObject(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var command = new DropCommand();
        var parsedCommand = ParsedCommand.Valid("drop", "lamp");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        inventory.Verify(i => i.Remove("LAMP"), Times.Once);
    }

    [Fact]
    public void LookCommand_Always_ReturnsSuccessWithRoomDisplay()
    {
        // Arrange
        var command = new LookCommand();
        var parsedCommand = ParsedCommand.Valid("look");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        Assert.True(result.ShouldDisplayRoom);
    }

    [Fact]
    public void ExamineCommand_ValidObject_ReturnsDescription()
    {
        // Arrange
        var lamp = new GameObject
        {
            Id = "LAMP",
            Name = "brass lantern",
            Description = "A battery-powered brass lantern.",
            Flags = ObjectFlags.Visible
        };

        _mockGameState.Setup(gs => gs.CurrentRoomId).Returns("LIVING-ROOM");
        _mockWorld.Setup(w => w.FindObjectInRoom("LIVING-ROOM", "lamp")).Returns(lamp);

        var command = new ExamineCommand();
        var parsedCommand = ParsedCommand.Valid("examine", "lamp");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        Assert.Equal(lamp.Description, result.Message);
    }

    [Fact]
    public void InventoryCommand_WithItems_ListsItems()
    {
        // Arrange
        var lamp = new GameObject { Id = "LAMP", Name = "brass lantern" };
        var sword = new GameObject { Id = "SWORD", Name = "elvish sword" };

        var inventory = new Mock<IInventory>();
        inventory.Setup(i => i.GetAllItems()).Returns(new List<IGameObject> { lamp, sword });

        _mockWorld.Setup(w => w.PlayerInventory).Returns(inventory.Object);

        var command = new InventoryCommand();
        var parsedCommand = ParsedCommand.Valid("inventory");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        Assert.Contains("brass lantern", result.Message);
        Assert.Contains("elvish sword", result.Message);
    }

    [Fact]
    public void InventoryCommand_NoItems_ReturnsEmptyMessage()
    {
        // Arrange
        var inventory = new Mock<IInventory>();
        inventory.Setup(i => i.GetAllItems()).Returns(new List<IGameObject>());

        _mockWorld.Setup(w => w.PlayerInventory).Returns(inventory.Object);

        var command = new InventoryCommand();
        var parsedCommand = ParsedCommand.Valid("inventory");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
        Assert.Contains("empty-handed", result.Message);
    }
}
