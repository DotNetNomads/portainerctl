namespace PortainerClient.Api.Model
{
    /// <summary>
    /// Model for authorization process
    /// </summary>
    public class AuthInfo
    {
        /// <summary>
        /// User's login
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }
    }
}
