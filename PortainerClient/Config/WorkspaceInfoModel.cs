using System;
using System.Collections.Generic;
using System.Linq;
using PortainerClient.Api.Model;

namespace PortainerClient.Config;

/// <summary>
/// Represents workspace information
/// </summary>
public class WorkspaceInfoModel
{
    /// <summary>
    /// Memberships in teams
    /// </summary>
    public IEnumerable<Membership> Memberships { get; set; } = null!;

    /// <summary>
    /// Endpoints in Portainer
    /// </summary>
    public IEnumerable<Endpoint> Endpoints { get; set; } = null!;

    /// <summary>
    /// User identifier
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Load from file
    /// </summary>
    /// <returns></returns>
    public static WorkspaceInfoModel Load() => WorkspaceInfoHelpers.Load();

    /// <summary>
    /// Get endpoint Id by Name from Workspace Info
    /// </summary>
    /// <param name="clusterName"></param>
    /// <returns></returns>
    public static Endpoint GetClusterEndpoint(string clusterName) =>
        Load().Endpoints?.FirstOrDefault(w => w.Name == clusterName) ??
        throw new InvalidOperationException("Endpoint with this name is not found");
}
