using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using IO.Swagger.Api;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Stack
{
    [Command("deploy", "Deploy new Swarm stack from file")]
    public class StackDeployCmd : BaseApiCommand<StacksApi>
    {
        [Option("--file", "Docker Swarm stack definition file path", CommandOptionType.SingleValue, ShortName = "f")]
        [Required]
        public string FilePath { get; set; }

        [Option("--endpoint-id", "ID of endpoint used to deploy (get it from Portainer)",
            CommandOptionType.SingleValue, ShortName = "eid")]
        [Required]
        public int EndpointId { get; set; }

        [Option("--swarm-id", "Swarm cluster id", CommandOptionType.SingleValue, ShortName = "sid")]
        [Required]
        public string SwarmId { get; set; }

        [Option("--env",
            "Environment variable used in definition file (format -e NAME1=VALUE1 -e NAME2=VALUE2 -e ...) (optional)",
            CommandOptionType.MultipleValue, ShortName = "e")]
        public string[] Envs { get; set; }

        [Argument(0, "stackName", "Name for new stack")]
        [Required]
        public string StackName { get; set; }


        public override void Do(CommandLineApplication app, IConsole console)
        {
            if (!File.Exists(FilePath))
            {
                throw new Exception("Stack definition file is not found. Please, give correct path to file.");
            }

            var stackEnvs = CmdHelpers.ParseEnvs(Envs);

            var fileStream = File.OpenRead(FilePath);
            // 1 - swarm stack
            console.WriteLine("Sending deploy request to Portainer...");
            var result = ApiClient.StackCreate(type: 1, method: "file", endpointId: EndpointId, body: null,
                name: StackName,
                endpointID: EndpointId.ToString(), swarmID: SwarmId, file: fileStream,
                env: JsonConvert.SerializeObject(stackEnvs));
            console.WriteLine("Stack deployed.");
        }
    }
}