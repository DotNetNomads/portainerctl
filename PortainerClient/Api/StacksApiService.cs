using System.Collections.Generic;
using System.Text.Json;
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
        public IEnumerable<StackInfo> GetStacks(bool debug = false) =>
            Get<List<StackInfo>>("stacks", debug, ("filter", ""));

        /// <summary>
        /// Get file content for a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        /// <param name="debug"></param>
        /// <returns>Stack file content</returns>
        public string? GetStackFile(int stackId, bool debug = false) =>
            Get<StackFileInspect>($"stacks/{stackId}/file", debug).StackFileContent;

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
        /// <param name="debug">Print content of request and response</param>
        /// <returns>StackInfo instance for newly deployed stack</returns>
        public StackInfo DeployStack(int endpointId, string name, string swarmId, string stackFilePath,
            List<Env> env, bool debug = false) =>
            Post<StackInfo>("stacks/create/swarm/file",
                debug,
                ("endpointId", endpointId, ParamType.QueryParam),
                ("file", stackFilePath, ParamType.File),
                ("SwarmID", swarmId, ParamType.BodyParam),
                ("Name", name, ParamType.BodyParam),
                ("Env", JsonSerializer.Serialize(env), ParamType.BodyParam)
            );

        /// <summary>
        /// Update a stack
        /// </summary>
        /// <param name="stackId">Stack identifier</param>
        /// <param name="envs">List of environment variables</param>
        /// <param name="fileContent">Deploy file content</param>
        /// <param name="prune">Prune unnecessary services</param>
        /// <param name="endpointId">Endpoint identifier where to update a stack</param>
        /// <param name="pullImage"></param>
        /// <returns>StackInfo instance for updated stack</returns>
        public StackInfo UpdateStack(int stackId, List<Env> envs, string fileContent, bool prune,
            int endpointId, bool pullImage, bool debug)
        {
            return Put<StackInfo>($"stacks/{stackId}",
                debug,
                ("endpointId", endpointId, ParamType.QueryParam),
                (null, new
                {
                    StackFileContent = fileContent,
                    Env = envs,
                    Prune = prune,
                    PullImage = pullImage
                }, ParamType.JsonBody));
        }
    }
}
