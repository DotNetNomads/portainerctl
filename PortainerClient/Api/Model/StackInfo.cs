using System.Collections;
using System.Collections.Generic;

namespace PortainerClient.Api.Model
{
    public class StackInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public StackType Type { get; set; }
        public int EndpointId { get; set; }
        public string EntryPoint { get; set; }
        public string SwarmId { get; set; }
        public string ProjectPath { get; set; }
        public IEnumerable<StackEnv> Env { get; set; }
    }
}