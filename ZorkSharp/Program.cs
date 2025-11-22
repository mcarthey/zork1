using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using ZorkSharp.Core;
using ZorkSharp.Engine;
using ZorkSharp.Parser;
using ZorkSharp.World;
using ZorkSharp.Commands;
using ZorkSharp.Events;
using ZorkSharp.UI;
using ZorkSharp.Data;

namespace ZorkSharp;

/// <summary>
/// Main program entry point with modern .NET hosting, DI, and logging
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Create and configure the host
        var host = CreateHostBuilder(args).Build();

        // Get the game engine from DI container
        var gameEngine = host.Services.GetRequiredService<IGameEngine>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Starting Zork I: The Great Underground Empire");

        // Start the game
        try
        {
            gameEngine.Initialize();
            gameEngine.Start();
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Fatal error occurred during game execution");

            var output = host.Services.GetRequiredService<IOutputWriter>();
            output.WriteLine($"\nA fatal error occurred: {ex.Message}");
            output.WriteLine("The game will now exit.");
        }
        finally
        {
            logger.LogInformation("Zork I shutting down");
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();

                // Configure Serilog
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(context.Configuration)
                    .MinimumLevel.Debug()
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                    .WriteTo.File("logs/zork-.log",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();

                logging.AddSerilog(Log.Logger);
            })
            .ConfigureServices((context, services) =>
            {
                // Configuration
                var configuration = context.Configuration;

                // Core services
                services.AddSingleton<IInputReader, ConsoleInputReader>();
                services.AddSingleton<IOutputWriter, ConsoleOutputWriter>();
                services.AddSingleton<IGameState, GameState>();

                // World services
                services.AddSingleton<GameWorld>();
                services.AddSingleton<IWorld>(sp => sp.GetRequiredService<GameWorld>());
                services.AddSingleton<IDataLoader, JsonDataLoader>();

                // Parser services
                services.AddSingleton<IParser, GameParser>();

                // Command services
                services.AddSingleton<ICommandFactory, CommandFactory>();

                // Event services
                services.AddSingleton<IGameClock>(sp =>
                    new GameClock(sp.GetRequiredService<IOutputWriter>()));

                // Data services
                services.AddSingleton<WorldBuilder>(sp =>
                {
                    var world = sp.GetRequiredService<GameWorld>();
                    var dataLoader = sp.GetRequiredService<IDataLoader>();
                    var logger = sp.GetRequiredService<ILogger<WorldBuilder>>();
                    var dataDir = configuration.GetValue<string>("Game:DataDirectory") ?? "Data";

                    return new WorldBuilder(world, dataLoader, logger, dataDir);
                });

                // Game engine
                services.AddSingleton<IGameEngine>(sp =>
                {
                    var gameState = sp.GetRequiredService<IGameState>();
                    var world = sp.GetRequiredService<IWorld>();
                    var parser = sp.GetRequiredService<IParser>();
                    var commandFactory = sp.GetRequiredService<ICommandFactory>();
                    var output = sp.GetRequiredService<IOutputWriter>();
                    var input = sp.GetRequiredService<IInputReader>();
                    var clock = sp.GetRequiredService<IGameClock>();

                    // Build the world
                    var worldBuilder = sp.GetRequiredService<WorldBuilder>();
                    worldBuilder.BuildWorld();

                    return new GameEngine(gameState, world, parser, commandFactory, output, input, clock);
                });
            });
}
