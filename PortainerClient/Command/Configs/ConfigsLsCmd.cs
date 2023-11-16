using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Configs list operation
/// </summary>
[Command(Name = "ls", Description = "List all configs")]
public class ConfigsLsCmd: BaseApiCommand<EndpointsApiService>
{
    /// <summary>
    /// Endpoint identifier
    /// </summary>
    [Argument(order: 0, name: "endpoint-id", description: "ID of endpoint used to deploy (get it from Portainer)")]
    [Required]
    public int EndpointId { get; set; }

    /// <inheritdoc />
    protected override void Do(CommandLineApplication app, IConsole console)
    {
        var list = ApiClient.GetConfigs(EndpointId).Select(c=> c.Spec);
        var yamlSerializer = new Serializer();
        Console.WriteLine("--- CONFIGS ---");
        Console.WriteLine(yamlSerializer.Serialize(list));
        Console.WriteLine($"--- Total count: {list.Count()} ---");
    }
}
