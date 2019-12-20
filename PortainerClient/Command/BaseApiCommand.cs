using System;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Helpers;

namespace PortainerClient.Command
{
    public abstract class BaseApiCommand<T> : ICommand where T : class, new()
    {
        protected T ApiClient;

        protected BaseApiCommand()
        {
            ApiClient = new T();
        }

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

        public abstract void Do(CommandLineApplication app, IConsole console);
    }
}