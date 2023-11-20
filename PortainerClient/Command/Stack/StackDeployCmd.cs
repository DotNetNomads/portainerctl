using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Config;
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
        public string? FilePath { get; set; } = null!;

        /// <summary>
        /// Cluster name
        /// </summary>
        [Option("--cluster", "Name of the cluster in Portainer UI", CommandOptionType.SingleValue)]
        [Required]
        public string ClusterName { get; set; } = null!;

        /// <summary>
        /// Print content of request and response
        /// </summary>
        [Option("--debug")]
        public bool Debug { get; set; }

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


        /// <summary>
        /// Read stack file from STDIN
        /// </summary>
        [Option("--in",
            "Read updated stack definition file from stdin, can be used as an alternative to --file (optional)",
            CommandOptionType.NoValue, ShortName = "i")]
        public bool? FileFromStdin { get; set; } = false;


        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            var endpoint = WorkspaceInfoModel.GetClusterEndpoint(ClusterName);
            var memberships = WorkspaceInfoModel.Load().Memberships;

            var fileContent = CmdHelpers.GetFileContent(FilePath, FileFromStdin);
            if (fileContent == null)
            {
                throw new InvalidOperationException("Please provide file content of a new stack");
            }
            var stackEnvs = CmdHelpers.ParseEnvs(Envs);
            // 1 - swarm stack
            console.WriteLine("Sending deploy request to Portainer...");
            ApiClient.DeployStack(endpointId: endpoint.Id,
                name: StackName,
                swarmId: endpoint.SwarmId,
                fileContent: fileContent,
                env: stackEnvs, memberships,
                debug: Debug);
            console.WriteLine("Stack deployed.");
        }
    }
}
