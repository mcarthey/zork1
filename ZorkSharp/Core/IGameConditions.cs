namespace ZorkSharp.Core;

using ZorkSharp.World;

/// <summary>
/// Game ending types
/// </summary>
public enum GameEndingType
{
    None,
    Victory,
    Death,
    Quit,
    Restart
}

/// <summary>
/// Death causes
/// </summary>
public enum DeathCause
{
    None,
    EatenByGrue,
    KilledByThief,
    KilledByTroll,
    KilledByCyclops,
    FellIntoChasm,
    Drowned,
    Burned,
    Crushed,
    Poisoned,
    Starved,
    Other
}

/// <summary>
/// Manages game ending conditions (death and victory)
/// </summary>
public interface IGameConditions
{
    bool CheckVictoryCondition(IGameState gameState, IWorld world);
    DeathCause? CheckDeathConditions(IGameState gameState, IWorld world);
    void TriggerDeath(DeathCause cause, IGameState gameState, IOutputWriter output);
    void TriggerVictory(IGameState gameState, IOutputWriter output);
    GameEndingType CurrentEnding { get; }
}

/// <summary>
/// Victory requirements
/// </summary>
public interface IVictoryCondition
{
    string Id { get; }
    string Description { get; }
    bool IsMet(IGameState gameState, IWorld world);
    int PointValue { get; }
}

/// <summary>
/// Death condition checker
/// </summary>
public interface IDeathCondition
{
    string Id { get; }
    DeathCause Cause { get; }
    bool Check(IGameState gameState, IWorld world);
    string GetDeathMessage();
}

/// <summary>
/// Game conditions manager (stub - not fully implemented)
/// TODO: Implement all victory and death conditions
/// </summary>
public class GameConditions : IGameConditions
{
    private readonly IOutputWriter _output;
    private readonly List<IVictoryCondition> _victoryConditions = new();
    private readonly List<IDeathCondition> _deathConditions = new();

    public GameEndingType CurrentEnding { get; private set; } = GameEndingType.None;

    public GameConditions(IOutputWriter output)
    {
        _output = output;
        InitializeConditions();
    }

    private void InitializeConditions()
    {
        // TODO: Register all victory conditions
        // _victoryConditions.Add(new AllTreasuresCollectedCondition());

        // TODO: Register all death conditions
        // _deathConditions.Add(new GrueDeathCondition());
        // _deathConditions.Add(new ChasmDeathCondition());
        // etc.
    }

    public bool CheckVictoryCondition(IGameState gameState, IWorld world)
    {
        // TODO: Implement victory checking
        // - All treasures in trophy case?
        // - All major puzzles solved?
        // - Required score reached?

        foreach (var condition in _victoryConditions)
        {
            if (!condition.IsMet(gameState, world))
            {
                return false;
            }
        }

        // If no conditions registered, not victorious
        return _victoryConditions.Count > 0;
    }

    public DeathCause? CheckDeathConditions(IGameState gameState, IWorld world)
    {
        // TODO: Implement death checking
        // - In darkness too long? (Grue)
        // - In water without boat? (Drowned)
        // - In volcano? (Burned)
        // - Fell into chasm? (Fell)

        foreach (var condition in _deathConditions)
        {
            if (condition.Check(gameState, world))
            {
                return condition.Cause;
            }
        }

        return null;
    }

    public void TriggerDeath(DeathCause cause, IGameState gameState, IOutputWriter output)
    {
        CurrentEnding = GameEndingType.Death;

        // Find the specific death condition to get custom message
        var deathCondition = _deathConditions.FirstOrDefault(d => d.Cause == cause);
        string message = deathCondition?.GetDeathMessage() ?? GetGenericDeathMessage(cause);

        output.WriteLine("");
        output.WriteLine("****  You have died  ****");
        output.WriteLine("");
        output.WriteLine(message);
        output.WriteLine("");
        output.WriteLine($"You scored {gameState.Score} out of a possible {gameState.MaxScore}, in {gameState.Moves} moves.");
        output.WriteLine("");
        output.WriteLine("Would you like to RESTART, RESTORE a saved game, or QUIT?");
    }

    public void TriggerVictory(IGameState gameState, IOutputWriter output)
    {
        CurrentEnding = GameEndingType.Victory;
        gameState.HasWon = true;

        output.WriteLine("");
        output.WriteLine("****  You have won  ****");
        output.WriteLine("");
        output.WriteLine("Congratulations! You have completed Zork I: The Great Underground Empire!");
        output.WriteLine("");
        output.WriteLine($"Your score is {gameState.Score} out of {gameState.MaxScore}, in {gameState.Moves} moves.");
        output.WriteLine("");
        output.WriteLine(GetRankMessage(gameState.Score));
        output.WriteLine("");
        output.WriteLine("Thank you for playing!");
    }

