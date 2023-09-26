using System;
using System.Collections.Generic;

namespace PortainerClient.Api.Model
{
    /// <summary>
    /// Represents stack instance information.
    /// </summary>
    public class StackInfo
    {
        /// <summary>
        /// Gets or sets a list of additional files associated with the stack.
        /// </summary>
        public List<string> AdditionalFiles { get; set; }

        /// <summary>
        /// Gets or sets the auto-update information for the stack.
        /// </summary>
        public AutoUpdate AutoUpdate { get; set; }

        /// <summary>
        /// Gets or sets the endpoint identifier in Portainer where the stack is deployed.
        /// </summary>
        public int EndpointId { get; set; }

        /// <summary>
        /// Gets or sets the location of the stack file.
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the list of environment variables for the stack.
        /// </summary>
        public List<Env> Env { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the stack.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the stack.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the option for the stack.
        /// </summary>
        public Option Option { get; set; }

        /// <summary>
        /// Gets or sets the resource control information for the stack.
        /// </summary>
        public ResourceControl ResourceControl { get; set; }

        /// <summary>
        /// Gets or sets the status of the stack.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the swarm identifier in Portainer where the stack is deployed.
        /// </summary>
        public string SwarmId { get; set; }

        /// <summary>
        /// Gets or sets the type of the stack.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the user who created the stack.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the stack.
        /// </summary>
        public long CreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the stack was created from an application template.
        /// </summary>
        public bool FromAppTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Git configuration for the stack.
        /// </summary>
        public GitConfig GitConfig { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the stack is in Compose format.
        /// </summary>
        public bool IsComposeFormat { get; set; }

        /// <summary>
        /// Gets or sets the namespace of the stack.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the path on disk to the repository that hosts the stack file.
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the stack.
        /// </summary>
        public long UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last updated the stack.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
