using Xunit;
using ZorkSharp.Core;
using ZorkSharp.World;

namespace ZorkSharp.Tests;

public class GameWorldTests
{
    [Fact]
    public void AddRoom_ValidRoom_AddsSuccessfully()
    {
        // Arrange
        var world = new GameWorld();
        var room = new Room
        {
            Id = "TEST-ROOM",
            Name = "Test Room",
            Description = "A test room"
        };

        // Act
        world.AddRoom(room);
        var retrieved = world.GetRoom("TEST-ROOM");

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("TEST-ROOM", retrieved.Id);
        Assert.Equal("Test Room", retrieved.Name);
    }

    [Fact]
    public void AddObject_ValidObject_AddsSuccessfully()
    {
        // Arrange
        var world = new GameWorld();
        var obj = new GameObject
        {
            Id = "TEST-OBJ",
            Name = "test object",
            Description = "A test object"
        };

        // Act
        world.AddObject(obj);
        var retrieved = world.GetObject("TEST-OBJ");

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("TEST-OBJ", retrieved.Id);
        Assert.Equal("test object", retrieved.Name);
    }

    [Fact]
    public void FindObjectInRoom_ObjectExists_ReturnsObject()
    {
        // Arrange
        var world = new GameWorld();
        var room = new Room
        {
            Id = "LIVING-ROOM",
            Name = "Living Room",
            Items = new List<string> { "LAMP" }
        };
        var lamp = new GameObject
        {
            Id = "LAMP",
            Name = "brass lantern",
            Synonyms = new[] { "lamp", "lantern" }
        };

        world.AddRoom(room);
        world.AddObject(lamp);

        // Act
        var found = world.FindObjectInRoom("LIVING-ROOM", "lamp");

        // Assert
        Assert.NotNull(found);
        Assert.Equal("LAMP", found.Id);
    }

    [Fact]
    public void FindObjectInRoom_ObjectNotInRoom_ReturnsNull()
    {
        // Arrange
        var world = new GameWorld();
        var room = new Room
        {
            Id = "LIVING-ROOM",
            Name = "Living Room",
            Items = new List<string>()
        };

        world.AddRoom(room);

        // Act
        var found = world.FindObjectInRoom("LIVING-ROOM", "lamp");

        // Assert
        Assert.Null(found);
    }

    [Fact]
    public void MoveObject_ValidMove_UpdatesLocation()
    {
        // Arrange
        var world = new GameWorld();
        var room1 = new Room { Id = "ROOM1", Items = new List<string> { "OBJ1" } };
        var room2 = new Room { Id = "ROOM2", Items = new List<string>() };
        var obj = new GameObject { Id = "OBJ1", LocationId = "ROOM1" };

        world.AddRoom(room1);
        world.AddRoom(room2);
        world.AddObject(obj);

        // Act
        world.MoveObject("OBJ1", "ROOM2");

        // Assert
        Assert.Equal("ROOM2", obj.LocationId);
        Assert.DoesNotContain("OBJ1", room1.Items);
        Assert.Contains("OBJ1", room2.Items);
    }

    [Fact]
    public void GetVisibleObjectsInRoom_OnlyReturnsVisible()
    {
        // Arrange
        var world = new GameWorld();
        var room = new Room
        {
            Id = "TEST-ROOM",
            Items = new List<string> { "VISIBLE-OBJ", "HIDDEN-OBJ" }
        };
        var visibleObj = new GameObject
        {
            Id = "VISIBLE-OBJ",
            Name = "visible object",
            Flags = ObjectFlags.Visible
        };
        var hiddenObj = new GameObject
        {
            Id = "HIDDEN-OBJ",
            Name = "hidden object",
            Flags = ObjectFlags.None
        };

        world.AddRoom(room);
        world.AddObject(visibleObj);
        world.AddObject(hiddenObj);

        // Act
        var visibleObjects = world.GetVisibleObjectsInRoom("TEST-ROOM");

        // Assert
        Assert.Single(visibleObjects);
        Assert.Equal("VISIBLE-OBJ", visibleObjects[0].Id);
    }
}

