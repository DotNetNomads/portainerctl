using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD for Stack update operation (updates: completely or partial)
    /// </summary>
    [Command("update", Description = "Update stack settings (env vars or stack file, or both)")]
    public class StackUpdateCmd : BaseApiCommand<StacksApiService>
    {
        /// <summary>
        /// Stack name
        /// </summary>
        [Argument(0, "stackName", "Stack name")]
        [Required]
        public string StackName { get; set; } = null!;

        /// <summary>
        /// Path of stack file
        /// </summary>
        [Option("--file", "Docker Swarm updated stack definition file path (optional)",
            CommandOptionType.SingleValue, ShortName = "f")]
        public string? FilePath { get; set; } = null;

        /// <summary>
        /// Read stack file from STDIN
        /// </summary>
        [Option("--in",
            "Read updated stack definition file from stdin, can be used as an alternative to --file (optional)",
            CommandOptionType.NoValue, ShortName = "i")]
        public bool FileFromStdin { get; set; } = false;

        /// <summary>
        /// Environment variables to update
        /// </summary>
        [Option("--env",
            "Environment variable to update (format -e NAME1=VALUE1 -e NAME2=VALUE2 -e ...) (optional)",
            CommandOptionType.MultipleValue, ShortName = "e")]
        public string[]? Envs { get; set; } = null;

        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            // getting stack info
            var stack = ApiClient.GetStacks().FirstOrDefault(s => s.Name == StackName);
            if (stack == null)
            {
                throw new Exception("Stack with this name is not found");
            }

            Debug.Assert(stack.Id != null, "stack.Id != null");
            var stackId = int.Parse(stack.Id);
            // check file
            string? fileContent = null;
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                if (!File.Exists(FilePath))
                {
                    throw new Exception("Definition file is not found. Provide valid path.");
                }

                fileContent = File.ReadAllText(FilePath);
            }

            if (FileFromStdin && fileContent == null)
            {
                var sb = new StringBuilder();
                string? s;
                while ((s = Console.ReadLine()) != null)
                {
                    sb.AppendLine(s);
                }

                fileContent = sb.ToString();
            }

            var envs = CmdHelpers.ParseEnvs(Envs);
            // if file content not provided (we use old file and change only provided env vars)
            if (fileContent == null)
            {
                Debug.Assert(stack.Env != null, "stack.Env != null");
                envs.AddRange(stack.Env.Where(e => envs.All(newEnv => newEnv.Name != e.Name)));
                fileContent = ApiClient.GetStackFile(stackId);
            }

            console.Write("Sending stack update request to Portainer...");
            Debug.Assert(fileContent != null, nameof(fileContent) + " != null");
            ApiClient.UpdateStack(stackId, envs, fileContent, prune: false, stack.EndpointId);
            console.WriteLine("Done!");
        }
    }
}
