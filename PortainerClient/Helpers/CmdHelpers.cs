using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api.Model;

namespace PortainerClient.Helpers
{
    /// <summary>
    /// Helpers for CMD
    /// </summary>
    public static class CmdHelpers
    {
        /// <summary>
        /// Returns message when user is not typed a subcommand
        /// </summary>
        /// <param name="app"></param>
        /// <param name="console"></param>
        /// <returns></returns>
        public static int SpecifyCommandResult(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            console.WriteLine(app.GetHelpText());
            return 1;
        }

        /// <summary>
        /// Writes error to user's console
        /// </summary>
        /// <param name="console"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static int WriteError(this IConsole console, Exception exception)
        {
            console.BackgroundColor = ConsoleColor.Red;
            console.Error.WriteLine($"Error: {exception.Message}");
            return 1;
        }

        /// <summary>
        /// Parses environment variables from console input
        /// </summary>
        /// <param name="envs"></param>
        /// <returns>List of envs</returns>
        /// <exception cref="Exception">Occurs when env has incorrect format</exception>
        public static List<Env> ParseEnvs(string[]? envs)
        {
            var stackEnvs = new List<Env>();
            if (envs == null) return stackEnvs;
            foreach (var env in envs)
            {
                if (!env.Contains("="))
                {
                    throw new Exception($"Incorrect env var format: {env}");
                }

                var splited = env.Split("=", 2);
                stackEnvs.Add(new Env { Name = splited[0], Value = splited[1] });
            }

            return stackEnvs;
        }

        /// <summary>
        /// Retrieve file content from file or STDIN
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileFromStdin"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string? GetFileContent(string? filePath, bool? fileFromStdin)
        {
            string? fileContent = null;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                if (!File.Exists(filePath))
                    throw new Exception("Definition file is not found. Provide valid path.");
                fileContent = File.ReadAllText(filePath);
            }

            if (fileFromStdin == null || !fileFromStdin.Value || fileContent != null) return fileContent;

            var sb = new StringBuilder();
            while (Console.ReadLine() is { } s) sb.AppendLine(s);
            fileContent = sb.ToString();
            return fileContent;
        }
    }
}
