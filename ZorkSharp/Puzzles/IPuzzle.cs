namespace ZorkSharp.Puzzles;

using ZorkSharp.Core;
using ZorkSharp.World;
using ZorkSharp.Parser;

/// <summary>
/// Represents a puzzle in the game
/// </summary>
public interface IPuzzle
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    PuzzleState State { get; set; }
    int PointValue { get; }

    bool CanAttempt(IGameState gameState, IWorld world);
    PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world);
    void Reset();
    string GetHint(int hintLevel);
}

/// <summary>
/// Puzzle states
/// </summary>
public enum PuzzleState
{
    NotStarted,
    InProgress,
    Solved,
    Failed,
    Locked
}

/// <summary>
/// Result of a puzzle attempt
/// </summary>
public record PuzzleResult(
    bool Success,
    bool PuzzleSolved,
    string Message,
    int PointsAwarded = 0
);

/// <summary>
/// Base class for puzzles (stub for future implementation)
/// TODO: Implement specific puzzle logic for each puzzle in the game
/// </summary>
public abstract class PuzzleBase : IPuzzle
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PuzzleState State { get; set; } = PuzzleState.NotStarted;
    public int PointValue { get; set; }

    protected List<string> Hints { get; set; } = new();

    public virtual bool CanAttempt(IGameState gameState, IWorld world)
    {
        // Default: can attempt if not already solved
        return State != PuzzleState.Solved;
    }

    public abstract PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world);

    public virtual void Reset()
    {
        State = PuzzleState.NotStarted;
    }

    public virtual string GetHint(int hintLevel)
    {
        if (hintLevel >= 0 && hintLevel < Hints.Count)
        {
            return Hints[hintLevel];
        }
        return "No more hints available.";
    }

    protected PuzzleResult Success(string message, bool solved = true, int points = 0)
    {
        if (solved)
        {
            State = PuzzleState.Solved;
        }
        return new PuzzleResult(true, solved, message, points);
    }

    protected PuzzleResult Failure(string message)
    {
        return new PuzzleResult(false, false, message);
    }
}

/// <summary>
/// Manages all puzzles in the game
/// </summary>
public interface IPuzzleManager
{
    void RegisterPuzzle(IPuzzle puzzle);
    IPuzzle? GetPuzzle(string puzzleId);
    List<IPuzzle> GetPuzzlesInRoom(string roomId);
    List<IPuzzle> GetSolvedPuzzles();
    List<IPuzzle> GetUnsolvedPuzzles();
    int GetTotalPuzzlePoints();
    int GetEarnedPuzzlePoints();
}

/// <summary>
/// Puzzle manager implementation (stub - not fully implemented)
/// TODO: Implement puzzle tracking, hints, and coordination
/// </summary>
public class PuzzleManager : IPuzzleManager
{
    private readonly Dictionary<string, IPuzzle> _puzzles = new();
    private readonly Dictionary<string, List<string>> _roomPuzzles = new();

    public void RegisterPuzzle(IPuzzle puzzle)
    {
        _puzzles[puzzle.Id] = puzzle;
    }

    public void RegisterPuzzleForRoom(string puzzleId, string roomId)
    {
        if (!_roomPuzzles.ContainsKey(roomId))
        {
            _roomPuzzles[roomId] = new List<string>();
        }
        _roomPuzzles[roomId].Add(puzzleId);
    }

    public IPuzzle? GetPuzzle(string puzzleId)
    {
        _puzzles.TryGetValue(puzzleId, out var puzzle);
        return puzzle;
    }

    public List<IPuzzle> GetPuzzlesInRoom(string roomId)
    {
        if (_roomPuzzles.TryGetValue(roomId, out var puzzleIds))
        {
            return puzzleIds
                .Select(id => GetPuzzle(id))
                .Where(p => p != null)
                .Cast<IPuzzle>()
                .ToList();
        }
        return new List<IPuzzle>();
    }

    public List<IPuzzle> GetSolvedPuzzles()
    {
        return _puzzles.Values
            .Where(p => p.State == PuzzleState.Solved)
            .ToList();
    }

    public List<IPuzzle> GetUnsolvedPuzzles()
    {
        return _puzzles.Values
            .Where(p => p.State != PuzzleState.Solved)
            .ToList();
    }

    public int GetTotalPuzzlePoints()
    {
        return _puzzles.Values.Sum(p => p.PointValue);
    }

    public int GetEarnedPuzzlePoints()
    {
        return _puzzles.Values
            .Where(p => p.State == PuzzleState.Solved)
            .Sum(p => p.PointValue);
    }
}

// Example puzzle implementations (stubs to show the pattern)

/// <summary>
/// Trap door puzzle (under the rug in living room)
/// TODO: Implement full puzzle logic
/// </summary>
public class TrapDoorPuzzle : PuzzleBase
{
    public TrapDoorPuzzle()
    {
        Id = "TRAP-DOOR-PUZZLE";
        Name = "Trap Door";
        Description = "A trap door hidden under the oriental rug";
        PointValue = 5;

        Hints.Add("Have you examined everything in the room carefully?");
        Hints.Add("What might be under the rug?");
        Hints.Add("Try moving the rug to see what's underneath.");
    }

    public override PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world)
    {
        // TODO: Implement puzzle logic
        // - Check if rug has been moved
        // - Check if trap door is revealed
        // - Check if player has key
        // - Open trap door to cellar

        return Failure("The trap door puzzle is not yet implemented.");
    }
}

/// <summary>
/// Trophy case puzzle (deposit treasures for points)
/// TODO: Implement full puzzle logic
/// </summary>
public class TrophyCasePuzzle : PuzzleBase
{
    private readonly HashSet<string> _depositedTreasures = new();

    public TrophyCasePuzzle()
    {
        Id = "TROPHY-CASE-PUZZLE";
        Name = "Trophy Case";
        Description = "Place all treasures in the trophy case";
        PointValue = 290; // Total for all treasures

        Hints.Add("Have you found any treasures?");
        Hints.Add("Try putting valuable items in the trophy case.");
        Hints.Add("Each treasure is worth points when placed in the case.");
    }

    public override PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world)
    {
        // TODO: Implement puzzle logic
        // - Check if object is a treasure
        // - Add to deposited treasures
        // - Award points
        // - Check if all treasures collected

        return Failure("The trophy case puzzle is not yet implemented.");
    }
}

/// <summary>
/// Dam puzzle (flood control)
/// TODO: Implement full puzzle logic
/// </summary>
public class DamPuzzle : PuzzleBase
{
    public DamPuzzle()
    {
        Id = "DAM-PUZZLE";
        Name = "Dam Control";
        Description = "Control the dam to reveal new areas";
        PointValue = 10;

        Hints.Add("There's a control panel at the dam.");
        Hints.Add("Try operating the controls.");
        Hints.Add("Emptying the reservoir might reveal something useful.");
    }

    public override PuzzleResult Attempt(ParsedCommand command, IGameState gameState, IWorld world)
    {
        // TODO: Implement puzzle logic
        // - Check if at dam controls
        // - Open/close sluice gates
        // - Drain reservoir
        // - Reveal path to new area

        return Failure("The dam puzzle is not yet implemented.");
    }
}
