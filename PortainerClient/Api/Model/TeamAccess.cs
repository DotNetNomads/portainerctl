namespace PortainerClient.Api.Model;

/// <summary>
/// Represents team access information.
/// </summary>
public class TeamAccess
{
    /// <summary>
    /// Gets or sets the access level for the team access.
    /// </summary>
    public int AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the team.
    /// </summary>
    public int TeamId { get; set; }
}
