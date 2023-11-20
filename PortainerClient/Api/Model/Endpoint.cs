namespace PortainerClient.Api.Model;

/// <summary>
/// Portainer Endpoint
/// </summary>
public class Endpoint
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name in portainer's interface
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Identifier of swarm cluster
    /// </summary>
    public string SwarmId { get; set; } = null!;
}
