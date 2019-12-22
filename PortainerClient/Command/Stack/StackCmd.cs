using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD command for Stacks
    /// </summary>
    [Command(Name = "stack", Description = "Docker Stack management commands")]
    [Subcommand(typeof(StackLsCmd),
        typeof(StackGetFileCmd),
        typeof(StackDeployCmd),
        typeof(StackUpdateCmd),
        typeof(StackRmCmd),
        typeof(StackInspectCmd)
    )]
    public class StackCmd : ICommand
    {
        /// <inheritdoc />
        public int OnExecute(CommandLineApplication app, IConsole console) =>
            CmdHelpers.SpecifyCommandResult(app, console);
    }
}
