namespace PortainerClient.Api.Model;

/// <summary>
/// Represents an environment variable.
/// </summary>
public class Env
{
    /// <summary>
    /// Gets or sets the name of the environment variable.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value of the environment variable.
    /// </summary>
    public string Value { get; set; }
}
