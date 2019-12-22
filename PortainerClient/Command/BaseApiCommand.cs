using System;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Helpers;

namespace PortainerClient.Command
{
    /// <summary>
    /// Base abstractions for CMD command
    /// </summary>
    /// <typeparam name="T">Portainer API type</typeparam>
    public abstract class BaseApiCommand<T> : ICommand where T : class, new()
    {
        /// <summary>
        /// Instance of specific Portainer API
        /// </summary>
        protected readonly T ApiClient;

        /// <inheritdoc />
        protected BaseApiCommand()
        {
            ApiClient = new T();
        }

        /// <inheritdoc />
        public virtual int OnExecute(CommandLineApplication app, IConsole console)
        {
            try
            {
                Do(app, console);
                return 0;
            }
            catch (Exception ex)
            {
                return console.WriteError(ex);
            }
        }

        /// <summary>
        /// Main implementation of command
        /// </summary>
        /// <param name="app"></param>
        /// <param name="console"></param>
        protected abstract void Do(CommandLineApplication app, IConsole console);
    }
}
