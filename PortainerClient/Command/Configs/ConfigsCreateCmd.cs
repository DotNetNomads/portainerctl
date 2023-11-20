using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Config;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Configs;

/// <summary>
/// CMD command for Config create operation
/// </summary>
[Command(Name = "create", Description = "Create config")]
public class ConfigsCreateCmd : BaseApiCommand<EndpointsApiService>
{
    /// <summary>
    /// Cluster name
    /// </summary>
    [Option("--cluster", "Name of the cluster in Portainer UI", CommandOptionType.SingleValue)]
    [Required]
    public string ClusterName { get; set; } = null!;

    /// <summary>
    /// Path of config file
    /// </summary>
    [Option("--file", "Docker Swarm config file path (optional)",
        CommandOptionType.SingleValue, ShortName = "f")]
    public string? FilePath { get; set; } = null;

    /// <summary>
    /// Read config file from STDIN
    /// </summary>
    [Option("--in",
        "Read config content from stdin, can be used as an alternative to --file (optional)",
        CommandOptionType.NoValue, ShortName = "i")]
    public bool FileFromStdin { get; set; } = false;

    /// <summary>
    /// Print content of request and response
    /// </summary>
    [Option("--debug")]
    public bool Debug { get; set; }

    /// <summary>
    /// Stack name
    /// </summary>
    [Argument(0, "configName", "Name for new config")]
    [Required]
    public string ConfigName { get; set; } = null!;

    /// <inheritdoc />
    protected override void Do(CommandLineApplication app, IConsole console)
    {
        var endpoint = WorkspaceInfoModel.GetClusterEndpoint(ClusterName);
        var memberships = WorkspaceInfoModel.Load().Memberships;

        // get content
        var fileContent = CmdHelpers.GetFileContent(FilePath, FileFromStdin);

        if (fileContent == null)
            throw new InvalidOperationException("Provide config definition by file or stdin");
        if (ConfigName == null)
            throw new InvalidOperationException("Provide name for new config");

        Console.WriteLine("Creating new config...");
        ApiClient.CreateConfig(ConfigName, fileContent, memberships, endpoint, Debug);
        Console.WriteLine("Done!");
    }
}
