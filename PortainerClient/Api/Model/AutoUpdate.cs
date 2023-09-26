namespace PortainerClient.Api.Model;

/// <summary>
/// Represents auto-update information.
/// </summary>
public class AutoUpdate
{
    /// <summary>
    /// Gets or sets a value indicating whether to force pulling the image.
    /// </summary>
    public bool ForcePullImage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to force the update.
    /// </summary>
    public bool ForceUpdate { get; set; }

    /// <summary>
    /// Gets or sets the interval for updates in a human-readable format (e.g., "1m30s").
    /// </summary>
    public string Interval { get; set; }

    /// <summary>
    /// Gets or sets the job ID associated with the auto-update.
    /// </summary>
    public string JobID { get; set; }

    /// <summary>
    /// Gets or sets the webhook URL for auto-updates.
    /// </summary>
    public string Webhook { get; set; }
}
