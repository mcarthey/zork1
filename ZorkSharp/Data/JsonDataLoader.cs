namespace ZorkSharp.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using ZorkSharp.Core;
using ZorkSharp.World;
using Microsoft.Extensions.Logging;

/// <summary>
/// JSON data models for deserialization
/// </summary>
public class RoomData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("longDescription")]
    public string LongDescription { get; set; } = string.Empty;

    [JsonPropertyName("flags")]
    public List<string> Flags { get; set; } = new();

    [JsonPropertyName("exits")]
    public Dictionary<string, JsonElement> Exits { get; set; } = new();

    [JsonPropertyName("items")]
    public List<string> Items { get; set; } = new();

    [JsonPropertyName("globalItems")]
    public List<string> GlobalItems { get; set; } = new();

    [JsonPropertyName("actionHandler")]
    public string? ActionHandler { get; set; }
}

public class ObjectData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("synonyms")]
    public List<string> Synonyms { get; set; } = new();

    [JsonPropertyName("adjectives")]
    public List<string> Adjectives { get; set; } = new();

    [JsonPropertyName("flags")]
    public List<string> Flags { get; set; } = new();

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("contents")]
    public List<string> Contents { get; set; } = new();

    [JsonPropertyName("actionHandler")]
    public string? ActionHandler { get; set; }
}

public class RoomDataFile
{
    [JsonPropertyName("rooms")]
    public List<RoomData> Rooms { get; set; } = new();
}

public class ObjectDataFile
{
    [JsonPropertyName("objects")]
    public List<ObjectData> Objects { get; set; } = new();
}

/// <summary>
/// Loads game data from JSON files
/// </summary>
public interface IDataLoader
{
    void LoadRooms(GameWorld world, string filePath);
    void LoadObjects(GameWorld world, string filePath);
}

/// <summary>
/// JSON data loader implementation
/// </summary>
public class JsonDataLoader : IDataLoader
{
    private readonly ILogger<JsonDataLoader> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonDataLoader(ILogger<JsonDataLoader> logger)
    {
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
    }

    public void LoadRooms(GameWorld world, string filePath)
    {
        try
        {
            _logger.LogInformation("Loading rooms from {FilePath}", filePath);

            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<RoomDataFile>(json, _jsonOptions);

            if (data == null || data.Rooms == null)
            {
                _logger.LogWarning("No room data found in {FilePath}", filePath);
                return;
            }

            foreach (var roomData in data.Rooms)
            {
                var room = ConvertToRoom(roomData);
                world.AddRoom(room);
                _logger.LogDebug("Loaded room: {RoomId} - {RoomName}", room.Id, room.Name);
            }

            _logger.LogInformation("Loaded {Count} rooms from {FilePath}", data.Rooms.Count, filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading rooms from {FilePath}", filePath);
            throw;
        }
    }

    public void LoadObjects(GameWorld world, string filePath)
    {
        try
        {
            _logger.LogInformation("Loading objects from {FilePath}", filePath);

            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<ObjectDataFile>(json, _jsonOptions);

            if (data == null || data.Objects == null)
            {
                _logger.LogWarning("No object data found in {FilePath}", filePath);
                return;
            }

            foreach (var objectData in data.Objects)
            {
                var obj = ConvertToGameObject(objectData);
                world.AddObject(obj);
                _logger.LogDebug("Loaded object: {ObjectId} - {ObjectName}", obj.Id, obj.Name);
            }

            _logger.LogInformation("Loaded {Count} objects from {FilePath}", data.Objects.Count, filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading objects from {FilePath}", filePath);
            throw;
        }
    }

    private Room ConvertToRoom(RoomData data)
    {
        var room = new Room
        {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            LongDescription = string.IsNullOrEmpty(data.LongDescription) ? data.Description : data.LongDescription,
            Flags = ParseRoomFlags(data.Flags),
            Items = new List<string>(data.Items),
            GlobalItems = new List<string>(data.GlobalItems),
            ActionHandler = data.ActionHandler
        };

        // Parse exits
        foreach (var exit in data.Exits)
        {
            if (Enum.TryParse<Direction>(exit.Key, true, out var direction))
            {
                var roomExit = ParseExit(exit.Value);
                room.Exits[direction] = roomExit;
            }
            else
            {
                _logger.LogWarning("Unknown direction '{Direction}' in room {RoomId}", exit.Key, data.Id);
            }
        }

        return room;
    }

    private IRoomExit ParseExit(JsonElement exitElement)
    {
        // Simple string destination
        if (exitElement.ValueKind == JsonValueKind.String)
        {
            return new RoomExit
            {
                DestinationRoomId = exitElement.GetString()
            };
        }

        // Object with message or conditions
        if (exitElement.ValueKind == JsonValueKind.Object)
        {
            var exit = new RoomExit();

            if (exitElement.TryGetProperty("destination", out var dest))
            {
                exit.DestinationRoomId = dest.GetString();
            }

            if (exitElement.TryGetProperty("message", out var msg))
            {
                exit.CustomMessage = msg.GetString();
            }

            // TODO: Parse condition if present
            // if (exitElement.TryGetProperty("condition", out var cond))
            // {
            //     exit.Condition = ParseCondition(cond);
            // }

            return exit;
        }

        return new RoomExit();
    }

    private GameObject ConvertToGameObject(ObjectData data)
    {
        var obj = new GameObject
        {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            Synonyms = data.Synonyms.ToArray(),
            Adjectives = data.Adjectives.ToArray(),
            Flags = ParseObjectFlags(data.Flags),
            Size = data.Size,
            Capacity = data.Capacity,
            Value = data.Value,
            LocationId = data.Location,
            Contents = new List<string>(data.Contents),
            ActionHandler = data.ActionHandler
        };

        return obj;
    }

    private RoomFlags ParseRoomFlags(List<string> flags)
    {
        var result = RoomFlags.None;

        foreach (var flag in flags)
        {
            if (Enum.TryParse<RoomFlags>(flag, true, out var parsed))
            {
                result |= parsed;
            }
            else
            {
                _logger.LogWarning("Unknown room flag: {Flag}", flag);
            }
        }

        return result;
    }

    private ObjectFlags ParseObjectFlags(List<string> flags)
    {
        var result = ObjectFlags.None;

        foreach (var flag in flags)
        {
            if (Enum.TryParse<ObjectFlags>(flag, true, out var parsed))
            {
                result |= parsed;
            }
            else
            {
                _logger.LogWarning("Unknown object flag: {Flag}", flag);
            }
        }

        return result;
    }
}
