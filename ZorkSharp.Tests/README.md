# ZorkSharp Tests

Unit tests for the ZorkSharp project using xUnit and Moq.

## Running Tests

### Command Line
```bash
cd ZorkSharp.Tests
dotnet test
```

### With Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Verbose Output
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Test Organization

### ParserTests.cs
Tests for the natural language parser (`GameParser`):
- ✅ Simple command parsing (take, go, look)
- ✅ Complex command parsing (open door with key)
- ✅ Article stripping (the, a, an)
- ✅ Command shortcuts (x for examine, i for inventory)
- ✅ Invalid input handling
- ✅ Unknown verb detection

### CommandTests.cs
Tests for command implementations:
- ✅ TakeCommand - picking up objects
- ✅ DropCommand - dropping objects
- ✅ LookCommand - room display
- ✅ ExamineCommand - object examination
- ✅ InventoryCommand - listing items
- ✅ Edge cases (missing objects, invalid state)

Uses **Moq** to mock dependencies (`IWorld`, `IGameState`, `IOutputWriter`).

### WorldTests.cs
Tests for the game world system:
- ✅ GameWorld - room and object management
- ✅ Inventory - item carrying and weight limits
- ✅ GameObject - flags, matching, containers
- ✅ Object movement between rooms
- ✅ Visibility filtering

## Writing New Tests

### Basic Test Structure
```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
{
    // Arrange - Set up test data
    var obj = new GameObject { /* ... */ };

    // Act - Execute the method being tested
    var result = obj.DoSomething();

    // Assert - Verify the result
    Assert.Equal(expected, result);
}
```

### Theory Tests (Multiple Test Cases)
```csharp
[Theory]
[InlineData("input1", "expected1")]
[InlineData("input2", "expected2")]
public void Test_WithDifferentInputs(string input, string expected)
{
    // Test logic
}
```

### Using Mocks
```csharp
// Create a mock
var mockWorld = new Mock<IWorld>();

// Setup method behavior
mockWorld.Setup(w => w.GetRoom("ROOM-ID"))
         .Returns(new Room { Id = "ROOM-ID" });

// Verify method was called
mockWorld.Verify(w => w.GetRoom("ROOM-ID"), Times.Once);
```

## Test Coverage Goals

Target coverage by component:
- **Parser**: 90%+ (critical for gameplay)
- **Commands**: 85%+ (core game mechanics)
- **World**: 80%+ (state management)
- **Game Engine**: 70%+ (complex integration)

## Adding New Tests

When adding new features, include tests for:
1. **Happy path** - Normal successful operation
2. **Edge cases** - Boundary conditions
3. **Error cases** - Invalid input, missing data
4. **Integration** - Multiple components working together

### Example: Testing a New Command

```csharp
public class NewCommandTests
{
    private readonly Mock<IWorld> _mockWorld;
    private readonly Mock<IGameState> _mockGameState;

    public NewCommandTests()
    {
        _mockWorld = new Mock<IWorld>();
        _mockGameState = new Mock<IGameState>();
    }

    [Fact]
    public void NewCommand_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var command = new NewCommand();
        var parsedCommand = ParsedCommand.Valid("newcmd", "arg");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Success, result.Status);
    }

    [Fact]
    public void NewCommand_InvalidInput_ReturnsFailed()
    {
        // Arrange
        var command = new NewCommand();
        var parsedCommand = ParsedCommand.Valid("newcmd");

        // Act
        var result = command.Execute(parsedCommand, _mockWorld.Object, _mockGameState.Object);

        // Assert
        Assert.Equal(CommandStatus.Failed, result.Status);
        Assert.NotNull(result.Message);
    }
}
```

## Best Practices

1. **One assertion per test** (when possible) - Makes failures clearer
2. **Descriptive test names** - `MethodName_Scenario_ExpectedResult`
3. **Arrange-Act-Assert pattern** - Clear test structure
4. **Mock external dependencies** - Isolate the code being tested
5. **Test behavior, not implementation** - Focus on outcomes
6. **Keep tests independent** - No shared state between tests
7. **Fast tests** - No file I/O, network calls, or sleeps in unit tests

## Continuous Integration

These tests are designed to run in CI/CD pipelines:
- Fast execution (< 1 second for entire suite currently)
- No external dependencies
- Deterministic results
- Clear failure messages

## Resources

- [xUnit Documentation](https://xunit.net/)
- [Moq Documentation](https://github.com/moq/moq4)
- [.NET Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
