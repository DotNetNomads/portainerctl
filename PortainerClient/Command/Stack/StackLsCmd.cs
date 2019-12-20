using System;
using System.Collections;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Stack
{
    [Command(Name = "ls", Description = "List all stacks")]
    public class StackLsCmd : BaseApiCommand<StacksApiService>
    {
        public override void Do(CommandLineApplication app, IConsole console)
        {
            var list = ApiClient.GetStacks();
            var yamlSerializer = new Serializer();
            Console.WriteLine("--- STACKS ---");
            Console.WriteLine(yamlSerializer.Serialize(list));
            Console.WriteLine($"--- Total count: {list.Count()} ---");
        }
    }
}