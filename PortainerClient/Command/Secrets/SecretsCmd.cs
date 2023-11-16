using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Command.Configs;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Secrets;

/// <summary>
/// CMD command for Secrets
/// </summary>
[Command(Name = "secrets", Description = "Docker Swarm Secrets management commands")]
[Subcommand(typeof(SecretsLsCmd))]
public class SecretsCmd : ICommand
{
    /// <inheritdoc />
    public int OnExecute(CommandLineApplication app, IConsole console) => CmdHelpers.SpecifyCommandResult(app, console);
}
