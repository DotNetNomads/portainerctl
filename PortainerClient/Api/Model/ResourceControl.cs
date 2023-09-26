using System.Collections.Generic;

namespace PortainerClient.Api.Model;

/// <summary>
/// Represents resource control information.
/// </summary>
public class ResourceControl
{
    /// <summary>
    /// Gets or sets the access level for the resource control.
    /// </summary>
    public int AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether only administrators have access to the resource.
    /// </summary>
    public bool AdministratorsOnly { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the resource control.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the owner ID of the resource control.
    /// </summary>
    public int OwnerId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the resource is public.
    /// </summary>
    public bool Public { get; set; }

    /// <summary>
    /// Gets or sets the unique resource ID associated with the resource control.
    /// </summary>
    public string ResourceId { get; set; }

    /// <summary>
    /// Gets or sets a list of sub-resource IDs.
    /// </summary>
    public List<string> SubResourceIds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the resource control is system-level.
    /// </summary>
    public bool System { get; set; }

    /// <summary>
    /// Gets or sets the list of team accesses associated with the resource control.
    /// </summary>
    public List<TeamAccess> TeamAccesses { get; set; }

    /// <summary>
    /// Gets or sets the type of the resource control.
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Gets or sets the list of user accesses associated with the resource control.
    /// </summary>
    public List<UserAccess> UserAccesses { get; set; }
}
