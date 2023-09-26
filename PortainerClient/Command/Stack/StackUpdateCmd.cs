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
        /// Force pull image (if has not changed)
        /// </summary>
        [Option("--force-pull",
            "Force pull image (if same tag has changed)",
            CommandOptionType.NoValue)]
        public bool PullImage { get; set; } = false;

        /// <summary>
        /// Environment variables to update
        /// </summary>
        [Option("--env",
            "Environment variable to update (format -e NAME1=VALUE1 -e NAME2=VALUE2 -e ...) (optional)",
            CommandOptionType.MultipleValue, ShortName = "e")]
        public string[]? Envs { get; set; } = null;

        /// <summary>
        /// Print content of request and response
        /// </summary>
        [Option("--debug")]
        public bool Debug { get; set; }

        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            // getting stack info
            var stack = ApiClient.GetStacks(Debug).FirstOrDefault(s => s.Name == StackName);
            if (stack == null)
            {
                throw new Exception("Stack with this name is not found");
            }

            var stackId = stack.Id;
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
                while (Console.ReadLine() is { } s)
                {
                    sb.AppendLine(s);
                }

                fileContent = sb.ToString();
            }

            var envs = CmdHelpers.ParseEnvs(Envs);
            // if file content not provided (we use old file and change only provided env vars)
            if (fileContent == null)
            {
                envs.AddRange(stack.Env.Where(e => envs.All(newEnv => newEnv.Name != e.Name)));
                fileContent = ApiClient.GetStackFile(stackId, Debug);
            }

            console.Write("Sending stack update request to Portainer...");
            ApiClient.UpdateStack(stackId, envs, fileContent!, prune: false, endpointId: stack.EndpointId,
                pullImage: PullImage, Debug);
            console.WriteLine("Done!");
        }
    }
}
