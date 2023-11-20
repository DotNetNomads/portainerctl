using System;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using YamlDotNet.Serialization;

namespace PortainerClient.Command;

/// <summary>
/// Print list of endpoints
/// </summary>
[Command(Name = "endpoints", Description = "List all endpoints")]
public class EndpointsLsCmd: BaseApiCommand<EndpointsApiService>
{
    /// <inheritdoc />
    protected override void Do(CommandLineApplication app, IConsole console)
    {
        var list = ApiClient.GetEndpoints();
        var yamlSerializer = new Serializer();
        Console.WriteLine("--- Endpoints ---");
        Console.WriteLine(yamlSerializer.Serialize(list));
        Console.WriteLine($"--- Total count: {list.Count()} ---");
    }
}
