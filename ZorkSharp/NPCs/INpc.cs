namespace ZorkSharp.NPCs;

using ZorkSharp.Core;
using ZorkSharp.World;
using ZorkSharp.Combat;

/// <summary>
/// Represents a non-player character
/// </summary>
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

/// <summary>
/// NPC state machine states
/// </summary>
public enum NpcState
{
    Idle,
    Wandering,
    Hostile,
    Friendly,
    Fleeing,
    Dead,
    Sleeping,
    Trading
}

/// <summary>
/// Types of NPC behavior patterns
/// </summary>
public enum NpcBehaviorType
{
    Stationary,      // Stays in one room
    Wandering,       // Moves randomly
    Patrolling,      // Follows a path
    Hunting,         // Seeks player
    Fleeing,         // Avoids player
    Scripted         // Follows scripted events
}

/// <summary>
/// Base class for NPCs (stub implementation)
/// TODO: Implement full AI behaviors, state machines, and interactions
/// </summary>
public abstract class NpcBase : CombatantBase, INpc
{
    public string CurrentRoomId { get; set; } = string.Empty;
    public NpcState State { get; set; } = NpcState.Idle;
    public abstract NpcBehaviorType BehaviorType { get; }
    public List<string> Inventory { get; set; } = new();
    public virtual bool CanTalk => false;

    protected NpcBase(int maxHealth, int attackPower, int defense)
        : base(maxHealth, attackPower, defense)
    {
    }

    public virtual void OnPlayerEnter(IGameState gameState, IWorld world)
    {
        // TODO: Implement NPC reaction to player entering room
        // - Thief steals items
        // - Troll attacks
        // - Friendly NPC greets
    }

    public virtual void OnPlayerLeave(IGameState gameState, IWorld world)
    {
        // TODO: Implement NPC reaction to player leaving
    }

    public virtual void OnTurn(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // TODO: Implement NPC turn logic
        // - Execute current behavior
        // - Update state
        // - Perform actions
    }

    public virtual string? GetDialogue(string topic)
    {
        // TODO: Implement dialogue system
        return null;
    }

    protected virtual void Move(IWorld world, string newRoomId, IOutputWriter output)
    {
        // TODO: Implement NPC movement
        var oldRoom = world.GetRoom(CurrentRoomId);
        var newRoom = world.GetRoom(newRoomId);

        if (newRoom != null)
        {
            // Remove from old room
            oldRoom?.Items.Remove(Id);

            // Add to new room
            CurrentRoomId = newRoomId;
            // NPCs would need to be tracked differently than items
            // This is a simplification
        }
    }

    protected virtual Direction? ChooseDirection(IWorld world, IGameState gameState)
    {
        // TODO: Implement pathfinding/direction choice based on behavior
        return null;
    }
}

/// <summary>
/// NPC manager to handle all NPCs in the game
/// </summary>
public interface INpcManager
{
    void RegisterNpc(INpc npc);
    void RemoveNpc(string npcId);
    INpc? GetNpc(string npcId);
    List<INpc> GetNpcsInRoom(string roomId);
    void OnPlayerEnterRoom(string roomId, IGameState gameState, IWorld world);
    void OnPlayerLeaveRoom(string roomId, IGameState gameState, IWorld world);
    void ProcessNpcTurns(IGameState gameState, IWorld world, IOutputWriter output);
}

/// <summary>
/// Manages all NPCs in the game world
/// TODO: Implement NPC lifecycle, spawning, and coordination
/// </summary>
public class NpcManager : INpcManager
{
    private readonly Dictionary<string, INpc> _npcs = new();
    private readonly IOutputWriter _output;

    public NpcManager(IOutputWriter output)
    {
        _output = output;
    }

    public void RegisterNpc(INpc npc)
    {
        _npcs[npc.Id] = npc;
    }

    public void RemoveNpc(string npcId)
    {
        _npcs.Remove(npcId);
    }

    public INpc? GetNpc(string npcId)
    {
        _npcs.TryGetValue(npcId, out var npc);
        return npc;
    }

    public List<INpc> GetNpcsInRoom(string roomId)
    {
        return _npcs.Values
            .Where(npc => npc.CurrentRoomId == roomId && npc.IsAlive)
            .ToList();
    }

    public void OnPlayerEnterRoom(string roomId, IGameState gameState, IWorld world)
    {
        var npcs = GetNpcsInRoom(roomId);
        foreach (var npc in npcs)
        {
            npc.OnPlayerEnter(gameState, world);
        }
    }

    public void OnPlayerLeaveRoom(string roomId, IGameState gameState, IWorld world)
    {
        var npcs = GetNpcsInRoom(roomId);
        foreach (var npc in npcs)
        {
            npc.OnPlayerLeave(gameState, world);
        }
    }

    public void ProcessNpcTurns(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // TODO: Process each NPC's turn
        // - Execute behavior
        // - Move NPCs
        // - Handle NPC actions
        foreach (var npc in _npcs.Values.Where(n => n.IsAlive))
        {
            npc.OnTurn(gameState, world, output);
        }
    }
}
