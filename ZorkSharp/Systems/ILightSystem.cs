namespace ZorkSharp.Systems;

using ZorkSharp.Core;
using ZorkSharp.World;
using ZorkSharp.Events;

/// <summary>
/// Manages lighting and light sources in the game
/// </summary>
public interface ILightSystem
{
    bool IsRoomLit(string roomId, IWorld world);
    bool HasActiveLightSource(IWorld world);
    List<ILightSource> GetActiveLightSources(IWorld world);
    void TurnOnLight(ILightSource lightSource);
    void TurnOffLight(ILightSource lightSource);
    void ToggleLight(ILightSource lightSource);
    int GetTurnsInDarkness(IGameState gameState);
    void IncrementDarknessCounter(IGameState gameState);
    void ResetDarknessCounter(IGameState gameState);
}

/// <summary>
/// Represents a light source
/// </summary>
public interface ILightSource
{
    string Id { get; }
    string Name { get; }
    bool IsLit { get; set; }
    bool IsBatteryPowered { get; }
    int BatteryLife { get; set; }
    int MaxBatteryLife { get; }
    bool IsDepleted { get; }
    LightSourceType Type { get; }

    void ConsumeBattery(int turns);
    void Recharge(int amount);
    bool CanBeLit();
    string GetStatusMessage();
}

/// <summary>
/// Types of light sources
/// </summary>
public enum LightSourceType
{
    Lamp,           // Battery powered
    Torch,          // Burns out
    Candle,         // Burns out
    Matches,        // Single use
    Permanent       // Never runs out (room lighting)
}

/// <summary>
/// Light source implementation (stub - not fully implemented)
/// TODO: Implement battery depletion, warnings, and light source management
/// </summary>
public class LightSource : ILightSource
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsLit { get; set; }
    public bool IsBatteryPowered { get; set; }
    public int BatteryLife { get; set; }
    public int MaxBatteryLife { get; set; }
    public bool IsDepleted => BatteryLife <= 0;
    public LightSourceType Type { get; set; }

    public void ConsumeBattery(int turns)
    {
        if (IsBatteryPowered && IsLit)
        {
            BatteryLife = Math.Max(0, BatteryLife - turns);

            if (IsDepleted)
            {
                IsLit = false;
            }
        }
    }

    public void Recharge(int amount)
    {
        if (IsBatteryPowered)
        {
            BatteryLife = Math.Min(MaxBatteryLife, BatteryLife + amount);
        }
    }

    public bool CanBeLit()
    {
        // Can't light if depleted
        if (IsDepleted) return false;

        // Matches can only be lit once
        if (Type == LightSourceType.Matches && BatteryLife < MaxBatteryLife)
            return false;

        return true;
    }

    public string GetStatusMessage()
    {
        if (!IsBatteryPowered)
        {
            return IsLit ? $"The {Name} is lit." : $"The {Name} is not lit.";
        }

        if (IsDepleted)
        {
            return $"The {Name} is dead.";
        }

        if (BatteryLife < 10)
        {
            return $"The {Name} is almost out of power! ({BatteryLife} turns remaining)";
        }

        if (BatteryLife < 50)
        {
            return $"The {Name} is getting dim. ({BatteryLife} turns remaining)";
        }

        return IsLit ? $"The {Name} is on." : $"The {Name} is off.";
    }
}

/// <summary>
/// Light system implementation (stub - not fully implemented)
/// TODO: Implement full light management, darkness tracking, and warnings
/// </summary>
public class LightSystem : ILightSystem
{
    private readonly Dictionary<string, int> _darknessCounters = new();

    public bool IsRoomLit(string roomId, IWorld world)
    {
        var room = world.GetRoom(roomId);
        if (room == null) return false;

        // Room itself is lit
        if (room.IsLit) return true;

        // Check for light sources in the room or inventory
        return HasActiveLightSource(world);
    }

    public bool HasActiveLightSource(IWorld world)
    {
        var lightSources = GetActiveLightSources(world);
        return lightSources.Any(ls => ls.IsLit && !ls.IsDepleted);
    }

