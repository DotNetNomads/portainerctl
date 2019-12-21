using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace PortainerClient.Command.Stack
{
    [Command(Name = "getfile", Description = "Get compose file of stack")]
    public class StackGetFileCmd : BaseApiCommand<StacksApiService>
    {
        [Argument(0, "stackId", "Stack instance identifier")]
        [Required]
        public int StackId { get; set; }

        public override void Do(CommandLineApplication app, IConsole console)
        {
            var data = ApiClient.GetStackFile(StackId);
            console.Write(data);
        }
    }
}
