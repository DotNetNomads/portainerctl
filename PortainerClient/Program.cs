using System;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Command;
using PortainerClient.Command.Auth;
using PortainerClient.Command.Stack;
using PortainerClient.Helpers;

namespace PortainerClient
{
    [Command("portainerctl", Description = "Console client for Portainer (2.19.x) by DotNetNomads :)")]
    [Subcommand(typeof(AuthCmd), typeof(StackCmd))]
    [HelpOption]
    class Program : ICommand
    {
        public int OnExecute(CommandLineApplication app, IConsole console) =>
            CmdHelpers.SpecifyCommandResult(app, console);

        static int Main(string[] args)
        {
            try
            {
                return CommandLineApplication.Execute<Program>(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return 1;
            }
        }
    }
}
