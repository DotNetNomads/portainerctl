namespace PortainerClient.Api.Model;

/// <summary>
/// Represents user access information.
/// </summary>
public class UserAccess
{
    /// <summary>
    /// Gets or sets the access level for the user access.
    /// </summary>
    public int AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int UserId { get; set; }
}
