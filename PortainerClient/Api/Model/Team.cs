namespace PortainerClient.Api.Model;

/// <summary>
/// Team info
/// </summary>
public class Team
{
    /// <summary>
    /// Identifier of Team
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Team name
    /// </summary>
    public string Name { get; set; } = null!;
}
