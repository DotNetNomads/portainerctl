using System.Collections.Generic;
using System.IO;
using PortainerClient.Api;
using PortainerClient.Api.Model;

namespace PortainerClient.Command.Stack
{
    public class StacksApiService : BaseApiService
    {
        public IEnumerable<StackInfo> GetStacks() => Get<List<StackInfo>>("stacks");

        public string GetStackFile(int stackId) => Get<StackFileInspect>($"stacks/{stackId}/file").StackFileContent;

        public StackInfo GetStackInfo(in int stackId) => Get<StackInfo>($"stacks/{stackId}");

        public void RemoveStack(int stackId) => Delete($"stacks/{stackId}");

        public object DeployStack(in int endpointId, string name, string swarmID, string stackFilePath, string env)
        {
            throw new System.NotImplementedException();
        }
    }
}
