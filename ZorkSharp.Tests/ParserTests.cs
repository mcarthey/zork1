using Xunit;
using ZorkSharp.Parser;

namespace ZorkSharp.Tests;

public class GameParserTests
{
    private readonly GameParser _parser;

    public GameParserTests()
    {
        _parser = new GameParser();
    }

    [Theory]
    [InlineData("take lamp", "take", "lamp", null, null)]
    [InlineData("get lamp", "get", "lamp", null, null)]
    [InlineData("go north", "go", "north", null, null)]
    [InlineData("n", "go", "n", null, null)]
    [InlineData("look", "look", null, null, null)]
    [InlineData("inventory", "inventory", null, null, null)]
    [InlineData("i", "inventory", null, null, null)]
    public void Parse_SimpleCommands_ReturnsCorrectParsing(
        string input,
        string expectedVerb,
        string? expectedDirectObject,
        string? expectedIndirectObject,
        string? expectedPreposition)
    {
        // Act
        var result = _parser.Parse(input);

        // Assert
        Assert.True(result.IsValid);
        Assert.Equal(expectedVerb, result.Verb);
        Assert.Equal(expectedDirectObject, result.DirectObject);
        Assert.Equal(expectedIndirectObject, result.IndirectObject);
        Assert.Equal(expectedPreposition, result.Preposition);
    }

    [Theory]
    [InlineData("open door with key", "open", "door", "key", "with")]
    [InlineData("put lamp in case", "put", "lamp", "case", "in")]
    [InlineData("attack troll with sword", "attack", "troll", "sword", "with")]
    public void Parse_ComplexCommands_ParsesCorrectly(
        string input,
        string expectedVerb,
        string expectedDirectObject,
        string expectedIndirectObject,
        string expectedPreposition)
    {
        // Act
        var result = _parser.Parse(input);

        // Assert
        Assert.True(result.IsValid);
        Assert.Equal(expectedVerb, result.Verb);
        Assert.Equal(expectedDirectObject, result.DirectObject);
        Assert.Equal(expectedIndirectObject, result.IndirectObject);
        Assert.Equal(expectedPreposition, result.Preposition);
    }

    [Theory]
    [InlineData("take the lamp", "take", "lamp")]
    [InlineData("take a sword", "take", "sword")]
    [InlineData("open the door", "open", "door")]
    public void Parse_CommandsWithArticles_StripsArticles(
        string input,
        string expectedVerb,
        string expectedDirectObject)
    {
        // Act
        var result = _parser.Parse(input);

        // Assert
        Assert.True(result.IsValid);
        Assert.Equal(expectedVerb, result.Verb);
        Assert.Equal(expectedDirectObject, result.DirectObject);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Parse_EmptyInput_ReturnsInvalid(string? input)
    {
        // Act
        var result = _parser.Parse(input!);

        // Assert
        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

    [Fact]
    public void Parse_UnknownVerb_ReturnsInvalid()
    {
        // Arrange
        string input = "xyzabc lamp";

        // Act
        var result = _parser.Parse(input);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("don't understand", result.ErrorMessage);
    }

    [Theory]
    [InlineData("x lamp", "examine", "lamp")]
    [InlineData("examine lamp", "examine", "lamp")]
    [InlineData("look at lamp", "examine", "lamp")]
    public void Parse_ExamineShortcuts_ParsesCorrectly(
        string input,
        string expectedVerb,
        string expectedDirectObject)
    {
        // Act
        var result = _parser.Parse(input);

        // Assert
        Assert.True(result.IsValid);
        Assert.Equal(expectedVerb, result.Verb);
        Assert.Equal(expectedDirectObject, result.DirectObject);
    }
}
