# ZorkSharp Stub Architecture Guide

This document describes the stub architecture created for features that are not yet fully implemented. These stubs provide the framework and extension points for future development while maintaining SOLID principles.

## Overview

The stub architecture provides:
- ✅ **Interfaces** defining contracts
- ✅ **Abstract base classes** with common functionality
- ✅ **Partial implementations** with clear TODOs
- ✅ **Integration points** with existing systems
- ✅ **Example implementations** showing the pattern

All stubs are clearly marked with `// TODO:` comments indicating what needs to be implemented.

---

## Combat System

**Location**: `ZorkSharp/Combat/`

### Interfaces

#### `ICombatant`
Represents any entity that can participate in combat (player or NPC).
```csharp
public interface ICombatant
{
    string Id { get; }
    string Name { get; }
    int Health { get; set; }
    int MaxHealth { get; }
    int AttackPower { get; }
    int Defense { get; }
    bool IsAlive { get; }
    IGameObject? EquippedWeapon { get; set; }
}
```

#### `ICombatEngine`
Manages combat interactions between combatants.
```csharp
public interface ICombatEngine
{
    CombatResult Attack(ICombatant attacker, ICombatant defender, IGameObject? weapon = null);
    CombatResult Defend(ICombatant defender, int incomingDamage);
    bool IsCombatActive { get; }
    void StartCombat(ICombatant player, ICombatant enemy);
    void EndCombat();
}
```

### Implementations

- ✅ `CombatantBase`: Abstract base class for combatants
- ⚠️ `CombatEngine`: Stub implementation with basic structure
- ✅ `CombatResult`: Record for combat action results
- ✅ `WeaponType`: Enum for weapon categories

### What's Implemented
- Health tracking
- Basic attack/defend structure
- Combat state management

### What Needs Implementation
- Hit chance calculations
- Weapon effectiveness against specific enemies
- Critical hits
- Status effects
- Combat AI for NPCs
- Armor and defense calculations

### Usage Example
```csharp
var combatEngine = new CombatEngine(output);
var player = new PlayerCombatant(100, 10, 5);
var troll = new TrollNpc(50, 15, 3);

combatEngine.StartCombat(player, troll);
var result = combatEngine.Attack(player, troll, sword);
if (result.TargetDied)
{
    combatEngine.EndCombat();
}
```

---

## NPC System

**Location**: `ZorkSharp/NPCs/`

### Interfaces

#### `INpc`
Represents a non-player character with AI behavior.
```csharp
public interface INpc : ICombatant
{
    string CurrentRoomId { get; set; }
    NpcState State { get; set; }
    NpcBehaviorType BehaviorType { get; }
    List<string> Inventory { get; }

    void OnPlayerEnter(IGameState gameState, IWorld world);
    void OnPlayerLeave(IGameState gameState, IWorld world);
    void OnTurn(IGameState gameState, IWorld world, IOutputWriter output);
    bool CanTalk { get; }
    string? GetDialogue(string topic);
}
```

#### `INpcManager`
Manages all NPCs in the game world.

### Implementations

- ✅ `NpcBase`: Abstract base class for all NPCs
- ✅ `NpcManager`: Manager for NPC lifecycle
- ✅ `NpcState`: Enum for NPC states (Idle, Hostile, Friendly, etc.)
- ✅ `NpcBehaviorType`: Enum for behavior patterns

### What's Implemented
- NPC registration and tracking
- Room-based NPC queries
- Event hooks (OnPlayerEnter, OnTurn)
- State machine foundation

### What Needs Implementation
- Specific NPC implementations (Thief, Troll, Cyclops)
- AI behavior for each behavior type
- Pathfinding and movement
- Item stealing (Thief)
- Dialogue system
- NPC-to-NPC interactions

### Usage Example
```csharp
// Create a Thief NPC
public class ThiefNpc : NpcBase
{
    public override NpcBehaviorType BehaviorType => NpcBehaviorType.Wandering;

    public override void OnTurn(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // Implement thief AI
        // - Move randomly
        // - Steal treasures
        // - Attack if cornered
    }
}

// Register with manager
npcManager.RegisterNpc(new ThiefNpc { CurrentRoomId = "MAZE-1" });
```

---

## Save/Load System

**Location**: `ZorkSharp/Persistence/`

