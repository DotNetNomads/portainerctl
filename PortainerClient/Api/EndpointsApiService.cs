using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortainerClient.Api.Base;
using PortainerClient.Api.Model;

namespace PortainerClient.Api;

/// <summary>
/// API implementation for endpoints API
/// </summary>
public class EndpointsApiService : BaseApiService
{
    /// <summary>
    /// Get all configs
    /// </summary>
    /// <param name="endpointId"></param>
    /// <returns>List of available configs</returns>
    public IEnumerable<DockerDto> GetConfigs(int endpointId) =>
        Get<List<DockerDto>>($"endpoints/{endpointId}/docker/configs");

    /// <summary>
    /// Get all secrets
    /// </summary>
    /// <param name="endpointId"></param>
    /// <returns>List of available secrets</returns>
    public IEnumerable<DockerDto> GetSecrets(int endpointId) =>
        Get<List<DockerDto>>($"endpoints/{endpointId}/docker/secrets");

    /// <summary>
    /// Get list of available endpoints
    /// </summary>
    /// <returns>List of endpoints</returns>
    public IEnumerable<Endpoint> GetEndpoints() =>
        Get<List<Endpoint>>("endpoints", false, ("type", 2)).Select(e =>
            {
                e.SwarmId = Get<EndpointSwarm>($"endpoints/{e.Id}/docker/swarm").Id;
                return e;
            }
        );

    /// <summary>
    /// Create new secret in specific Swarm endpoint
    /// </summary>
    /// <param name="secretName"></param>
    /// <param name="fileContent"></param>
    /// <param name="memberships"></param>
    /// <param name="endpoint"></param>
    /// <param name="debug"></param>
    public void CreateSecret(string secretName, string fileContent, IEnumerable<Membership>? memberships,
        Endpoint endpoint, bool debug = false)
    {
        var secretSpec = new ConfigSpec
        {
            Name = secretName,
            Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileContent))
        };
        var secret = Post<DockerDto>($"endpoints/{endpoint.Id}/docker/secrets/create", debug,
            ("", secretSpec, ParamType.JsonBody));
        SetAcl(memberships, debug, secret.Portainer.ResourceControl.Id);
    }

    /// <summary>
    /// Create new config in specific Swarm endpoint
    /// </summary>
    /// <param name="configName"></param>
    /// <param name="fileContent"></param>
    /// <param name="memberships"></param>
    /// <param name="endpoint"></param>
    /// <param name="debug"></param>
    public void CreateConfig(string configName, string fileContent, IEnumerable<Membership>? memberships, Endpoint endpoint, bool debug)
    {
        var configSec = new ConfigSpec
        {
            Name = configName,
            Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileContent))
        };
        var secret = Post<DockerDto>($"endpoints/{endpoint.Id}/docker/configs/create", debug,
            ("", configSec, ParamType.JsonBody));
        SetAcl(memberships, debug, secret.Portainer.ResourceControl.Id);
    }
}
