using System.Collections;
using System.Collections.Generic;

namespace PortainerClient.Api.Model
{
    /// <summary>
    /// Stack instance information
    /// </summary>
    public class StackInfo
    {
        /// <summary>
        /// Stack id in portainer
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Stack name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Type of stack
        /// </summary>
        public StackType Type { get; set; }
        /// <summary>
        /// Endpoint identifier in Portainer where the stack is deployed
        /// </summary>
        public int EndpointId { get; set; }
        /// <summary>
        /// Stack file location
        /// </summary>
        public string? EntryPoint { get; set; }
        /// <summary>
        /// Swarm identifier in Portainer where the stack is deployed
        /// </summary>
        public string? SwarmId { get; set; }
        /// <summary>
        /// Path on disk to the repository that hosts the stack file
        /// </summary>
        public string? ProjectPath { get; set; }
        /// <summary>
        /// List of environment variables for the stack
        /// </summary>
        public IEnumerable<StackEnv>? Env { get; set; }
    }
}