public class InventoryTests
{
    [Fact]
    public void Add_ValidObject_AddsToInventory()
    {
        // Arrange
        var world = new GameWorld();
        var obj = new GameObject
        {
            Id = "LAMP",
            Size = 5,
            Flags = ObjectFlags.Takeable
        };
        world.AddObject(obj);

        var inventory = new Inventory(world) { MaxWeight = 100 };

        // Act
        var result = inventory.Add("LAMP");

        // Assert
        Assert.True(result);
        Assert.Contains("LAMP", inventory.Items);
    }

    [Fact]
    public void CanCarry_ExceedsWeight_ReturnsFalse()
    {
        // Arrange
        var world = new GameWorld();
        var heavyObj = new GameObject
        {
            Id = "BOULDER",
            Size = 150, // Exceeds max weight
            Flags = ObjectFlags.Takeable
        };
        world.AddObject(heavyObj);

        var inventory = new Inventory(world) { MaxWeight = 100 };

        // Act
        var canCarry = inventory.CanCarry(heavyObj);

        // Assert
        Assert.False(canCarry);
    }

    [Fact]
    public void TotalWeight_CalculatesCorrectly()
    {
        // Arrange
        var world = new GameWorld();
        var obj1 = new GameObject { Id = "OBJ1", Size = 10 };
        var obj2 = new GameObject { Id = "OBJ2", Size = 15 };
        world.AddObject(obj1);
        world.AddObject(obj2);

        var inventory = new Inventory(world);
        inventory.Add("OBJ1");
        inventory.Add("OBJ2");

        // Act
        var totalWeight = inventory.TotalWeight;

        // Assert
        Assert.Equal(25, totalWeight);
    }

    [Fact]
    public void Remove_ExistingItem_RemovesSuccessfully()
    {
        // Arrange
        var world = new GameWorld();
        var obj = new GameObject { Id = "LAMP", Size = 5 };
        world.AddObject(obj);

        var inventory = new Inventory(world);
        inventory.Add("LAMP");

        // Act
        var result = inventory.Remove("LAMP");

        // Assert
        Assert.True(result);
        Assert.DoesNotContain("LAMP", inventory.Items);
    }
}

public class GameObjectTests
{
    [Fact]
    public void HasFlag_FlagSet_ReturnsTrue()
    {
        // Arrange
        var obj = new GameObject
        {
            Flags = ObjectFlags.Takeable | ObjectFlags.Visible
        };

        // Act & Assert
        Assert.True(obj.HasFlag(ObjectFlags.Takeable));
        Assert.True(obj.HasFlag(ObjectFlags.Visible));
        Assert.False(obj.HasFlag(ObjectFlags.Weapon));
    }

    [Fact]
    public void MatchesName_CaseInsensitive_ReturnsTrue()
    {
        // Arrange
        var obj = new GameObject
        {
            Name = "brass lantern",
            Synonyms = new[] { "lamp", "lantern" }
        };

        // Act & Assert
        Assert.True(obj.MatchesName("lamp"));
        Assert.True(obj.MatchesName("LAMP"));
        Assert.True(obj.MatchesName("lantern"));
        Assert.True(obj.MatchesName("brass lantern"));
        Assert.False(obj.MatchesName("sword"));
    }

    [Fact]
    public void CanContain_ExceedsCapacity_ReturnsFalse()
    {
        // Arrange
        var container = new GameObject
        {
            Flags = ObjectFlags.Container,
            Capacity = 10,
            Contents = new List<string>()
        };

        var largeObj = new GameObject
        {
            Size = 15
        };

        // Act
        var canContain = container.CanContain(largeObj);

        // Assert
        Assert.False(canContain);
    }
}
