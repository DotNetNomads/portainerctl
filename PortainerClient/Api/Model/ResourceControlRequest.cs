using System.Collections.Generic;

namespace PortainerClient.Api.Model;

/// <summary>
/// Model to change resource control settings
/// </summary>
public class ResourceControlRequest
{
    /// <summary>
    /// Allow only for admins
    /// </summary>
    public bool AdministratorsOnly { get; set; }
    /// <summary>
    /// Accessible for any user
    /// </summary>
    public bool Public { get; set; }
    /// <summary>
    /// Users have access
    /// </summary>
    public IEnumerable<int> Users { get; set; }
    /// <summary>
    /// Teams have access
    /// </summary>
    public IEnumerable<int> Teams { get; set; }
}
