using System.Collections.Generic;

namespace PortainerClient.Api.Model;

/// <summary>
/// Spec of Docker Swarm Config instance
/// </summary>
public class ConfigSpec
{
    public string Data { get; set; }
    public Dictionary<string, object> Labels { get; set; }
    public string Name { get; set; }
}
