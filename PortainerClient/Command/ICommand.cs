using McMaster.Extensions.CommandLineUtils;

namespace PortainerClient.Command
{
    /// <summary>
    /// Base interface for CMD command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes command and returns exit code.
        /// </summary>
        /// <param name="app">CMD app</param>
        /// <param name="console">Console instance</param>
        /// <returns>Exit code</returns>
        int OnExecute(CommandLineApplication app, IConsole console);
    }
}
