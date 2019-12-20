using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using IO.Swagger.Api;
using IO.Swagger.Model;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using PortainerClient.Helpers;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Stack
{
    [Command("update", Description = "Update stack settings (env vars or stack file, or both)")]
    public class StackUpdateCmd : BaseApiCommand<StacksApi>
    {
        [Argument(0, "stackName", "Stack name")]
        [Required]
        public string StackName { get; set; }

        [Option("--file", "Docker Swarm updated stack definition file path (optional)",
            CommandOptionType.SingleValue, ShortName = "f")]
        public string FilePath { get; set; } = null;

        [Option("--in",
            "Read updated stack definition file from stdin, can be used as an alternative to --file (optional)",
            CommandOptionType.NoValue, ShortName = "i")]
        public bool FileFromStdin { get; set; }

        [Option("--env",
            "Environment variable to update (format -e NAME1=VALUE1 -e NAME2=VALUE2 -e ...) (optional)",
            CommandOptionType.MultipleValue, ShortName = "e")]
        public string[] Envs { get; set; } = null;

        public override void Do(CommandLineApplication app, IConsole console)
        {
            // getting stack info
            var stack = ApiClient.StackList(null).FirstOrDefault(s => s.Name == StackName);
            if (stack == null)
            {
                throw new Exception("Stack with this name is not found");
            }

            var stackId = int.Parse(stack.Id);
            // check file
            string fileContent = null;
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
                string s;
                while ((s = Console.ReadLine()) != null)
                {
                    sb.AppendLine(s);
                }

                fileContent = sb.ToString();
            }

            var envs = CmdHelpers.ParseEnvs(Envs);
            // if file content not provided (we use old file and change only provider env vars)
            if (fileContent == null)
            {
                envs.AddRange(stack.Env.Where(e => envs.All(newEnv => newEnv.Name != e.Name)));
                fileContent = ApiClient.StackFileInspect(stackId).StackFileContent;
            }

            console.Write("Sending stack update request to Portainer...");
            var updateRequest = new StackUpdateRequest
            {
                Prune = false,
                Env = envs,
                StackFileContent = fileContent,
                StackId = stackId
            };
            var updatedStack = ApiClient.StackUpdate(stackId, updateRequest, stack.EndpointID);
            console.WriteLine("Done!");
        }
    }
}