    private string GetGenericDeathMessage(DeathCause cause)
    {
        return cause switch
        {
            DeathCause.EatenByGrue => "You have been eaten by a grue. It appears that the grue had been without food for quite some time, and you looked tasty.",
            DeathCause.KilledByThief => "The thief's blade finds its mark. Your adventure ends here.",
            DeathCause.KilledByTroll => "The troll's mighty blow strikes you down. Your last thought is that you should have brought a bigger weapon.",
            DeathCause.KilledByCyclops => "The Cyclops crushes you beneath his mighty foot. You should have been more clever.",
            DeathCause.FellIntoChasm => "You fall into the chasm. As you plummet into darkness, you reflect that you should have been more careful.",
            DeathCause.Drowned => "You are unable to swim and sink beneath the water. Your adventure ends here.",
            DeathCause.Burned => "The flames consume you. Perhaps entering a volcano wasn't the best idea.",
            DeathCause.Crushed => "You have been crushed. It appears that gravity still works.",
            DeathCause.Poisoned => "The poison courses through your veins. You should not have consumed that.",
            DeathCause.Starved => "You have died of starvation. Perhaps you should have eaten something.",
            _ => "You have died. Your adventure ends here."
        };
    }

    private string GetRankMessage(int score)
    {
        return score switch
        {
            >= 350 => "You are a Master Adventurer! You have achieved the highest rank possible!",
            >= 300 => "You are an Adventurer! You have proven your worth in the Great Underground Empire.",
            >= 200 => "You are a Junior Adventurer. You have shown promise in your exploration.",
            >= 100 => "You are a Novice Adventurer. You have taken your first steps into the world of adventure.",
            >= 50 => "You are a Beginner. You have much to learn about the Great Underground Empire.",
            _ => "You are a beginner. Keep exploring to improve your rank!"
        };
    }
}

// Example condition implementations (stubs)

/// <summary>
/// Victory condition: All treasures in trophy case
/// TODO: Implement full logic
/// </summary>
public class AllTreasuresCollectedCondition : IVictoryCondition
{
    public string Id => "ALL-TREASURES";
    public string Description => "All treasures must be placed in the trophy case";
    public int PointValue => 290;

    public bool IsMet(IGameState gameState, IWorld world)
    {
        // TODO: Implement checking
        // - Get trophy case object
        // - Check if all 20 treasures are inside
        // - Return true if complete

        return false; // Stub
    }
}

/// <summary>
/// Death condition: Eaten by Grue in darkness
/// TODO: Implement full logic
/// </summary>
public class GrueDeathCondition : IDeathCondition
{
    public string Id => "GRUE-DEATH";
    public DeathCause Cause => DeathCause.EatenByGrue;

    private int _turnsInDarkness = 0;
    private const int MaxTurnsInDarkness = 3;

    public bool Check(IGameState gameState, IWorld world)
    {
        // TODO: Implement checking
        // - Is room dark?
        // - Does player have light source?
        // - Increment turns in darkness
        // - Return true if too many turns

        if (gameState.IsInDarkness)
        {
            _turnsInDarkness++;
            return _turnsInDarkness >= MaxTurnsInDarkness;
        }

        _turnsInDarkness = 0;
        return false;
    }

    public string GetDeathMessage()
    {
        return "Oh no! You have walked into the slavering fangs of a lurking grue!\n\n" +
               "The grue is a sinister, lurking presence in the dark places of the earth. " +
               "Its favorite diet is adventurers, but its insatiable appetite is tempered by its fear of light. " +
               "No grue has ever been seen by the light of day, and few have survived its fearsome jaws to tell the tale.";
    }
}

/// <summary>
/// Death condition: Fell into chasm
/// TODO: Implement full logic
/// </summary>
public class ChasmDeathCondition : IDeathCondition
{
    public string Id => "CHASM-DEATH";
    public DeathCause Cause => DeathCause.FellIntoChasm;

    public bool Check(IGameState gameState, IWorld world)
    {
        // TODO: Implement checking
        // - Check if at chasm edge
        // - Check if player tried to cross without rope
        // - Return true if they fell

        return false; // Stub
    }

    public string GetDeathMessage()
    {
        return "You have fallen into the chasm. As you plummet through the darkness, " +
               "you reflect that it was a long way down. Your adventure ends abruptly at the bottom.";
    }
}
