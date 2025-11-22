namespace ZorkSharp.Parser;

using ZorkSharp.World;

/// <summary>
/// Advanced parser features beyond basic parsing
/// </summary>
public interface IParserExtensions
{
    void SetPronounReference(string pronoun, string objectName);
    string? ResolvePronoun(string pronoun);
    List<IGameObject> ResolveMultiObject(string objectSpec, IWorld world, string currentRoomId);
    DisambiguationResult Disambiguate(string objectName, List<IGameObject> candidates);
    void RecordCommand(string command);
    string? GetLastCommand();
}

/// <summary>
/// Result of disambiguation
/// </summary>
public record DisambiguationResult(
    bool NeedsDisambiguation,
    IGameObject? SelectedObject,
    List<IGameObject> Candidates,
    string? Question
);

/// <summary>
/// Command history for AGAIN/OOPS support
/// </summary>
public interface ICommandHistory
{
    void AddCommand(string command);
    string? GetPreviousCommand();
    string? GetLastWord();
    void ReplaceLastWord(string newWord);
    List<string> GetHistory(int count = 10);
    void Clear();
}

/// <summary>
/// Parser extensions implementation (stub - not fully implemented)
/// TODO: Implement pronoun resolution, multi-object handling, and disambiguation
/// </summary>
public class ParserExtensions : IParserExtensions
{
    private readonly Dictionary<string, string> _pronounReferences = new();
    private readonly ICommandHistory _commandHistory;

    public ParserExtensions(ICommandHistory commandHistory)
    {
        _commandHistory = commandHistory;
    }

    public void SetPronounReference(string pronoun, string objectName)
    {
        // TODO: Implement pronoun tracking
        // - "it" refers to last mentioned singular object
        // - "them" refers to last mentioned plural objects
        // - Track gender-specific pronouns if needed

        _pronounReferences[pronoun.ToLowerInvariant()] = objectName;
    }

    public string? ResolvePronoun(string pronoun)
    {
        // TODO: Implement pronoun resolution
        // - Look up pronoun in reference dictionary
        // - Return the object it refers to
        // - Return null if no reference

        _pronounReferences.TryGetValue(pronoun.ToLowerInvariant(), out var reference);
        return reference;
    }

    public List<IGameObject> ResolveMultiObject(string objectSpec, IWorld world, string currentRoomId)
    {
        // TODO: Implement multi-object resolution
        // - "all" or "everything": all takeable objects in room
        // - "all except X": all except specified object
        // - "all [type]": all of a certain type (e.g., "all treasures")

        var results = new List<IGameObject>();

        if (objectSpec.Equals("all", StringComparison.OrdinalIgnoreCase) ||
            objectSpec.Equals("everything", StringComparison.OrdinalIgnoreCase))
        {
            // TODO: Get all takeable objects in current room
            results.AddRange(world.GetVisibleObjectsInRoom(currentRoomId)
                .Where(obj => obj.IsTakeable));
        }
        else if (objectSpec.StartsWith("all except", StringComparison.OrdinalIgnoreCase))
        {
            // TODO: Get all except specified
            // Parse the exception
            // Get all objects
            // Remove the exception
        }

        return results;
    }

    public DisambiguationResult Disambiguate(string objectName, List<IGameObject> candidates)
    {
        // TODO: Implement disambiguation logic
        // - If only one candidate, return it
        // - If multiple candidates, ask which one
        // - Generate appropriate question
        // - Prefer visible objects over inventory
        // - Prefer nearby objects over distant

        if (candidates.Count == 0)
        {
            return new DisambiguationResult(false, null, candidates, null);
        }

        if (candidates.Count == 1)
        {
            return new DisambiguationResult(false, candidates[0], candidates, null);
        }

        // Multiple candidates - need disambiguation
        var candidateNames = candidates.Select(c => c.Name).ToList();
        string question = $"Which {objectName} do you mean: {string.Join(", ", candidateNames)}?";

        return new DisambiguationResult(true, null, candidates, question);
    }

    public void RecordCommand(string command)
    {
        _commandHistory.AddCommand(command);
    }

    public string? GetLastCommand()
    {
        return _commandHistory.GetPreviousCommand();
    }
}

