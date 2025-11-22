namespace ZorkSharp.Commands;

using ZorkSharp.Core;
using ZorkSharp.Parser;
using ZorkSharp.World;

/// <summary>
/// Opens an object
/// </summary>
public class OpenCommand : BaseCommand
{
    public override string Name => "open";
    public override string Description => "Open an object";

    public override CommandResult Execute(ParsedCommand parsedCommand, IWorld world, IGameState gameState)
    {
        if (parsedCommand.DirectObject == null)
            return Failed("What do you want to open?");

        var obj = FindObject(parsedCommand.DirectObject, world, gameState);

        if (obj == null)
            return Failed($"You don't see any {parsedCommand.DirectObject} here.");

        if (!obj.HasFlag(ObjectFlags.Openable))
            return Failed($"You can't open the {obj.Name}.");

        if (obj.HasFlag(ObjectFlags.Locked))
            return Failed($"The {obj.Name} is locked.");

        if (obj.HasFlag(ObjectFlags.IsOpen))
            return Failed($"The {obj.Name} is already open.");

        obj.SetFlag(ObjectFlags.IsOpen);

        // If it's a container with contents, show what's inside
        if (obj.IsContainer && obj.Contents.Any())
        {
            var itemNames = obj.Contents
                .Select(id => world.GetObject(id)?.Name ?? id)
                .ToList();

            if (itemNames.Count == 1)
                return Success($"Opening the {obj.Name} reveals {itemNames[0].WithDefiniteArticle()}.");
            else
                return Success($"Opening the {obj.Name} reveals: {string.Join(", ", itemNames)}.");
        }

        return Success($"The {obj.Name} is now open.");
    }
}
