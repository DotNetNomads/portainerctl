using System.Collections.Generic;
using PortainerClient.Api;
using PortainerClient.Api.Model;

namespace PortainerClient.Command.Stack
{
    public class StacksApiService : BaseApiService
    {
        public IEnumerable<StackInfo> GetStacks() => Get<List<StackInfo>>("stacks");
    }
}