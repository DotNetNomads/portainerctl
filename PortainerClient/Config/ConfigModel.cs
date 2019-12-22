namespace PortainerClient.Config
{
    /// <summary>
    /// Represents the application configuration
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// Portainer's URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// JWT token (portainerctl auth)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Load configuration from configuration file
        /// </summary>
        /// <returns></returns>
        public static ConfigModel Load() => ConfigHelpers.Load();
    }
}