/// <summary>
/// Command history implementation (stub - not fully implemented)
/// TODO: Implement full command history with AGAIN and OOPS support
/// </summary>
public class CommandHistory : ICommandHistory
{
    private readonly List<string> _history = new();
    private const int MaxHistorySize = 100;

    public void AddCommand(string command)
    {
        _history.Add(command);

        // Keep history size manageable
        if (_history.Count > MaxHistorySize)
        {
            _history.RemoveAt(0);
        }
    }

    public string? GetPreviousCommand()
    {
        return _history.Count > 0 ? _history[^1] : null;
    }

    public string? GetLastWord()
    {
        var lastCommand = GetPreviousCommand();
        if (lastCommand == null) return null;

        var words = lastCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return words.Length > 0 ? words[^1] : null;
    }

    public void ReplaceLastWord(string newWord)
    {
        // TODO: Implement OOPS command support
        // - Get last command
        // - Replace last word with new word
        // - Update history

        if (_history.Count == 0) return;

        var lastCommand = _history[^1];
        var words = lastCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        if (words.Count > 0)
        {
            words[^1] = newWord;
            _history[^1] = string.Join(' ', words);
        }
    }

    public List<string> GetHistory(int count = 10)
    {
        int startIndex = Math.Max(0, _history.Count - count);
        return _history.Skip(startIndex).ToList();
    }

    public void Clear()
    {
        _history.Clear();
    }
}

/// <summary>
/// Smart object matcher with fuzzy matching and context awareness
/// TODO: Implement fuzzy matching, spelling correction, and context-aware matching
/// </summary>
public interface IObjectMatcher
{
    List<IGameObject> FindMatches(string objectName, IWorld world, string currentRoomId, bool includeInventory = true);
    IGameObject? FindBestMatch(string objectName, IWorld world, string currentRoomId, bool preferVisible = true);
    int CalculateMatchScore(string input, IGameObject obj);
}

/// <summary>
/// Object matcher implementation (stub - not fully implemented)
/// TODO: Implement fuzzy matching and smart object resolution
/// </summary>
public class ObjectMatcher : IObjectMatcher
{
    public List<IGameObject> FindMatches(string objectName, IWorld world, string currentRoomId, bool includeInventory = true)
    {
        // TODO: Implement smart matching
        // - Exact name matches
        // - Synonym matches
        // - Partial matches
        // - Fuzzy matches (typo tolerance)
        // - Adjective + noun combinations

        var matches = new List<IGameObject>();

        // Search in current room
        var roomObjects = world.GetVisibleObjectsInRoom(currentRoomId);
        matches.AddRange(roomObjects.Where(obj => MatchesName(obj, objectName)));

        // Search in inventory
        if (includeInventory)
        {
            var inventoryObjects = world.PlayerInventory.GetAllItems();
            matches.AddRange(inventoryObjects.Where(obj => MatchesName(obj, objectName)));
        }

        return matches;
    }

    public IGameObject? FindBestMatch(string objectName, IWorld world, string currentRoomId, bool preferVisible = true)
    {
        // TODO: Implement best match logic
        // - Score each candidate
        // - Prefer exact matches over partial
        // - Prefer visible objects if preferVisible is true
        // - Return highest scoring match

        var matches = FindMatches(objectName, world, currentRoomId);

        if (matches.Count == 0) return null;
        if (matches.Count == 1) return matches[0];

        // If multiple matches, prefer visible objects in room
        if (preferVisible)
        {
            var visibleMatch = matches.FirstOrDefault(m =>
                world.GetVisibleObjectsInRoom(currentRoomId).Contains(m));
            if (visibleMatch != null) return visibleMatch;
        }

        return matches[0];
    }

    public int CalculateMatchScore(string input, IGameObject obj)
    {
        // TODO: Implement scoring algorithm
        // - Exact match: 100 points
        // - Exact synonym match: 90 points
        // - Partial match: 50 points
        // - Fuzzy match: 30 points
        // - Adjective match bonus: +10 points

        return 0; // Stub
    }

    private bool MatchesName(IGameObject obj, string objectName)
    {
        // Simple matching - TODO: enhance with fuzzy matching
        if (obj is World.GameObject gameObj)
        {
            return gameObj.MatchesName(objectName);
        }

        return obj.Name.Equals(objectName, StringComparison.OrdinalIgnoreCase);
    }
}
