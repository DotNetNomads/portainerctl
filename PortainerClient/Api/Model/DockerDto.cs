using System;

namespace PortainerClient.Api.Model;

/// <summary>
/// Docker specific instance info
/// </summary>
public class DockerDto
{
    public DateTime CreatedAt { get; set; }
    public string ID { get; set; }
    public Portainer Portainer { get; set; }
    public ConfigSpec Spec { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Version Version { get; set; }
}
