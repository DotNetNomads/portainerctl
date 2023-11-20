using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Config;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Secrets;

/// <summary>
/// CMD command for Secrets create operation
/// </summary>
[Command(Name = "create", Description = "Create secret")]
public class SecretsCreateCmd : BaseApiCommand<EndpointsApiService>
{
    /// <summary>
    /// Cluster name
    /// </summary>
    [Option("--cluster", "Name of the cluster in Portainer UI", CommandOptionType.SingleValue)]
    [Required]
    public string ClusterName { get; set; } = null!;

    /// <summary>
    /// Path of secret file
    /// </summary>
    [Option("--file", "Docker Swarm secret file path (optional)",
        CommandOptionType.SingleValue, ShortName = "f")]
    public string? FilePath { get; set; } = null;

    /// <summary>
    /// Read secret file from STDIN
    /// </summary>
    [Option("--in",
        "Read secret content from stdin, can be used as an alternative to --file (optional)",
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
    [Argument(0, "secretName", "Name for new secret")]
    [Required]
    public string SecretName { get; set; } = null!;

    /// <inheritdoc />
    protected override void Do(CommandLineApplication app, IConsole console)
    {
        var endpoint = WorkspaceInfoModel.GetClusterEndpoint(ClusterName);
        var memberships = WorkspaceInfoModel.Load().Memberships;

        // get content
        var fileContent = CmdHelpers.GetFileContent(FilePath, FileFromStdin);

        if (fileContent == null)
            throw new InvalidOperationException("Provide secret definition by file or stdin");
        if (SecretName == null)
            throw new InvalidOperationException("Provide name for new secret");

        Console.WriteLine("Creating new secret...");
        ApiClient.CreateSecret(SecretName, fileContent, memberships, endpoint, Debug);
        Console.WriteLine("Done!");
    }
}