### Interfaces

#### `ISaveGameService`
Handles saving and loading game state to disk.
```csharp
public interface ISaveGameService
{
    Task<bool> SaveGame(SaveGameData data, string slotName);
    Task<SaveGameData?> LoadGame(string slotName);
    Task<List<SaveGameInfo>> GetSaveSlots();
    Task<bool> DeleteSave(string slotName);
}
```

#### `IGameStateSerializer`
Converts game state to/from serializable format.

### Implementations

- ✅ `SaveGameData`: Complete data model for saved games
- ✅ `ObjectState`, `RoomState`, `NpcState`: State capture classes
- ⚠️ `SaveGameService`: Stub with file path logic
- ⚠️ `GameStateSerializer`: Stub with structure

### What's Implemented
- Save data model
- File path management
- Save directory creation

### What Needs Implementation
- JSON serialization/deserialization
- Compression (optional)
- Version migration
- Error handling and validation
- Backup/restore functionality
- Auto-save support

### Usage Example
```csharp
var saveService = new SaveGameService();
var serializer = new GameStateSerializer();

// Save
var saveData = serializer.SerializeGameState(gameState, world);
saveData.SaveName = "My Adventure";
await saveService.SaveGame(saveData, "slot1");

// Load
var loadedData = await saveService.LoadGame("slot1");
if (loadedData != null)
{
    serializer.DeserializeGameState(loadedData, gameState, world);
}
```

---

## Puzzle System

**Location**: `ZorkSharp/Puzzles/`

### Interfaces

#### `IPuzzle`
Represents a solvable puzzle in the game.
```csharp
public interface IPuzzle
{
    string Id { get; }
    string Name { get; }
    PuzzleState State { get; set; }
    int PointValue { get; }

    bool CanAttempt(IGameState gameState, IWorld world);
    PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world);
    void Reset();
    string GetHint(int hintLevel);
}
```

#### `IPuzzleManager`
Manages all puzzles and tracks completion.

### Implementations

- ✅ `PuzzleBase`: Abstract base class for puzzles
- ✅ `PuzzleManager`: Puzzle tracking and queries
- ✅ Example puzzle stubs: TrapDoorPuzzle, TrophyCasePuzzle, DamPuzzle

### What's Implemented
- Puzzle state tracking
- Hint system structure
- Point value tracking
- Room-based puzzle queries

### What Needs Implementation
- Specific puzzle logic for all 20+ major puzzles
- Puzzle chaining (solving A unlocks B)
- Puzzle hints integrated with HELP command
- Puzzle completion events
- Alternative solutions

### Usage Example
```csharp
public class TrapDoorPuzzle : PuzzleBase
{
    public TrapDoorPuzzle()
    {
        Id = "TRAP-DOOR";
        Name = "Trap Door Puzzle";
        PointValue = 5;
    }

    public override PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world)
    {
        // Check if rug moved
        if (command.Verb == "move" && command.DirectObject == "rug")
        {
            // Reveal trap door
            return Success("You move the rug, revealing a trap door!", false);
        }

        // Check if opening trap door
        if (command.Verb == "open" && command.DirectObject == "door")
        {
            // Check for key, open door
            return Success("The trap door opens!", true, 5);
        }

        return Failure("Nothing happens.");
    }
}
```

---

## Death & Victory System

**Location**: `ZorkSharp/Core/IGameConditions.cs`

### Interfaces

#### `IGameConditions`
Manages game ending conditions.
```csharp
public interface IGameConditions
{
    bool CheckVictoryCondition(IGameState gameState, IWorld world);
    DeathCause? CheckDeathConditions(IGameState gameState, IWorld world);
    void TriggerDeath(DeathCause cause, IGameState gameState, IOutputWriter output);
    void TriggerVictory(IGameState gameState, IOutputWriter output);
}
```

#### `IVictoryCondition` & `IDeathCondition`
Specific condition checkers.

### Implementations

- ✅ `GameConditions`: Main condition manager
- ✅ `DeathCause`: Enum for death types
- ✅ `GameEndingType`: Enum for ending types
- ✅ Example conditions: AllTreasuresCollectedCondition, GrueDeathCondition

### What's Implemented
- Condition checking framework
- Death messages
- Victory messages
- Rank system