    public List<ILightSource> GetActiveLightSources(IWorld world)
    {
        // TODO: Implement finding all light sources
        // - Check player inventory for lights
        // - Check current room for lights
        // - Return all active light sources

        var lightSources = new List<ILightSource>();

        // This is a stub - would need to scan inventory and room for light sources
        // and convert IGameObject to ILightSource where applicable

        return lightSources;
    }

    public void TurnOnLight(ILightSource lightSource)
    {
        if (lightSource.CanBeLit())
        {
            lightSource.IsLit = true;
        }
    }

    public void TurnOffLight(ILightSource lightSource)
    {
        lightSource.IsLit = false;
    }

    public void ToggleLight(ILightSource lightSource)
    {
        if (lightSource.IsLit)
        {
            TurnOffLight(lightSource);
        }
        else
        {
            TurnOnLight(lightSource);
        }
    }

    public int GetTurnsInDarkness(IGameState gameState)
    {
        _darknessCounters.TryGetValue("player", out int turns);
        return turns;
    }

    public void IncrementDarknessCounter(IGameState gameState)
    {
        if (!_darknessCounters.ContainsKey("player"))
        {
            _darknessCounters["player"] = 0;
        }
        _darknessCounters["player"]++;
    }

    public void ResetDarknessCounter(IGameState gameState)
    {
        _darknessCounters["player"] = 0;
    }
}

/// <summary>
/// Clock event for lamp battery depletion
/// TODO: Implement battery consumption over time
/// </summary>
public class LampBatteryEvent : ClockEventBase
{
    private readonly ILightSystem _lightSystem;
    private readonly IWorld _world;
    private bool _warningGiven = false;
    private bool _finalWarningGiven = false;

    public LampBatteryEvent(ILightSystem lightSystem, IWorld world)
    {
        _lightSystem = lightSystem;
        _world = world;
        Id = "LAMP-BATTERY";
        Interval = 1; // Every turn
    }

    public override void Execute(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // TODO: Implement battery depletion
        // - Get all active light sources
        // - Consume battery for each
        // - Give warnings at certain thresholds
        // - Turn off when depleted

        var lightSources = _lightSystem.GetActiveLightSources(world);

        foreach (var lightSource in lightSources.Where(ls => ls.IsLit))
        {
            lightSource.ConsumeBattery(1);

            // Give warnings
            if (lightSource.BatteryLife == 10 && !_finalWarningGiven)
            {
                output.WriteLine("The lamp is getting very dim. It won't last much longer!");
                _finalWarningGiven = true;
            }
            else if (lightSource.BatteryLife == 50 && !_warningGiven)
            {
                output.WriteLine("The lamp is getting dim.");
                _warningGiven = true;
            }
            else if (lightSource.IsDepleted)
            {
                output.WriteLine($"The {lightSource.Name} has gone out!");
                _warningGiven = false;
                _finalWarningGiven = false;
            }
        }
    }
}

/// <summary>
/// Clock event for darkness threat (Grue warning)
/// TODO: Implement progressive Grue warnings
/// </summary>
public class DarknessWarningEvent : ClockEventBase
{
    private readonly ILightSystem _lightSystem;

    public DarknessWarningEvent(ILightSystem lightSystem)
    {
        _lightSystem = lightSystem;
        Id = "DARKNESS-WARNING";
        Interval = 1; // Every turn
    }

    public override void Execute(IGameState gameState, IWorld world, IOutputWriter output)
    {
        // TODO: Implement darkness warnings
        // - Check if in darkness
        // - Track turns in darkness
        // - Give progressive warnings
        // - Trigger death after too many turns

        if (!_lightSystem.IsRoomLit(gameState.CurrentRoomId, world))
        {
            _lightSystem.IncrementDarknessCounter(gameState);
            int turnsInDarkness = _lightSystem.GetTurnsInDarkness(gameState);

            // Progressive warnings
            if (turnsInDarkness == 1)
            {
                output.WriteLine("It is pitch black. You are likely to be eaten by a grue.");
            }
            else if (turnsInDarkness == 2)
            {
                output.WriteLine("You hear a lurking grue in the darkness...");
            }
            else if (turnsInDarkness >= 3)
            {
                // Death would be handled by GameConditions
                output.WriteLine("The grue strikes!");
            }
        }
        else
        {
            _lightSystem.ResetDarknessCounter(gameState);
        }
    }
}
