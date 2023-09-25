using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD command for Stack deploy operation
    /// </summary>
    [Command("deploy", "Deploy new Swarm stack from file")]
    public class StackDeployCmd : BaseApiCommand<StacksApiService>
    {
        /// <summary>
        /// Path of a stack file
        /// </summary>
        [Option("--file", "Docker Swarm stack definition file path", CommandOptionType.SingleValue, ShortName = "f")]
        [Required]
        public string FilePath { get; set; } = null!;

        /// <summary>
        /// Endpoint identifier
        /// </summary>
        [Option("--endpoint-id", "ID of endpoint used to deploy (get it from Portainer)",
            CommandOptionType.SingleValue, ShortName = "eid")]
        [Required]
        public int EndpointId { get; set; }

        /// <summary>
        /// Swarm identifier
        /// </summary>
        [Option("--swarm-id", "Swarm cluster id", CommandOptionType.SingleValue, ShortName = "sid")]
        [Required]
        public string SwarmId { get; set; } = null!;

        /// <summary>
        /// List of environment variables
        /// </summary>
        [Option("--env",
            "Environment variable used in definition file (format -e NAME1=VALUE1 -e NAME2=VALUE2 -e ...) (optional)",
            CommandOptionType.MultipleValue, ShortName = "e")]
        public string[]? Envs { get; set; }

        /// <summary>
        /// Stack name
        /// </summary>
        [Argument(0, "stackName", "Name for new stack")]
        [Required]
        public string StackName { get; set; } = null!;


        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            if (!File.Exists(FilePath))
            {
                throw new Exception("Stack definition file is not found. Please, give correct path to file.");
            }

            var stackEnvs = CmdHelpers.ParseEnvs(Envs);
            // 1 - swarm stack
            console.WriteLine("Sending deploy request to Portainer...");
            var result = ApiClient.DeployStack(endpointId: EndpointId,
                StackName,
                SwarmId,
                FilePath,
                stackEnvs);
            console.WriteLine("Stack deployed.");
        }
    }
}
