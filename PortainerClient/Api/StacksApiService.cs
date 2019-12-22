using System.Collections.Generic;
using PortainerClient.Api.Base;
using PortainerClient.Api.Model;
using RestSharp;

namespace PortainerClient.Api
{
    /// <summary>
    /// API implementation for Stacks
    /// </summary>
    public class StacksApiService : BaseApiService
    {
        /// <summary>
        /// Get all stacks
        /// </summary>
        /// <returns>List of available stacks</returns>
        public IEnumerable<StackInfo> GetStacks() => Get<List<StackInfo>>("stacks");

        /// <summary>
        /// Get file content for a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        /// <returns>Stack file content</returns>
        public string GetStackFile(int stackId) => Get<StackFileInspect>($"stacks/{stackId}/file").StackFileContent;

        /// <summary>
        /// Get information about a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        /// <returns>StackInfo model</returns>
        public StackInfo GetStackInfo(in int stackId) => Get<StackInfo>($"stacks/{stackId}");

        /// <summary>
        /// Remove a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        public void RemoveStack(int stackId) => Delete($"stacks/{stackId}");

        /// <summary>
        /// Deploy a stack
        /// </summary>
        /// <param name="endpointId">Endpoint identifier where to deploy a stack</param>
        /// <param name="name">The stack name</param>
        /// <param name="swarmId">Swarm identifier where to deploy a stack</param>
        /// <param name="stackFilePath">Path to stack deployment file</param>
        /// <param name="env">List of the stack envs</param>
        /// <returns>StackInfo instance for newly deployed stack</returns>
        public StackInfo DeployStack(int endpointId, string name, string swarmId, string stackFilePath,
            List<StackEnv> env) =>
            Post<StackInfo>("stacks",
                ("type", "1", ParamType.QueryParam),
                ("method", "file", ParamType.QueryParam),
                ("endpointId", endpointId, ParamType.QueryParam),
                ("Name", name, ParamType.BodyParam),
                ("EndpointID", endpointId, ParamType.BodyParam),
                ("SwarmID", swarmId, ParamType.BodyParam),
                ("Env", SimpleJson.SerializeObject(env), ParamType.BodyParam),
                ("file", stackFilePath, ParamType.File)
            );

        /// <summary>
        /// Update a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        /// <param name="envs">List of environment variables</param>
        /// <param name="fileContent">Deploy file content</param>
        /// <param name="prune">Prune unnecessary services</param>
        /// <param name="endpointId">Endpoint identifier where to update a stack</param>
        /// <returns>StackInfo instance for updated stack</returns>
        public StackInfo UpdateStack(int stackId, List<StackEnv> envs, string fileContent, bool prune,
            int endpointId)
        {
            return Put<StackInfo>($"stacks/{stackId}",
                ("endpointId", endpointId, ParamType.QueryParam),
                (null, new
                {
                    StackFileContent = fileContent,
                    Env = envs,
                    Prune = prune
                }, ParamType.JsonBody));
        }
    }
}
