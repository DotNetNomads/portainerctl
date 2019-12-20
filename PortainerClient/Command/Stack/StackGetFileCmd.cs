using System;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Api;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Helpers;

namespace PortainerClient.Command.Stack
{
    [Command(Name = "getfile", Description = "Get compose file of stack")]
    public class StackGetFileCmd : BaseApiCommand<StacksApi>
    {
        [Argument(0, "stackId", "Stack instance identifier")]
        [Required]
        public int StackId { get; set; }

        public override void Do(CommandLineApplication app, IConsole console)
        {
            var data = ApiClient.StackFileInspect(StackId);
            console.Write(data.StackFileContent);
        }
    }
}