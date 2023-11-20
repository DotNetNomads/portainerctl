namespace PortainerClient.Api.Model;

/// <summary>
/// Membership entity
/// </summary>
public class Membership
{
    /// <summary>
    /// Team ID
    /// </summary>
    public int TeamId { get; set; }
    /// <summary>
    /// Role in team
    /// </summary>
    public int Role { get; set; }
    /// <summary>
    /// Team name
    /// </summary>
    public string TeamName { get; set; } = null!;
}
