namespace PortainerClient.Api.Model;

/// <summary>
/// Represents Git configuration information.
/// </summary>
public class GitConfig
{
    /// <summary>
    /// Gets or sets the authentication information for Git.
    /// </summary>
    public Authentication Authentication { get; set; }

    /// <summary>
    /// Gets or sets the path to the Git configuration file.
    /// </summary>
    public string ConfigFilePath { get; set; }

    /// <summary>
    /// Gets or sets the hash of the Git configuration.
    /// </summary>
    public string ConfigHash { get; set; }

    /// <summary>
    /// Gets or sets the reference name (e.g., "refs/heads/branch_name").
    /// </summary>
    public string ReferenceName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether TLS verification should be skipped for Git operations.
    /// </summary>
    public bool TlsSkipVerify { get; set; }

    /// <summary>
    /// Gets or sets the URL of the Git repository.
    /// </summary>
    public string Url { get; set; }
}
