using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Command.Stack;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Configs
/// </summary>
[Command(Name = "configs", Description = "Docker Swarm Configs management commands")]
[Subcommand(typeof(ConfigsLsCmd))]
public class ConfigsCmd : ICommand
{
    /// <inheritdoc />
    public int OnExecute(CommandLineApplication app, IConsole console) => CmdHelpers.SpecifyCommandResult(app, console);
}
