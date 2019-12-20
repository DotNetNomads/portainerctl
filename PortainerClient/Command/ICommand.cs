using McMaster.Extensions.CommandLineUtils;

namespace PortainerClient.Command
{
    public interface ICommand
    {
        int OnExecute(CommandLineApplication app, IConsole console);
    }
}