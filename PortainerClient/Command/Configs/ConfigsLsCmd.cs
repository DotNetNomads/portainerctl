using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Config;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Configs list operation
/// </summary>
[Command(Name = "ls", Description = "List all configs")]
public class ConfigsLsCmd : BaseApiCommand<EndpointsApiService>
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
        var list = ApiClient.GetConfigs(WorkspaceInfoModel.GetClusterEndpoint(ClusterName).Id).Select(c =>
        {
            try
            {
                c.Spec.Data = Base64Decode(c.Spec.Data);
            }
            catch
            {
                // ignored
            }

            return c.Spec;
        });

        var yamlSerializer = new Serializer();
        Console.WriteLine("--- CONFIGS ---");
        Console.WriteLine(yamlSerializer.Serialize(list));
        Console.WriteLine($"--- Total count: {list.Count()} ---");
    }

    private static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
