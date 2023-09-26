namespace PortainerClient.Api.Model
{
    /// <summary>
    /// General model for Portainer API error
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Message about occured error
        /// </summary>
        public string? message { get; set; }
        /// <summary>
        /// Occured error details
        /// </summary>
        public string? details { get; set; }
    }
}
