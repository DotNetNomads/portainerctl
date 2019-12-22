namespace PortainerClient.Api.Model
{
    /// <summary>
    /// Model for stack's environment variable
    /// </summary>
    public class StackEnv
    {
        /// <summary>
        /// Environment variable name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Environment variable value
        /// </summary>
        public string Value { get; set; }
    }
}