### What Needs Implementation
- All 10+ death scenarios
- Victory condition logic
- Restart/restore after death
- Endgame sequences
- Score calculation for endings

### Usage Example
```csharp
var gameConditions = new GameConditions(output);

// Check for death each turn
var deathCause = gameConditions.CheckDeathConditions(gameState, world);
if (deathCause.HasValue)
{
    gameConditions.TriggerDeath(deathCause.Value, gameState, output);
    return;
}

// Check for victory
if (gameConditions.CheckVictoryCondition(gameState, world))
{
    gameConditions.TriggerVictory(gameState, output);
}
```

---

## Advanced Parser Features

**Location**: `ZorkSharp/Parser/IParserExtensions.cs`

### Interfaces

#### `IParserExtensions`
Advanced parsing capabilities.
```csharp
public interface IParserExtensions
{
    void SetPronounReference(string pronoun, string objectName);
    string? ResolvePronoun(string pronoun);
    List<IGameObject> ResolveMultiObject(string objectSpec, IWorld world, string currentRoomId);
    DisambiguationResult Disambiguate(string objectName, List<IGameObject> candidates);
}
```

#### `ICommandHistory`
Tracks command history for AGAIN/OOPS.

#### `IObjectMatcher`
Smart object matching with fuzzy logic.

### Implementations

- ⚠️ `ParserExtensions`: Stub with basic structure
- ✅ `CommandHistory`: Full history tracking
- ⚠️ `ObjectMatcher`: Stub for smart matching

### What's Implemented
- Command history storage
- Basic pronoun tracking structure
- Disambiguation framework

### What Needs Implementation
- Pronoun resolution (IT, THEM)
- Multi-object parsing (TAKE ALL)
- Fuzzy matching and typo tolerance
- Context-aware disambiguation
- AGAIN command
- OOPS command

### Usage Example
```csharp
var parserExtensions = new ParserExtensions(new CommandHistory());

// Set pronoun reference
parserExtensions.SetPronounReference("it", "sword");

// Later: "take it" resolves to "take sword"
var objectName = parserExtensions.ResolvePronoun("it"); // Returns "sword"

// Multi-object
var objects = parserExtensions.ResolveMultiObject("all", world, currentRoomId);
```

---

## Light/Battery System

**Location**: `ZorkSharp/Systems/ILightSystem.cs`

### Interfaces

#### `ILightSystem`
Manages lighting and light sources.
```csharp
public interface ILightSystem
{
    bool IsRoomLit(string roomId, IWorld world);
    bool HasActiveLightSource(IWorld world);
    void TurnOnLight(ILightSource lightSource);
    void TurnOffLight(ILightSource lightSource);
    int GetTurnsInDarkness(IGameState gameState);
}
```

#### `ILightSource`
Represents a light source with battery.

### Implementations

- ✅ `LightSource`: Complete light source implementation
- ⚠️ `LightSystem`: Stub with basic structure
- ✅ `LampBatteryEvent`: Clock event for battery depletion
- ✅ `DarknessWarningEvent`: Clock event for Grue warnings

### What's Implemented
- Battery tracking
- Light on/off state
- Depletion warnings
- Multiple light source types

### What Needs Implementation
- Integration with IGameObject
- Finding light sources in inventory/room
- Battery consumption per turn
- Lamp refueling mechanics
- Different burn rates for different light types

### Usage Example
```csharp
var lightSystem = new LightSystem();
var lamp = new LightSource
{
    Id = "LANTERN",
    Name = "brass lantern",
    Type = LightSourceType.Lamp,
    IsBatteryPowered = true,
    MaxBatteryLife = 200,
    BatteryLife = 200
};

lightSystem.TurnOnLight(lamp);

// Each turn
lamp.ConsumeBattery(1);

if (lamp.BatteryLife < 10)
{
    output.WriteLine(lamp.GetStatusMessage()); // Warnings
}
```

---

## Integration Guide

### Adding to GameEngine

To integrate these systems into the game engine:

