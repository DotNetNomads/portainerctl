using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Configs
/// </summary>
[Command(Name = "configs", Description = "Docker Swarm Configs management commands")]
[Subcommand(typeof(ConfigsLsCmd), typeof(ConfigsCreateCmd))]
public class ConfigsCmd : ICommand
{
    /// <inheritdoc />
    public int OnExecute(CommandLineApplication app, IConsole console) => CmdHelpers.SpecifyCommandResult(app, console);
}
