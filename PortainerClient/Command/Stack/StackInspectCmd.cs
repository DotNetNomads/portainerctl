using System.ComponentModel.DataAnnotations;
using IO.Swagger.Api;
using McMaster.Extensions.CommandLineUtils;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Stack
{
    [Command("inspect", Description = "Show information about stack")]
    public class StackInspectCmd : BaseApiCommand<StacksApi>
    {
        [Argument(0, "stackId", "Stack identifier")]
        [Required]
        public int StackId { get; set; }

        public override void Do(CommandLineApplication app, IConsole console)
        {
            var stackInfo = ApiClient.StackInspect(StackId);
            console.WriteLine(new Serializer().Serialize(stackInfo));
        }
    }
}