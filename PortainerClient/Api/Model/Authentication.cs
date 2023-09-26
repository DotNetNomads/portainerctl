namespace PortainerClient.Api.Model;

/// <summary>
/// Represents authentication information for Git.
/// </summary>
public class Authentication
{
    /// <summary>
    /// Gets or sets the Git credential ID.
    /// </summary>
    public int GitCredentialID { get; set; }

    /// <summary>
    /// Gets or sets the Git password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the Git username.
    /// </summary>
    public string Username { get; set; }
}
