using System;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;

namespace PortainerClient.Helpers
{
    public static class CmdHelpers
    {
        public static int SpecifyCommandResult(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            console.WriteLine(app.GetHelpText());
            return 1;
        }

        public static int WriteError(this IConsole console, Exception exception)
        {
            console.BackgroundColor = ConsoleColor.Red;
            console.Error.WriteLine($"Error: {exception.Message}");
            return 1;
        }

//        public static List<StackEnv> ParseEnvs(string[] envs)
//        {
//            var stackEnvs = new List<StackEnv>();
//            if (envs != null)
//            {
//                foreach (var env in envs)
//                {
//                    if (!env.Contains("="))
//                    {
//                        throw new Exception($"Incorrect env var format: {env}");
//                    }
//
//                    var splited = env.Split("=", 2);
//                    stackEnvs.Add(new StackEnv {Name = splited[0], Value = splited[1]});
//                }
//            }
//
//            return stackEnvs;
//        }
    }
}