```csharp
public class GameEngine : IGameEngine
{
    // Add new dependencies
    private readonly ICombatEngine _combatEngine;
    private readonly INpcManager _npcManager;
    private readonly IPuzzleManager _puzzleManager;
    private readonly IGameConditions _gameConditions;
    private readonly ILightSystem _lightSystem;
    private readonly ISaveGameService _saveService;

    public GameEngine(
        // ... existing dependencies ...
        ICombatEngine combatEngine,
        INpcManager npcManager,
        IPuzzleManager puzzleManager,
        IGameConditions gameConditions,
        ILightSystem lightSystem,
        ISaveGameService saveService)
    {
        // ... existing initialization ...
        _combatEngine = combatEngine;
        _npcManager = npcManager;
        _puzzleManager = puzzleManager;
        _gameConditions = gameConditions;
        _lightSystem = lightSystem;
        _saveService = saveService;
    }

    public void ProcessCommand(string input)
    {
        // ... existing command processing ...

        // Check death conditions
        var deathCause = _gameConditions.CheckDeathConditions(GameState, World);
        if (deathCause.HasValue)
        {
            _gameConditions.TriggerDeath(deathCause.Value, GameState, Output);
            return;
        }

        // Process NPC turns
        _npcManager.ProcessNpcTurns(GameState, World, Output);

        // Check victory
        if (_gameConditions.CheckVictoryCondition(GameState, World))
        {
            _gameConditions.TriggerVictory(GameState, Output);
            return;
        }
    }
}
```

### Updating Program.cs

```csharp
// Create new systems
var combatEngine = new CombatEngine(output);
var npcManager = new NpcManager(output);
var puzzleManager = new PuzzleManager();
var gameConditions = new GameConditions(output);
var lightSystem = new LightSystem();
var saveService = new SaveGameService();

// Pass to GameEngine
var gameEngine = new GameEngine(
    gameState, world, parser, commandFactory, output, input, clock,
    combatEngine, npcManager, puzzleManager, gameConditions, lightSystem, saveService
);
```

---

## Development Workflow

### Implementing a Stub System

1. **Find the stub interface** in the appropriate namespace
2. **Review the TODO comments** to understand what needs implementation
3. **Implement the concrete logic** following SOLID principles
4. **Add unit tests** for your implementation
5. **Integrate** with GameEngine if needed
6. **Update TODO.md** to mark the feature as complete

### Example: Implementing the Thief NPC

```csharp
// 1. Create concrete implementation
public class ThiefNpc : NpcBase
{
    public override NpcBehaviorType BehaviorType => NpcBehaviorType.Wandering;

    public ThiefNpc() : base(maxHealth: 30, attackPower: 10, defense: 5)
    {
        Id = "THIEF";
        Name = "thief";
    }

    public override void OnTurn(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // Implement AI behavior
        if (CurrentRoomId == gameState.CurrentRoomId)
        {
            // Attack or steal
            AttemptSteal(gameState, world, output);
        }
        else
        {
            // Wander
            MoveRandomly(world, output);
        }
    }

    private void AttemptSteal(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // Implementation
    }

    private void MoveRandomly(IWorld world, IOutputWriter output)
    {
        // Implementation
    }
}

// 2. Register in WorldBuilder
public void BuildWorld()
{
    // ... existing code ...

    var thief = new ThiefNpc { CurrentRoomId = "MAZE-1" };
    npcManager.RegisterNpc(thief);
}
```

---

## Testing Stubs

Even though stubs aren't fully implemented, you can still write tests for the architecture:

```csharp
[Test]
public void CombatEngine_StartCombat_SetsCombatActive()
{
    var engine = new CombatEngine(mockOutput);
    var player = new TestCombatant();
    var enemy = new TestCombatant();

    engine.StartCombat(player, enemy);

    Assert.IsTrue(engine.IsCombatActive);
}

[Test]
public void NpcManager_RegisterNpc_CanRetrieveById()
{
    var manager = new NpcManager(mockOutput);
    var npc = new TestNpc { Id = "TEST-NPC" };

    manager.RegisterNpc(npc);
    var retrieved = manager.GetNpc("TEST-NPC");

    Assert.AreEqual(npc, retrieved);
}
```

---

## Summary

All stub systems provide:
- ✅ Clear interfaces following Interface Segregation Principle
- ✅ Abstract base classes following Template Method pattern
- ✅ Integration points with existing architecture
- ✅ TODO comments marking what needs implementation
- ✅ Example implementations showing the pattern
- ✅ Type safety and compile-time checking

Contributors can implement any system independently while maintaining architectural consistency!
