using System.Collections.Generic;
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
}
