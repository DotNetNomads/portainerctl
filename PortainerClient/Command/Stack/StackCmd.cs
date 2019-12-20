using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Command.Stack;
using PortainerClient.Helpers;

namespace PortainerClient.Command
{
    [Command(Name = "stack", Description = "Docker Stack management commands")]
    [Subcommand(
        typeof(StackLsCmd)
//        , 
//        typeof(StackGetFileCmd),
//        typeof(StackDeployCmd),
//        typeof(StackUpdateCmd),
//        typeof(StackRmCmd),
//        typeof(StackInspectCmd)
    )]
    public class StackCmd : ICommand
    {
        public int OnExecute(CommandLineApplication app, IConsole console) =>
            CmdHelpers.SpecifyCommandResult(app, console);
    }
}