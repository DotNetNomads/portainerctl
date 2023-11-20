using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Config;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Secrets list operation
/// </summary>
[Command(Name = "ls", Description = "List all secrets")]
public class SecretsLsCmd : BaseApiCommand<EndpointsApiService>
{
    /// <summary>
    /// Cluster name
    /// </summary>
    [Argument(order: 0, name: "cluster-name", description: "Name of the cluster in Portainer UI")]
    [Required]
    public string ClusterName { get; set; } = null!;

    /// <inheritdoc />
    protected override void Do(CommandLineApplication app, IConsole console)
    {
        var list = ApiClient.GetSecrets(WorkspaceInfoModel.GetClusterEndpoint(ClusterName).Id)
            .Select(c => c.Spec);
        var yamlSerializer = new Serializer();
        Console.WriteLine("--- SECRETS ---");
        Console.WriteLine(yamlSerializer.Serialize(list));
        Console.WriteLine($"--- Total count: {list.Count()} ---");
    }
}
